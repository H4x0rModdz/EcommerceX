# Innovative Ecommerce Project with C# .NET 8 API and Dockerized Containers

Welcome to the README for our cutting-edge Ecommerce project! This project showcases a fully functional Ecommerce application developed using C# .NET 8. It features a powerful API, Docker containerization, xUnit testing, ASP.NET WebApp integration, JWT-based authentication, SQL Server for the database, and a comprehensive tutorial to get you started. 

Keep an eye on this README as I work on the project. Please stay tuned for updates on new features and enhancements that will be released.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Authentication](#authentication)
- [Database](#database)
- [Testing](#testing)
- [WebApp Integration](#webapp-integration)
- [Project Dependencies](#project-dependencies)
- [Project Design](#project-design)
- [To-Do List](#to-do-list)
- [License](#license)

## Introduction

Our Ecommerce project aims to revolutionize online shopping by providing a robust, scalable, and feature-rich platform built using C# .NET 8. The project is divided into modular Docker containers for easy deployment and maintenance.

## Features

- **Powerful API:** The core of the project is its API, allowing seamless interaction between clients and the Ecommerce platform.
- **Docker Containers:** We embrace containerization to ensure consistent environments and simple scalability.
- **xUnit Testing:** Robust testing with xUnit guarantees the reliability of our application.
- **ASP.NET WebApp:** Integration of the WebApp enriches the user interface and experience.
- **JWT Authentication:** Secure your application with JSON Web Tokens for seamless and safe authentication.
- **SQL Server:** We use SQL Server to handle the database, ensuring data integrity and reliability.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Installation

1. Clone this repository.
2. Navigate to the project directory.
3. Build the Docker containers: `docker-compose build`.
4. Run the containers: `docker-compose up`.

or

1. Just press F5 button

### Configuration

- API configuration: `appsettings.json`
- Database connection: Update connection string in `appsettings.json`
- JWT configuration: `appsettings.json`

## Authentication

Our project utilizes JWT-based authentication for enhanced security. To obtain a token, register on our API using the `auth/register` endpoint. After that, perform a login using the `auth/login` endpoint, and your token will be successfully generated. Use this token with the lock symbol labeled 'Authorize' as described in the API example. This will grant you access and authorization.

## Database

We utilize SQL Server for the database. The migrations are automatically applied during container startup.

## Testing

Our codebase is thoroughly tested using xUnit. Run tests with the following command:

```bash
dotnet test
```

## WebApp Integration

The ASP.NET WebApp provides a user-friendly interface for browsing and purchasing products. It interacts seamlessly with the API.

## Project Dependencies

1. Entity Framework Core
2. JWT Token Auth
3. Swagger
4. xUnit
5. ASP.NET Core
6. SQL Server

## Project Design

Our project follows a clean and modular design, separating concerns for scalability and maintainability.

## To-Do List

Here are some exciting features we plan to implement in the future:

1. **MySQL Implementation**: *Support for MySQL database for enhanced flexibility.*
2. **SQLite Integration**: *Integrate SQLite for lightweight local development and testing.*
3. **Web Pages Development**: *Create dynamic and responsive web pages for an immersive user experience.*
4. **Test Suite Expansion**: *Enhance test coverage and reliability.*
5. **Soon...**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
