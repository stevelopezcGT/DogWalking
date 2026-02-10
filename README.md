# DogWalking – WinForms ERP Sample Application

## Overview

DogWalking is a Windows Forms (WinForms) desktop application built with **C# and .NET Framework 4.8.1**. The application simulates a small ERP-style system for managing clients, their dogs, and recorded dog walking events.

The goal of this project is to demonstrate **senior-level WinForms development practices**, including layered architecture, data persistence, validation, background processing, and testability, rather than to deliver a fully-featured production system.

---

## Key Features Implemented (So Far)

* WinForms desktop application (.NET Framework 4.8.1)
* Layered architecture (UI / Business Layer / Data Layer)
* Entity Framework 6 (Code First)
* Automatic database creation and migrations on startup
* Login flow (demo user seeded via migrations)
* Client, Dog, and Walk domain model
* Data persistence using SQL Server LocalDB
* Repository pattern with a mixed approach (generic base + specific repositories)
* Search functionality (clients and dogs by name)
* Explicit validation in the Business Layer
* Explicit multithreading model for long-running operations (WinForms-safe)
* Designed to be unit-test friendly

---

## Technology Stack

* **Language:** C#
* **Framework:** .NET Framework 4.8.1
* **UI:** Windows Forms (WinForms)
* **ORM:** Entity Framework 6 (Code First)
* **Database:** SQL Server LocalDB
* **Testing (planned):** MSTest + Moq

---

## Project Structure

```
DogWalking.sln
│
├─ DogWalking.WinForms   // UI layer (Forms, user interaction)
├─ DogWalking.BL         // Business Layer (services, validators, DTOs)
├─ DogWalking.DL         // Data Layer (EF, entities, repositories)
└─ DogWalking.Tests      // Unit tests (to be added)
```

### Layer Responsibilities

#### WinForms (UI)

* Displays data and handles user interaction
* Manages UI state (busy / enabled / disabled)
* Orchestrates calls to the Business Layer
* Does **not** contain business rules or EF logic

#### Business Layer (BL)

* Orchestrates use cases
* Applies validation rules
* Contains application logic
* Depends on repository **interfaces**, not EF

#### Data Layer (DL)

* Entity Framework configuration
* Entities (POCOs)
* Repositories (EF implementations)
* Database migrations and seed data

---

## Data Persistence

* Entity Framework 6 is used in **Code First** mode
* SQL Server LocalDB is used for persistence
* Database schema is created and updated automatically on application startup
* A default demo user is seeded via EF migrations

On startup, the application ensures:

* Database exists
* Latest migrations are applied
* Seed data is available

---

## Validation Strategy

* Validation is implemented **explicitly in the Business Layer**
* Each DTO has a corresponding validator
* Validators throw clear, user-friendly exceptions
* The UI is responsible only for displaying validation messages

No external validation frameworks are used in order to keep dependencies minimal and the validation logic explicit.

---

## Multithreading & Performance Model

Long-running and I/O-bound operations (such as database access) are executed **outside the UI thread** to keep the WinForms interface responsive.

The application follows the same multithreading pattern used in real-world WinForms ERP systems:

* Background execution using `Task.Run`
* Explicit marshaling back to the UI thread using `Invoke` / `BeginInvoke`
* Explicit control of UI state (busy flags, button enable/disable, cursor state)
* No implicit or hidden threading behavior

This approach avoids UI freezes and cross-thread exceptions while remaining easy to debug and maintain.

---

## Search Functionality

The application includes a simple and effective search feature:

* Search by **Client Name**
* Search by **Dog Name**
* Filtering is executed at the database level (not in-memory)

This design provides acceptable performance while keeping the UI and business logic simple.

---

## Dependency Injection Approach

No IoC container is used.

Dependencies are injected **manually via constructors**, following a simple and explicit composition model suitable for WinForms applications:

* Forms act as the composition root
* Business services depend on repository interfaces
* Repositories encapsulate EF and data access

This avoids unnecessary complexity while keeping the code testable and maintainable.

---

## Unit Testing Strategy (Planned)

Unit tests are designed to focus on:

* Business Layer validators
* Core service logic
* Edge cases and error scenarios

Repositories are mocked using interfaces, allowing tests to run without a database or Entity Framework.

---

## Assumptions & Trade-offs

* Authentication is intentionally simple and not production-ready
* Passwords are stored in plain text for demo purposes only
* Advanced security, encryption, and role-based access control are out of scope
* The UI focuses on clarity and correctness rather than advanced styling
* The project favors explicit, readable code over heavy abstraction

---

## How to Run the Application

1. Clone the repository
2. Open `DogWalking.sln` in Visual Studio
3. Ensure SQL Server LocalDB is available
4. Build the solution
5. Run the `DogWalking.WinForms` project

The database will be created and migrated automatically on first run.

---

## Next Steps / Improvements

If more time were available, the following improvements would be prioritized:

* Additional unit test coverage
* More advanced search and filtering options
* Improved error handling and logging
* Enhanced UI feedback and usability
* Role-based security model
* Reporting and analytics features

---

## AI Usage Note

AI tools were used as a documentation assistant. All architectural decisions, trade-offs, and final code were intentionally built manually to ensure consistency and correctness.
