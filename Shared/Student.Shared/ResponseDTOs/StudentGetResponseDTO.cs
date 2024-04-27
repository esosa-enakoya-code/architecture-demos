namespace Student.Shared.ResponseDTOs;

public class StudentGetResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? CourseName { get; set; }
}
