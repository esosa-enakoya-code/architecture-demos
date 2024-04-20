namespace API.DTOs.CourseDTOs;

public sealed record CourseCreateResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
