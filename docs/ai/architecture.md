# Architecture Overview

## Layers

- Domain
- Application
- Infrastructure
- API
- Avalonia UI

## Communication

- Controller → Service

## Error handling

- Result<T>
- No exceptions for flow control

## Cross-cutting

- Logging via decorators
- Validation pipeline

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

## Mapping Strategy

- Mapster is used as the single mapping solution
- Mapping is performed in Application layer
- UI and API never map Domain directly
- Domain objects are never mutated during mapping

Reasons:

- Compile-time safety
- High performance
- Explicit control over mappings