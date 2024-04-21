using Microsoft.EntityFrameworkCore;
using Student;
using Course;

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

        var connectionString = configuration.GetConnectionString("DbConnectionString") 
            ?? throw new InvalidOperationException("No DbConnectionString Found");

        services.AddStudentModule(connectionString);
        services.AddCourseModule(connectionString);

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
