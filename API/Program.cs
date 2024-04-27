using Microsoft.EntityFrameworkCore;
using Student;
using Course;
using MassTransit;
using Student.Consumers;
using Course.Consumers;

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

        var rabbitMQConfiguration = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<StudentGetConsumer>();
            x.AddConsumer<StudentCreateConsumer>();
            x.AddConsumer<StudentGetBatchConsumer>();
            x.AddConsumer<StudentEnrollConsumer>();

            x.AddConsumer<CourseGetConsumer>();
            x.AddConsumer<CourseCreateConsumer>();
            x.AddConsumer<CourseEnrollConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                _ = ushort.TryParse(rabbitMQConfiguration["Port"], out ushort port);
                cfg.Host(rabbitMQConfiguration["Host"], port, rabbitMQConfiguration["VirtualHost"], h =>
                {
                    h.Username(rabbitMQConfiguration["Username"] ?? throw new InvalidOperationException("No Username Found"));
                    h.Password(rabbitMQConfiguration["Password"] ?? throw new InvalidOperationException("No Password Found"));
                });

                cfg.ConfigureEndpoints(context);
            });
        });

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
