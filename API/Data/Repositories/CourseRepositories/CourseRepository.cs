using API.Data.Entities;
using API.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.CourseRepositories;

public class CourseRepository(IMapper mapper, BaseDbContext context) : AbstractBaseRepository(context), ICourseRepository
{
    public async Task<Course> GetAsync(int id)
    {
        var courseEntity = await Context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(ce => ce.Id == id)
                ?? throw new KeyNotFoundException("No course found");

        return mapper.Map<Course>(courseEntity);
    }

    public async Task<Course> CreateAsync(Course course)
    {
        var courseEntity = mapper.Map<CourseEntity>(course);

        await Context.Courses.AddAsync(courseEntity);
        await Context.SaveChangesAsync();

        return mapper.Map<Course>(courseEntity);
    }
}
