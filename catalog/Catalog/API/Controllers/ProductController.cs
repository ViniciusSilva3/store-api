using Catalog.Domain;
using Catalog.Domain.Utils;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("catalog")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpPost]
    [Route("product")]
    public async Task<ActionResult> CreateProduct(ProductModel product)
    {
        Result res = await _productService.SaveProduct(product);

        if (res.IsNotSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, res.Error);
        }

        return new OkResult();
    }
}