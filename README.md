# Store API

This project is a RESTful API for managing a store, including categories, orders, products, and users.

## Project Features

- **REST Architecture**: The project is based on REST architecture principles.
- **Dependency Injection (DI)**: We used DI to enable layer separation and improve code maintainability.
- **Async/Await**: For scalability, we used async/await for asynchronous operations.
- **DB First**: The project utilizes a SQL database with a DB First approach.
- **Configuration Files**: The project uses configuration files for managing various settings.
- **Error Handling**: All errors are handled using an error middleware. Errors are written to a logger, and fatal errors are sent via email.
- **Request Logging**: Every request to the system is logged for rating and analysis purposes.
- **Clean Code**: The code is written following Clean Code principles.

## Swagger Link

(https://localhost:7254/swagger/index.html)

## Project Structure

The project is divided into the following layers:

### DTO (Data Transfer Objects) Layer

This layer contains the objects used to transfer data between the API and the various services. All records are represented in DTOs.

### Services Layer

This layer contains the business logic of the project. Services interact with the DTO layer and the Data Access Layer.

### Data Access Layer

This layer is responsible for accessing data from the database. The layer uses a DB First approach.

### Controllers Layer

This layer contains the API controllers, which handle HTTP requests and return responses to the client.

### AutoMapper

Conversion between layers is done using AutoMapper, which enables automatic mapping between objects.

### Dependency Injection

Layers communicate with each other using Dependency Injection, allowing for separation of concerns and making the code more maintainable and scalable.

### Error Handling

All errors are handled using an error middleware. Errors are written to a logger, and fatal errors are sent via email.

### Request Logging

Every request to the system is logged for rating and analysis purposes.

## Using DB First with SQL

The project uses a SQL database with a DB First approach. To create the database, use the following commands:

```sh
dotnet ef dbcontext scaffold "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

## Configuration Files

The project uses configuration files for managing various settings.

## Endpoints

### Categories
- `GET /api/Categories`
  - Retrieves a list of categories.

### Orders
- `POST /api/Orders`
  - Creates a new order.
  - Request Body: `OrderDTO`
  - Responses: `GetOrderDTO`
  
- `GET /api/Orders/{id}`
  - Retrieves an order by ID.
  - Path Parameter: `id` (integer)
  - Responses: `OrderDTO`

### Products
- `GET /api/Products`
  - Retrieves a list of products with optional filtering by description, minimum price, maximum price, and category IDs.
  - Query Parameters:
    - `desc` (string)
    - `minPrice` (integer)
    - `maxPrice` (integer)
    - `categoryIds` (array of integers)

### Users
- `GET /api/Users`
  - Retrieves a list of usernames.

- `POST /api/Users`
  - Creates a new user.
  - Request Body: `UserDTO`
  - Responses: `User`
  
- `GET /api/Users/{id}`
  - Retrieves a user by ID.
  - Path Parameter: `id` (integer)
  - Responses: `User`

- `PUT /api/Users/{id}`
  - Updates a user by ID.
  - Path Parameter: `id` (integer)
  - Request Body: `UserDTO`
  - Responses: `GetUserDTO`

- `POST /api/Users/login`
  - Logs in a user.
  - Query Parameters:
    - `username` (string)
    - `password` (string)
  - Responses: `GetUserDTO`

- `POST /api/Users/CheckPassword`
  - Checks a user's password.
  - Request Body: string
  - Responses: integer


