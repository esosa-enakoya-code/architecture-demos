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
5. build all projects in shared folder

6. run the application
