using System;

namespace BuildingBlocks.Contracts;

public record UserCreated(Guid UserId, string Name, string Email);

public record ProductCreated(Guid ProductId, string Name, Guid UserId);

public record SaleRegistered(Guid SaleId, Guid ProductId, Guid UserId, decimal Amount);
