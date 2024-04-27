using Microsoft.EntityFrameworkCore;
using Shared.Data.DbContexts;
using Student.Data.Entities;

namespace Student.Data;

public class StudentDbContext : AbstractBaseDbContext<StudentDbContext>
{
    public DbSet<StudentEntity> Students { get; set; }

    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }
}
