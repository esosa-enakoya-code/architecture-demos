namespace Course.Shared.ResponseDTOs;

public sealed record CourseCreateResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
