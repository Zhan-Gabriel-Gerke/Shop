# ShopTARgv24 - ASP.NET Core Learning Project 🚀

> **Status:** 🚧 Work in Progress (Active Development)

This repository serves as a comprehensive learning project for mastering **ASP.NET Core MVC** and enterprise-level application architecture.

The goal is to build a scalable web application with separation of concerns, database integration, and consumption of external REST APIs.

### 🏗️ Architecture
The solution follows the **N-Tier Architecture** principles to ensure modularity and testability:

* **📂 ShopTARgv24 (Web):** The presentation layer (MVC Controllers, Views, ViewModels).
* **📂 ShopTARgv24.Core:** The domain layer containing Entities, DTOs, and Service Interfaces.
* **📂 ShopTARgv24.Data:** Infrastructure layer for Database Context and EF Core Migrations.
* **📂 ShopTARgv24.ApplicationServices:** Business logic implementation.
* **🧪 Tests:** Unit and Integration tests (`.RealEstate`, `.Spaceships`).

### ✨ Key Features & Modules

**1. Core Functionality (CRUD)**
* **Real Estate Module:** Full management of property listings (Create, Read, Update, Delete).
* **Spaceships Module:** A creative module to practice complex data models and image handling.
* **File Handling:** Uploading, storing, and retrieving images/files associated with entities.

**2. External API Integrations**
* **⛅ Weather API:** Fetches real-time weather data (AccuWeather integration).
* **🍹 Cocktail API:** Search and display cocktail recipes.
* **🤠 Chuck Norris API:** Fetches random jokes to demonstrate simple API consumption.

**3. Database & ORM**
* **SQL Server** via **Entity Framework Core**.
* **Code-First Approach** with automated migrations.

### 🛠️ Tech Stack
* **Framework:** .NET 8 / .NET 9 (ASP.NET Core MVC)
* **Language:** C#
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core
* **Testing:** xUnit, Moq
* **Frontend:** Razor Views, Bootstrap 5, jQuery

---
*🎓 This project is part of my coursework at Tallinn Industrial Education Center and is regularly updated with new features and refactoring.*
