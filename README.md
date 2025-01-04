# .NET API Test

This repository contains a .NET solution with four projects demonstrating CRUD operations:

1. **WebApi**: Serves as the backend API providing data services.
2. **WebRazor**: A Razor Pages frontend consuming the WebApi.
3. **WebBlazor**: A Blazor Server frontend consuming the WebApi.
4. **WebAsm**: A Blazor WebAssembly frontend consuming the WebApi.

## Getting Started
## Database Schema
### Database Name: Opus
### Ordertbl

| Column Name   | Data Type | Description        |
|---------------|-----------|--------------------|
| OrderId       | INT       | Unique identifier for each order |
| ProductId     | INT       | Foreign key referencing Product table |
| OrderQuantity | INT       | Quantity of the product ordered |
| OrderDate     | DATETIME  | Date and time when the order was placed |

### Product

| Column Name | Data Type | Description        |
|-------------|-----------|--------------------|
| ProductId   | INT       | Unique identifier for each product |
| ProductName | VARCHAR   | Name of the product |
| Price       | DECIMAL   | Price of the product |

1. **Clone the repository**:

   ```bash
   git clone https://github.com/mojahidaltarif28/.NET-API-Test.git 
   ```
2.  **Navigate to the solution directory:**
  ```bash
   cd APITEST
```
3. **Run Project:**
  ```bash
   dotnet watch run --project WEB.API
   dotnet watch run --project WEBBLAZOR
   dotnet watch run --project WEBRAZOR
   dotnet watch run --project WEBWASM```
