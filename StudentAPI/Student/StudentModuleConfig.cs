using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student.Data;
using Student.Data.Repositories;
using Student.Services;

namespace Student;

public static class StudentModuleConfig
{
    public static IServiceCollection AddStudentModule(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<StudentDbContext>(o => o.UseSqlServer(dbConnectionString));
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddAutoMapper(typeof(StudentModuleConfig).Assembly);

        services.AddHttpClient("CourseAPI", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5161");
        });

        return services;
    }
}
