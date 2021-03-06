# REST API Boilerplate

Everything in this repo is meant as a boilerplate to copy when starting a new API project. Even the rest of this readme file is example-text.

## About this project

This is a boilerplate for a REST API.

Hosted at:

- https://dev-mywebsite.azurewebsites.net
- https://test-mywebsite.azurewebsites.net
- https://mywebsite.azurewebsites.net

**Highlights:**

- .NET 5
- CQRS architecture and mediator pattern
- The API is documented using Swagger UI at `/ui/swagger`.
- An SQL Server database is administered using Entity Framework and the code-first principle with migrations.
- SendGrid integration for sending emails
- The project is also setup to host a SPA alongside the API.

### Prerequisites

- .Net SDK 5.0.1

## Getting Started

### Configuration
Configurations are contained within the appsettings.json file.
Depending on the `ASPNETCORE_ENVIRONMENT` environment variable, an additional appsettings.{env}.json file will be read, which contains additional environment-specific configuration.

These files should never contain secrets as they will be committed to github.

Instead, set secret values as environment variables when running the project.
Using VS Code, a gitignored launch.json file can have a configuration containing an env object as such:
```
"env": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "ConnectionStrings:DbConnection": "{myConnectionString}",
    "SendGrid:ApiKey": "{myApiKey}";
    "Authorization:JwtKey": "{myJwtKey}"
}
```

## Entity Framework
Migrations are applied when the application starts and should generally not be applied manually using the command line.

This means that upon deploying the app, migrations will automatically update the database.

While you are working with a new database scheme you should **not** connect to the Test or Production database,
because you might accidentally apply migrations to the database.
It should instead point at a local database running on your machine.

I use the following connection string to point at a locally running LocalDb, which can be installed as part of the SQL Server Express installation. `Server=(localdb)\\MSSQLLocalDB;Initial Catalog=OperationDashboard-local;Integrated Security=true"`
Connect to the local database in SSMS using server name: (LocalDB)\MSSQLLocalDB.

To add new migrations

```sh
cd ./src/Boilerplate.Api/
dotnet ef migrations add MyMigrationName -o ./Infrastructure/Database/Migrations
```

## Run the app

Using dotnet command-line

```sh
dotnet run --project ./src/Boilerplate.Api/
```

## Tests

We're using Xunit and NSubstitute.
The general strategy for testing is the following:

- Unit test all business logic
- Integration test all database integrations with an in-memory database
- Mock/Stub external services. No integrations with external services are tested.
- Acceptance test all endpoints in the API using WebApplicationFactory.

To run tests use a test explorer in the code editor or run the command: `dotnet test`.
The same command should be run as part of the deploy pipeline. Nothing should be deployed if any test fails.

## Deployment & hosting

The application is currently hosted at Simply.com.
We use github actions as CI/CD tool. Upon pushing to the Dev branch, any changes will be deployed to our Test server.
In the csproj file the following applies to the Debug configuration:

```xml
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <AspNetCoreModuleName>AspNetCoreModule</AspNetCoreModuleName>
    <AspNetCoreHostingModel>OutOfProcess</AspNetCoreHostingModel>
</PropertyGroup>
```

This enables running multiples applications in the IIS app-pool on the server.
The main application runs In-process and all others run Out-of-process.
