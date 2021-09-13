# IMDB Clone

Built following the clean architecture principles.
Project separated in 3 layers.

## Layers of Backend

1. IMDBClone.Data - The lowest layer, the nearest to the database. Contains all the entities, commons and migrations.
2. IMDBClone.Domain - Responsible for the business logic. It works as a communication layer between the Application and Data. Mos of the information is getting processed there. It is the place of DTO, Middleware, ExtensionMethods, Validations, and Mappings.
3. IMDB.Application. Is the third layer of the application. "Open to outside". Has direct connection only with the Domain part of application. It just gets information passes to Domain for processing and gets back the result. It is also the place where the environment gets configured, remember appSettings, Startup and Program.

## How it works
IMDBClone.Application is an ASP.NET 5 (Core) Web API application. It is the place where all the requests will come and be handled. There will be configured the Environment properties, Authentication, Authorization, Database Connection, Dependency Injection, Exception Handling, etc. To become more loosely coupled we have segregated that logic into Extension methods, but it is the Application where everything takes place.

1.  For this system we have used PostgreSQL as a Database. For that we have configured its context with the help of the Npgsql library, from Nuget, where we have specified the Migration assembly also, where the migration will take place.

2. For authentication we have implemented Asp.Net 5 (Core) Identity, by configuring properly Users, Roles and Claims.

3. For Authorization of the requests and increasing the level of security we have used JWT Bearer over the RFC protocol to authorize requests using tokens of validation. They have an expiration time which we have set 5 hours(development purposes).

4. We have used Automapper for performing mapping between different entities. There are two main advantages of it: Mapping faster and provides easy conventions for translating an object to another.

5. For validation we have used Fluent Validation, which fits great with SOLID principles. 

6. We have used dependency injection to make Application dependent on Abstractions and not Concretions.
7. For serialization we have used NewtonSoft and we have applied CORS.
---

## Front End and Layers
The front-end applciation is written in Vanilla Javascript and JQuery for 2 main reasons: 
1. Time - Easier to build simple architectures.
2. The system wasn't too much complex for going for a framework.

## How it works
First since we have implemented JWT Token for Authorizing requests, User has firstly to login in order to get Auth token. So we have the first layer of 2 components:
1. Login
2. Register.

After the user is logged in he can see movies and rate them. 
Also can apply different kind of filters.
He can search based in text attributes of a movie or even based on an older or newer release date, even in fewer of more stars.
That is another component of the front-end.
  
User can also add movies by completing the movie fields. It will be shown in the main page afterwards.

Also implemented simple backend pagination.

## Tests
We have Implemented Unit Tests with XUnit, For performing simple validation tests.

---

### References:
https://gist.github.com/ygrenzinger/14812a56b9221c9feca0b3621518635b
  
https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.jwtbearer?view=aspnetcore-5.0

https://github.com/ardalis/CleanArchitecture
