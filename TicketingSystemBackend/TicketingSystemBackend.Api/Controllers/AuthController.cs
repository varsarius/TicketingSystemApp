using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketingSystemBackend.Application.Commands.Auth;
using TicketingSystemBackend.Application.Exceptions;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public AuthController(UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (UserExistException ex)
        {
            return Conflict(new { error = ex.Message });  // HTTP 409 Conflict
        }
        catch (Exception ex)
        {
            // You can log the exception here if needed
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("debug/token")]
    public IActionResult DebugToken()
    {
        return Ok(new
        {
            User.Identity.IsAuthenticated,
            Claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }

    [Authorize]
    [HttpPatch("users/{username}/name")]
    public async Task<IActionResult> UpdateUserName(string username, [FromBody] UpdateUserNameRequest request)
    {
        //Console.WriteLine("---");
        //Console.WriteLine(username);
        //Console.WriteLine(User.Identity.Name);
        //Console.WriteLine("---------");
        var userIdentity = User.FindFirst("name")?.Value;

        if (username != userIdentity)
        {
            return Forbid(); // user can only change their own username (or expand for admins)
        }

        try
        {
            var command = new UpdateUserNameCommand(username, request.NewUserName);
            await _mediator.Send(command);
            return NoContent(); // 204 - success, no body
        }
        catch (KeyNotFoundException)
        {
            return NotFound("the user with that name were nit found");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    //TO CHANGE: apply CQRS and clean architecture and so on and so on apply it all
    [Authorize(Roles = "Admin")] // Only admins should change roles
    [HttpPatch("users/{username}/role")]
    public async Task<IActionResult> UpdateUserRole(string username, [FromBody] UpdateUserRoleRequest request)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            return NotFound(new { error = "User not found" });

        var currentRoles = await _userManager.GetRolesAsync(user);

        // Remove current roles (only one role in your setup, but removing all is safer)
        if (currentRoles.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return BadRequest(new { error = "Failed to remove existing role" });
        }

        // Add the new role
        var addResult = await _userManager.AddToRoleAsync(user, request.NewRole);
        if (!addResult.Succeeded)
            return BadRequest(new { error = "Failed to assign new role" });

        return NoContent();
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var command = new RefreshTokenCommand(request.RefreshToken);
            var result = await _mediator.Send(command);
            return Ok(result); // returns new access & refresh tokens
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}


public class UpdateUserNameRequest
{
    [Required]
    [JsonPropertyName("newUserName")]
    public string NewUserName { get; set; } = null!;
}

public class RefreshTokenRequest
{
    [Required]
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}

// DTO for request body
public class UpdateUserRoleRequest
{
    public string NewRole { get; set; } = null!;
}