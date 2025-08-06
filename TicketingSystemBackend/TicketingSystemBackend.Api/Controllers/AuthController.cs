using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketingSystemBackend.Api.Auth;
using TicketingSystemBackend.Api.Models;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomJwtTokenService _jwtTokenService;

    public AuthController(UserManager<ApplicationUser> userManager, CustomJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomRegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.UserName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = _jwtTokenService.GenerateJwtToken(user);
            return Ok(new LoginResponse
            {
                TokenType = "Bearer",
                AccessToken = token,
                ExpiresIn = 3600, // 1 hour in seconds
                RefreshToken = "" // Add refresh token logic if needed
            });
        }
        return Unauthorized(new { error = "Invalid email or password" });
        //return Ok();
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

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{

    public string TokenType { get; set; }
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}