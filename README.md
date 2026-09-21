<img width="1892" height="902" alt="image" src="https://github.com/user-attachments/assets/3aa5d0c8-f5cf-40fd-af6d-b4edf899c1e6" />
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

Dashboard with live stat cards

Dark mode with persistent user preference.

Responsive layout (mobile-friendly).

Custom icon set (Bootstrap Icons) and typography (Inter).

Searchable dropdowns for customer and movie selection (Tom Select).

Toast-style success/error alerts.

##Tech Stack##
Layer	Technology
Framework	ASP.NET Core 8 MVC
Language	C# 12
ORM	Entity Framework Core 8
Database	SQL Server LocalDB (dev)
Authentication	ASP.NET Core Identity
Frontend	Razor Views, Bootstrap 5, Chart.js, Tom Select, Bootstrap Icons
Fonts	Inter (Google Fonts)

Default Credentials
The app seeds an admin user on first run:

Email: admin@movieshop.local

Password: Admin123!

⚠️ Change this immediately in any deployed environment. See Data/DbSeeder.cs.


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

