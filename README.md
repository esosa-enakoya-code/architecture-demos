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
