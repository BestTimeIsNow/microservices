using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Extensions
{
    public static class ItemExtension
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDtTm);
        }
    }
}