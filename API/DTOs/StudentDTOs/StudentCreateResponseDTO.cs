namespace API.DTOs.StudentDTOs;

public sealed record StudentCreateResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
}