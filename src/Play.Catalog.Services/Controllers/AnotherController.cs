using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Repositories;

[ApiController]
[Route("another")]
public class AnotherController : ControllerBase
{
   private readonly ItemsRepository itemsRepository;

   public AnotherController(ItemsRepository itemsRepository)
   {
      this.itemsRepository = itemsRepository;
   }

   [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
    {
        return Ok(await itemsRepository.GetItemsAsync());
    }
}