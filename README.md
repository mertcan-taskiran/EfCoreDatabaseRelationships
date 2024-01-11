# Basic Use of the Entity Framework 

## .Net Core - ORM - Entity Framework

## Database Reletionships (One to One, One to Many, Many to Many)

> Run the following commands:

### New .Net Project
```
dotnet new console
```

### Database Provider
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.1
```

### Migrations
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### Microsoft.Extensions.Logging.Console
```
dotnet add package Microsoft.Extensions.Logging.Console --version 8.0.0
```