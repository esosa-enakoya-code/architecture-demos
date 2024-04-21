
using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
}
