# AI Guidelines

## Tech stack
- Language: C# 14
- Framework: .NET 10
- Architecture: Clean Architecture + DDD
- API: ASP.NET Core Web API
- Persistence: EF Core
- Validation: FluentValidation
- Mapping: Mapster

## Architecture rules
- Domain has NO dependencies
- Application depends only on Domain
- Infrastructure is replaceable
- No EF entities outside Infrastructure
- No async void
- CancellationToken everywhere

## Coding rules
- Records for DTOs
- Result<T> instead of exceptions
- Explicit interfaces
- One public class per file
- No static state

## Testing
- xUnit
- Arrange–Act–Assert
- No shared mutable fixtures

## What NOT to do
- No God services
- No direct DbContext usage in controllers
- No magic strings