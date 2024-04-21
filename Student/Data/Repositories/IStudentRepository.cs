using Student.Domain;

namespace Student.Data.Repositories;

public interface IStudentRepository
{
    public Task<StudentDomain> GetAsync(int id);
    public Task<StudentDomain> CreateAsync(StudentDomain student);
}
