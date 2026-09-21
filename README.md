MovieApp — DVD Rental Shop Management System
A full-stack web application built with ASP.NET Core MVC to help a small-town DVD rental shop manage its inventory, customers, and rentals. What started as a learning project grew into a practical business tool with real authentication, business rules, and a modern UI.

Features
Core Management
DVD Inventory — Add, edit, delete, and browse movies with full details (title, genre, release year, director, stock count).

Customer Records — Manage customer profiles with contact information and rental history.

Rental Tracking — Record rentals, mark returns, and track due dates.

Overdue Monitoring — Dedicated page showing all overdue rentals with day counts and quick-return actions.

Business Rules Enforced
A customer cannot rent a movie if no copies are available.

A customer with overdue rentals cannot rent further until they return what they've borrowed.

A customer with active rentals cannot be deleted from the system.

A rental cannot be returned twice.

Search, Filter, Sort
Full-text search on movies, customers, and rentals.

Filter rentals by status: All / Out / Overdue / Returned.

Sortable columns with preserved search state during pagination.

Server-side pagination on all list pages.

Authentication & Authorization
ASP.NET Core Identity with role-based access (Admin and Staff).

Login/logout with secure cookie-based sessions.

Admin-only actions: deleting movies and customers.

Friendly access-denied handling.

Modern UI
Hero landing page with gradient design.

Dashboard with live stat cards and a 7-day rental chart (Chart.js).

Dark mode with persistent user preference.

Responsive layout (mobile-friendly).

Custom icon set (Bootstrap Icons) and typography (Inter).

Searchable dropdowns for customer and movie selection (Tom Select).

Toast-style success/error alerts.

Tech Stack
Layer	Technology
Framework	ASP.NET Core 8 MVC
Language	C# 12
ORM	Entity Framework Core 8
Database	SQL Server LocalDB (dev)
Authentication	ASP.NET Core Identity
Frontend	Razor Views, Bootstrap 5, Chart.js, Tom Select, Bootstrap Icons
Fonts	Inter (Google Fonts)
Architecture
The app follows a layered MVC architecture with clear separation of concerns:

text
Browser  →  Controller  →  Service  →  DbContext  →  Database
              ↓
            View (Razor)
Controllers handle HTTP concerns only — receiving requests, model validation, redirects. They delegate business logic to services.

Services (IRentalService / RentalService) contain the business rules — availability checks, overdue enforcement, rental creation.

DbContext (MoviesDBContext) manages persistence via Entity Framework Core.

ViewModels isolate the view from entity models, allowing per-page shaping of data (DashboardViewModel, PagedResult<T>, LoginViewModel).

Views are strongly typed Razor templates with Tag Helpers for URL generation and form binding.

Getting Started
Prerequisites
.NET 8 SDK

Visual Studio 2022 (Community edition is fine) or VS Code

SQL Server LocalDB (installed with Visual Studio)

Setup
Clone the repository:

bash
git clone https://github.com/yourusername/MovieApp.git
cd MovieApp
Restore dependencies:

bash
dotnet restore
Apply database migrations:

bash
dotnet ef database update
Or from Visual Studio's Package Manager Console:

powershell
Update-Database
Run the app:

bash
dotnet run
Or press F5 in Visual Studio.

Open https://localhost:5001 (or whatever port the console prints).

Default Credentials
The app seeds an admin user on first run:

Email: admin@movieshop.local

Password: Admin123!

⚠️ Change this immediately in any deployed environment. See Data/DbSeeder.cs.

Project Structure
text
MovieApp/
├── Controllers/          HTTP endpoints
│   ├── HomeController.cs        Dashboard and home page
│   ├── MoviesController.cs      DVD inventory CRUD
│   ├── CustomersController.cs   Customer CRUD
│   ├── RentalsController.cs     Rental flow (create, return, overdue)
│   └── AccountController.cs     Login, logout, access denied
│
├── Data/
│   ├── MoviesDBContext.cs       EF Core context (Identity + app entities)
│   └── DbSeeder.cs              Seeds roles and admin user
│
├── Models/               Database entities
│   ├── Movie.cs
│   ├── Customer.cs
│   ├── Rental.cs
│   └── ApplicationUser.cs       Extends IdentityUser
│
├── Services/             Business logic
│   ├── IRentalService.cs
│   └── RentalService.cs         Rental rules, return logic, overdue queries
│
├── ViewModels/           View-specific data shapes
│   ├── DashboardViewModel.cs
│   ├── PagedResult.cs
│   └── LoginViewModel.cs
│
├── Views/                Razor templates
│   ├── Movies/           Index, Details, Create, Edit, Delete
│   ├── Customers/        Same CRUD structure
│   ├── Rentals/          Index, Create, Return, Overdue
│   ├── Account/          Login, AccessDenied
│   └── Shared/           _Layout, _Alert, partials
│
├── wwwroot/              Static assets
│   ├── css/site.css      Design system (CSS variables + components)
│   └── js/site.js
│
├── Migrations/           EF Core migrations
├── Program.cs            App entry point and DI configuration
└── appsettings.json      Connection strings and configuration
Key Business Rules
These rules are enforced server-side in RentalService and RentalsController:

Availability — copies rented out cannot exceed QuantityInStock.

Overdue block — customers with IsReturned = false and DueOn < DateTime.Now cannot rent.

Return integrity — a rental can only be marked as returned once.

Cascade delete guard — customers with active (unreturned) rentals cannot be deleted.

Rental terms — every new rental gets a 7-day due window (configurable).

Roadmap
☑ Phase 1–3 — MVC fundamentals, EF Core, relationships
☑ Phase 4 — Rental workflow, overdue tracking, dashboard
☑ Phase 5 — Search, filter, sort, async
☑ Phase 6 — Pagination, services, ViewModels
☑ Phase 7 — UI overhaul: hero, dark mode, charts
□ Phase 8 — Authentication & role-based authorization ⏳
□ Phase 9 — Email notifications, receipts, reports, fines
□ Phase 10 — Automated testing (xUnit, integration tests)
□ Phase 11 — Deployment (Azure / IIS)
□ Phase 12 — Monitoring, logging, production hardening
What I Learned Building This
This project was built from scratch as a learning exercise in modern ASP.NET Core development. Every phase taught new concepts:

MVC request lifecycle and separation of concerns

Entity Framework Core relationships and migrations

Async/await in web applications

Dependency injection and the service layer pattern

URL state management (search + sort + pagination)

Authentication vs. authorization with ASP.NET Core Identity

Modern UI design with CSS variables, dark mode, and third-party JS libraries

The biggest lesson: architecture decisions made early compound, and refactoring later is painful. Building the service layer and ViewModels before adding UI complexity made the later phases much easier.

Contributing
This is a personal learning project, but feedback and suggestions are welcome. If you spot a bug or have a suggestion, feel free to open an issue.

License
MIT License — free to use, modify, and learn from.

Acknowledgments
Microsoft's official ASP.NET Core documentation

The MvcMovie tutorial — the starting point for this journey

Bootstrap, Chart.js, Tom Select, and Bootstrap Icons — for making the UI feel modern without reinventing wheels

Inter font by Rasmus Andersson

Built with ☕ and a lot of debugging.

