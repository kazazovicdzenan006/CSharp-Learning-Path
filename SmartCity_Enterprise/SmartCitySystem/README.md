

SmartCity Enterprise 🏙️
SmartCity Enterprise is a robust, highly scalable IoT management system designed to monitor and manage urban infrastructure. Built with Clean Architecture principles, the system tracks everything from traffic density and parking availability to library inventories and environmental sensors.

🏗️ Architecture & Design Patterns
The project is structured into four distinct layers to ensure separation of concerns, testability, and maintainability:

Domain: Contains Enterprise Entities, Interfaces, and Business Logic.

Data: Infrastructure layer handling SQL Server persistence via EF Core, implementing the Repository and Unit of Work patterns.

Service: Application layer containing business services, DTOs, and AutoMapper profiles.

API / UI: The presentation layer. Currently features a functional Console UI for interaction and a modern ASP.NET Core Web API (in development).

🛠️ Tech Stack & Key Features
Advanced OOP: Leverages Inheritance, Encapsulation, Polymorphism, and Interfaces to model complex city entities.

EF Core with TPT: Implements Table-Per-Type (TPT) inheritance for a highly normalized SQL database schema.

Fluent API: Precise database relationship configuration (One-to-Many, Many-to-Many).

Data Transfer Objects (DTOs): Used throughout the API to decouple the domain model from the presentation layer, powered by AutoMapper.

Asynchronous Programming: End-to-end async/await for high-performance I/O operations.

Dependency Injection (DI): Built-in .NET DI container for managing service lifetimes.

Security & Auth: Implemented **JWT (JSON Web Token) Authentication** with **RBAC (Role-Based Access Control)** to secure API endpoints.

Validation: Integrated **FluentValidation** for clean, decoupled, and robust data validation logic.

Global Usings: Utilized **C# Global Usings** to simplify project structure and reduce boilerplate code across all layers.

Refactored Service Layer: Completed a full refactor to move all **AutoMapper** logic from Controllers to the **Service layer**, strictly following the "Thin Controller" principle.

Clean Code & Safety: Implements Custom Exceptions, Delegates, and extensive Data Validation.

📊 Domain Model Overview
The system manages a complex hierarchy of urban assets:

Infrastructure: Grad (City) acts as the root, managing collections of Nodes, Devices, and Inventories.

City Nodes: A base for CrossRoad (Traffic density tracking) and ParkingLot (Capacity and real-time availability).

IoT Devices: A base for Senzor (Air quality/Environment) and PhysicalController (Status and channel management).

Library System: BookStoreItems base for Film (Director, Duration) and Book (Author, Page count).

🚀 Getting Started
Prerequisites
.NET 8 SDK or newer.

SQL Server (Express or Developer).

Installation & Execution
Clone the repository:

Bash
git clone https://github.com/yourusername/SmartCity_Enterprise.git
Database Setup: Update the connection string in the API_UI or UI project's configuration and run:

Bash
dotnet ef database update --project Data
Run the Project:

Set the UI (Console) or API_UI project as the Startup Project.

Press F5 in Visual Studio or use dotnet run.

Note: Docker support for containerized deployment is coming soon!

🧪 Testing
The architecture is fully testable. Logic from predecessor projects (Senzors, SmartCity, BookStore) is currently being integrated into automated unit tests for this Enterprise version.