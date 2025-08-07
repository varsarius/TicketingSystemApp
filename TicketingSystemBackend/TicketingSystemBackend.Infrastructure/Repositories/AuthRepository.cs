using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;


    public AuthRepository(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(string Token, string RefreshToken)> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new Exception("Invalid email or password.");

        
        var token = _jwtTokenService.GenerateJwtToken(user.Id, user.Email, user.UserName);
        var refreshToken = ""; // Generate or retrieve from DB

        return (token, refreshToken);
    }

    public async Task RegisterUserAsync(string email, string username, string password, CancellationToken cancellationToken)
    {

        var user = new ApplicationUser
        {
            Email = email,
            UserName = username,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new Exception(errors); // or your custom exception
        }
    }
}
