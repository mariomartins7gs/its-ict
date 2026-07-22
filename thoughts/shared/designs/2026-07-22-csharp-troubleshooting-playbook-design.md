---
date: 2026-07-22
topic: "C# Project Troubleshooting Playbook – Patterns & Best Practices"
status: validated
---

## Problem Statement

We fixed a broken ASP.NET Core MVC + EF Core + SQL Server project where every page threw errors. The root causes were layered — a fake connection string, missing database/tables, schema mismatches between models and tables, and port conflicts. We need a reusable playbook so these same diagnostic patterns can be applied to any future C# project.

## The Diagnostic Methodology (6-Step Flow)

The single most important takeaway: **never guess — isolate each layer independently before changing anything**.

### Step 1: Identify the Error Boundary
Before touching code, determine which layer is failing:
- **Browser-level** (404, connection refused) → app isn't running or port mismatch
- **Application-level** (500, yellow screen) → code/database logic error
- **Database-level** (SQL exception, login failure) → connection or schema problem

In our case, the yellow screen showed `SqlException: login failed for user 'bad'` — that immediately told us it was database-layer.

### Step 2: Test Connectivity Independently
Don't rely on the app to test database connectivity. Use `sqlcmd` from the command line with the same credentials:

```
sqlcmd -S <server> -E -C -Q "SELECT 1"    # Windows Auth
sqlcmd -S <server> -U <user> -P <pwd> -C -Q "SELECT 1"  # SQL Auth
```

**Key flags:** `-C` (TrustServerCertificate, for dev environments) and `-E` (Windows/Integrated Auth).

This step surfaces three issues at once:
- Is the server reachable?
- Do the credentials work?
- Does SSL/certificate trust need `TrustServerCertificate=True`?

### Step 3: Audit Every Layer in Order
Check each layer top-to-bottom before fixing any:

| Layer | What to Check | Tools |
|-------|--------------|-------|
| **Config** | Connection string, environment variables, `launchSettings.json` ports | Read `appsettings.json`, `appsettings.Development.json`, `Program.cs` |
| **Server** | Is SQL Server running? Which instances? | `Get-Service -Name "*SQL*"`, `sqllocaldb info` |
| **Database** | Does the database exist? Which one? | `sqlcmd ... SELECT name FROM sys.databases` |
| **Tables** | Do tables match the EF model? All columns present? Right types? | Compare `DbContext.OnModelCreating()` with `INFORMATION_SCHEMA.COLUMNS` |
| **Dependencies** | EF tools installed? Migrations applied? | `dotnet ef database update` |

### Step 4: Fix at the Source, Not the Symptom
Once you've found the root cause, fix it at the deepest layer possible:
- **Connection string** → change in `appsettings.json` OR environment variable (`CONN_STR`)
- **Missing database** → `CREATE DATABASE` or run migrations
- **Schema mismatch** → alter tables to match the model, OR regenerate the scaffold
- **Port conflict** → kill the previous process; only ONE instance per port

### Step 5: Verify End-to-End
After fixing, verify every page/feature that touches the changed layer:
- Navigate to all CRUD pages
- Check both HTTP and HTTPS
- If data was expected, verify it renders

### Step 6: Handle Localization Traps
SQL Server's date format varies by locale. Italian SQL Server uses DMY (day-month-year), not the ISO YYYY-MM-DD. Always use **unambiguous formats**:
- **YYYYMMDD** (e.g., `'20260722'`) — works in every locale, no conversion needed
- Or use `CONVERT(DATETIME, '2026-07-22', 120)` with explicit style parameter

## Common Pitfall Patterns & Their Fixes

### Pattern 1: The "Bad" Placeholder Credential
**Symptom:** `SqlException: Login failed for user 'bad'`  
**Root cause:** Connection string contains a placeholder (`User ID=bad`) that was never updated.  
**Fix:** Switch to Windows Auth (`Trusted_Connection=True`) or provide real SQL credentials.  
**Pro-tip:** Add a startup check in `Program.cs` that logs a warning when placeholder credentials are detected:

```csharp
if ((connectionString ?? string.Empty).IndexOf("User Id=bad", StringComparison.OrdinalIgnoreCase) >= 0)
    logger.LogWarning("Placeholder credential detected. Update your connection string.");
```

### Pattern 2: Database-First Scaffold, Missing Schema
**Symptom:** `SqlException: Invalid column name 'X'`  
**Root cause:** The DbContext was scaffolded from an existing database (Database-First), but the target database doesn't exist or has a different schema. The scaffolded `OnModelCreating()` configuration doesn't auto-create tables — it expects them to already exist.  
**Fix:** Either:
- A) Create the database and tables manually with SQL matching the scaffolded model
- B) Generate a migration from the scaffolded context: `dotnet ef migrations add InitialCreate --context YourContext`
- C) Use `Database.EnsureCreated()` on startup (for dev only)

**Detection:** Compare the Fluent API config in `OnModelCreating()` (table names, column types, keys) against what `INFORMATION_SCHEMA.COLUMNS` shows.

### Pattern 3: Model-Table Type Mismatch
**Symptom:** `SqlException: Operand type clash: nvarchar is incompatible with int`  
**Root cause:** The C# model property type (e.g., `int Codice`) doesn't match the SQL column type (e.g., `NVARCHAR(50) Codice`). This happens when manually creating tables without checking the model first.  
**Fix:** Always read the model class AND the Fluent API config before writing CREATE TABLE statements. Pay attention to:
- `HasKey()` → PRIMARY KEY
- `ValueGeneratedNever()` → not an IDENTITY column
- `.HasColumnType("datetime")` → exact SQL type
- Navigation properties → FOREIGN KEY constraints

### Pattern 4: Port Conflict (IDE vs CLI)
**Symptom:** Visual Studio "Failed to start" or `ERR_CONNECTION_REFUSED`  
**Root cause:** Another process (e.g., a previous `dotnet run` from terminal) is holding the port.  
**Fix:** Kill the background process or check with `Get-NetTCPConnection -State Listen | Where-Object LocalPort -eq 7075`. Only ONE instance can bind to a port.

### Pattern 5: Identity Tables Missing
**Symptom:** Error on Register/Login, but business pages work fine  
**Root cause:** Identity migrations (`AspNetUsers`, `AspNetRoles`, etc.) haven't been applied.  
**Fix:** Run `dotnet ef database update --context ApplicationDbContext`. Identity has its own migration pipeline separate from the business context.

## The Environment Variable Pattern

The project used a smart pattern in `Program.cs`:

```csharp
var connectionString = Environment.GetEnvironmentVariable("CONN_STR");
if (string.IsNullOrEmpty(connectionString))
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```

This means you can **override the connection string without changing code** — just set the `CONN_STR` environment variable. This is the pattern we should apply to all new projects because:
- It keeps secrets out of `appsettings.json` (no passwords in source control)
- Different environments (dev, CI, prod) point to different databases
- Fallback works for local development

## Quick Diagnostic Checklist

When a C# project won't run, go through this in order:

1. **Can you reach the URL?** → Is the app running? Check ports in `launchSettings.json`
2. **Is it a 500 error?** → Check the browser's developer exception page (yellow screen in dev mode)
3. **Is it a SQL error?** → Test connectivity with `sqlcmd` independently
4. **Login failure?** → Check if credentials are placeholders; try Windows Auth
5. **Invalid column/table?** → Compare model classes + Fluent API config with actual DB schema
6. **Port taken?** → Kill other instances; check with `netstat` or `Get-NetTCPConnection`
7. **Identity broken?** → Run `dotnet ef database update --context ApplicationDbContext`
8. **Date errors?** → Use `YYYYMMDD` format, never locale-dependent dates

## Testing Strategy

For new projects adopting this playbook, test each concern independently:

| Test | What it verifies |
|------|-----------------|
| `sqlcmd` connectivity | Database is reachable with given credentials |
| `SELECT name FROM sys.databases` | Target database exists |
| Compare model ↔ `INFORMATION_SCHEMA.COLUMNS` | Schema matches EF model |
| `dotnet ef database update --context X` | Migrations apply cleanly |
| Navigate all CRUD pages | End-to-end functionality |
| Check both HTTP and HTTPS | SSL/TLS configuration |
