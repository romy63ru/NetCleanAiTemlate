# GitHub Copilot – Project Instructions

## General
- Tech stack: .NET 10, C# 14
- UI: Avalonia
- Backend: ASP.NET Core Web API
- Architecture: Clean Architecture
- Prefer explicit, maintainable code
- Do not invent frameworks or libraries
- Ask for clarification if UI or UX requirements are unclear

## Solution Structure
- /src
  - Api
  - Application
  - Domain
  - Infrastructure
  - Ui.Avalonia
- /tests mirror /src structure

## Architecture Rules
- Domain has no dependencies
- Application depends only on Domain
- Infrastructure depends on Application + Domain
- UI depends only on Application contracts
- No direct Infrastructure usage from UI

## API Layer (ASP.NET Core)
- Controllers are thin
- No business logic in controllers
- Use DTOs for input/output
- Explicit HTTP status codes
- Use RFC 7807 ProblemDetails for error responses
- No exception-based flow control
- Validation happens before use cases
- Map Application Result<T> to status codes (e.g., 200/201 on success, 400/404/409 for validation/not found/conflict, 500 only for unexpected failures)

## Avalonia UI Rules
- Strict MVVM:
  - View (.axaml)
  - ViewModel (no UI framework dependencies)
- No code-behind logic except bindings
- No async void
- UI thread usage must be explicit
- Use commands, not event handlers

## ViewModels
- ViewModels are UI-only
- No Domain logic
- No Infrastructure access
- Communicate via Application interfaces
- Expose immutable state where possible

## State Management
- Explicit state containers
- No hidden static state
- UI state is predictable and testable
- Avoid global singletons in UI

## Error Handling
- No exceptions for control flow
- Application returns Result<T>
- UI translates errors to user-friendly messages
- No raw error messages shown to users
- API errors return ProblemDetails with appropriate status codes

## Async Rules
- async/await everywhere
- CancellationToken propagated
- No blocking calls
- UI async calls must handle cancellation
- No fire-and-forget; async methods return Task
- Do not use CancellationToken.None in production paths

## Validation
- Domain invariants enforced in Domain
- UI validates input for UX only
- API validates input for correctness
- No duplicated validation logic

## Logging
- No logging in UI
- No logging in Domain
- Logging only in API and Infrastructure
- Structured logs only

## Testing
- Domain: pure unit tests
- Application: use-case tests
- UI:
  - ViewModels are testable
  - No UI framework in tests
- No snapshot tests unless explicitly requested

## Naming
- Components: `SomethingView`, `SomethingViewModel`
- Services: `SomethingService`
- Interfaces: `ISomething`
- Commands: verbs (`Load`, `Save`, `Refresh`)
- Avoid abbreviations

## Code Generation Rules
- Generate minimal viable code
- No scaffolding unless explicitly requested
- Do not mix Blazor and Avalonia concepts
- Shared logic goes to Application layer only

## Forbidden
- Business logic in UI
- HTTP calls directly from UI components
- ViewModels depending on Infrastructure
- God components / God ViewModels
- Static mutable state