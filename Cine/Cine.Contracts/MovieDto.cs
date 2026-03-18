namespace Cine.Contracts;

public sealed record MovieDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public double Stars { get; init; }
}
