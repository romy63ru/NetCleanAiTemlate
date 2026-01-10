# Architecture Overview

## Layers and Responsibilities

- Domain: Entities, value objects, domain services, invariants; no dependencies.
- Application: Use cases/handlers orchestrating domain; ports for persistence/integration; mapping and validation live here; depends only on Domain.
- Infrastructure: Adapters for persistence, messaging, file/storage, external services; implements Application ports; replaceable.
- API: HTTP surface, authentication/authorization, request/response DTOs; calls Application only.
- Avalonia UI: Desktop surface using Application DTOs; never touches Infrastructure or Domain directly.

## Communication

- API/UI → Application handlers/services → Domain; Infrastructure is accessed only through Application ports.
- Prefer mediator-style commands/queries or clearly scoped services; keep handlers focused and bounded.

## Error handling

- Domain invariants may throw domain exceptions; Application/API/UI should convert to `Result<T>`/ProblemDetails.
- Use typed `Result<T>` with error codes; reserve exceptions for unexpected faults.
- Surface errors consistently: API returns ProblemDetails; UI shows mapped messages.
- Log exceptions and unexpected failures once at the boundary (API/UI).

## Cross-cutting

- Logging/metrics/tracing via decorators or pipeline behaviors registered in Application/API.
- Validation pipeline runs before handlers; no controller/handler logic without validation.
- Authentication/authorization at API; propagate user context into Application as needed.
- Use an Anti-Corruption Layer (ACL) for external systems: define ports in Application, implement adapters in Infrastructure that translate to/from domain language and shield the domain from external models, errors, and contracts.

## Architecture rules

- Domain has NO dependencies.
- Application depends only on Domain; exposes ports for Infrastructure.
- Infrastructure is replaceable; never leak EF/entities outside Infrastructure.
- No async void; CancellationToken on all async entry points and downstream calls; caller owns token.
- One public class per file; explicit interfaces for ports; no static mutable state.

## Coding rules

- Records for DTOs; keep DTOs immutable.
- Use `Result<T>`/typed errors instead of exceptions for flow control.
- Avoid magic strings: use enums/constants/options.
- Keep services bounded; break apart “God” services.

## Testing

- xUnit; Arrange–Act–Assert; no shared mutable fixtures.
- Domain tests run pure and in-memory.
- Application tests cover handlers with fake ports; include mapping tests (Mapster generation or compile-time validation).
- Infrastructure tests are adapter-focused or integration; API tests cover contracts and ProblemDetails behavior.

## What NOT to do / Do instead

- No God services → keep handlers/services small and cohesive.
- No direct DbContext usage in controllers → use Application ports/handlers.
- No magic strings → use typed options/constants/enums; centralize configuration keys.

## Mapping Strategy

- Mapster is the single mapping solution; configurations live in Application.
- Generate mappings at compile time where possible; add tests to guard mapping configs.
- API/UI map only via Application DTOs; never map Domain directly.
- Domain objects are never mutated during mapping; keep mapping side-effect free.

Reasons:

- Compile-time safety
- High performance
- Explicit control over mappings
