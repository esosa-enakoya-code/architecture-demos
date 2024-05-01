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
}
```
2. update database with migrations in root of application
```powershell
dotnet ef database update -p Student -s API -c StudentDbContext
dotnet ef database update -p Course -s API -c CourseDbContext
```
3. build all projects in shared folder

4. run the application

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

## Modular Monolithic
If you look at the file structure you'll notice that it's completely different from a monolithic application. I have split the student and course into separate projects but still within the same solution. I have also ensured that components needed in both projects are in a shared location. Finally, I split the DTOs and service interfaces into a shared subcategory, specifically student.shared and course.shared. This is the minimum amount of coupling needed between the modules to ensure proper communication.

But thats not enough to achieve the loose coupling that modular monolithic promises. we will have to change some of the code of the [Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Monolithic) codebase.

Let's start with the dbcontext we now split it into two seperate contexts that only have the data it should handle. 

`Course/Data/CourseDbContext.cs` 
```c# 
//... class CourseDbContext
public DbSet<CourseEntity> Courses { get; set; }
//...
```

`Student/Data/StudentDbContext.cs`
```c# 
//... class StudentDbContext
public DbSet<StudentEntity> Students { get; set; }
//...
```

Then I had to remove the hard coupling in the persistence layer. So in `Course/Data/Repositories/CourseRepository.cs` You can see that I obtain a student ID from my communication layer and no longer access the student list. This removes the hard coupling between the student and course module because the course module no longer needs to understand the internal workings of the student module to function.
```c#
//... class CourseRepository
public async Task<CourseDomain> AddStudentToCourseAsync(int id, int studentId)
{
    var courseEntity = await Context.Courses
        .FirstOrDefaultAsync(ce => ce.Id == id)
            ?? throw new KeyNotFoundException("No course found");

    await Context.Courses
        .Where(ce => ce.StudentIds.Contains(studentId))
        .ForEachAsync(ce => ce.StudentIds.Remove(studentId));
    courseEntity.StudentIds.Add(studentId);

    await Context.SaveChangesAsync();

    return mapper.Map<CourseDomain>(courseEntity);
}
//...
```

Now, there must be a way to still retrieve the correct student data. I achieved this by requesting the needed student data from the student service within the course service `Course/Services/CourseService.cs`. The student service then handles this request, and the course service simply takes the data and uses it for its own purposes, regardless of what it receives. This approach creates loose coupling between the two modules.
```c#
//... class CourseService
public async Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
{
    var studentResponse = await studentService.Value.GetAsync(newEnrollment.StudentId);
    var course = await repository.AddStudentToCourseAsync(newEnrollment.Id, studentResponse.Id);
    await studentService.Value.EnrollStudentAsync(studentResponse.Id, course.Id);

    var studentBatchResponse = await studentService.Value.GetBatchAsync([.. course.StudentIds]);
    var response = mapper.Map<CourseEnrollResponseDTO>(course);
    response.StudentNames = studentBatchResponse.Students.Select(s => s.Name).ToList();

    return response;
}
//...
```
*As a sidenote you can also see that I send an enroll request to the student service. That is so that the student gets updated with the course ID.*

Something that you can now start to see is that you will need to write more code to achieve the same functionality. However, the benefits are greater because now you don't have to think about students and courses but just one module specifically. The code is also more separated, so once you start adding more features, you will still be able to find your way to the correct code. Additionally, now multiple teams can work in parallel because one could work on the student module and another on the course module.

Next up we have: [Microservices](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices)
