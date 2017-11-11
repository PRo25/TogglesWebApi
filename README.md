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
- GET /api/Toggles/{toggleId}
- GET /api/Toggles/ByApp/{applicationCodeName}/{applicationVersion}
- POST /api/Toggles
- PUT /api/Toggles/{toggleId}
- DELETE /api/Toggles/{toggleId}

You can test examples of these requests using [Postman](https://www.getpostman.com/) and importing the collection "[Postman/Toggles.postman_collection.json](Postman/Toggles.postman_collection.json)".
