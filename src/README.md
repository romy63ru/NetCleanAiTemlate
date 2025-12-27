# Source Layout

Projects (per copilot-instructions):

- Api: ASP.NET Core Web API; thin controllers; DTOs only
- Application: use cases, `Result<T>`; depends only on Domain
- Domain: entities, value objects, invariants; no dependencies
- Infrastructure: persistence, integrations; depends on Application + Domain
- Ui.Avalonia: MVVM; uses Application interfaces only; no direct Infrastructure

Async & errors: propagate CancellationToken; no fire-and-forget; map `Result<T>` to HTTP codes with ProblemDetails in API.
