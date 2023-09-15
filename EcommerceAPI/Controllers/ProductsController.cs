using EcommerceAPI.Models;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [SwaggerResponse(200, "The list of products was successfully returned.", typeof(List<Product>))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "The specified product was found and returned.", typeof(Product))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        [SwaggerResponse(404, "The specified product was not found.")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [SwaggerResponse(201, "The product was successfully created.", typeof(Product))]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                await _productService.AddProductAsync(product);

                return CreatedAtAction("GetById", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "The specified product was successfully updated.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                product.Id = id;
                await _productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "The specified product was successfully deleted.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}