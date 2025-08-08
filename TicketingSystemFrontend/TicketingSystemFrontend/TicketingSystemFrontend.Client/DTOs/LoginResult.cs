using System.Text.Json.Serialization;

namespace TicketingSystemFrontend.Client.DTOs;

public class LoginResult
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = string.Empty;

    public double ExpiresIn { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public bool IsSuccess => !string.IsNullOrWhiteSpace(AccessToken);
}
