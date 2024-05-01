# architecture-demos
- [Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Monolithic)
- [Modular-Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic)
- [Modular-Monolithic (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic-Event-Driven)
- [Microservices](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices)
- [Microservices (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices-Event-Driven)

## Setup
1. install RabbitMQ via [rabbitmq.com](https://www.rabbitmq.com/docs/download)
2. make sure its running
3. update appsettings with your own configuration
```c#
"ConnectionStrings": {
    "DbConnectionString": ""
},
"RabbitMQ": {
    "Host": "",
    "VirtualHost": "",
    "Port": "",
    "Username": "",
    "Password": ""
},
```
4. update database with migrations in root of application
```powershell
dotnet ef database update -p Student -s API -c StudentDbContext
dotnet ef database update -p Course -s API -c CourseDbContext
```
5. run the application

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

## Modular-Monolithic (with Event-Driven)
now you know how to create Modular-Monolithic and Microservices applications. But there is a problem you probably noticed by now. That is the exponential code complexity. Something that will make it easier again is Event-Driven architecture.

To set that up I used RabbitMQ via MassTransit which is a message bus. Important to note that this isn't an in-memory bus but one that lives on a seperate process. The reason I choose for that is so that unhandled events keep on living while also making migration from modular monolithic too Microservices seamless.

We will start from the [Modular-Monolithic](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic) codebase.

Again lets start with the structure. Something you will imediatly notice is that there is no service anymore. This has been replaced with consumers that means that they subscribe to certain events and handle the data from that event. This seperates all functionality in seperate classes that handle one thing specifically. Again increasing code clarity. One of those consumers is for enrolling `Course/Consumers/CourseEnrollConsumer.cs`.

Normally if you look at it you will start seeing that this is again just another implementation of the same method that we've been looking at. But instead of calling to an api or to a service you put events on an event bus and listen to other events. Lets look at the first call to get a studentResponse>

We put an event on the bus that is defined by a contract interface so now the `IStudentGetContract` that means that any component that listens to that type of event will now take the request data. Then we also say what type we expect in return which is a `StudentGetResponseDTO` meaning that this consumer will wait till another consumer returns that specific data. This significantly abstracts the handling of data making sure that everything is loosly coupled. For the rest the logic that you already know stays the same.

```c#
public sealed class CourseEnrollConsumer
    (IMapper mapper,
    ICourseRepository repository,
    IRequestClient<IStudentGetContract> getRequestClient,
    IRequestClient<IStudentEnrollContract> enrollRequestClient,
    IRequestClient<IStudentGetBatchContract> getBatchRequestClient) : IConsumer<ICourseEnrollContract>
{
    public async Task Consume(ConsumeContext<ICourseEnrollContract> context)
    {
        var studentResponse = await getRequestClient.GetResponse<StudentGetResponseDTO>(new { Id = context.Message.StudentId });
        var course = await repository.AddStudentToCourseAsync(context.Message.CourseId, studentResponse.Message.Id);
        await enrollRequestClient.GetResponse<StudentEnrollResponseDTO>(new { StudentId = studentResponse.Message.Id, CourseId = course.Id });

        var studentBatchResponse = await getBatchRequestClient.GetResponse<StudentGetBatchResponseDTO>(new { Ids = course.StudentIds });
        var response = mapper.Map<CourseEnrollResponseDTO>(course);
        response.StudentNames = studentBatchResponse.Message.Students.Select(s => s.Name).ToList();

        await context.RespondAsync(response);
    }
}
```

But for this to work we also had to change the controllers so that they also call to the event bus instead of too services. So lets look at the course controller `Course/Controllers/CourseController.cs`.

Again this works with the same principal. It puts an event on the bus and then listens for another event to get the requested data.

```c#
//... class CourseController
[HttpPost("{id}/[action]/{studentId}")]
public async Task<IActionResult> Enroll([FromRoute] int id, [FromRoute] int studentId) 
{
    var client = bus.CreateRequestClient<ICourseEnrollContract>();
    return Ok(await client.GetResponse<CourseEnrollResponseDTO>(new { CourseId = id, StudentId = studentId }));
}
//...
```

For all of this to work I also had to add another category to the shared folders which is called contracts. So thats where all the contracts reside and thats what consumers listen too. The contract for student enrollment can be found at `Course.Shared/MessageContracts/ICourseEnrollContract.cs`

In program.cs `API/Program.cs` we also had to define some queues. queues are what events wait in till a subscriber takes an event or till the expiration date has passed.

*This is something more aimed at MassTransit but whithout it the eventbus wouldn't work*
```c#
//... method Main
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
        //...
        cfg.ConfigureEndpoints(context);
    });
});
//...
```

All the other logic can be retained. Now, you can see that everything is even more structured; everything is separated and works without knowing about other implementations, meaning that it is loosely coupled. This approach seems extra for Modular Monolithic applications, but since those are meant to be migrated to Microservices, this is well worth it. This is because all we will have to do now is just separate the projects, and everything will keep working. Like this, if you just want to scale one module, you can also just separate it, and it will still be able to properly communicate with the monolith.

So finally lets go over to: [Microservices (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Microservices-Event-Driven)
