using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Repositories;
using Play.Catalog.Services.Extensions;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly ItemsRepository itemsRepository = new();
        public static readonly List<ItemDto> items = new List<ItemDto>
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Heal some HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Inflicts some HP", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var result = await itemsRepository.GetItemsAsync();
            return Ok(result.Select(item => item.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            var result = await itemsRepository.GetItemAsync(id);

            if(result == null) return NotFound();

            return Ok(result.AsDto());

        }

        // If successful, return 201 (resource created) & ItemDto
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(AddItemDto addItem)
        {
            // create Item Entity from AddItemDto and invoke AddItemAsync(Item)
            // then "CreateAtAction..." with Item.AsDto()

            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = addItem.name,
                Description = addItem.description,
                Price = addItem.price,
                CreatedDtTm = DateTimeOffset.UtcNow
            };

            var result = false;

            result = await itemsRepository.AddItemAsync(item);

            if (result)
            {
                // Note: At runtime, aspnetcore remove "async" from nameof, so either keep name less 'async'
                //       or in startup services, add option to controllers to suppress this behavior
                return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
            }
            
            return BadRequest("There was a problem with adding the item.  Please review and try again.");

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            // Retrieve item, esp for the CreatedDtTm
            // Create Item Entity
            // Invoke UpdateItemAsync(Item) & return based on result

            var item = await itemsRepository.GetItemAsync(id);

            var itemEntity = new Item
            {
                Id = id,
                Name = updateItemDto.name,
                Description = updateItemDto.description,
                Price = updateItemDto.price,
                CreatedDtTm = item.CreatedDtTm
            };

            var success = await itemsRepository.UpdateItemAsync(itemEntity);

            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("There was a problem with updating the item. Please review and try again.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            // Invoke DeleteItemAsync(id) & return based on result
            var success = await itemsRepository.DeleteItemAsync(id);

            if (success) return NoContent();

            return BadRequest("There was a problem deleting this item. Plesae review and try again.");
        }   


    }
}