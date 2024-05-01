# architecture-demos
- [Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Monolithic)
- [Modular-Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic)
- [Modular-Monolithic (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic-Event-Driven)
- [Microservices](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices)
- [Microservices (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices-Event-Driven)

## Setup
1. update appsettings with your own configuration
```c#
"ConnectionStrings": {
    "DbConnectionString": ""
},
```
2. update database with migrations in root of application
```powershell
dotnet ef database update
```
3. run the application

## Explanation
This is an API that can do the following:
- Create a student
- Get a student
- Create a course
- Get a course
- Enroll a student to a course

I use a layered structure to seperate logic for better showcasing and those layers are: 
- Controller (used to call to API(s))
- Comunication (used to comunicate between persistence and controller in the form of services or consumers)
- Persistence (used for storing and obtaining data)

For the extras:
- AutoMapper (mapping between classes)
- DTOs & domain objects (for comunication between layers)
- Migrations via EntityFrameworkCore (to setup the database(s))

## Monolithic architecture
As you can see for monolithic i made sure all functionality is in one solution and one project named API.

There is only just one dbContext at `API/Data/BaseDbContext.cs` because I am allowed to have tables that reference eachother in a single database.
```c#
//... class BaseDbContext
public DbSet<StudentEntity> Students { get; set; }
public DbSet<CourseEntity> Courses { get; set; }
//...
```

In `API/Data/Repositories/CourseRepositories/CourseRepository.cs` you can see that I am allowed to call to not only the courses dbset but also the dbset of students:
```c#
//... method AddStudentToCourseAsync
var courseEntity = await Context.Courses
    .Include(ce => ce.Students)
    .FirstOrDefaultAsync(ce => ce.Id == id)
        ?? throw new KeyNotFoundException("No course found");

var studentEntity = await Context.Students
    .FirstOrDefaultAsync(se => se.Id == studentId)
        ?? throw new KeyNotFoundException("No student found");
//...
```
That is a hard coupling which is alright for this architecture. Because of this its easier to get functionality up and running.

Because everything is hard coupled my services don't have extra logic except mapping. This makes it easier to program yet again here were looking at `API/Services/CourseServices/CourseService.cs`:
```c#
//... class CourseService
public async Task<Course> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
{
    return await repository.AddStudentToCourseAsync(newEnrollment.Id, newEnrollment.StudentId);
}
//...
```
And finally in `API/Program.cs` I am also allowed to add both parts of the application in one API:
```c#
//... method Main
services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

services.AddScoped<IStudentService, StudentService>();
services.AddScoped<IStudentRepository, StudentRepository>();

services.AddScoped<ICourseService, CourseService>();
services.AddScoped<ICourseRepository, CourseRepository>();
//...
```
Now you can see it's very easy to set this up and also easy to add features. However, if you look at the folder structure, you'll notice that as more features get added and more data it will become hard to navigate through everything. This is because everything is mixed. Other architectures will make this separation between different features more clear, which will make big code bases more manageable.

Next up we have: [Modular-Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic)
