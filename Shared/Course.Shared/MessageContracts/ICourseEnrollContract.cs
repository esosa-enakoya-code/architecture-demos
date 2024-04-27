namespace Course.Shared.MessageContracts;

public interface ICourseEnrollContract
{
    public int CourseId { get; }
    public int StudentId { get; }
}
