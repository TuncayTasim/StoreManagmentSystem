using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models;
using StoreManagmentSystem.Models.ProductModels;
using StoreManagmentSystem.Service;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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

        [HttpGet("Barcode/{barcode}")]
        public async Task<ActionResult<Product>> GetProductByBarcode(string barcode)
        {
            return await _productService.GetProductByBarcode(barcode);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductModel product)
        {
            await _productService.AddProduct(product);
            return Ok("Product successfully added.");
        }

        [HttpPut("{ProductId}")]
        public async Task<ActionResult> UpdateProduct(Guid ProductId, ProductModel newProductInfo)
        {
           var updatedProduct = await _productService.UpdateProduct(ProductId, newProductInfo);

            if (updatedProduct == null)
            {
                return NotFound($"Product with ID {ProductId} was not found.");
            }
            return Ok(new
            {
                Message = $"Product with ID: {ProductId} successfully updated",
                UpdatedProduct = updatedProduct
            });
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
