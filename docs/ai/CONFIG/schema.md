# Config Schema

- provider.default: string (e.g., "openai")
- model.default: string (e.g., "gpt-5")
- safety.policies: array of policy ids
- prompts.paths: object { system, tasks, templates }

Policy keys:

- policy.contentProvider: string (e.g., "microsoft")
- policy.violationResponse: string (exact fallback message)

Error handling:

- api.error.problemDetails: boolean

Async rules:

- async.requireCancellationToken: boolean
- async.disallowFireAndForget: boolean
