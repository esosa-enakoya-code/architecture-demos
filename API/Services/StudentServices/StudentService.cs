using API.Data.Repositories.StudentRepositories;
using API.Domain;
using API.DTOs.StudentDTOs;
using AutoMapper;

namespace API.Services.StudentServices;

public class StudentService(IMapper mapper, IStudentRepository repository) : IStudentService
{
    public async Task<Student> GetAsync(int id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<Student> CreateAsync(StudentCreateRequestDTO newStudent)
    {
        var student = mapper.Map<Student>(newStudent);
        return await repository.CreateAsync(student);
    }
}
