namespace Cine.Contracts;

public sealed record LoginResponseDto
{
    public required string Token { get; init; }
}
