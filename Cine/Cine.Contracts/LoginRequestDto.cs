namespace Cine.Contracts;

public sealed record LoginRequestDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
