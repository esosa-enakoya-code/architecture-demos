namespace API.DTOs.Course;

public class CourseCreateRequestDTO(string name)
{
    public string Name { get; } = name;
}
