# DogWalking – WinForms ERP Sample Application

## Overview

DogWalking is a desktop ERP-style application built with **C# and .NET Framework 4.8.1**.

The system simulates a small business workflow to manage:

- Clients  
- Dogs  
- Dog walks  
- Authentication and login  

The solution follows a clean layered architecture:

```
UI (WinForms)
   ↓
Business Layer (Services + Validation)
   ↓
Data Layer (EF6 + Repositories + Migrations)
```

This project was developed as a code-challenge style solution focused on:

- Explicit layering  
- Clean separation of concerns  
- Testable business logic  
- Controlled multithreading  
- Pragmatic maintainability  

---

## Architecture

### UI Layer – `DogWalking.WinForms`

- Windows Forms (WinForms)
- `BaseForm` encapsulating BackgroundWorker execution
- Explicit service usage through `ServiceFactory`
- No direct repository usage in UI
- DTO-only interaction (entities never exposed to UI)
- Controlled async execution via `ExecuteAsync`
- ERP-style cascading selection (Client → Dog) in Walk module

### Business Layer – `DogWalking.BL`

- DTOs (ClientDto, DogDto, WalkDto, etc.)
- Services:
  - `AuthService`
  - `ClientService`
  - `DogService`
  - `WalkService`
- Explicit validators:
  - `LoginValidator`
  - `ClientValidator`
  - `DogValidator`
  - `WalkValidator`

Services:

- Validate input  
- Orchestrate repository calls  
- Map entities ↔ DTOs  
- Do not depend directly on EF  
- Are fully unit-testable  

### Data Layer – `DogWalking.DL`

- Entity Framework 6 (Code First)
- `DogWalkingContext`
- Repository pattern (generic base + specific repositories)
- Soft delete support (`IsActive`)
- Audit fields (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Automatic audit population using application session
- EF migrations with seed configuration

---

## Current Technical Features

- Repository-based data access
- Soft delete implemented at entity level
- Automatic audit tracking (CreatedBy / UpdatedBy)
- Shared application session for current user tracking
- Login flow (`admin/admin` seeded via migrations)
- Clients, Dogs, and Walks fully integrated in UI
- Cascading dropdown logic in Walk module (Client → Dog)
- Date + time selection for walks (US format: MM/dd/yyyy hh:mm tt)
- DTO boundary enforced between layers
- Service and validator unit test coverage
- BackgroundWorker-based async execution pattern
- XML documentation standardized across BL/DL/tests

---

## Multithreading Strategy

Instead of using `async/await`, the UI uses a **BackgroundWorker encapsulated inside BaseForm**.

### Trade-off

| Option | Chosen | Reason |
|--------|--------|--------|
| async/await (Task-based) | ❌ | Modern but less aligned with legacy ERP WinForms environments |
| BackgroundWorker encapsulated | ✅ | Explicit, predictable, common in long-lived WinForms ERP systems |

All async operations are executed through:

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

## Repository Pattern & DTO Boundary

### Trade-off: Expose Entities vs Use DTOs

| Approach | Chosen | Reason |
|----------|--------|--------|
| Return EF entities to UI | ❌ | Couples UI to data layer |
| Return DTOs from services | ✅ | Clean separation of concerns |

All services:

- Accept DTOs  
- Return DTOs  
- Internally map to entities  

The UI never references EF entities.

---

## Soft Delete & Audit Strategy

Entities include:

- `IsActive`
- `CreatedAt`
- `UpdatedAt`
- `CreatedBy`
- `UpdatedBy`

Instead of physically deleting records, repositories perform soft delete.

### Trade-off

| Approach | Chosen | Reason |
|----------|--------|--------|
| Hard delete | ❌ | Data loss risk |
| Soft delete | ✅ | Safer, enterprise-aligned pattern |

Filtering is handled at repository level.

Audit fields are automatically populated using the shared `AppSession` context, without introducing UI dependencies into the data layer.

---

## ServiceFactory vs IoC Container

The application does **not** use a dependency injection container.

### Trade-off

| Approach | Chosen | Reason |
|----------|--------|--------|
| Full IoC container | ❌ | Adds unnecessary complexity for a WinForms challenge |
| Manual factory composition | ✅ | Explicit, readable, test-friendly |

The `ServiceFactory` ensures:

- Short-lived `DbContext`
- No context lifetime leakage
- Clean separation
- Predictable instantiation

---

## Unit Testing Strategy

- MSTest
- Moq (Strict behavior)
- Service-level behavior testing
- Validator testing
- No EF dependency in tests

Covered areas:

- Add / Update / Delete
- GetAll / Search / GetById
- Validation failures
- Edge cases
- Repository interaction verification

The Business Layer is fully testable without infrastructure dependencies.

---

## Database and Migrations

Database initialization:

```csharp
MigrateDatabaseToLatestVersion<DogWalkingContext, Configuration>
```

Seed includes:

- Username: `admin`
- Password: `admin`

LocalDB is used for reproducibility and ease of setup.

---

## Architecture Summary for Reviewers

This solution intentionally demonstrates:

- Clear UI → BL → DL separation
- DTO boundary enforcement
- Explicit validation per use case
- Controlled background execution model for WinForms
- Repository abstraction over EF6
- Soft delete and audit-ready entities
- ERP-style cascading dropdown behavior
- Service-level unit testing without database dependency

Design priorities were:

1. Predictability over over-engineering  
2. Explicitness over hidden magic  
3. Testability over convenience  
4. Enterprise-aligned WinForms patterns  

The code avoids unnecessary frameworks and focuses on clarity, maintainability, and correctness.

---

## Development Philosophy

This project was implemented **manually**, following:

- Clean layering
- Explicit validation
- Controlled multithreading
- DTO boundary enforcement
- ERP-style UI state management
- Test-driven behavior validation

No scaffolding generators or auto-architecture tools were used.

Patterns applied reflect modern best practices adapted to a WinForms ERP context.

---

## Use of AI

AI tools were used **only as documentation and review assistants** to:

- Refine documentation clarity  
- Review architectural consistency  
- Identify potential edge cases  
- Improve explanatory structure  

All implementation, architectural decisions, and code structure were designed and written manually.

---

## Build and Run

From solution root:

```powershell
dotnet build DogWalking/DogWalking.sln -c Debug
```

Run via Visual Studio (recommended for .NET Framework WinForms), or:

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

## Known Limitations

- Authentication is basic (no hashing for demo purposes)
- No role-based authorization
- No advanced reporting
- No advanced filtering or pagination
- UI styling intentionally minimal

The focus of this project is architectural clarity, correctness, and maintainability.
