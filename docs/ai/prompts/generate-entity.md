# Prompt: Generate Entity End-to-End (DDD)

Use conventions from `docs/ai/conventions.md`.
Use the entity specification from `docs/ai/entities/<Entity>.md`.

Goal: generate the full vertical slice for `<Entity>` end-to-end, strictly following the spec and conventions.

Tasks:
1) Domain
- Create aggregate root `<Entity>` and internal entities (if any)
- Create all required Enums in `src/Domain/<BC>/Enums` (one type per file)
- Create all required ValueTypes in `src/Domain/<BC>/ValueTypes` (one type per file)
- Enforce invariants in the domain model
- Keep Domain persistence-ignorant (no EF/JSON attributes)

2) Persistence (EF Core)
- Add EF Core Fluent configuration in `src/Infrastructure/Persistence/Configurations`
- Map strongly typed IDs using value converters
- Map value objects as owned types when appropriate
- Apply indexes/constraints described in the spec
- Do not add EF attributes to Domain

3) API Contracts
- Add request/response DTOs as `record` types in `src/Contracts/<EntityPlural>/`
- Do not expose Domain types through API contracts

4) Mapping
- Add explicit Mapster configuration in `src/Application/Mapping`
- Cover all properties:
  - Domain ↔ DTO
  - Domain ↔ persistence model (only if persistence model is separate)

Constraints:
- One public type per file; file name matches type name
- No nested Enums or ValueTypes inside aggregates
- Do not introduce new patterns not present in conventions
- Nullable reference types enabled
- Use `Result<T>` pattern for errors (no exceptions for flow control)

Output format:
- List all created/modified files
- Provide full content for each file