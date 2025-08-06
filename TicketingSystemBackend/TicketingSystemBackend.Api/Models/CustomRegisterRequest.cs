namespace TicketingSystemBackend.Api.Models;

public sealed class CustomRegisterRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string UserName { get; init; }
    //public string? Description { get; init; }
}