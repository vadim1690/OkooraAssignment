1. **Connection String (SQL Server)**
   - File: `Domain/Data/Configuration/DbConfiguration.cs`
   - Update the `connectionString` with your local SQL Server credentials:
     ```
     private static readonly string connectionString =
         "Server=YOUR_SERVER;Database=RateDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True";
     ```

2. **API Key (Exchange Rate API)**
   - File: `RateFetcher/Services/RateFetcherService.cs`
   - Replace `_apiKey` with your own API key:
     ```
     private readonly string _apiKey = "YOUR_API_KEY";
     ```

3. **Database Setup**
   - Ensure you have a running SQL Server instance.
   - The application will automatically create the database using `EnsureCreated()`.

4. **Running**
   - Open the solution in Visual Studio or VS Code.
   - Set `RateFetcher` and `RatePrinter` as startup projects.
   - Alternatively, run via terminal:
     ```
     dotnet run --project .\RateFetcher\
     dotnet run --project .\RatePrinter\
     ```

6. **Swagger (API Testing)**
   - Access Swagger UI when `RatePrinter` is running:
     ```
     https://localhost:<port>/swagger
     ```
