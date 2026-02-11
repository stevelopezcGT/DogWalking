# DogWalking – WinForms ERP Sample Application

## Overview

DogWalking is a Windows Forms (WinForms) desktop application built with **C# and .NET Framework 4.8.1**. The application simulates a small ERP-style system for managing clients, their dogs, and recorded dog walking events.

The goal of this project is to demonstrate **senior-level WinForms development practices**, including layered architecture, data persistence, validation, background processing, auditability, soft delete handling, and testability.

---

## Key Features Implemented

- WinForms desktop application (.NET Framework 4.8.1)
- Layered architecture (UI / Business Layer / Data Layer)
- Entity Framework 6 (Code First)
- Automatic database creation and migrations on startup
- Login flow (demo user seeded via migrations)
- Client, Dog, and Walk domain model
- Data persistence using SQL Server LocalDB
- Repository pattern (generic base + specific repositories)
- Search functionality (clients and dogs by name)
- Explicit validation in the Business Layer
- Explicit multithreading model for long-running operations (WinForms-safe)
- Audit fields (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Transparent soft delete implementation (IsActive)
- Designed to be fully unit-testable

---

## Technology Stack

- **Language:** C#
- **Framework:** .NET Framework 4.8.1
- **UI:** Windows Forms (WinForms)
- **ORM:** Entity Framework 6 (Code First)
- **Database:** SQL Server LocalDB
- **Testing:** MSTest + Moq

---

## Project Structure

