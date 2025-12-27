# Tests Layout

Mirrors /src:

- Api: controller/endpoint tests (ProblemDetails, status codes)
- Application: use-case tests around `Result<T>`
- Domain: pure unit tests for invariants
- Infrastructure: integration tests with fakes/test containers
- Ui.Avalonia: ViewModel tests (no UI framework)

Guidelines: no snapshot tests unless requested; no async void; inject CancellationToken.
