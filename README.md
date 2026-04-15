# Test Site — ASP.NET Razor Pages + HTMX

## Prerequisites

- .NET 10 SDK: https://dotnet.microsoft.com/download/dotnet/10.0
- A running copy of the database: https://github.com/FrameworkBenchFullStack-RepPack/database-seed

## Install .NET 10

### macOS (Homebrew)

```bash
brew install dotnet
```

Or download the installer directly from [dot.net/download](https://dotnet.microsoft.com/download/dotnet/10.0).

Verify the installation:

```bash
dotnet --version
```

### Linux (Ubuntu)

```bash
sudo apt update && sudo apt install -y dotnet-sdk-10.0
```

## Install Dependencies

```bash
cd test-site
dotnet restore
```

## Build and Run

Do this when you need to run the server for benchmarking purposes.

Run server:

```bash
ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=benchmark;Username=benchmark;Password=benchmark" ASPNETCORE_URLS="http://localhost:5223" ASPNETCORE_ENVIRONMENT="Production" dotnet run
```

- `ConnectionStrings__DefaultConnection` is the connection string to the PostgreSQL database.
- `ASPNETCORE_URLS` defines the url and port on which the website is served.

When the server is ready, it logs a large multiline block of text which includes:

```bash
Now listening on: http://localhost:5223
Application started.
```

## Run Test-Server for Development:

Do this if you need a quick preview of the website, or are actively working on it.

Update the connection string in `test-site/appsettings.json` to point to the postgreSQL server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=benchmark;Username=benchmark;Password=benchmark"
}
```

Run the default launch profile (HTTP on port 5223):

```bash
dotnet run
```

Or run on a specific port:

```bash
dotnet run --urls "http://localhost:8080"
```
