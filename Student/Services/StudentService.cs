using AutoMapper;
using Student.Data.Repositories;
using Student.Domain;
using Student.Shared.DTOs.RequestDTOs;
using Student.Shared.DTOs.ResponseDTOs;
using Student.Shared.Services;

namespace Student.Services;
public class StudentService(IMapper mapper, IStudentRepository repository) : IStudentService
{
    public async Task<StudentGetResponseDTO> GetAsync(int id)
    {
        var student = await repository.GetAsync(id);
        return mapper.Map<StudentGetResponseDTO>(student);
    }

    public async Task<StudentCreateResponseDTO> CreateAsync(StudentCreateRequestDTO studentCreateRequest)
    {
        var student = mapper.Map<StudentDomain>(studentCreateRequest);
        student = await repository.CreateAsync(student);
        return mapper.Map<StudentCreateResponseDTO>(student);
    }
}
