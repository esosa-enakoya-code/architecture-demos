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
dotnet ef database update -p Student -s StudentAPI -c StudentDbContext
dotnet ef database update -p Course -s CourseAPI -c CourseDbContext
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

## Microservices
This time once you look at the structure it changed less this time I made the 2 modules actual separate components that live in different solutions. I also moved the shared projects to there seperate solutions so that everything is truly seperate. And again thats not all that I changed because now we can't interact via the services anymore. So instead I opted for comunication trough API calls.

This time I had to change less so what has changed is the service implementation and the API configuration.

> **Important to note, to have a complete microservice each service should have a completely seperate database this means a student database and a course database**

First I setup the course service `CourseAPI/Course/Services/CourseService.cs` for http comunication. This keeps the loosly coupled nature but sends requests through the network instead of in-memory.
```c#
//... class CourseService
public async Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
{
    var studentResponse = (await _httpClient.FetchAsync<StudentGetResponseDTO>(HttpMethod.Get, $"student/{newEnrollment.StudentId}"))
        ?? throw new KeyNotFoundException($"No student with id '{newEnrollment.StudentId}' found");
    var course = await repository.AddStudentToCourseAsync(newEnrollment.Id, studentResponse.Id);

    _ = (await _httpClient.FetchAsync<StudentGetResponseDTO>(HttpMethod.Post, $"student/{studentResponse.Id}/enroll/{course.Id}"))
        ?? throw new KeyNotFoundException($"No student with id '{newEnrollment.StudentId}' found");

    var studentBatchRequest = new StudentGetBatchRequestDTO([.. course.StudentIds]);
    var studentBatchResponse = await _httpClient.FetchAsync<StudentGetBatchResponseDTO>(HttpMethod.Get, studentBatchRequest, $"student/batch");

    var response = mapper.Map<CourseEnrollResponseDTO>(course);
    response.StudentNames = studentBatchResponse?.Students.Select(s => s.Name).ToList() ?? [];

    return response;
}
//...
```

Then I updated the program.cs meaning that every service has one with its specific needs.

`StudentAPI/StudentAPI/Program.cs`
```c#
//... method Main
var connectionString = configuration.GetConnectionString("DbConnectionString")
    ?? throw new InvalidOperationException("No DbConnectionString Found");

services.AddStudentModule(connectionString);
//...
```
`CourseAPI/CourseAPI/Program.cs`
```c#
//... method Main
var connectionString = configuration.GetConnectionString("DbConnectionString")
    ?? throw new InvalidOperationException("No DbConnectionString Found");

services.AddCourseModule(connectionString);
//...
```

Now, every module is almost completely independent of the others. You can scale modules separately, work with teams in parallel, and even use different technologies if you prefer to do so.

But as you can start to see this is becoming complicated really fast. Thats why I will talk about using event driven architecture together with our previous architectures to make comunication easier and ensure proper loose coupling.

Next up we have: [Modular-Monolithic (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic-Event-Driven)
