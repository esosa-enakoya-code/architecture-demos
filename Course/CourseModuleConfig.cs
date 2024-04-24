using Course.Data;
using Course.Data.Repositories;
using Course.Services;
using Course.Shared.Services;
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
        services.AddScoped(provider => new Lazy<ICourseService> (() => provider.GetRequiredService <ICourseService> ()));
        services.AddAutoMapper(typeof(CourseModuleConfig).Assembly);

        return services;
    }
}
