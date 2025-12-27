# Application

Use cases and orchestrations.

- Depends only on Domain
- Returns `Result<T>`; no exceptions for control flow
- Validation happens here (post-UI checks)
- Async everywhere; propagate CancellationToken
