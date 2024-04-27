using Student.Domain;

namespace Student.Data.Repositories;

public interface IStudentRepository
{
    public Task<StudentDomain> GetAsync(int id);
    public Task<List<StudentDomain>> GetBatchAsync(int[] ids);
    public Task<StudentDomain> CreateAsync(StudentDomain student);
    public Task<StudentDomain> AddCourseToStudentAsync(int studentId, int courseId);
}
