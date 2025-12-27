# Orchestration

How prompts chain and tools are called. Define entrypoints, steps, and fallbacks.

Architecture and flow guardrails:

- UI interacts via Application interfaces only; no direct Infrastructure usage.
- Controllers are thin; validation occurs before invoking use cases.
- Map Application `Result<T>` to explicit HTTP status codes.
- Errors return RFC 7807 ProblemDetails in API layer.

Async behavior:

- Propagate `CancellationToken` through all async calls.
- No fire-and-forget; async methods return `Task`.
