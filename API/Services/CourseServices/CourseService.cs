using API.Data.Repositories.CourseRepositories;
using API.Domain;
using API.DTOs.CourseDTOs;
using AutoMapper;

namespace API.Services.CourseServices;

public class CourseService(IMapper mapper, ICourseRepository repository) : ICourseService
{
    public async Task<Course> GetAsync(int id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<Course> CreateAsync(CourseCreateRequestDTO newCourse)
    {
        var course = mapper.Map<Course>(newCourse);
        return await repository.CreateAsync(course);
    }
}
