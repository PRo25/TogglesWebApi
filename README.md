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

- GET /api/Toggles
  - Gets all toggles.
- GET /api/Toggles/{toggleId}
  - Gets a toggle by ID.
- GET /api/Toggles/ByApp/{applicationCodeName}/{applicationVersion}
  - Gets all toggle values that applies to a client application. This will return all toggle values specifically defined for the client application plus the globally defined values for any other remaining toggle.
- POST /api/Toggles
  - Creates a new toggle.
- PUT /api/Toggles/{toggleId}
  - Updates an existing toggle.
- DELETE /api/Toggles/{toggleId}
  - Deletes an existing toggle.
  
Note: The toggle values that are defined globally are identified by the application code name "Global".

You can test examples of these requests using [Postman](https://www.getpostman.com/) and importing the collection "[Postman/Toggles.postman_collection.json](Postman/Toggles.postman_collection.json)".

## TODO

- Implement OAuth2 authentication server
- Implement toggle changes notifications hub using [ASP.NET Core WebSockets](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets) or wait for [ASP.NET Core SignalR](https://github.com/aspnet/SignalR/releases)
