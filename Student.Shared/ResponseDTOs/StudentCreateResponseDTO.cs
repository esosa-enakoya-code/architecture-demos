namespace Student.Shared.ResponseDTOs;

public sealed record StudentCreateResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
