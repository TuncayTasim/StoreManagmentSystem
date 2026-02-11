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
        public async Task<IEnumerable<WarehouseRestock>> GetAllRestocks()
        {
            return await _warehouseService.GetAllRestocks();
        }

        [HttpGet("GetRestock/{stockId}")]
        public async Task<ActionResult<WarehouseRestock>> GetRestockById(int stockId)
        {
            return await _warehouseService.GetRestockById(stockId);
        }

        [HttpGet("AllRestockRecords")]
        public async Task<IEnumerable<Warehouse>> GetAllStocksInWarehouse()
        {
            return await _warehouseService.GetAllStocksInWarehouse();
        }

        [HttpGet("{warehouseId}")]
        public async Task<ActionResult<Warehouse>> GetRestockRecordById(int warehouseId)
        {
            return await _warehouseService.GetRestockRecordById(warehouseId);
        }

        [HttpGet("StocksByProduct/{productId}")]
        public async Task<IEnumerable<WarehouseModel>> GetAllRestocksByProductId(Guid productId)
        {
            var allStocksByProductId = await _warehouseService.GetAllRestocksByProductId(productId);
            return allStocksByProductId;
        }

        [HttpPost]
        public async Task<ActionResult> Restock(WarehouseModel stock)
        {
            try
            {
                await _warehouseService.Restock(stock);
                return Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{stockId}")]
        public async Task<ActionResult> UpdateRestock(int stockId, WarehouseModelNoId stock)
        {
            try
            {
                var updatedStock = await _warehouseService.UpdateRestock(stockId, stock);

                if (updatedStock == null)
                {
                    return NotFound($"Stock with ID {stockId} was not found.");
                }
                return Ok(new
                {
                    Message = $"Stock with ProductId: {stockId} successfully updated",
                    UpdatedStock = updatedStock
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //next 2 methods will be tranformed to reject methods
        //[HttpDelete("{stockId}")]
        //public async Task<ActionResult> DeleteStock(int stockId)
        //{
        //    var deleteResult = await _warehouseService.DeleteStock(stockId);
        //    if (deleteResult == null)
        //    {
        //        return NotFound($"Stock with ID {stockId} was not found.");
        //    }

        //    if (!deleteResult.Success)
        //    {
        //        return BadRequest(deleteResult.Message);
        //    }

        //    return Ok(new
        //    {
        //        deleteResult.Message,
        //        deleteResult.DeletedStock,
        //        deleteResult.QuantityInWarehouse,
        //        deleteResult.RestockRecommended
        //    });

        //}

        //[HttpDelete("RemoveExpiredStocks")]
        //public async Task<ActionResult> RemoveExpiredStocks()
        //{
        //    var removedList = await _warehouseService.RemoveExpiredStocks();
        //    if (removedList == null)
        //    {
        //        return NotFound("No expired stocks found to remove.");
        //    }
        //    return Ok(new
        //    {
        //        Message = $"{removedList.Count()} expired stocks were removed from the warehouse.",
        //        ListOfStocks = removedList
        //    });
        //}

    }
}
