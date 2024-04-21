using API.Domain;
using API.DTOs.StudentDTOs;

namespace API.Services.StudentServices;

public interface IStudentService
{
    public Task<Student> GetAsync(int id);
    public Task<Student> CreateAsync(StudentCreateRequestDTO newStudent);
}
