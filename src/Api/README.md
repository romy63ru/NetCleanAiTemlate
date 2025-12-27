# Api

ASP.NET Core Web API.

- Controllers are thin; no business logic
- Use DTOs for input/output
- Map Application `Result<T>` to HTTP status codes
- Errors use RFC 7807 ProblemDetails
- No exception-based control flow
