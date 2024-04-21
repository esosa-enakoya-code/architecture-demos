using Course.Shared.DTOs.RequestDTOs;
using Course.Shared.DTOs.ResponseDTOs;

namespace Course.Shared.Services;

public interface ICourseService
{
    public Task<CourseGetResponseDTO> GetAsync(int id);
    public Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment);
    public Task<CourseCreateResponseDTO> CreateAsync(CourseCreateRequestDTO newCourse);
}
