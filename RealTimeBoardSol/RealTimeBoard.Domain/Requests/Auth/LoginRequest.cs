namespace RealTimeBoard.Domain.Requests.Auth;

public record LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}