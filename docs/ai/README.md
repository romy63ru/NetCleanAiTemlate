# AI Docs

This folder organizes AI interactions, configuration, providers, prompts, workflows, safety, evaluation, and decisions.

Aligned with project rules: .NET 10, C# 14, Clean Architecture, Avalonia MVVM. See [docs/ai/AI_GUIDELINES.md](docs/ai/AI_GUIDELINES.md) for principles and standards.

Key guardrails reflected across docs:

- Controllers are thin; validation before use cases
- Map `Result<T>` to explicit HTTP codes; use RFC 7807 ProblemDetails for errors
- UI uses Application interfaces only; no direct Infrastructure usage
- Async everywhere: propagate `CancellationToken`; no fire-and-forget
- Safety: follow Microsoft content policy; reject disallowed requests consistently

