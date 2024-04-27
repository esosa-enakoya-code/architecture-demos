namespace Course.Shared.MessageContracts;

public interface ICourseCreateContract
{
    public int Id { get; }
    public string? Name { get; }
}
