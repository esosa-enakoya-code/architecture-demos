using API.Domain;
using API.DTOs.CourseDTOs;

namespace API.Services.CourseServices;

public interface ICourseService
{
    public Task<Course> GetAsync(int id);
    public Task<Course> CreateAsync(CourseCreateRequestDTO newCourse);
}
