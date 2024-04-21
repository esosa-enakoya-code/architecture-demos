using Course.Domain;

namespace Course.Data.Repositories;

public interface ICourseRepository
{
    public Task<CourseDomain> GetAsync(int id);
    public Task<CourseDomain> AddStudentToCourseAsync(int id, int studentId);
    public Task<CourseDomain> CreateAsync(CourseDomain course);
}
