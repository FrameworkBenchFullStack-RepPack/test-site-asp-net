# test-site — ASP.NET Razor Pages + HTMX

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/download/)

---

## 1. Install .NET 10

### macOS (Homebrew)

```bash
brew install dotnet
```

Or download the installer directly from [dot.net/download](https://dotnet.microsoft.com/download/dotnet/10.0).

Verify the installation:

```bash
dotnet --version
```

### Linux (Ubuntu/Debian)

```bash
wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update && sudo apt-get install -y dotnet-sdk-10.0
```

---

## 2. Set Up PostgreSQL

The app expects a PostgreSQL database with the following defaults (defined in `appsettings.json`):

| Setting  | Value     |
| -------- | --------- |
| Host     | localhost |
| Database | benchmark |
| Username | benchmark |
| Password | benchmark |

To use different credentials, update the connection string in `test-site/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=benchmark;Username=benchmark;Password=benchmark"
}
```

---

## 3. Install Dependencies & Apply Migrations

```bash
cd test-site
dotnet restore
dotnet ef database update
```

> `dotnet ef` requires the EF Core CLI. If it's not installed:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

## 4. Run the App

### Default (uses launch profile — HTTP on port 5223)

```bash
dotnet run
```

### Run on a specific port

```bash
dotnet run --urls "http://localhost:8080"
```

### Run on multiple URLs (HTTP + HTTPS)

```bash
dotnet run --urls "https://localhost:7239;http://localhost:5223"
```

### Run a specific launch profile

```bash
dotnet run --launch-profile http    # http://localhost:5223
dotnet run --launch-profile https   # https://localhost:7239
```

### Set the port via environment variable

```bash
ASPNETCORE_URLS="http://localhost:8080" dotnet run
```

To overwrite the default db port with env vars

```bash
ConnectionStrings__DefaultConnection="Host=localhost;Port={Port here};Database=benchmark;Username=benchmark;Password=benchmark" dotnet run
```

The app will be available at the configured URL. The browser launches automatically in Development mode.
