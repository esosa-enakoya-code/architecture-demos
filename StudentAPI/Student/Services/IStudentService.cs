using Student.Shared.DTOs.RequestDTOs;
using Student.Shared.DTOs.ResponseDTOs;

namespace Student.Services;

public interface IStudentService
{
    public Task<StudentGetResponseDTO> GetAsync(int id);
    public Task<StudentGetBatchResponseDTO> GetBatchAsync(int[] ids);
    public Task<StudentEnrollResponseDTO> EnrollStudentAsync(int studentId, int courseId);
    public Task<StudentCreateResponseDTO> CreateAsync(StudentCreateRequestDTO studentCreateRequest);
}
