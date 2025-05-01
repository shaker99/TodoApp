TodoApp

TodoApp.API is a simple Web API built with ASP.NET Core 6 to manage tasks and users.
Technologies Used

    ASP.NET Core 6

    Entity Framework Core

    JWT Authentication

    AutoMapper

    FluentValidation

    Docker & Docker Compose

    Swagger

Project Structure

    TodoApp.API/: API Layer (Controllers, Middleware)

    TodoApp.Application/: Application Layer (DTOs, Interfaces, Mapping)

    TodoApp.Domain/: Domain Layer (Entities, Enums)

    TodoApp.Infrastructure/: Infrastructure Layer (Database (Migrations), Repositories)

Main Features
User Authentication

    Login with JWT token generation.

Role-Based Authorization

    Owner and Guest roles with different permissions.

Task Management

    CRUD operations (Create, Read, Update, Delete).

    Filter tasks by Priority and Category.

    Pagination support.

Global Error Handling

    Custom Exception Middleware for error handling.

Automatic Validation

    Automatic request validation using FluentValidation.

Dockerized

    Ready for deployment with Docker and Docker Compose.

Run with Docker

    Build and start the containers:

    docker-compose up --build

    Access the API: You can access the Swagger UI at: http://localhost:5000/swagger/index.html

    For authorize :
    username : admin
    password : 123456
