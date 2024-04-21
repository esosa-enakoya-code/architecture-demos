using AutoMapper;
using Course.Shared.Services;
using Course.Data.Repositories;
using Course.Domain;
using Course.Shared.DTOs.RequestDTOs;
using Course.Shared.DTOs.ResponseDTOs;

namespace Course.Services;

public class CourseService(IMapper mapper, ICourseRepository repository) : ICourseService
{
    public async Task<CourseGetResponseDTO> GetAsync(int id)
    {
        var course = await repository.GetAsync(id);
        return mapper.Map<CourseGetResponseDTO>(course);
    }

    public async Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
    {
        var course = await repository.AddStudentToCourseAsync(newEnrollment.Id, newEnrollment.StudentId);
        return mapper.Map<CourseEnrollResponseDTO>(course);
    }

    public async Task<CourseCreateResponseDTO> CreateAsync(CourseCreateRequestDTO newCourse)
    {
        var course = mapper.Map<CourseDomain>(newCourse);
        course = await repository.CreateAsync(course);
        return mapper.Map<CourseCreateResponseDTO>(course);
    }
}
