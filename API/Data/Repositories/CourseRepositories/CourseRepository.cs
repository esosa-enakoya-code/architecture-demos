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
            .Include(ce => ce.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(ce => ce.Id == id)
                ?? throw new KeyNotFoundException("No course found");

        return mapper.Map<Course>(courseEntity);
    }

    public async Task<Course> AddStudentToCourseAsync(int id, int studentId)
    {
        var courseEntity = await Context.Courses
            .Include(ce => ce.Students)
            .FirstOrDefaultAsync(ce => ce.Id == id)
                ?? throw new KeyNotFoundException("No course found");

        var studentEntity = await Context.Students
            .FirstOrDefaultAsync(se => se.Id == studentId)
                ?? throw new KeyNotFoundException("No student found");

        var linkedStudent = courseEntity.Students.FirstOrDefault(s => s.Id == studentId);
        if (linkedStudent is not null)
            courseEntity.Students.Remove(linkedStudent);

        courseEntity.Students.Add(studentEntity);

        await Context.SaveChangesAsync();
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
