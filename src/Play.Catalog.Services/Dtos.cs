using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Services.Dtos
{
    public record ItemDto(Guid Id, string name, string description, decimal price, DateTimeOffset CreateDtTm);

    public record AddItemDto([Required]string name, [Required]string description, [Required][Range(1,9999)]decimal price);

    public record UpdateItemDto([Required]string name, [Required]string description, [Required][Range(0,9999)]decimal price);
}