## Architecture:
- Clean Architecture
## Layers:
- **API**: Handles HTTP requests and routes them to the Application layer.
- **Application**: Contains application logic, command and query handlers, and DTO mappings.
- **Domain**: Defines core business logic and domain entities.
- **Infrastructure**: Implements data access, repositories, and external services.
## Patterns:
- **CQRS**: For efficient data handling and separation of read/write operations.
- **Repository**: For abstracting data access and promoting code reusability.
## Libraries:
- **Mapster**: For efficient object mapping and DTO conversions.
- **MediatR**: For implementing the CQRS pattern and simplifying command/query handling.
- **Entity** Framework: For object-relational mapping and database interactions.
## Cloud Integration:
- **Azure Identity**: For secure authentication and authorization.
- **Azure KeyVault**: For managing secrets and encryption keys.
- **StackExchange Redis**: For high-performance caching and session storage.
-------------------------------------------------------------------------------

## Data in SQL 
![image](https://github.com/duc-beluga/CleanArchitecture/assets/98554622/360e1432-1160-4394-969d-5206f5a239cb)

## Cached Data in Redis
![image](https://github.com/duc-beluga/CleanArchitecture/assets/98554622/53472584-f616-4da6-9ecb-e5be54338819)

