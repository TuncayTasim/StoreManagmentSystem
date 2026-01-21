using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Service;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;
        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShelfRestock>> GetAllItemsOnShelf()
        {
            return await _shelfService.GetAllItemsOnShelf();
        }

        [HttpGet("{shelfId}")]
        public async Task<ActionResult<ShelfRestock>> GetItemById(int shelfId)
        {
            return await _shelfService.GetItemById(shelfId);
        }

        [HttpPut]
        public async Task<ActionResult> AddItem(ShelfModel item)
        {
            await _shelfService.AddItem(item);
            return Ok();
        }

        [HttpPost("{shelfId}")]
        public async Task<ActionResult> UpdateItem(int shelfId, ShelfModelNoId item)
        {
            var updatedItem = await _shelfService.UpdateItem(shelfId, item);
            if (updatedItem == null)
            {
                NotFound($"Item with ID {shelfId} was not found.");
            }
            return Ok(new
            {
                Message = $"Item with ProductId: {shelfId} successfully updated",
                UpdatedStock = updatedItem
            });
        }

        [HttpDelete("{shelfId}")]
        public async Task<ActionResult> DeleteItem(int shelfId)
        {
            var deleteResult = await _shelfService.DeleteItem(shelfId);
            if (deleteResult == null)
            {
                return NotFound($"Item with ID {shelfId} was not found.");
            }

            if (!deleteResult.Success)
            {
                return BadRequest(deleteResult.Message);
            }

            return Ok(new
            {
                deleteResult.Message,
                deleteResult.DeletedItem,
                deleteResult.QuantityInShelf,
                deleteResult.RestockRecommended
            });
        }

        [HttpGet("ItemsByProduct/{ProductId}")]
        public async Task<IEnumerable<ShelfRestock>> GetAllItemsByProductId(Guid ProductId)
        {
            var allStocksByProductId = await _shelfService.GetAllItemsByProductId(ProductId);
            return allStocksByProductId;
        }

    }
}
