# Prompt: Generate Using Existing Example

Use conventions from `docs/ai/conventions.md`.

Use `<ExistingAggregate>` implementation as the reference style/pattern.
Generate `<Entity>` according to `docs/ai/entities/<Entity>.md`.

Constraints:
- Mirror folder layout, naming, Result<T> usage, validation approach from `<ExistingAggregate>`
- No new architectural patterns
- Enums in `Domain/<BC>/Enums`, ValueTypes in `Domain/<BC>/ValueTypes`, one type per file

Output format:
- List created/modified files
- Provide full content for each file