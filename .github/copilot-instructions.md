# AccountMov - AI Coding Agent Instructions

## Architecture Overview

This is a .NET 8.0 banking system following **Clean Architecture** with three main layers:

- **AccountMovAPI** (Presentation): REST API controllers, DTOs, and API-specific configuration
- **CORE.Account** (Domain/Application): Business logic, domain models (M* prefix), services, and interfaces
- **Infrastructure.Persistence** (Data): EF Core repositories, entities, DbContext, and AutoMapper profiles

**Key Principle**: Dependencies flow inward. Core has NO dependencies on Infrastructure or API layers.

## Dependency Injection Pattern

Services are registered via extension methods in dedicated `Extensions/Configure*.cs` files:

- `CORE.Account/Extensions/ConfigureCore.cs`: Registers application services (Scoped) and PasswordHashingService (Singleton)
- `Infrastructure.Persistence/Extensions/ConfigureInfrastructure.cs`: Registers DbContext, AutoMapper (Singleton), and repositories (Scoped)

In `Program.cs`, use: `builder.Services.UseInfrastructurePersistence(config)` and `builder.Services.UseCoreServices()`

## Naming Conventions

- **Domain Models**: `M` prefix (e.g., `MCliente`, `MCuenta`, `MMovimiento`) in `CORE.Account/Domain/Model`
- **Entities**: No prefix (e.g., `Cliente`, `Cuenta`, `Movimiento`) in `Infrastructure.Persistence/Entity/Accounts`
- **DTOs**: `D` prefix (e.g., `DCliente`, `DCuenta`) - split between API layer (API-specific) and CORE layer (shared)
- **Interfaces**: Standard `I` prefix, defined in `CORE.Account/Interfaces` for repositories

## AutoMapper Configuration

AutoMapper is configured as **Singleton** for performance in `EntityToModelProfile.cs`. Maps bidirectionally between:
- Domain Models (`MCliente`) ↔ Entities (`Cliente`)
- Navigation properties are explicitly ignored in mappings to avoid circular references

## Service Lifetime Patterns

- **Scoped**: All repositories (`I*Repository`), application services (`I*Service`), `IDateTimeProvider`
- **Singleton**: `AutoMapper`, `IPasswordHashingService` (stateless, performance-critical)
- **Never Transient**: Avoid for services with dependencies (use Scoped instead)

## Custom Exceptions

Domain-specific exceptions in `CORE.Account/Exception`:
- `ClienteException`, `CuentaException`, `MovimientosException` - for business rule violations
- `NotFoundException` - for entity not found scenarios

Controllers catch these and return appropriate HTTP status codes (400 BadRequest, 404 NotFound, 500 InternalServerError).

## Password Security

Passwords are hashed using SHA256 via `IPasswordHashingService` before persistence. See `PasswordHashingService.cs` for implementation.

## Development Workflow

**Build**: `dotnet build` from root (solution level)
**Test**: `dotnet test` - Tests use Moq for repository mocking, NUnit framework
**Run API**: `dotnet run --project AccountMovAPI` - Swagger UI at `http://localhost:8432/swagger`
**Docker**: `docker-compose up -d` starts SQL Server (port 8433) and API (port 8432)

**Environment Setup**: Copy `.env.example` to `.env` and update `SA_PASSWORD` before using Docker Compose.

## Database Configuration

Connection string must be in `appsettings.json` as `ConnectionStrings:AccountsDB`. Never hardcode.
The app throws `InvalidOperationException` if connection string is missing (see `ConfigureInfrastructure.cs`).

## Nullable Reference Types

Enabled project-wide. Existing code has some warnings - when modifying related code, fix nullability warnings but don't break existing functionality.

## Testing Patterns

Unit tests in `TEST.TestProject` use:
- Moq for mocking repositories and services
- Setup patterns: See `MovimientosTest.cs` `InitMoq()` method for repository mock configuration examples
- Integration tests require running API (currently fail if API isn't running on port 8432)
