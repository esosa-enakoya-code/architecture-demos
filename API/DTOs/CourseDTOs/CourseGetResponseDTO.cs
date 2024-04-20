namespace API.DTOs.CourseDTOs;

public sealed record CourseGetResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
