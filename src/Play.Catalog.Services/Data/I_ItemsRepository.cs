using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Data
{
    public interface I_ItemsRepository
    {

        Task<IReadOnlyCollection<Item>> GetItemsAsync();

        Task<Item> GetItemAsync(Guid id);

        Task<bool> AddItemAsync(Item item);

        Task<bool> UpdateItemAsync(Item item);
 
        Task<bool> DeleteItemAsync(Guid id);
    }
}