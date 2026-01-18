# Prompt: Sync Code with Updated Specification

Use conventions from `docs/ai/conventions.md`.
The specification for `<Entity>` has changed in `docs/ai/entities/<Entity>.md`.

Synchronize the code with the updated specification using minimal, targeted changes.

Tasks:
- Update Domain model (aggregate, entities, behavior, invariants)
- Update Enums and ValueTypes if required
- Update EF Core Fluent configuration to match the new model
- Update Contracts DTOs (requests/responses) to match the new API section
- Update Mapster mapping config to cover all properties

Constraints:
- Do not touch unrelated aggregates / bounded contexts
- Preserve architecture boundaries (Domain stays persistence-ignorant)
- Do not introduce new abstractions unless explicitly required by spec
- If a breaking change is required, clearly list it and why
- Keep code style consistent with existing project

Output format:
- List modified files only
- Provide updated content for each modified file
- Briefly explain what changed per file (1–3 bullet points)