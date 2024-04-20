namespace API.Domain;

public class Course(string name)
{
    public int Id { get; init; }
    public string Name { get; } = name;
}
