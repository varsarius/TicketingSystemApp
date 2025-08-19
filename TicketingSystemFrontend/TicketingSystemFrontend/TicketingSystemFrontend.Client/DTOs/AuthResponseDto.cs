using System.Text.Json.Serialization;

namespace TicketingSystemFrontend.Client.DTOs;

public class AuthResponseDto
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}
