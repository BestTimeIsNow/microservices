using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Services.Data;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Repositories
{
    public class ItemsRepository : I_ItemsRepository
    {
        // 3 private fields for coll name, coll, filter
        // constructor db location, connection, and collection
        // all async: getitems, getitem, additem, updateitem, deleteitem

        // private: const string, readonly IMongoCollection, FilterDefinition
        private const string ItemsCollectionName = "items";
        private readonly IMongoCollection<Item> ItemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        // constructor
        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDb = mongoClient.GetDatabase("Catalog");
            ItemsCollection = mongoDb.GetCollection<Item>(ItemsCollectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetItemsAsync()
        {
            // returning everything means filter would be empty
            return await ItemsCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await ItemsCollection.Find(filterBuilder.Where(item => item.Id == id)).FirstOrDefaultAsync();
        }

        // if "InsertOneAsync" fails, it throws an exception (nothing built-in to return)
        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                await ItemsCollection.InsertOneAsync(item);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        
        public async Task<bool> UpdateItemAsync(Item item) 
        {
            try
            {
               var result = await ItemsCollection.ReplaceOneAsync(filterBuilder.Where(x => x.Id == item.Id), item);
               if (result.ModifiedCount > 0) return true;
               else return false;
            }
            catch (System.Exception exc)
            {
                var x = exc;
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            try
            {
                var result = await ItemsCollection.DeleteOneAsync(filterBuilder.Where(item => item.Id == id));
                if (result.DeletedCount > 0) return true;
                else return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}