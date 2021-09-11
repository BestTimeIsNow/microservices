using System;

namespace Play.Catalog.Services.Dtos
{
    public record ItemDto(Guid Id, string name, string description, decimal price, DateTimeOffset CreatedDtTm);
    public record AddItemDto(string name, string description, decimal price);
    public record UpdateItemDto(string name, string description, decimal price);
}