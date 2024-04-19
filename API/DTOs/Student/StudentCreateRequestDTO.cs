namespace API.DTOs.Student;

public class StudentCreateRequestDTO(string name)
{
    public string Name { get; } = name;
}
