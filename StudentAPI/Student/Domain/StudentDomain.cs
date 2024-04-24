namespace Student.Domain;

public class StudentDomain(string name)
{
    public int Id { get; init; }
    public string Name { get; } = name;

    public int CourseId { get; init; }
}
