# Prompt: Change API Contract Safely

Use conventions from `docs/ai/conventions.md`.

The API contract for `<Entity>` must be updated as described below:
<Describe the contract change precisely>

Tasks:
- Update Contracts DTOs only (requests/responses)
- Update mapping configuration so DTOs are fully covered
- Update endpoint/handler signatures only if necessary

Constraints:
- Do not change Domain or EF configuration unless explicitly required
- Keep existing JSON shape stable unless breaking change is requested
- Use records; keep naming conventions

Output format:
- List modified files
- Provide updated content