using API.Domain;

namespace API.Data.Repositories.CourseRepositories;

public interface ICourseRepository
{
    public Task<Course> GetAsync(int id);
    public Task<Course> CreateAsync(Course course);
}
