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
dotnet ef database update -p Student -s StudentAPI -c StudentDbContext
dotnet ef database update -p Course -s CourseAPI -c CourseDbContext
```
5. build all projects in shared folder

6. run the application

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

## Microservices (with Event-Driven)
The only thing that changes from [Modular-Monolithic (with Event-Driven)](https://github.com/esosa-enakoya-code/architecture-demos/tree/Modular-Monolithic-Event-Driven) is that all modules are now seperate. So now Shared projects get put in seperate solutions and the actual projects aswell while giving them there own database and API.

I hope that you now understand why you shouldn't use services or API calls to interact with other components, it will add a lot of code spaghetti that you don't want. This approach cleans everything up with proper separation of concerns. While making it future-proof as well, everything can scale properly too. And events can keep existing even on crashes of multiple services. Adding new modules is easy now as well, this is because you won't have to copy-paste all API calls and double-check the available API. You just put any event on the bus, and if there is a consumer, it will get handled. It doesn't matter what service responds either all that matters is that you have data when you need it.

Now you know how to create the main architectures that are relevant in 2024. Congratulations on getting this far.
