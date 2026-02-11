using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Service;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }
        [HttpPost]
        public async Task<ActionResult> SellItem(SaleModel sale)
        {
            var sellResult = await _salesService.SellItem(sale);
            if (sellResult == null)
            {
                return NotFound($"Item with productId {sale.ProductId} was not found.");
            }

            if (!sellResult.Success)
            {
                return BadRequest(sellResult.Message);
            }

            return Ok(new
            {
                sellResult.Message,
                sellResult.QuantityInShelf,
                sellResult.RestockRecommended
            });
        }
    }
}
