using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;
using Student.Data.Entities;
using Student.Domain;

namespace Student.Data.Repositories;

public sealed class StudentRepository(IMapper mapper, StudentDbContext context) : AbstractBaseRepository<StudentDbContext>(context), IStudentRepository
{
    public async Task<StudentDomain> GetAsync(int id)
    {
        var studentEntity = await Context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(se => se.Id == id)
                ?? throw new KeyNotFoundException("No student found");

        return mapper.Map<StudentDomain>(studentEntity);
    }

    public async Task<List<StudentDomain>> GetBatchAsync(int[] ids)
    {
        var studentEntities = await Context.Students
            .AsNoTracking()
            .Where(s => ids.Contains(s.Id))
            .ToListAsync()
                ?? throw new KeyNotFoundException("No students found"); ;

        return mapper.Map<List<StudentDomain>>(studentEntities);
    }

    public async Task<StudentDomain> CreateAsync(StudentDomain student)
    {
        var studentEntity = mapper.Map<StudentEntity>(student);

        await Context.Students.AddAsync(studentEntity);
        await Context.SaveChangesAsync();

        return mapper.Map<StudentDomain>(studentEntity);
    }

    public async Task<StudentDomain> AddCourseToStudentAsync(int studentId, int courseId)
    {
        var studentEntity = await Context.Students
            .FirstOrDefaultAsync(se => se.Id == studentId)
                ?? throw new KeyNotFoundException("No student found");
        studentEntity.CourseId = courseId;

        await Context.SaveChangesAsync();

        return mapper.Map<StudentDomain>(studentEntity);
    }
}
