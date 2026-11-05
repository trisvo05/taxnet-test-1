
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Exceptions;
using ProductApi.Services;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    // GET /api/products/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        try
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message,
                productId = ex.ProductId
            });
        }
    }

    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create(
        [FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
        {
            return BadRequest("Tên và giá sản phẩm không hợp lệ");
        }

        try
        {
            var createdProduct = await _productService.CreateAsync(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.Id },
                createdProduct
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
