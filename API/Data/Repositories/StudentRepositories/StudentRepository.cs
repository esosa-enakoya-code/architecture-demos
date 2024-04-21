using API.Data.Entities;
using API.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.StudentRepositories;

public sealed class StudentRepository(IMapper mapper, BaseDbContext context) : AbstractBaseRepository(context), IStudentRepository
{
    public async Task<Student> GetAsync(int id)
    {
        var studentEntity = await Context.Students
            .Include(se => se.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(se => se.Id == id)
                ?? throw new KeyNotFoundException("No student found");

        return mapper.Map<Student>(studentEntity);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        var studentEntity = mapper.Map<StudentEntity>(student);

        await Context.Students.AddAsync(studentEntity);
        await Context.SaveChangesAsync();

        return mapper.Map<Student>(studentEntity);
    }
}
