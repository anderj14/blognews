# API Application

### Execute the API
Go to the folder API in the terminal and write: dotnet run or dotnet watch --no-hot-reload

## Description

This application is an API that manages articles, comments, categories, and statuses. It allows CRUD operations for these entities, as well as mapping configurations between DTOs and entities using AutoMapper. It also uses a generic repository to handle common data access operations.

## Features

- **Articles**: Create, read, update, and delete articles.
- **Comments**: Create, read, update, and delete comments associated with articles.
- **Categories**: Create, read, update, and delete categories for articles.
- **Statuses**: Create, read, update, and delete article statuses.

## Project Structure

- **Controllers**: Contains the controllers that handle HTTP requests.
- **DTOs**: Data transfer objects used for requests and responses.
- **Entities**: Classes that represent database entities.
- **Mappings**: AutoMapper configuration for mapping between DTOs and entities.
- **Specifications**: Classes for defining queries with specifications and filters.
- **Helper**: Utility classes and extensions.
- **Interfaces**: Interfaces for defining service and repository contracts.
- **Repositories**: Implementation of the generic repository pattern to handle common CRUD operations.

## Generic Repository

The application uses a generic repository to provide a reusable data access layer and simplify code in controllers. The generic repository allows for common CRUD operations without the need to repeatedly implement the same code for each entity.

### Example of Generic Repository

```csharp
public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
