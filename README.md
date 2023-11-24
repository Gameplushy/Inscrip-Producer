# Inscrip-Producer

The producer part of the AMQP-MailHog pipeline, using C#, EntityFramework, OpenAPI and RabbitMQ.
This branch does NOT PERSIST DATA in the database.

## Prerequisites

You'll need :
- Visual Studio 2022 (with "ASP.NET and web development" kit) (Optional, you can use the cmd if you don't need to debug it.)
- DotNET 7 (download the SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))
- Docker

## How to begin

### Using Visual Studio
Open the solution (.sln file) using Visual Studio. In case there are compiler errors showing up, you might need to get the NuGet packages; right-click the solution and select "Restore NuGet Packages".

### Using cmd
Go to the solution's folder. Use the command `dotnet restore`.

### Using the exe
Download the zip file found [here](https://github.com/Gameplushy/Inscrip-Producer/releases/tag/in-memory-release)

## How to use

### For all 3 possibilities
Execute in a cmd `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management` do run RabbitMQ.
You can check RabbitMQ's status with the following link: `http://localhost:15672/#/` 

### Using Visual Studio
Build and execute the code. A web page should open. This will open a Swagger page where you will be able to test the API.

### Using cmd
In cmd, use `dotnet run`. Follow the given URL (`Now listening on: http://localhost:<port number>`)). Add to the URL `/swagger`. This will open a Swagger page where you will be able to test the API.

### Using the exe
Execute the exe file. Follow the given URL (`Now listening on: http://localhost:<port number>`)). Add to the URL `/swagger`. This will open a Swagger page where you will be able to test the API.