using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Service;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService inventoryService)
        {
            _warehouseService = inventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory()
        {
            return await _warehouseService.GetAllStocksInInventory();
        }

        [HttpGet("{StockId}")]
        public async Task<ActionResult<WarehouseRestock>> GetStockById(int StockId)
        {
            return await _warehouseService.GetStockById(StockId);
        }

        [HttpGet("StocksByProduct/{ProductId}")]
        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksByProductId(Guid ProductId)
        {
            var allStocksByProductId = await _warehouseService.GetAllStocksByProductId(ProductId);
            return allStocksByProductId;
        }

        [HttpPost]
        public async Task<ActionResult> AddStock(WarehouseModel stock)
        {
            await _warehouseService.AddStock(stock);
            return Ok();
        }

        [HttpPut("{StockId}")]
        public async Task<ActionResult> UpdateStock(int StockId, WarehouseModelNoId stock)
        {
            var updatedStock = await _warehouseService.UpdateStock(StockId, stock);

            if (updatedStock == null)
            {
                return NotFound($"Stock with ID {StockId} was not found.");
            }
            return Ok(new
            {
                Message = $"Stock with ProductId: {StockId} successfully updated",
                UpdatedStock = updatedStock
            });
        }

        [HttpDelete("{StockId}")]

        public async Task<ActionResult> DeleteStock(int StockId)
        {
            var deleteResult = await _warehouseService.DeleteStock(StockId);
            if (deleteResult == null)
            {
                return NotFound($"Stock with ID {StockId} was not found.");
            }

            if (!deleteResult.Success)
            {
                return BadRequest(deleteResult.Message);
            }

            return Ok(new
            {
                deleteResult.Message,
                deleteResult.DeletedStock,
                deleteResult.QuantityInWarehouse,
                deleteResult.RestockRecommended
            });
        }
    }
}
