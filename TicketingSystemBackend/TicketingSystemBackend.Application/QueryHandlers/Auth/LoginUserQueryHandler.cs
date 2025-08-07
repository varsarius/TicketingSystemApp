using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Auth;
using Microsoft.Extensions.Configuration;


namespace TicketingSystemBackend.Application.QueryHandlers.Auth;
public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly double _accessTokenLifetimeSeconds;
    public LoginUserQueryHandler(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        var expirationMinutes = double.TryParse(configuration["Jwt:AccessTokenExpirationMinutes"], out var minutes)
            ? minutes
            : throw new Exception("Invalid AccessTokenExpirationMinutes configuration value.");

        _accessTokenLifetimeSeconds = expirationMinutes * 60;
    }
    public async Task<LoginResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _authRepository.LoginAsync(request.Email, request.Password, cancellationToken);
        if (token == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        return new LoginResponse(
            AccessToken: token,
            ExpiresIn: _accessTokenLifetimeSeconds,
            RefreshToken: refreshToken
        );
    }
}
