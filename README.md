# Library Management System

This is a simple library management system built with the following structure:

- **Application**: Handles business logic and application services.
- **Domain**: Contains core entities and domain logic.
- **Infrastructure.EFCore**: Manages Entity Framework Core setup and database interactions.
- **Infrastructure**: Handles other infrastructure concerns.
- **Presentation.Api**: ASP.NET Core API for interacting with the system.

## Getting Started
1. Clone the repository.
2. Restore dependencies using `dotnet restore`.
3. Build the solution using `dotnet build`.

## Prerequisites
- .NET 8 SDK
- PostgreSQL for database

## License
[MIT License](LICENSE)
