# Enterprise Employee Management System (Master-Details CRUD)

A robust, enterprise-level web application built using **ASP.NET Core MVC** following the **Code-First Approach**. This project demonstrates a complete implementation of complex **Master-Details CRUD operations** using modern architectural patterns, client-side enhancements with jQuery AJAX, and secure role-based access control.

---

## 🚀 Features & Implementation Details

### 1. Core Architecture & Database (.NET Core)
*   **Code-First Approach:** Database schema generated entirely from C# POCO classes using Entity Framework Core Migrations.
*   **Relational Data:** Implements strong one-to-many ($1:N$) relationships (e.g., An **Employee** master record linked with multiple **Academic Details** rows).
*   **Data Annotations:** Applied rigorously on model classes for database constraints, strong type validation, error messages, and UI display formatting.
*   **Diverse Data Types:** Robust handling of multiple data types including Text, Numbers, Booleans, Dates, and Image.

### 2. Advanced UI/UX Components
*   **View Models:** Clean separation of concerns using specific View Models (`EmployeeViewModel`) for structured, type-safe data transfer between Views and Controllers.
*   **Partial Views:** Reusable UI blocks utilized within the Master-Details interface to dynamically render and handle child table rows.
*   **View Components:** Native ASP.NET Core View Components built for rendering modular, dynamic, and isolated UI parts (like dynamic navigation or summary stats panels).
*   **jQuery AJAX:** Fully asynchronous client-side operations for seamless sub-form manipulation (adding, removing, or validation of detail rows without full-page reloads).

### 3. Security & Routing
*   **Authentication & Authorization:** Secure user login session management with precise Role-Based Access Control (`[Authorize(Roles = "Admin, User")]`).
*   **Custom Routing:** Configured user-friendly and clean URL patterns customized in the .NET Core middleware pipeline (`Program.cs`).

---

## 🛠️ Technology Stack

*   **Backend:** ASP.NET Core MVC Entity Framework Core
*   **Frontend:** HTML5, CSS3, Bootstrap, jQuery, jQuery Validation & Unobtrusive AJAX
*   **Database:** Microsoft SQL Server

---

## 📂 Project Structure

The logical directory structure and architectural separation of the application are detailed below:

├── Models/                  # Core Database Entities (Employee, AcademicDetail)
├── ViewModels/              # Specialized Data Transfer Objects (DTOs) for Views
├── Controllers/             # MVC Controllers handling Business Logic
├── ViewComponents/          # Dynamic .NET Core View Components
├── Views/
│   ├── Employee/           # Master-Details CRUD Views (Create, Edit,Delete)
│   └── Shared/
│       ├── Components/     # View Component Default Razor Views
│       └── _AcademicTable.cshtml  # Partial View for dynamic AJAX rows
└── wwwroot/
    ├── images/              # Dynamic uploaded profile photos
    └── js/                  # Custom AJAX scripts for Master-Details manipulation
