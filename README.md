# AccountMov API

Sistema de gestión de cuentas bancarias y movimientos desarrollado con .NET 8.0 siguiendo principios de Clean Architecture, SOLID y Clean Code.

## Tecnologías

- **.NET 8.0 LTS** - Framework principal
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **AutoMapper 13.0** - Mapeo de objetos
- **SQL Server 2022** - Base de datos
- **Swagger/OpenAPI** - Documentación de API
- **Docker & Docker Compose** - Containerización

## Arquitectura

El proyecto sigue Clean Architecture con separación clara de responsabilidades:

- **AccountMovAPI**: Capa de presentación (API REST)
- **CORE.Account**: Lógica de negocio y modelos de dominio
- **Infrastructure.Persistence**: Acceso a datos y repositorios
- **TEST.TestProject**: Pruebas unitarias

## Principios Aplicados

### SOLID
- **Single Responsibility**: Cada clase tiene una única responsabilidad
- **Dependency Inversion**: Uso de interfaces y inyección de dependencias
- **Interface Segregation**: Interfaces específicas por responsabilidad

### Clean Code
- Nombres descriptivos y autoexplicativos
- Métodos pequeños y enfocados
- Eliminación de código duplicado
- Gestión adecuada de nullable references

### Optimizaciones de Memoria
- AutoMapper configurado como singleton
- Uso de Scoped lifetime para repositorios
- LINQ queries optimizados

## Configuración con Docker Compose

### Inicio rápido
```bash
docker-compose up -d
```

Esto iniciará:
- SQL Server en el puerto 8433
- API en el puerto 8432

### Configuración manual (alternativa)

La carpeta `Tools` contiene el script `BUILDRunSQLServer.ps1` para crear imágenes y contenedores en Docker de forma individual.

```powershell
cd Tools
.\BUILDRunSQLServer.ps1
```

## Variables de Entorno

La aplicación usa las siguientes variables configurables:

- `ConnectionStrings__AccountsDB`: Cadena de conexión a SQL Server
- `ASPNETCORE_ENVIRONMENT`: Ambiente de ejecución (Development/Production)

## Estructura de la Base de Datos

- **Clientes**: Información de clientes bancarios
- **Cuenta**: Cuentas bancarias asociadas a clientes
- **Movimientos**: Transacciones y movimientos de las cuentas

## API Endpoints

La documentación completa de la API está disponible en Swagger UI:
- Desarrollo: `http://localhost:8432/swagger`

Los archivos de Postman con las configuraciones de todos los métodos están en la carpeta `Tools/Postman`:
- Colección de métodos
- Variables de entorno

## Desarrollo Local

### Requisitos
- .NET 8.0 SDK
- SQL Server 2022 o Docker

### Compilar
```bash
dotnet build
```

### Ejecutar
```bash
dotnet run --project AccountMovAPI
```

### Pruebas
```bash
dotnet test
```

## Seguridad

- Passwords hasheados con SHA256
- Connection strings externalizadas en configuración
- Validación de datos de entrada
- Nullable reference types habilitados

## Mejoras Implementadas

### Versión 2.0
- Actualización a .NET 8.0 LTS desde .NET 6.0
- Implementación de docker-compose.yml
- Servicio dedicado de hash de contraseñas
- AutoMapper configurado como singleton
- Eliminación de hardcoded connection strings
- Corrección de warnings de nullable references
- Optimización de lifetime de servicios (Scoped vs Transient)
- Limpieza de código y usings innecesarios
