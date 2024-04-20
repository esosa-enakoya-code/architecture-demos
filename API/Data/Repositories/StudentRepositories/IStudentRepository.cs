using API.Domain;

namespace API.Data.Repositories.StudentRepositories;

public interface IStudentRepository
{
    public Task<Student> GetAsync(int id);
    public Task<Student> CreateAsync(Student student);
}
