# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a .NET solution called "Nelson" with three main projects:

- **Api/**: A GraphQL API built with HotChocolate (net8.0)
- **BackOffice/**: A Blazor Server app with MudBlazor UI (net9.0) 
- **Database/**: Entity Framework Core data layer (net9.0) with MySQL support

## Common Commands

### Building the solution
```bash
dotnet build Nelson.sln
```

### Running individual projects
```bash
# GraphQL API
dotnet run --project Api/Api.csproj

# Blazor BackOffice
dotnet run --project BackOffice/BackOffice.csproj
```

### Database operations (from Database project directory)
```bash
# Add migration
dotnet ef migrations add [MigrationName]

# Update database
dotnet ef database update
```

### Testing
```bash
dotnet test Nelson.sln
```

## Architecture Overview

The solution follows a layered architecture:

1. **Api Project**: Minimal GraphQL API using HotChocolate with a single Query type that returns book data
2. **BackOffice Project**: Blazor Server application with MudBlazor components for administration
3. **Database Project**: Contains Entity Framework Core setup with MySQL provider (currently empty but configured for EF migrations)

The API currently has simple domain models (Book, Author) defined as records in the Types namespace. The BackOffice uses standard Blazor components with MudBlazor for UI.

## Key Dependencies

- **HotChocolate**: GraphQL server framework (v15.1.8)
- **MudBlazor**: Blazor component library (v8.*)
- **Entity Framework Core**: ORM with MySQL provider (v9.0.7)
- **Pomelo.EntityFrameworkCore.MySql**: MySQL database provider (v8.0.3)