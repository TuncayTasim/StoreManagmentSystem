using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models;
using StoreManagmentSystem.Service;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<Product>> GetProductById(Guid ProductId)
        {
            return await _productService.GetProductById(ProductId);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _productService.AddProduct(product);
            return Ok("Product successfully added.");
        }

        [HttpPut("{ProductId}")]
        public async Task<ActionResult> UpdateProduct(Guid ProductId, Product newProductInfo)
        {
           var updatedProduct = await _productService.UpdateProduct(ProductId, newProductInfo);

            if (updatedProduct == null)
            {
                return NotFound($"Product with ID {ProductId} was not found.");
            }

            return Ok($"Product with ID: {ProductId} successfully updated\n New product:\n{updatedProduct}");
        }

        [HttpDelete("{ProductId}")]
        public async Task<ActionResult> DeleteProduct(Guid ProductId)
        {
            var productToDelete = await _productService.DeleteProduct(ProductId);
            if (productToDelete == null)
            {
                return NotFound($"Product with ID {ProductId} was not found.");
            }
            return Ok($"Product with ID: {ProductId} successfully deleted.");
        }   
    }
}
