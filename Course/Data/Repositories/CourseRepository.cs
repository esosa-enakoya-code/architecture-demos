using AutoMapper;
using Course.Data.Entities;
using Course.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Course.Data.Repositories;

public class CourseRepository(IMapper mapper, CourseDbContext context) : AbstractBaseRepository<CourseDbContext>(context), ICourseRepository
{
    public async Task<CourseDomain> GetAsync(int id)
    {
        var courseEntity = await Context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(ce => ce.Id == id)
                ?? throw new KeyNotFoundException("No course found");

        return mapper.Map<CourseDomain>(courseEntity);
    }

    public async Task<CourseDomain> AddStudentToCourseAsync(int id, int studentId)
    {
        var courseEntity = await Context.Courses
            .FirstOrDefaultAsync(ce => ce.Id == id)
                ?? throw new KeyNotFoundException("No course found");

        /*
        var studentEntity = await Context.StudentIds
            .FirstOrDefaultAsync(se => se.Id == studentId)
                ?? throw new KeyNotFoundException("No student found");

        var linkedStudent = courseEntity.Students.FirstOrDefault(s => s.Id == studentId);
        if (linkedStudent is not null)
            courseEntity.Students.Remove(linkedStudent);

        courseEntity.Students.Add(studentEntity);

        await Context.SaveChangesAsync();
        */

        return mapper.Map<CourseDomain>(courseEntity);
    }

    public async Task<CourseDomain> CreateAsync(CourseDomain course)
    {
        var courseEntity = mapper.Map<CourseEntity>(course);

        await Context.Courses.AddAsync(courseEntity);

        await Context.SaveChangesAsync();
        return mapper.Map<CourseDomain>(courseEntity);
    }
}
