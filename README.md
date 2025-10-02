BookingApp
A layered C#/.NET solution for a booking system, organized into API, Services, Domain, and Data Access layers. The goal is to keep application logic cleanly separated and easy to extend, test, and deploy. The repository currently includes:
BookingApp/
├─ BookingApp.Api/        # ASP.NET Web API (HTTP endpoints)
├─ BookingApp.Services/   # Application services / use-cases
├─ BookingApp.Domain/     # Core domain models & interfaces
├─ BookingApp.Dal/        # Data access (repositories, persistence)
└─ BookingApp.sln         # Solution file

Repo: entrl/BookingApp. GitHub

✨ Features (current & planned)
Layered architecture: Domain-driven separation of concerns (API ⇢ Services ⇢ Domain ⇢ DAL).


Testable design: Business logic isolated from transport & persistence.


Replaceable persistence: DAL encapsulates data access (swap databases without touching domain/services).


Room to grow: Add auth, validation, caching, background jobs, etc., without breaking boundaries.


Note: Specific endpoints, models, and database choices are intentionally kept flexible; fill in details as the implementation evolves.

🧱 Architecture
BookingApp.Domain
 Core entities, value objects, domain interfaces, and domain services.


BookingApp.Services
 Application service layer orchestrating use-cases (e.g., create booking, cancel booking, list availability). Depends on Domain interfaces and is consumed by the API.


BookingApp.Dal
 Repository implementations and persistence concerns. Depends on Domain interfaces. (If using EF Core, typical contents: DbContext, entity configurations, migrations.)


BookingApp.Api
 Thin HTTP layer exposing endpoints/controllers and request/response contracts (DTOs). Depends on Services.


Repo layout reference: entrl/BookingApp. GitHub

🚀 Getting Started
Prerequisites
.NET SDK (7 or 8 recommended)


Database (e.g., SQL Server / PostgreSQL / SQLite). Configure the connection string as described below.


Clone
git clone https://github.com/entrl/BookingApp.git
cd BookingApp

Restore & Build
dotnet restore
dotnet build

Configure Environment
Set a connection string (example using a user-secrets or env var approach):
# Example (PowerShell)
$env:ConnectionStrings__Default="Host=localhost;Port=5432;Database=bookingapp;Username=postgres;Password=postgres"

Or in appsettings.Development.json under BookingApp.Api:
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=BookingApp;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

Adjust the key name and value to match your DAL choice. If you’re using EF Core, add migrations and update the database:
dotnet ef migrations add InitialCreate -p BookingApp.Dal -s BookingApp.Api
dotnet ef database update -p BookingApp.Dal -s BookingApp.Api

Run the API
dotnet run --project BookingApp.Api

By default, the API will listen on the configured Kestrel ports. If Swagger is enabled in Development, browse to /swagger to explore the endpoints.

🧪 Testing (suggested layout)
Add a test project (e.g., BookingApp.Tests) and validate:
Domain unit tests (pure logic).


Service layer tests (use in-memory doubles).


API integration tests (WebApplicationFactory).


dotnet new xunit -n BookingApp.Tests
dotnet add BookingApp.Tests reference BookingApp.Domain BookingApp.Services


📦 Common Scripts (optional)
Create a Directory.Build.props or Makefile/pwsh scripts for convenience:
# Restore, build, test
dotnet restore && dotnet build && dotnet test

# Run API
dotnet run --project BookingApp.Api


🔧 Configuration & Secrets
Typical environment variables:
ConnectionStrings__Default – database connection string


ASPNETCORE_ENVIRONMENT – Development / Staging / Production


Use [dotnet user-secrets] in development to avoid committing secrets.

🗺️ Roadmap (examples)
Entities: Booking, Resource, User/Customer, Timeslot, AvailabilityRule


Endpoints: create/cancel booking, check availability, list bookings


Validation & error handling (ProblemDetails)


Authentication/Authorization (JWT)


Observability (logging, tracing)


Caching for availability lookups


CI/CD workflow



🤝 Contributing
Fork the repo and create a feature branch


Keep changes small and focused per layer


Add tests where applicable


Open a PR with a clear description



📄 License
Add your preferred license (e.g., MIT). Place the text in LICENSE.

📚 References
Repo overview & structure: entrl/BookingApp (folders: BookingApp.Api, BookingApp.Services, BookingApp.Domain, BookingApp.Dal, solution file). GitHub

