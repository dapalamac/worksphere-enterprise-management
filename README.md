# WorkSphere

Sistema de gestión de empleados desarrollado como aplicación full-stack,
con backend en ASP.NET Core y frontend en React + TypeScript.

## Tecnologías

### Backend
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt
- Hangfire
- Repository Pattern
- Dependency Injection
- Clean Architecture

### Frontend
- React
- TypeScript
- Vite
- Context API
- CSS

### Testing
- xUnit
- Unit Testing

## Estructura del proyecto

```text
WorkSphere/
├── backend/
│   ├── WorkSphere.Api/
│   ├── WorkSphere.Application/
│   ├── WorkSphere.Domain/
│   ├── WorkSphere.Infrastructure/
│   ├── WorkSphere.Tests/
│   └── WorkSphere.sln
│
├── frontend/
│   └── worksphere-frontend/
│
├── .github/
├── .gitignore
└── README.md
