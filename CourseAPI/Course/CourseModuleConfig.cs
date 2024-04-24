using Course.Data;
using Course.Data.Repositories;
using Course.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Course;

public static class CourseModuleConfig
{
    public static IServiceCollection AddCourseModule(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<CourseDbContext>(o => o.UseSqlServer(dbConnectionString));
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddAutoMapper(typeof(CourseModuleConfig).Assembly);

        services.AddHttpClient("StudentAPI", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5201");
        });

        return services;
    }
}
