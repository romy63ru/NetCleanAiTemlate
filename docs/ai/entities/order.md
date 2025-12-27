# Domain Entity: Order

## Purpose
Represents a customer order in the system.

## Invariants
- Order must have at least one OrderItem
- TotalPrice = sum(OrderItem.Price * Quantity)
- Order cannot be modified after confirmation

## Identity
- OrderId (GUID, value object)

## Lifecycle
- Created
- Confirmed
- Cancelled

## Domain Behavior
- AddItem(productId, quantity, price)
- Confirm()
- Cancel(reason)

## Persistence
- Stored in relational database
- Order is aggregate root
- OrderItems are owned entities

## API Exposure
- CreateOrderRequest
- GetOrderResponse
- No domain entities exposed directly