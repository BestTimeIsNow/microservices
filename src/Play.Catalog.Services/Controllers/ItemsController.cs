using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Services.Dtos;

namespace Play.Catalog.Services
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        public static readonly List<ItemDto> items = new List<ItemDto>
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Heal some HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Inflicts some HP", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetItems()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = items.Where(items => items.Id == id).FirstOrDefault();

            if(item == null){
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(AddItemDto addItem)
        {
            var newItem = new ItemDto(Guid.NewGuid(), addItem.name, addItem.description, addItem.price, DateTimeOffset.UtcNow);
            items.Add(newItem);

            return CreatedAtAction(nameof(GetItem), new {id = newItem.Id}, newItem);
        }


        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();

            if(item == null){
                return NotFound();
            }
            
            var index = items.IndexOf(item);

            var updatedItem = new ItemDto(item.Id, updateItemDto.name, updateItemDto.description, updateItemDto.price, item.CreateDtTm);

            items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            // Option 1 works:
            // var item = items.Where(item => item.Id == id).SingleOrDefault();
            // items.Remove(item);

            // Option 2:
            var index = items.FindIndex(item => item.Id == id);

            // If index is not found, "FindIndex" returns -1
            if(index < 0){
                return NotFound();
            };

            items.RemoveAt(index);

            return NoContent();
        }


    }
}