## Architecture:
- Clean Architecture
## Layers:
- **API**: Handles HTTP requests and routes them to the Application layer.
- **Application**: Contains application logic, command and query handlers, and DTO mappings.
- **Domain**: Defines core business logic and domain entities.
- **Infrastructure**: Implements data access, repositories, and external services.
## Patterns: 
- **Repository**: Abstracts data access.
- **CQRS**: Separates read and write operations.
- **DTOs**: Transfers data between layers.
## Libraries:
- **Mapster**: For DTO mapping.
- **MediatR**: For implementing CQRS.
- **Entity Framework**: For ORM and data access.
