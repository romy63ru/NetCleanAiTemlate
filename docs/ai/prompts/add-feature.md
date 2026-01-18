# Prompt: Add Feature to Existing Aggregate

Use conventions from `docs/ai/conventions.md`.
Use the updated spec in `docs/ai/entities/<Entity>.md` (or a dedicated feature note if provided).

Feature:
<Describe the new feature in 1–3 sentences>

Tasks:
- Update Domain behavior and invariants (add methods, states, events if needed)
- Update Enums/ValueTypes if needed (separate files, correct folders)
- Update Persistence mapping (EF Core Fluent config)
- Update API Contracts (new fields, new request/response types)
- Update Mapster mapping config
- Add/adjust validation rules (if your project uses FluentValidation)

Constraints:
- Minimal changes outside `<Entity>`
- Do not refactor unrelated parts
- Keep backward compatibility unless explicitly allowed
- Follow naming and folder conventions strictly

Output format:
- List created/modified files
- Provide full content for each created/modified file
- Include a short migration note if DB schema changes are required