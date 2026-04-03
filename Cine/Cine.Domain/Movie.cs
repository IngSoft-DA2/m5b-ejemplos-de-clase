namespace Cine.Domain;

public sealed record Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public double Stars { get; set; }
}
