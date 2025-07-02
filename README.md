<div align="center">
  <img src="assets/logo-1250x1250.png" width="120" alt="ResultKit logo" />
  <h1>CleanArch.StarterKit</h1>
</div>

<p align="center">
  🧱 A robust, modular, and enterprise-ready Clean Architecture template for .NET 9.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-blue?logo=dotnet" />
  <img src="https://img.shields.io/badge/EF--Core-9.0-success?logo=entity-framework" />
  <img src="https://img.shields.io/badge/License-MIT-informational" />
</p>

---

## 📦 What is CleanArch.StarterKit?

**CleanArch.StarterKit** is a production-ready boilerplate that demonstrates a clean, layered approach to .NET application development using Clean Architecture and modular principles.

It features ready-to-use CQRS, MediatR, authentication (JWT), background jobs (Hangfire), HealthChecks, Audit Logging, ResultKit, RepositoryKit, and more.  
Designed for scalable, maintainable, and testable enterprise solutions.

---

## 🧰 Technologies Used

| Technology            | Purpose                          |
| --------------------- | -------------------------------- |
| ASP.NET Core 9        | Web API layer                    |
| Entity Framework Core | Data persistence                 |
| CQRS & MediatR        | Request/response separation      |
| FluentValidation      | Request validation               |
| Serilog               | Structured logging               |
| Hangfire              | Background jobs & scheduling     |
| HealthChecks & UI     | Health endpoints                 |
| ResultKit             | Result/Error handling            |
| RepositoryKit         | Generic repository pattern       |

---


## 🚀 Installation

### 1️⃣ Install via NuGet as a Template (Recommended)

You can install CleanArch.StarterKit as a dotnet template via NuGet:

```bash
dotnet new install CleanArch.StarterKit::9.0.1
```

After installation, create a new solution with:
```bash
dotnet new cleanarch-starterkit -n YourProjectName
```

### 2️⃣ Manual Clone
Alternatively, you can clone the repository directly:

```bash
git clone https://github.com/taberkkaya/CleanArch.StarterKit.git
cd CleanArch.StarterKit/src
```

---

## 🚀 Project Structure

```plaintext
📁 CleanArch.StarterKit
|
├── 📁 CleanArch.StarterKit.WebApi         → API layer (Controllers, middleware, DI)
├── 📁 CleanArch.StarterKit.Application    → CQRS, business rules, services, validation
├── 📁 CleanArch.StarterKit.Domain         → Domain models, entities, contracts
├── 📁 CleanArch.StarterKit.Infrastructure → Data access, Identity, integrations
```

## ⚙️ Getting Started
### 🚀 Installation
You can clone this repository and use it directly, or install as a template (optionally):

1. Clone the repository:

```bash
git clone https://github.com/taberkkaya/CleanArch.StarterKit.git
cd CleanArch.StarterKit/src
```

2. Update the connection string in appsettings.json
```json
"ConnectionStrings": {
  "DefaultConnection": "Your-Connection-String-Here"
}
```

3. Run the API:
```bash
dotnet run --project CleanArch.StarterKit.WebApi
```
- **Swagger:** [https://localhost:7294/swagger](https://localhost:7294/swagger)
- **HealthChecks UI:** [https://localhost:7294/health-ui](https://localhost:7294/health-ui)
- **Hangfire Dashboard:** [https://localhost:7294/hangfire](https://localhost:7294/hangfire)

## ✅ Benefits
- Separation of concerns with Clean Architecture
- Built-in CQRS with MediatR & validation
- Out-of-the-box JWT authentication and authorization
- Automatic health checks and monitoring UI
- Hangfire background jobs, with dashboard and DB authentication
- Full audit logging for all critical data changes and requests
- Generic repository and robust result patterns for maintainable code
- SOLID, modular, scalable, and easily testable structure

## 🔑 Authentication
- JWT-based user authentication (/api/Auth/login)
- Admin-only endpoints and role-based access out of the box
- Hangfire Dashboard login credentials managed via the database

## ✨ Contribution
Fork this repository and open a PR to contribute new features, improvements, or bugfixes!

<p align="center"> <img src="https://skillicons.dev/icons?i=dotnet,github,visualstudio" /> </p>