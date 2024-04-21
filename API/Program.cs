using API.Data;
using API.Data.Repositories.CourseRepositories;
using API.Data.Repositories.StudentRepositories;
using API.Services.CourseServices;
using API.Services.StudentServices;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddDbContext<BaseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
