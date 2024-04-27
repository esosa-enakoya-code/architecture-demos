using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student.Data;
using Student.Data.Repositories;

namespace Student;

public static class StudentModuleConfig
{
    public static IServiceCollection AddStudentModule(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<StudentDbContext>(o => o.UseSqlServer(dbConnectionString));
        services.AddAutoMapper(typeof(StudentModuleConfig).Assembly);
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
}
