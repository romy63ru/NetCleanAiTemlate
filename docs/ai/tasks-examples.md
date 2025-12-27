# Task Prompts


## Domain entity

Based on docs/ai/entities/order.md generate Domain Order aggregate only. No persistence, no DTOs.

## Application layer

Using Order aggregate and docs/ai/entities/order.md generate Application service for creating an order. Use Result.

## Infrastructure

Implement EF Core persistence for Order aggregate. Domain must not reference EF. Use owned entities.

## API 

Generate API endpoint for creating Order. Use DTOs. No domain types in API contracts.