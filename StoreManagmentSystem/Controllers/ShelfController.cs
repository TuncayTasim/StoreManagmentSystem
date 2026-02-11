using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
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
        public async Task<IEnumerable<Shelf>> GetAllItemsOnShelf()
        {
            return await _shelfService.GetAllItemsOnShelf();
        }
        [HttpGet("GetItem/{itemId}")]
        public async Task<ActionResult<ShelfRestock>> GetItemById(int itemId)
        {
            return await _shelfService.GetItemById(itemId);
        }
        [HttpGet("AllRestockRecords")]
        public async Task<IEnumerable<ShelfRestock>> GetAllMoves()
        {
            return await _shelfService.GetAllMoves();
        }
        [HttpGet("{shelfId}")]
        public async Task<ActionResult<Shelf>> GetMoveRecordById(int shelfId)
        {
            return await _shelfService.GetMoveRecordById(shelfId);
        }
        [HttpPost]
        public async Task<ActionResult> MoveToShelf(ShelfModel item)
        {
            try
            {
                var addedItemInfo = await _shelfService.MoveToShelf(item);
                return Ok(addedItemInfo);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{itemId}")]
        public async Task<ActionResult> UpdateItem(int itemId, ShelfModelNoId item)
        {
            try
            {
                var updatedItem = await _shelfService.UpdateItem(itemId, item);
                if (updatedItem == null)
                {
                    return NotFound($"Item with ID {itemId} was not found.");
                }
                return Ok(updatedItem);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ItemsByProduct/{ProductId}")]
        public async Task<IEnumerable<ShelfModel>> GetAllItemsByProductId(Guid ProductId)
        {
            var allStocksByProductId = await _shelfService.GetAllItemsByProductId(ProductId);
            return allStocksByProductId;
        }

        //[HttpDelete("{shelfId}")]
        //public async Task<ActionResult> DeleteItem(int shelfId)
        //{
        //    var deleteResult = await _shelfService.DeleteItem(shelfId);
        //    if (deleteResult == null)
        //    {
        //        return NotFound($"Item with ID {shelfId} was not found.");
        //    }

        //    if (!deleteResult.Success)
        //    {
        //        return BadRequest(deleteResult.Message);
        //    }

        //    return Ok(new
        //    {
        //        deleteResult.Message,
        //        deleteResult.DeletedItem,
        //        deleteResult.QuantityInShelf,
        //        deleteResult.RestockRecommended
        //    });
        //}
    }
}
