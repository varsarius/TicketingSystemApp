using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
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
}

//public class LoginResponse
//{

//    public string TokenType { get; set; }
//    [JsonPropertyName("accessToken")]
//    public string AccessToken { get; set; }
//    public int ExpiresIn { get; set; }
//    public string RefreshToken { get; set; }
//}