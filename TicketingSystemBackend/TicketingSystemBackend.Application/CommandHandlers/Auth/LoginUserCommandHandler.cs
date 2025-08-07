using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Auth;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Interfaces;


namespace TicketingSystemBackend.Application.CommandHandlers.Auth;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly double _accessTokenLifetimeSeconds;
    private readonly double _refreshTokenLifetimeDays;
    public LoginUserCommandHandler(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        var expirationMinutes = double.TryParse(configuration["Jwt:AccessTokenExpirationMinutes"], out var minutes)
            ? minutes
            : throw new Exception("Invalid AccessTokenExpirationMinutes configuration value.");

        _accessTokenLifetimeSeconds = expirationMinutes * 60;

        _refreshTokenLifetimeDays = double.TryParse(configuration["Jwt:RefreshTokenExpirationDays"], out var days)
            ? days
            : throw new Exception("Invalid AccessTokenExpirationMinutes configuration value.");
    }
    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var (userId, token) = await _authRepository.LoginAsync(request.Email, request.Password, cancellationToken);
        if (token == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var refreshTokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        // Prepare refresh token data
        var refreshTokenData = new RefreshTokenData
        {
            Token = refreshTokenString.ToString(),
            Expires = DateTime.UtcNow.AddDays(_refreshTokenLifetimeDays),
            UserId = userId
        };

        // Store refresh token
        await _authRepository.AddRefreshTokenAsync(refreshTokenData, cancellationToken);

        return new LoginResponse(
            AccessToken: token,
            ExpiresIn: _accessTokenLifetimeSeconds,
            RefreshToken: refreshTokenString
        );
    }
}
