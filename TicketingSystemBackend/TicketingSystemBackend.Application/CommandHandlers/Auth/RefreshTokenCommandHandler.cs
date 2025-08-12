using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Auth;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Auth;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public RefreshTokenCommandHandler(IAuthRepository authRepository, IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // 1️. Validate refresh token from DB
        var refreshTokenData = await _authRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (refreshTokenData == null || refreshTokenData.Expires < DateTime.UtcNow)
            throw new SecurityTokenException("Invalid or expired refresh token.");

        // 2️. Get user & role
        var user = await _authRepository.GetUserByIdAsync(refreshTokenData.UserId);

        // 3️. Generate new access token
        var accessToken = _jwtTokenService.GenerateJwtToken(user.Id, user.Email, user.UserName, user.Role);

        // 4️. Generate new refresh token
        var newRefreshToken = Guid.NewGuid().ToString("N");
        var refreshTokenExpires = DateTime.UtcNow.AddDays(7);
        await _authRepository.AddOrUpdateRefreshTokenAsync(
            new RefreshTokenData { UserId = user.Id, Token = newRefreshToken, Expires = refreshTokenExpires },
            cancellationToken
        );

        // 5️. Return both
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }
}
