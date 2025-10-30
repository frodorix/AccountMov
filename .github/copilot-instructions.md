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

## Controller Pattern

Controllers follow a consistent structure (see `ClientesController`, `MovimientosController`):

```csharp
[Route("api/[controller]")]
[ApiController]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientosService movimientosService;
    private readonly ILogger<MovimientosController> _logger;
    
    // Constructor injection of service and logger
    public MovimientosController(IMovimientosService movimientosService, ILogger<MovimientosController> _logger) { }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DNuevoMovimiento json)
    {
        if (json == null) return BadRequest("JSon Invalido");
        try {
            var result = await this.movimientosService.Crear(json.toMovimiento());
            return StatusCode(201, JsonConvert.SerializeObject(result.Id, new StringEnumConverter()));
        }
        catch (ClienteException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { _logger.LogError(ex.StackTrace); return StatusCode(500); }
    }
}
```

**Pattern**: Inject service + logger → Validate input → Try-catch with domain exceptions → Return appropriate HTTP status

## Repository Pattern

Repositories inherit from `Repository<TEntity>` base class and use AutoMapper for conversions:

```csharp
internal class ClientesRepository : Repository<Cliente>, IClientesRepository
{
    private readonly IMapper _mapper;
    
    public ClientesRepository(MyContext context, IMapper mapper) : base(context) 
    { 
        _mapper = mapper; 
    }
    
    public async Task<MCliente> Crear(MCliente cliente)
    {
        var entidad = _mapper.Map<Cliente>(cliente);  // Domain Model → Entity
        DB.Clientes.Add(entidad);
        await DB.SaveChangesAsync();
        cliente.Id = entidad.ClienteId;  // Set generated ID back to model
        return cliente;
    }
}
```

**Pattern**: AutoMapper for M* ↔ Entity conversion → SaveChangesAsync → Return domain model with generated keys

## Business Logic: Account Movements

Key business rules in `CuentasService` for banking operations:

**Credits** (`RegistrarCredito`):
- Always store as positive value: `Math.Abs(valor)`
- New balance = current balance + credit amount
- No validation needed (credits always allowed)

**Debits** (`RegistrarDebito`):
- Always store as negative value: `Math.Abs(valor) * -1`
- Validates: `saldoActual - valorDebito >= 0` (no overdraft)
- Validates daily limit: `totalRetiros + valorDebito <= cliente.LimiteDiario`
- Throws `CuentaException` with "Saldo no disponible" or "Cupo diario Excedido"

**Balance Calculation**:
- Current balance = `SUM(Movimientos.Valor)` where Valor includes sign (+ for credit, - for debit)
- Implemented in `CuentasRepository.ObtenerSaldoCuenta()`

**Transaction Pattern**: All credit/debit operations use `IDbContextTransaction` with try-catch-rollback

## Database Schema

**Cliente** (Bank customers):
- PK: `ClienteId` (int, auto-increment)
- Fields: `Nombre`, `Genero` (enum), `Edad`, `Identificacion`, `Direccion`, `Telefono`, `Contrasena` (hashed), `Estado` (enum), `LimiteDiario` (decimal)
- Relationship: One-to-Many with `Cuenta`

**Cuenta** (Bank accounts):
- PK: `NumeroCuenta` (int, auto-increment)
- FK: `ClienteId` → Cliente
- Fields: `Tipo`, `SaldoInicial` (decimal), `Estado` (enum)
- Relationship: Many-to-One with `Cliente`, One-to-Many with `Movimiento`

**Movimiento** (Transactions):
- PK: `Id` (int, auto-increment)
- FK: `NumeroCuenta` → Cuenta
- Fields: `Fecha` (datetime), `Tipo` (enum: Credito/Debito), `Valor` (decimal, signed), `Saldo` (decimal, running balance)
- Relationship: Many-to-One with `Cuenta`

**Key Design**: `Movimiento.Saldo` stores running balance snapshot at transaction time. Current balance = SUM(Valor).

## API Testing with Postman

Postman collections in `Tools/Postman/`:
- `DEVSU.postman_collection.json`: Complete API endpoint collection (Clientes, Cuentas, Movimientos, Reportes)
- `Desarrollo.postman_environment.json`: Environment variables (base URL, port 8432)

Import both files into Postman for ready-to-use API testing. Collection includes all CRUD operations and report endpoints.
