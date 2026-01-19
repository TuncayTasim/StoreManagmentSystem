using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ProductModels;
using StoreManagmentSystem.Models.StockModels;
using StoreManagmentSystem.Service;
using System.Text.Json;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _stockService;
        public WarehouseController(IWarehouseService inventoryService)
        {
            _stockService = inventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory()
        {
            return await _stockService.GetAllStocksInInventory();
        }

        [HttpGet("{StockId}")]
        public async Task<ActionResult<WarehouseRestock>> GetStockById(int StockId)
        {
            return await _stockService.GetStockById(StockId);
        }

        [HttpPost]
        public async Task<ActionResult> AddStock(WarehouseModel stock)
        {
            await _stockService.AddStock(stock);
            return Ok();
        }

        [HttpPut("{StockId}")]
        public async Task<ActionResult> UpdateStock(int StockId, WarehouseModelNoId stock)
        {
            var updatedStock = await _stockService.UpdateStock(StockId, stock);

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
            var deletedStock = await _stockService.DeleteStock(StockId);
            if (deletedStock == null)
            {
                return NotFound($"Stock with ID {StockId} was not found.");
            }
            return Ok($"Stock with ID: {StockId} successfully deleted");
        }

        [HttpGet("StockCount/{ProductId}")]
        public async Task<ActionResult> CheckStockCount(Guid ProductId)
        { 
            decimal stockCount = await _stockService.GetStockCount(ProductId);
            if (stockCount == -1)
            {
                NotFound("There is no product with this Id in stock!");
            }
            return Ok("The quantity of this product in stock is: " + stockCount);
        }
    }
}
