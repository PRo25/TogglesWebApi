# TogglesWebApi
An example Web API using ASP.NET Core to manage toggles used by client applications.

## Technologies

The technologies used to develop this Web API were:

- ASP.NET Core 2.0
- Entity Framework Core 2.0
- Sql Server 2014
- Visual Studio 2017

### Unit Tests

- NUnit
- Moq

## How To Run

1. Create a Sql Server database called "TogglesDataBase";
2. Run the "[DbScripts/CreateDB.sql](DbScripts/CreateDB.sql)" script in the "TogglesDataBase" to create the tables and insert some demo data;
3. Open TogglesWebApi.sln in Visual Studio 2017;
4. Configure the connection string in "appsettings.json -> ConnectionStrings -> TogglesConnectionString" according to your Sql Server instance configuration;
5. Run the solution that will start IIS Express with the following url: http://localhost:59888/.

## REST API Requests

With the solution running, you can start making requests to the REST API. The available operations are:

- **GET /api/Toggles**
  - Gets all toggles.
- **GET /api/Toggles/{toggleId}**
  - Gets a toggle by ID.
- **GET /api/Toggles/ByApp/{applicationCodeName}/{applicationVersion}**
  - Gets all toggle values that applies to a client application. This will return all toggle values specifically defined for the client application plus the globally defined values for any other remaining toggle.
- **POST /api/Toggles**
  - Creates a new toggle.
- **PUT /api/Toggles/{toggleId}**
  - Updates an existing toggle.
- **DELETE /api/Toggles/{toggleId}**
  - Deletes an existing toggle.
  
Note: The toggle values that are defined globally are identified by the application code name "Global".

You can test examples of these requests using [Postman](https://www.getpostman.com/) and importing the collection "[Postman/Toggles.postman_collection.json](Postman/Toggles.postman_collection.json)".

## Architecture

This solution follows the [Clean Architecture principles](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html). One of the most important concepts is that the business rules and entities are the core of the application and they must not depend on any code or libraries in the more exterior layers (UI, DB, Web API Controllers, etc.).

The solution uses a layered architectural pattern splitting the code in three layers: REST API Layer, Business Logic Layer and Data Access Layer.

### REST API Layer

- **TogglesWebApi**: this project contains the Web API controllers that handle the requests and the Startup code with the IOC Container configuration code.

### Business Logic Layer

- **Toggles.BusinessEntities**: in this project there are the entities that represent each business concept of the Toggles context;
- **Toggles.BusinessRules**: this project has the commands and loaders that implement each business use case. The code separation is inspired by [CQRS pattern](https://martinfowler.com/bliki/CQRS.html);
- **Toggles.BusinessRules.Contracts**: here are all the contracts to call interact with the business rules from outside the Business Logic Layer;
- **Toggles.Repositories.Contracts**: this project has all the contracts (repositories and unit of work) that the business rules require to be implemented by the Data Access Layer. This allows to abstract the data access logic and comply with the dependency rule of the Clean Architecture principles.

### Data Access Layer

- **Toggles.Repositories**: this project contains all the repositories and unit of work implementations of the contracts in Toggles.Repositories.Contracts. The queries to the data are defined here, in the corresponding repositories, and also the logic to add, update or delete entities and persist those changes to the DB;
- **Toggles.DataAccess**: in this project there are the DB context implementation using Entity Framework Core with the mappings configuration of the DB entities to the corresponding DB tables.

## TODO

- Implement OAuth2 authentication server;
- Implement toggle changes notifications hub using [ASP.NET Core WebSockets](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets) or wait for [ASP.NET Core SignalR](https://github.com/aspnet/SignalR/releases).
