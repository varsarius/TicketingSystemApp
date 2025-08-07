using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.DTOs.Auth;
public record LoginResponse(
    [property: JsonPropertyName("accessToken")] string AccessToken,
    double ExpiresIn,
    string RefreshToken
)
{
    public string TokenType { get; init; } = "Bearer";
}