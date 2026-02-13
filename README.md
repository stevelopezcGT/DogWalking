# DogWalking – WinForms ERP Technical Challenge Submission

## Overview

DogWalking is a Windows Forms (WinForms) desktop ERP-style application built with **C# and .NET Framework 4.8.1**.

The system manages:

- **Clients**
- **Dogs (linked to Clients)**
- **Walks (linked to Dogs and therefore Clients)**
- **Authentication (Login Flow)**

This solution was built as a technical challenge submission with emphasis on:

- Clean architecture
- Explicit separation of concerns
- Testable business logic
- Controlled WinForms multithreading
- Enterprise-aligned data integrity patterns
- Pragmatic maintainability

---

# 1. Assumptions

The following assumptions were made during implementation:

- The application is single-tenant.
- Authentication is simplified for demonstration purposes.
- Concurrency conflicts are out of scope for this challenge.
- Soft delete is preferred over hard delete to align with ERP data safety patterns.
- The system runs locally using SQL Server LocalDB.
- No multi-user real-time synchronization is required.

---

# 2. Out of Scope

To keep the challenge focused and maintainable within the timeframe, the following were intentionally excluded:

- Role-based authorization
- Password hashing / encryption
- Advanced reporting
- Pagination
- Logging framework implementation
- Advanced UI styling
- Multi-environment configuration management

---

# 3. Architecture

The solution follows a strict layered architecture:

```
UI (WinForms)
   ↓
Business Layer (Services + Validators)
   ↓
Data Layer (EF6 + Repositories + Migrations)
```

## 3.1 UI Layer – DogWalking.WinForms

- WinForms interface
- `BaseForm` encapsulates BackgroundWorker execution
- All background work runs via `ExecuteAsync(work, onCompleted)`
- UI interacts only with DTOs
- No repository access from UI
- Centralized error handling (`OnAsyncError`)

## 3.2 Business Layer – DogWalking.BL

- DTOs: ClientDto, DogDto, WalkDto
- Services:
  - AuthService
  - ClientService
  - DogService
  - WalkService
- Validators:
  - LoginValidator
  - ClientValidator
  - DogValidator
  - WalkValidator

Responsibilities:

- Validate input
- Enforce business rules
- Map entities ↔ DTOs
- Prevent illegal state transitions
- Remain fully unit-testable

## 3.3 Data Layer – DogWalking.DL

- Entity Framework 6 (Code First + Migrations)
- Repository pattern (generic base + specific repositories)
- Soft delete (`IsActive`)
- Audit fields:
  - CreatedAt
  - UpdatedAt
  - CreatedBy
  - UpdatedBy

Filtering of inactive records is handled at repository level.

---

# 4. Design Notes (Thought Process & Trade-offs)

## 4.1 BackgroundWorker vs async/await

| Option | Decision | Reason |
|--------|----------|--------|
| async/await | ❌ | Modern but less aligned with legacy WinForms ERP environments |
| Encapsulated BackgroundWorker | ✅ | Predictable, explicit, common in long-lived WinForms systems |

All background operations are wrapped inside BaseForm to avoid repetition and cross-thread exceptions.

---

## 4.2 DTO Boundary vs Exposing Entities

| Option | Decision | Reason |
|--------|----------|--------|
| Return EF entities to UI | ❌ | Creates tight coupling |
| Use DTO boundary | ✅ | Clean separation and improved testability |

The UI never references EF entities.

---

## 4.3 Soft Delete vs Hard Delete

| Option | Decision | Reason |
|--------|----------|--------|
| Hard delete | ❌ | Risk of accidental data loss |
| Soft delete | ✅ | Enterprise-aligned and safer |

Records are marked inactive rather than physically deleted.

---

## 4.4 Deletion Integrity Rules

To prevent orphaned data:

- A Client cannot be deleted if active Dogs exist.
- A Dog cannot be deleted if active Walks exist.

These rules are enforced in the Business Layer (not UI and not EF cascade rules).

This ensures predictable ERP-style behavior.

---

## 4.5 ServiceFactory vs IoC Container

| Option | Decision | Reason |
|--------|----------|--------|
| Full IoC container | ❌ | Unnecessary complexity for this scope |
| Manual factory | ✅ | Explicit, readable, predictable |

Short-lived DbContext instances are created inside ServiceFactory.

---

# 5. WinForms Best Practices Applied

- No heavy logic inside event handlers
- Centralized background execution
- UI state managed explicitly (buttons disabled during async)
- Clear error messaging
- Cascading selection pattern (Client → Dog in Walk form)
- DateTimePicker configured for US format (MM/dd/yyyy hh:mm tt)

---

# 6. Validation & Error Handling

- Explicit validators per DTO
- Exceptions thrown at BL level
- UI surfaces messages via centralized handler
- Edge cases handled:
  - Null inputs
  - Empty strings
  - Invalid duration
  - Attempting illegal deletions

---

# 7. Unit Testing

Testing framework:

- MSTest
- Moq (Strict behavior)

Covered:

- Service methods (Add, Update, Delete, Search, GetById)
- Deletion constraint behavior
- Validator rules
- Edge cases

The Business Layer is fully testable without EF dependency.

---

# 8. Production Mindset Considerations

Although logging was not implemented to maintain challenge scope, a production version would include:

- Centralized logging (e.g., Serilog)
- Password hashing
- Role-based authorization
- Environment configuration separation

No sensitive information is committed.

---

# 9. AI Usage Disclosure

AI was used as:

- Documentation assistant
- Architectural review helper
- Edge case reviewer

All implementation, architecture, and final code structure were designed and written manually.

No scaffolding or auto-generated architecture tools were used.

---

# 10. How to Build & Run

### Prerequisites

- Visual Studio
- SQL Server LocalDB

### Build

```powershell
dotnet build DogWalking/DogWalking.sln -c Debug
```

### Run

Run via Visual Studio or:

```powershell
DogWalking/DogWalking.WinForms/bin/Debug/DogWalking.WinForms.exe
```

---

# 11. Run Tests

```powershell
dotnet build DogWalking/DogWalking.sln -c Debug
dotnet vstest DogWalking/DogWalking.Tests/bin/Debug/DogWalking.Tests.dll
```

---

# 12. If I Had More Time

If additional time were available, the following improvements would be prioritized:

- Password hashing implementation
- Logging framework integration
- Role-based authorization
- Pagination and advanced filtering
- Integration tests
- Concurrency handling improvements
- Improved UI polish

---

# 13. Final Notes

This submission prioritizes:

- Clarity over cleverness
- Predictability over over-engineering
- Separation of concerns
- Enterprise-aligned WinForms patterns
- Testable business logic

The goal was not maximum feature density, but architectural discipline and maintainable design consistent with real-world ERP desktop systems.
