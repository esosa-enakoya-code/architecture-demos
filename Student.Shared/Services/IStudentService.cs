using Student.Shared.DTOs.RequestDTOs;
using Student.Shared.DTOs.ResponseDTOs;

namespace Student.Shared.Services;

public interface IStudentService
{
    public Task<StudentGetResponseDTO> GetAsync(int id);
    public Task<StudentCreateResponseDTO> CreateAsync(StudentCreateRequestDTO studentCreateRequest);
}
