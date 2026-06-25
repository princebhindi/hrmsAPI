<div align="center">

# 🏢 HRMS — Human Resource Management System

### A production-grade, full-stack HR platform built with enterprise architecture patterns

[![Backend](https://img.shields.io/badge/Backend-ASP.NET_Core_10-512BD4?logo=dotnet&logoColor=white)](https://github.com/princebhindi/hrmsAPI)
[![Frontend](https://img.shields.io/badge/Frontend-Angular_19-DD0031?logo=angular&logoColor=white)](https://github.com/princebhindi/hrmsAPI)
[![Mobile](https://img.shields.io/badge/Mobile-Flutter_3.x-02569B?logo=flutter&logoColor=white)](https://github.com/princebhindi/HRMS-APP)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![API Status](https://img.shields.io/badge/API-Live_on_Render-46E3B7?logo=render&logoColor=white)](https://hrmsapi-nwri.onrender.com)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Backend — ASP.NET Core API](#-backend--aspnet-core-api)
- [Frontend — Angular Dashboard](#-frontend--angular-dashboard)
- [Mobile — Flutter Employee App](#-mobile--flutter-employee-app)
- [Key Design Patterns](#-key-design-patterns)
- [Security](#-security)
- [Deployment](#-deployment)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)

---

## 🌟 Overview

HRMS is an **enterprise-grade, role-based Human Resource Management System** built with a clean, layered architecture. The platform serves two distinct user classes:

- 🔑 **HR Administrators** — Full access to the Angular web dashboard for managing employees, payroll, attendance, leaves, departments, jobs, notices, and documents.
- 👤 **Employees** — A dedicated Angular portal AND a Flutter mobile app for checking personal stats, attendance logs, salary slips, leave status, and live Punch-In/Punch-Out functionality.

The entire system is built with **CQRS + MediatR**, **Redis caching**, **JWT-based Role Auth**, and **FluentValidation** — reflecting real-world enterprise backend standards.

---

## 🏛️ Architecture

The backend follows **Clean Architecture** with strict layer separation:

```
┌─────────────────────────────────────────────────────────┐
│                    CLIENT LAYER                          │
│         Angular Web App  ⟷  Flutter Mobile App          │
└───────────────────────────┬─────────────────────────────┘
                            │ HTTPS / JWT
┌───────────────────────────▼─────────────────────────────┐
│              Register.API  (Presentation Layer)          │
│   Controllers · JWT Auth · Swagger · Serilog Logging     │
│        Global Exception Middleware · CORS Policy         │
└───────────────────────────┬─────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────┐
│          Register.APPLICATION  (Application Layer)       │
│                                                          │
│  ┌──────────────────────────────────────────────────┐   │
│  │              CQRS  via  MediatR                  │   │
│  │  Commands (Add / Update / Delete)                │   │
│  │  Queries  (GetAll / GetById / GetCount)          │   │
│  └──────────────────────────────────────────────────┘   │
│                                                          │
│  ┌─────────────────┐  ┌──────────────────────────────┐  │
│  │  Pipeline       │  │  Behaviors (Middleware Chain) │  │
│  │  ValidationBhvr │  │  ✦ ValidationBehavior         │  │
│  │  CachingBhvr    │  │  ✦ CachingBehavior (Redis)    │  │
│  │  CacheInvalBhvr │  │  ✦ CacheInvalidationBehavior  │  │
│  └─────────────────┘  └──────────────────────────────┘  │
│                                                          │
│  FluentValidation · AutoMapper · DTOs · Interfaces       │
└───────────────────────────┬─────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────┐
│         Register.PERSISTANCE  (Infrastructure Layer)     │
│  Entity Framework Core · Repository Pattern              │
│  SQL Server · Redis · CacheService                       │
└───────────────────────────┬─────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────┐
│            Register.DOMAIN  (Core Domain Layer)          │
│        Entities · Base Classes · Domain Logic            │
└─────────────────────────────────────────────────────────┘
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| **ASP.NET Core 10** | Web API framework |
| **Entity Framework Core** | ORM with Code-First migrations |
| **SQL Server** | Primary relational database |
| **MediatR** | CQRS mediator pattern |
| **Redis (StackExchange)** | Distributed query caching |
| **FluentValidation** | Input validation pipeline |
| **AutoMapper** | DTO ↔ Entity mapping |
| **Serilog** | Structured request & application logging |
| **JWT Bearer Auth** | Stateless role-based authentication |
| **Swagger / OpenAPI** | API documentation & live testing |
| **Docker** | Containerization for deployment |

### Frontend (Angular)
| Technology | Purpose |
|---|---|
| **Angular 19** | Single-Page Application framework |
| **TypeScript** | Strongly-typed component logic |
| **AuthGuard** | Route protection based on JWT role |
| **HTTP Interceptor** | Automatic Bearer token injection |
| **Reactive Forms** | Form validation & management |
| **Angular Services** | Centralized API communication layer |
| **Google Fonts + Custom CSS** | Premium glassmorphism design system |

### Mobile (Flutter)
| Technology | Purpose |
|---|---|
| **Flutter 3.x** | Cross-platform mobile app (Android & iOS) |
| **Dart** | Language for app logic |
| **HTTP package** | REST API calls |
| **SharedPreferences** | Persistent local JWT token storage |
| **JWT Decoder** | Parsing employee claims from token |
| **Google Fonts** | Premium Inter + Outfit typography |

---

## 📁 Project Structure

### Backend (`Register.*`)
```
Register/
├── Register.API/                  # Presentation Layer
│   ├── Controllers/               # 11 REST API Controllers
│   │   ├── AttendanceController
│   │   ├── EmployeeController
│   │   ├── LeavesController
│   │   ├── SalaryController
│   │   ├── DepartMentController
│   │   ├── JobController
│   │   ├── NoticesController
│   │   ├── EmployeeDocumentsController
│   │   ├── EmployeePortalController
│   │   ├── DashboardController
│   │   └── UserController
│   ├── Middleware/
│   │   └── GlobalExceptionMiddleware.cs   # Centralized error handling
│   ├── Dockerfile                         # Container deployment config
│   └── Program.cs                         # DI, Auth, Redis, MediatR setup
│
├── Register.APPLICATION/          # Application Layer (CQRS Core)
│   ├── Command/                   # Write operations (Add/Update/Delete)
│   ├── Queries/                   # Read operations (GetAll/GetById/Count)
│   ├── Handler/                   # 52 MediatR handlers
│   ├── Common/Behaviors/
│   │   ├── ValidationBehavior.cs      # Auto-validates before every handler
│   │   ├── CachingBehavior.cs         # Cache reads with Redis
│   │   └── CacheInvalidationBehavior.cs  # Invalidates cache on mutations
│   ├── DTO/                       # Data Transfer Objects
│   ├── Interface/                 # Repository abstractions
│   ├── Mapping/                   # AutoMapper profiles
│   └── Validators/                # FluentValidation rule sets
│
├── Register.DOMAIN/               # Core Domain Layer
│   ├── Entities/                  # Database entities
│   └── Common/Basic.cs            # Shared base class (Id, IsActive, etc.)
│
└── Register.PERSISTANCE/          # Infrastructure Layer
    ├── Context/                   # EF Core DbContext
    ├── Repository/                # 10 Concrete repository implementations
    ├── Services/CacheService.cs   # Redis/Memory cache abstraction
    ├── Configuratioon/            # EF entity configurations
    └── Migrations/                # EF Code-First migration history
```

### Frontend Angular (`FrontEnd/HMS/src/app/`)
```
app/
├── core/
│   ├── guard/
│   │   └── auth.guard.ts          # ✅ Route protection — redirects unauthenticated users
│   ├── interceptors/
│   │   └── auth.interceptor.ts    # ✅ Auto-attaches Bearer token to every request
│   └── services/                  # 10 dedicated API service classes
│       ├── auth.ts                # Login + token management
│       ├── employee.service.ts
│       ├── attendance.service.ts
│       ├── leave.service.ts
│       ├── salary.service.ts
│       ├── department.service.ts
│       ├── notice.service.ts
│       └── employee-portal.service.ts
│
├── features/
│   ├── auth/login/                # Login page
│   ├── dashboard/                 # Admin Dashboard
│   │   └── pages/
│   │       ├── overview/          # Dashboard stats & KPIs
│   │       ├── employees/         # Full employee CRUD
│   │       ├── attendance/        # Attendance management
│   │       ├── leaves/            # Leave approval workflow
│   │       ├── salaries/          # Payroll management
│   │       ├── departments/       # Department management
│   │       ├── notices/           # Company notice board
│   │       └── settings/          # System settings
│   └── employee-portal/           # Role-gated Employee Self-Service
│       └── pages/
│           ├── ep-overview/       # Personal dashboard & KPIs
│           ├── ep-attendance/     # Personal attendance logs
│           ├── ep-leaves/         # Leave history & status
│           ├── ep-salary/         # Personal salary slips
│           └── ep-profile/        # Profile page
│
└── environments/
    ├── environment.ts             # Development API URL
    └── environment.prod.ts        # Production Render API URL
```

### Mobile Flutter (`flutterApp/lib/`)
```
lib/
├── main.dart                      # App entry point + routing
├── constants/
│   └── app_constants.dart         # All API endpoint constants
├── screens/
│   ├── login_screen.dart          # Glassmorphism login UI
│   └── home_screen.dart           # Main app (tabbed navigation)
│       ├── Overview Tab           # Stats + Punch-In/Punch-Out card
│       ├── Attendance Tab         # Personal attendance log list
│       ├── Leaves Tab             # Personal leave history list
│       └── Profile Tab            # Employee profile + logout
└── services/
    ├── auth_service.dart           # Token storage & JWT decoding
    └── api_service.dart            # All API calls + client-side filtering
```

---

## ⚙️ Backend — ASP.NET Core API

### CQRS with MediatR
Every operation in the system goes through a **MediatR pipeline** — separating reads from writes for clean, testable, scalable code.

```csharp
// QUERY — Read data (cached automatically via Redis pipeline behavior)
public record GetAllEmployeesQuery(int PageNumber, int PageSize)
    : IRequest<APIResponse<IEnumerable<EmployeeDto>>>, ICacheable
{
    public string CacheKey => $"employees-page-{PageNumber}-{PageSize}";
}

// COMMAND — Write data (automatically invalidates related cache)
public record AddEmployeeCommand(EmployeeDto Employee)
    : IRequest<APIResponse<EmployeeDto>>, ICacheInvalidator
{
    public string CachePattern => "employees-*";
}
```

### Redis Caching Pipeline
Queries are automatically intercepted and cached **before** hitting the database. Cache is automatically **invalidated** when related mutations occur.

```csharp
// Registered as MediatR pipeline behaviors in Program.cs
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));
```

### Graceful Redis Fallback
The system gracefully degrades to an in-memory cache if a Redis connection string is not configured — ensuring the app works in any environment.

```csharp
var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");
if (string.IsNullOrEmpty(redisConnection))
    builder.Services.AddDistributedMemoryCache(); // fallback for local dev
else
    builder.Services.AddStackExchangeRedisCache(options => {
        options.Configuration = redisConnection;
    });
```

### API Modules

| Module | Endpoints | Role Access |
|---|---|---|
| **User / Auth** | Login, Register | Public |
| **Employee** | CRUD, Document Upload | Admin |
| **Department** | CRUD | Admin |
| **Job** | CRUD | Admin |
| **Attendance** | CRUD, GetAll | Admin, Employee |
| **Leaves** | CRUD, Approve/Reject | Admin, Employee |
| **Salary** | CRUD, Auto-deduction | Admin, Employee |
| **Notices** | CRUD | Admin |
| **Dashboard** | Aggregated stats | Admin |
| **Employee Portal** | Personal stats | Employee |

---

## 🅰️ Frontend — Angular Dashboard

### Role-Based Route Protection with AuthGuard

```typescript
// core/guard/auth.guard.ts
export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  return auth.isLoggedIn() ? true : inject(Router).createUrlTree(['/login']);
};

// Applied on every protected route
{ path: 'dashboard', canActivate: [authGuard], ... }
{ path: 'employee-portal', canActivate: [authGuard], ... }
```

### Automatic Token Injection via HTTP Interceptor

```typescript
// core/interceptors/auth.interceptor.ts
// Every outgoing HTTP request automatically gets the Bearer token attached
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');
  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;
  return next(authReq);
};
```

### Environment-Based API Configuration
The app uses Angular environments to switch between dev and production APIs — no hardcoded URLs.

```typescript
// environments/environment.prod.ts
export const environment = {
  production: true,
  apiUrl: 'https://hrmsapi-nwri.onrender.com/api'
};
```

### Dashboard Modules
- 📊 **Overview** — Real-time KPI cards (Total Employees, Active Leaves, Attendance Rate, Departments)
- 👥 **Employees** — Full CRUD with document upload (Azure Blob / local storage)
- 📅 **Attendance** — View, add, edit logs with total hours auto-calculation
- 🌴 **Leaves** — Submit, approve, reject leave requests with status tracking
- 💰 **Salaries** — Auto-computed payroll with configurable leave deductions
- 🏢 **Departments** — Manage organizational structure
- 📋 **Notices** — Company-wide announcements
- ⚙️ **Settings** — System configuration

---

## 📱 Mobile — Flutter Employee App

A dedicated **glassmorphism-styled** Flutter app for employees to manage their work-life on the go.

### Key Features

#### 🟢 Live Punch-In / Punch-Out
Employees can mark their own attendance directly from the phone. The app:
1. Checks if the employee has already punched in today on load.
2. Auto-calculates status (`Present` before 9:30 AM, `Late` after).
3. On **Punch Out**, the backend auto-computes `TotalHours`.

```dart
// api_service.dart — Attendance punch with smart status calculation
static Future<Map<String, dynamic>> punchIn() async {
  final now = DateTime.now();
  String status = (now.hour > 9 || (now.hour == 9 && now.minute > 30))
      ? 'Late' : 'Present';
  // POSTs new attendance record to /api/Attendance
}
```

#### 🔒 Secure Token-Based Auth
JWT token is decoded on login to extract `empId`, `username`, and `role`. Token and employee data are persisted locally with `SharedPreferences`.

#### 🔍 Smart Client-Side Data Filtering
The app fetches paginated global data and filters it client-side by `empId` — mirroring the exact same pattern used in the Angular Employee Portal.

```dart
jsonResponse['data'] = all
  .where((item) =>
    item['empId']?.toString().toLowerCase() == empId?.toLowerCase())
  .toList();
```

### App Screens

| Screen | Description |
|---|---|
| **Login** | Glassmorphism UI with animated gradient background |
| **Overview** | Personal stats cards + live Punch-In/Punch-Out banner |
| **Attendance** | Full attendance history log with status badges |
| **Leaves** | Leave history with Pending / Approved / Rejected status |
| **Profile** | Employee details with one-tap logout |

---

## 🎯 Key Design Patterns

| Pattern | Where Used | Benefit |
|---|---|---|
| **CQRS** | Backend — Application Layer | Separates reads & writes for scalability |
| **MediatR** | Backend — Handlers | Decoupled request/response pipeline |
| **Repository Pattern** | Backend — Persistence Layer | Abstracts DB access from business logic |
| **Pipeline Behaviors** | Backend — Validation + Caching | Cross-cutting concerns without code duplication |
| **Clean Architecture** | Backend — 4 project layers | Independent, testable layers |
| **AuthGuard** | Angular Frontend | Prevents unauthorized route access |
| **HTTP Interceptor** | Angular Frontend | DRY token attachment to API calls |
| **Provider Pattern** | Flutter — Services | Centralized API and auth state management |
| **DTO Pattern** | Backend ↔ Frontend | Controlled data exposure at API boundary |

---

## 🔐 Security

- **JWT Authentication** — Stateless, role-encoded tokens signed with HMAC-SHA256.
- **Role-Based Authorization** — All endpoints enforce `[Authorize(Roles = "Admin")]` or `[Authorize(Roles = "Admin,Employee")]`.
- **FluentValidation** — All incoming commands are validated before handler execution via the pipeline behavior.
- **Global Exception Middleware** — Catches all unhandled exceptions, logs them with Serilog, and returns standardized error responses.
- **CORS Policy** — Configured to allow only specific Netlify and localhost origins.
- **Password Hashing** — User passwords are never stored in plain text.
- **HTTP-Only Storage** — Mobile app stores JWT in `SharedPreferences` (device secure storage).

---

## 🚀 Deployment

| Service | Platform | URL |
|---|---|---|
| **Backend API** | Render (Docker) | `https://hrmsapi-nwri.onrender.com` |
| **Angular Frontend** | Netlify | Auto-deployed on push to main |
| **Flutter App** | GitHub Releases | `https://github.com/princebhindi/HRMS-APP` |
| **Database** | SQL Server | Managed cloud instance |

The backend is **fully containerized** with a multi-stage `Dockerfile`:
1. **Build stage** — Restores dependencies, compiles with .NET SDK 10.
2. **Publish stage** — Creates optimized release artifacts.
3. **Final stage** — Copies published app into a lean ASP.NET runtime image.

---

## 🏁 Getting Started

### Prerequisites
- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [Flutter SDK 3.x](https://flutter.dev/docs/get-started/install)
- SQL Server (local or cloud)
- Redis (optional — falls back to in-memory cache)

### 1. Clone the Repositories

```bash
# Backend + Frontend
git clone https://github.com/princebhindi/hrmsAPI.git
cd hrmsAPI

# Mobile App
git clone https://github.com/princebhindi/HRMS-APP.git
```

### 2. Backend Setup

```bash
# Restore dependencies
dotnet restore

# Update appsettings.json with your connection strings
# "DefaultConnection": "Server=...;Database=HRMS;..."
# "RedisConnection": "localhost:6379" (optional)
# "Jwt:Key": "your-secret-key-min-32-chars"

# Run EF migrations
dotnet ef database update --project Register.PERSISTANCE --startup-project Register.API

# Run the API
dotnet run --project Register.API
# API available at: https://localhost:7xxx / http://localhost:5xxx
```

### 3. Angular Frontend Setup

```bash
cd FrontEnd/HMS
npm install
ng serve
# App available at: http://localhost:4200
```

### 4. Flutter Mobile App Setup

```bash
cd HRMS-APP   # (or flutterApp if cloned with hrmsAPI)
flutter pub get
flutter run
```

> **Note:** Update `lib/constants/app_constants.dart` with your local API URL for development.

---

## 📖 API Documentation

When running locally, Swagger UI is available at the API root:

```
http://localhost:<port>/
```

All endpoints require a `Bearer <token>` header (except login/register). Use the login endpoint to obtain a JWT:

```http
POST /api/User/login
Content-Type: application/json

{
  "userName": "admin",
  "password": "yourpassword"
}
```

---

## 👨‍💻 Author

**Prince Bhindi** — Full-Stack Developer

[![GitHub](https://img.shields.io/badge/GitHub-princebhindi-181717?logo=github)](https://github.com/princebhindi)

---

<div align="center">

**Built with ❤️ using Clean Architecture, CQRS, Redis, Angular, and Flutter**

*"Code is like humor. When you have to explain it, it's bad." — Cory House*

</div>
