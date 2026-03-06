# Credit Card Application API

ASP.NET Core Web API for a credit card application workflow using:
- SQL Server + EF Core
- JWT authentication + role-based authorization
- BCrypt password hashing
- Layered architecture (Controller -> Service -> Repository)

## Project Structure

- `Controllers/`
- `Services/`
- `Repositories/`
- `Models/`
- `DTOs/`
- `Data/`

## Prerequisites

- Visual Studio 2022 (17.8+) with **ASP.NET and web development** workload
- SQL Server LocalDB (installed with Visual Studio) or SQL Server instance

## Run in Visual Studio

1. Open `Creditcard.Application.Api.csproj` in Visual Studio.
2. Restore NuGet packages.
3. Verify connection string in `appsettings.Development.json`.
4. Set startup profile to `https` or `http`.
5. Run the project.
6. Swagger opens at `/swagger`.

## Database Setup (without committed migration files)

Run these commands in **Package Manager Console** or terminal:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Default API Endpoints

- `POST /api/auth/register`
- `POST /api/auth/admin/register`
- `POST /api/auth/login`
- `POST /api/applications` (User)
- `GET /api/applications/my` (User)
- `GET /api/applications/my/{applicationNumber}` (User)
- `GET /api/applications/admin?status=Pending` (Admin)
- `PATCH /api/applications/admin/{applicationNumber}/approve` (Admin)
- `PATCH /api/applications/admin/{applicationNumber}/reject` (Admin)
