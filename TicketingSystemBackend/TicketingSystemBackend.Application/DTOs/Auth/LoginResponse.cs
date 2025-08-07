using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.DTOs.Auth;
public class LoginResponse
{
    public string TokenType { get; set; } = "Bearer";

    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }

    public double ExpiresIn { get; set; }

    public string RefreshToken { get; set; }
}
