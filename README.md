# DogWalking – WinForms ERP Sample Application

## Overview

DogWalking is a Windows Forms (WinForms) desktop application built with **C# and .NET Framework 4.8.1**.

It implements a small “dog walking business” workflow to manage:

- **Clients** (name, phone)
- **Dogs** (name, breed, age, linked to a client)
- **Walks** (scheduled **date + time**, duration, linked to a dog and therefore to a client)
- **Authentication / login**

The solution follows a clean layered architecture:

```
UI (WinForms)
   ↓
Business Layer (Services + Validation)
   ↓
Data Layer (EF6 + Repositories + Migrations)
```

This repository was built as a **code-challenge** submission with emphasis on:

- Explicit layering and separation of concerns
- Testable business logic (services + validators)
- Controlled multithreading for WinForms responsiveness
- Pragmatic maintainability aligned with long-lived ERP systems

---

## Challenge Requirements Coverage

### 1) Form Design

The UI implements the required fields across the domain forms:

- **Client**: name, phone
- **Dog**: name, breed, age
- **Walk**: walk date (**date + time**), duration, and selection of the related dog (and client through cascading selection)

Buttons implemented follow the challenge intent:

- **Save** (Create/Update) on edit forms
- **Delete** on list forms (**soft delete**)
- **Exit/Close** to close the current form

> Note: “Clear” is handled via form-level reset patterns (new record flow / reloading) rather than an explicit “Clear” button on every screen. Validation and error handling are explicit and user-facing.

### 2) Functionality

- **Validation** is implemented in the Business Layer via dedicated validators per DTO.
- **Save** persists to the database and refreshes the list view on completion.
- **Delete** removes the item from active listings using **soft delete** (`IsActive = false`).
- **Exit/Close** closes the form.
- **Error handling** is centralized in `BaseForm.OnAsyncError(...)`, displaying messages in the UI (`lblMessage`).

### 3) Data Persistence

- Persistence is implemented using **SQL Server LocalDB** with **Entity Framework 6 (Code First + Migrations)**.
- The database is created and migrated automatically on application startup.
- Data remains available after closing and reopening the application.

### 4) Useful Features

- **List screens** for Clients / Dogs / Walks provide retrieval of saved entries.
- **Search** is implemented on list screens.
- **Login flow** is implemented (seeded demo user).
- **Dog walk creation** supports a realistic ERP flow, including **cascading selection** (Client → Dog).

### 5) Pre-requirements

- Visual Studio (recommended for .NET Framework WinForms)
- SQL Server LocalDB
- Git (GitHub repository)

### 6) Delivery Expectations

- “Commit early, commit often” was followed during development.
- Unit tests are included (MSTest + Moq).
- Documentation (this README + inline XML docs) describes architecture and trade-offs.
- AI usage is explicitly disclosed (see below).

---

## How to Review This Project

For technical reviewers, the most relevant areas to inspect are:

- **`BaseForm`**: BackgroundWorker encapsulation (`ExecuteAsync(work, onCompleted)`) and error handling
- **`ServiceFactory`**: manual composition and short-lived `DbContext` boundaries
- **Walk module UX**: **Client → Dog cascading selection** and state/event sequencing
- **`RepositoryBase`**: soft delete (`IsActive`) filtering and audit field handling
- **Deletion business rules**: BL-level integrity checks (Client↔Dogs, Dog↔Walks)
- **Unit tests**: service behavior + validator tests using strict mocks

These areas reflect the core architectural decisions and trade-offs described in this document.

---

## Why .NET Framework 4.8.1 + EF6?

The stack was intentionally chosen to reflect real-world **legacy ERP environments** where **WinForms + EF6** are still widely used and maintained.

The goal of this challenge is to demonstrate clean architecture and maintainability within a realistic enterprise desktop context, rather than modern web-first tooling.

---

## Architecture

### UI Layer – `DogWalking.WinForms`

- WinForms UI
- `BaseForm` encapsulating BackgroundWorker execution
- Explicit service usage through `ServiceFactory`
- No direct repository usage in UI
- DTO-only interaction (entities are never exposed to UI)
- Controlled background execution via `ExecuteAsync`
- ERP-style cascading selection in Walk module (Client → Dog)

### Business Layer – `DogWalking.BL`

- DTOs (`ClientDto`, `DogDto`, `WalkDto`, etc.)
- Services:
  - `AuthService`
  - `ClientService`
  - `DogService`
  - `WalkService`
- Validators:
  - `LoginValidator`
  - `ClientValidator`
  - `DogValidator`
  - `WalkValidator`

Services:

- Validate input
- Orchestrate repository calls
- Map entities ↔ DTOs
- Do not depend directly on EF
- Are unit-testable without database dependency
- Enforce business rules (including safe deletion constraints)

### Data Layer – `DogWalking.DL`

- EF6 Code First + Migrations
- `DogWalkingContext`
- Repository pattern (generic base + specific repositories)
- Soft delete via `IsActive`
- Audit fields (`CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
- Automatic audit population via application session

### Shared – `DogWalking.Common`

- `AppSession` used to capture the authenticated username
- Enables audit fields to store the real current user without creating a dependency from DL → UI

---

## Current Technical Features

- Repository-based data access (EF6)
- Soft delete implemented consistently (`IsActive` filtered at repository base)
- Audit fields automatically managed (Created/Updated timestamps and user)
- Login flow (`admin/admin` seeded via migrations)
- Clients / Dogs / Walks modules integrated end-to-end (UI → BL → DL)
- Walk scheduling supports **date + time** selection (US format: `MM/dd/yyyy hh:mm tt`)
- Walk creation supports ERP cascading selection **Client → Dog**
- Safe deletion rules enforced in BL:
  - A **Client cannot be deleted** if it has **active Dogs**
  - A **Dog cannot be deleted** if it has **active Walks**
- DTO boundary enforced between layers
- Unit tests for services and validators (MSTest + Moq, strict behavior)
- XML documentation standardized across BL/DL/tests

---

## Multithreading Strategy

Instead of `async/await`, the UI uses a **BackgroundWorker encapsulated inside BaseForm** to keep the interface responsive.

### Trade-off

| Option | Chosen | Reason |
|--------|--------|--------|
| async/await (Task-based) | ❌ | Modern but less aligned with legacy ERP WinForms environments |
| BackgroundWorker encapsulated | ✅ | Explicit, predictable, and common in long-lived WinForms ERP systems |

All background work is executed through:

```csharp
ExecuteAsync(work, onCompleted);
```

No UI logic runs inside background threads.

---

## Cascading ERP Pattern (Client → Dog)

The Walk module implements a dependent dropdown model:

1. User selects a Client
2. Dogs are dynamically filtered and loaded
3. Dog selection becomes available
4. Walk is created for the selected Dog

This demonstrates:

- State-aware UI handling
- Dependent dataset loading
- Controlled event sequencing
- ERP-style master-detail behavior

The implementation preserves:

- Clean layering
- DTO boundary
- ExecuteAsync background execution
- No repository leakage into UI

---

## Safe Deletion Business Rules (ERP Integrity)

In ERP-style systems, parent/child lifecycles must remain consistent to avoid “orphan” records.

This solution enforces deletion constraints in the **Business Layer** (not in UI and not in EF mappings):

- **Clients** can only be deleted if they have **no active Dogs**
- **Dogs** can only be deleted if they have **no active Walks**

### Trade-off

| Approach | Chosen | Reason |
|----------|--------|--------|
| Cascade soft delete | ❌ | Risk of hiding too much data and increasing side effects |
| Block deletion if children exist | ✅ | Predictable ERP behavior and preserves integrity |

When a deletion is blocked, the BL throws a clear exception and the UI displays the message using the existing error handling flow.

---

## Repository Pattern & DTO Boundary

### Trade-off: Expose Entities vs Use DTOs

| Approach | Chosen | Reason |
|----------|--------|--------|
| Return EF entities to UI | ❌ | Couples UI to data layer and increases fragility |
| Return DTOs from services | ✅ | Maintains clean separation and improves testability |

All services accept and return DTOs, mapping internally to entities. The UI never references EF entities.

---

## Soft Delete & Audit Strategy

Entities include:

- `IsActive`
- `CreatedAt`, `UpdatedAt`
- `CreatedBy`, `UpdatedBy`

Instead of physically deleting records, repositories perform soft delete.

### Trade-off

| Approach | Chosen | Reason |
|----------|--------|--------|
| Hard delete | ❌ | Data loss risk |
| Soft delete | ✅ | Safer, enterprise-aligned pattern |

Filtering is handled at repository level so inactive rows do not show in queries.

Audit fields are automatically populated using the shared `AppSession` context.

---

## ServiceFactory vs IoC Container

This solution does **not** use a dependency injection container.

### Trade-off

| Approach | Chosen | Reason |
|----------|--------|--------|
| Full IoC container | ❌ | Adds complexity that is unnecessary for this scope |
| Manual factory composition | ✅ | Explicit, readable, and test-friendly for WinForms |

`ServiceFactory` ensures short-lived `DbContext` usage and predictable composition.

---

## Unit Testing Strategy

- MSTest
- Moq (Strict behavior)
- Service-level behavior testing
- Validator testing
- No EF dependency in tests

Covered areas include:

- Add / Update / Delete (including deletion constraints)
- GetAll / Search / GetById
- Validation failures and success cases
- Edge cases (null/empty/whitespace)
- Repository interaction verification

---

## Database and Migrations

Database initialization uses:

```csharp
MigrateDatabaseToLatestVersion<DogWalkingContext, Configuration>
```

Seed includes a demo user:

- Username: `admin`
- Password: `admin`

LocalDB is used for reproducibility and ease of setup.

---

## Build and Run

### Prerequisites

- Visual Studio (recommended for .NET Framework WinForms)
- SQL Server LocalDB installed and available

### Build

From solution root:

```powershell
dotnet build DogWalking/DogWalking.sln -c Debug
```

### Run

Run via Visual Studio, or execute the built binary:

```powershell
DogWalking/DogWalking.WinForms/bin/Debug/DogWalking.WinForms.exe
```

---

## Run Tests

```powershell
dotnet build DogWalking/DogWalking.sln -c Debug
dotnet vstest DogWalking/DogWalking.Tests/bin/Debug/DogWalking.Tests.dll
```

All service and validator tests pass locally.

---

## Development Philosophy

This project was implemented **manually**, applying:

- Clean layering
- Explicit validation
- Controlled multithreading
- DTO boundary enforcement
- ERP-style UI state management
- Unit-testable BL behavior

No scaffolding generators or auto-architecture tools were used. Patterns applied reflect modern best practices adapted to WinForms ERP constraints.

---

## Use of AI

AI tools were used as a **documentation and architectural review assistant only**.

- No code generators or scaffolding tools were used.
- All implementation, structure, layering decisions, and trade-offs were **manually designed and implemented**.

---

## Known Limitations

- Authentication is basic (no password hashing; demo only)
- No role-based authorization
- No advanced reporting
- No advanced filtering/pagination
- UI styling intentionally minimal

This project prioritizes clarity, correctness, and architectural discipline over feature density, reflecting how long-lived WinForms ERP systems are typically designed and maintained.
