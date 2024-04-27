namespace Course.Domain;

public class CourseDomain(string name)
{
    public int Id { get; init; }
    public string Name { get; } = name;

    public List<int> StudentIds { get; init; } = [];
}
