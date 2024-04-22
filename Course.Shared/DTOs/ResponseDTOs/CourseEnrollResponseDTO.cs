namespace Course.Shared.DTOs.ResponseDTOs;

public sealed record CourseEnrollResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public List<string?> StudentNames { get; set; } = [];
}
