# Defaults

- Default provider: OpenAI
- Default model: GPT-5
- Global setting: Enable GPT-5 for all clients
- Safety: Enabled
- Prompt sources: PROMPTS/system, PROMPTS/tasks, PROMPTS/templates

Policy & behavior:

- Content policy: Microsoft
- Violation response: "Sorry, I can't assist with that."
- API error format: RFC 7807 ProblemDetails
- Architecture: UI uses Application interfaces only; no direct Infrastructure
- Async: propagate `CancellationToken`; no fire-and-forget

