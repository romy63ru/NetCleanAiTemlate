# Task Prompts

Reusable task prompts (coding, review, test-gen, docs). Parameterize inputs.


## Domain entity

Based on docs/ai/entities/order.md generate Domain Order aggregate only. No persistence, no DTOs.

## Application layer

Using Order aggregate and docs/ai/entities/order.md generate Application service for creating an order. Use Result.

## Infrastructure

Implement EF Core persistence for Order aggregate. Domain must not reference EF. Use owned entities.

## API 

Generate API endpoint for creating Order. Use DTOs. No domain types in API contracts.



## {{task_name}}

- Objective: {{objective}}
- Inputs: {{inputs_list}}  (e.g., repo path, files, context)
- Parameters: {{parameters_list}}  (e.g., strictness, style_guide)
- Output: {{output_spec}}  (e.g., summary + actionable items)   
- Constraints: {{constraints}}  (e.g., no code edits, no external calls)

### Prompt
You are {{role}}. 
Goal: {{objective}}.

Context:
{{context_block}}

Inputs:
- {{input_1}}: {{value_1}}
- {{input_2}}: {{value_2}}

Requirements:
- Focus on {{focus_areas}}.
- Follow {{style_guide}}.
- Respect constraints: {{constraints}}.

Deliverables:
- {{deliverable_1}}
- {{deliverable_2}}

Output Format:
- {{format_spec}} (e.g., JSON with fields: summary, issues[], recommendations[])

