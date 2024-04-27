using Course.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Course.Data;

public sealed class CourseDbContext : DbContext
{
    public DbSet<CourseEntity> Courses { get; set; }

    public CourseDbContext()
    {
    }

    public CourseDbContext(DbContextOptions<CourseDbContext> options)
        : base(options)
    {
    }
}
