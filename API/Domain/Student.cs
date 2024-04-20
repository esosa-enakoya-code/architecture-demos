namespace API.Domain;

public class Student(string name)
{
    public int Id { get; init; }
    public string Name { get; } = name;
}