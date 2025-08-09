# Technical Challenge: API Development with .NET 8 and SQLite

This project is a technical challenge that involves extending an API developed in .NET 8 with a SQLite database. The API utilizes a layered architecture pattern consisting of Controller, Service, and Repository (Loader) layers, which promote clear separation of concerns and scalability.

## Challenge Overview

The challenge includes working with two main entities: **Company** and **Employee**, which have a **1:N** relationship. The implemented solution adheres to the best practices of clean architecture and object-oriented programming, providing both mandatory and optional features.

### 1. CRUD Operations

Complete CRUD operations for **Company** and **Employee** entities with the following features:

- **Validation** to prevent duplicate entries for Document fields.
- **Name field length** restricted to a maximum of 100 characters.
- **Soft delete (logical deletion)** for maintaining data integrity.
- A **unique 4-character PIN** is assigned to each employee upon registration.
- Endpoint for **clocking in/out** using an employee's PIN.
- **Detailed reporting** of work hours and overtime.

### 2. Reporting

An endpoint to generate a work hours report with filters for:

- **Start Date** (mandatory)
- **End Date** (mandatory)
- **Document** (optional)

The report includes:

- Date
- Employee Name
- Document
- Daily Punch Count
- Total Hours Worked
- Overtime Hours
- Day of the Week

Overtime is calculated as hours worked beyond **8 hours/day**.

### 3. Layered Architecture Pattern

The project follows the **Layered Architecture Pattern**, separating responsibilities into three layers:

- **Controller**: Handles API requests and responses.
- **Service**: Contains the service layer for processing logic.
- **Loader (Repository)**: Includes the repository for database interactions.

This architecture ensures the application is maintainable, scalable, and easy to extend.

### 4. Development Highlights

**Technology Stack:**

#### Backend:
- **.NET 8**
- **Entity Framework**
- **LINQ**
- **JWT Authentication**
- **RestFUL API**
- **SQLite** (with Entity Framework)
- **Clean architecture and object-oriented design principles**

#### Authentication (JWT):

For secure authentication, the application implements **JWT** (JSON Web Token). This approach ensures that user sessions are stateless, providing scalability and security. The server issues a token upon successful login, which clients include in subsequent requests to access protected resources. The tokens are signed to prevent tampering, ensuring the integrity of the authentication mechanism.

#### Password Hashing:

To enhance password security, the application uses the **PasswordHasher** library for password hashing. This ensures that user passwords are never stored in plain text, but instead as cryptographically hashed values. This library also applies salting and iterative hashing techniques, protecting against common vulnerabilities like brute-force attacks and rainbow table attacks.

### 5. Best Practices

- **Clear and readable code**: The codebase is designed to be easy to understand and maintain.
- **Adherence to software design patterns**: The project follows the **Repository pattern** for data access and separation of concerns.
- **Focus on functionality and reliability**: The application is built to be reliable, with proper validation and reporting features to meet the business requirements.

---

By using this approach, the application is scalable, maintainable, and secure, ensuring it can handle future extensions and evolving business needs effectively.
