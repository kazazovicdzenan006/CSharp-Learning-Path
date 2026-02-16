# SmartCity Enterprise 🏙️

**SmartCity Enterprise** is a sophisticated IoT management system designed for urban infrastructure monitoring. This project represents the evolution of several sub-projects, consolidated into a professional, highly scalable solution.



## 🏗️ Architecture Overview

The project is built on **Clean Architecture** principles, ensuring the system is decoupled, testable, and maintainable. It is organized into a Blank Solution with the following project structure:

* **`Domain`**: The core of the system. Contains Enterprise Entities (Grad, Senzor, Book, etc.) and basic interfaces. 
* **`Service`**: The Application layer. Contains all **Business Logic**, Service implementations, DTOs, and AutoMapper profiles. It acts as a bridge between the Data and UI layers.
* **`Data`**: The Infrastructure layer. Handles data persistence using **EF Core**. Implements **Repository** and **Unit of Work** patterns with a SQL Server backbone.
* **`UI` (Console)**: A legacy console interaction menu (to be decommissioned once API is finalized).
* **`API_UI`**: A modern **ASP.NET Core Web API** providing endpoints for external integration.

---

## 📊 Domain & Data Modeling

The system manages a complex ecosystem of city assets using an advanced database schema:

### **Inheritance & DB Schema**
I used the **Table-Per-Type (TPT)** model in EF Core to achieve high database normalization. 
* **City Infrastructure**: `Grad` connects to `CityNodes` (Base for `CrossRoad` and `ParkingLot`).
* **IoT Ecosystem**: `Uredjaj` (Base for `Senzor` and physical `Controllers`).
* **Library System**: `BookStoreItems` (Base for `Film` and `Knjiga`).

### **Fluent API**
Custom configurations are applied via Fluent API in the `MasterContext` to explicitly define relationships and constraints.

---

## 🛠️ Tech Stack & Implementation Details

* **Advanced OOP**: Heavy use of inheritance, polymorphism, and interfaces to model real-world urban entities.
* **AutoMapper**: Complete DTO (Data Transfer Object) implementation to decouple Domain models from the API response.
* **Unit of Work & Repository**: Abstracted data access to make the entire project unit-testable.
* **Asynchronous Programming**: End-to-end `async/await` implementation for optimized I/O.
* **Dependency Injection**: Native .NET DI for managing service lifecycles.
* **Custom Exceptions**: Domain-specific exception handling for device limits and validation errors.

---

## 🚀 Getting Started

Currently, the project is executed via Visual Studio. Future updates will include containerization.

### **Prerequisites**
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* SQL Server (LocalDB or Express)

### **Installation**
1.  **Clone the repository**:
    ```bash
    git clone [https://github.com/kazazovicdzenan006/CSharp-Learning-Path.git](https://github.com/kazazovicdzenan006/CSharp-Learning-Path.git)
    ```
2.  **Locate the Project**:
    Navigate to the `SmartCity_Enterprise` folder within the cloned repository.
3.  **Open the Solution**:
    Open the `.sln` file found inside the `SmartCity_Enterprise` directory using Visual Studio 2026 (or your preferred IDE).
4.  **Apply Migrations**:
    Ensure your connection string is set in `appsettings.json`, then run the following in the Package Manager Console:
    ```bash
    dotnet ef database update --project Data
    ```
5.  **Run**:
    Set `API_UI` or `UI` as the Startup Project and press `F5`.

> 🐳 **Note**: Docker support is planned for future releases to simplify environment setup.

---

## 🧪 Testing
The project is designed with a "Test-First" mindset. The current architecture supports full unit testing. Logic verified in predecessor projects (`Senzors`, `SmartCity`, `BookStore`) is being systematically integrated into the Enterprise test suite.

---

**Author**: [Dzenan Kazazovic]  
**Status**: In Active Development 🚀