using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Exceptions;
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
    private readonly AppDbContext _context;


    public AuthRepository(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService, AppDbContext context)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _context = context;
    }

    public async Task AddOrUpdateRefreshTokenAsync(RefreshTokenData refreshTokenData, CancellationToken cancellationToken)
    {
        var existingToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == refreshTokenData.UserId, cancellationToken);
        if (existingToken != null)
        {
            existingToken.Token = refreshTokenData.Token;
            existingToken.Expires = refreshTokenData.Expires;
            existingToken.UpdatedAt = DateTime.UtcNow;
            _context.RefreshTokens.Update(existingToken);
        }
        else
        {
            var newToken = new RefreshToken
            {
                Token = refreshTokenData.Token,
                Expires = refreshTokenData.Expires,
                UserId = refreshTokenData.UserId
            };
            await _context.RefreshTokens.AddAsync(newToken, cancellationToken);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRefreshTokenAsync(RefreshTokenData refreshTokenData, CancellationToken cancellationToken)
    {
        var tokenEntity = await _context.RefreshTokens
        .FirstOrDefaultAsync(rt => rt.Token == refreshTokenData.Token, cancellationToken);

        if (tokenEntity is not null)
        {
            _context.RefreshTokens.Remove(tokenEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Guid> FindUserIdByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) throw new ArgumentNullException(nameof(user));

        return user.Id;
    }

    public async Task<RefreshTokenData?> GetRefreshTokenAsync(string refreshTokenString, CancellationToken cancellationToken)
    {
        var tokenEntity = await _context.RefreshTokens
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Token == refreshTokenString, cancellationToken);

        if (tokenEntity == null)
            return null;

        return new RefreshTokenData
        {
            Token = tokenEntity.Token,
            Expires = tokenEntity.Expires,
            UserId = tokenEntity.UserId,
        };
    }

    public async Task<(Guid, string)> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new Exception("Invalid email or password.");

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty; // pick the first role or empty
        var token = _jwtTokenService.GenerateJwtToken(user.Id, user.Email, user.UserName, role);

        return (user.Id, token);
    }

    public async Task RegisterUserAsync(string email, string username, string password, CancellationToken cancellationToken)
    {
        // Check if user with the same username already exists
        var existingUser = await _userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            throw new UserExistException($"User with username '{username}' already exists.");
        }

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

    public async Task RemoveExpiredTokensAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        // Get all expired tokens
        var expiredTokens = await _context.RefreshTokens
            .Where(rt => rt.Expires <= now)
            .ToListAsync(cancellationToken);

        // and delete them
        if (expiredTokens.Any())
        {
            _context.RefreshTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<UserDto>> GetAllUsersWithRolesAsync()
    {
        var users = _userManager.Users.ToList();

        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault() ?? "EndUser"  // assuming single role per user
            });
        }

        var json = System.Text.Json.JsonSerializer.Serialize(userDtos);
        Console.WriteLine(json);

        return userDtos;
    }
}
