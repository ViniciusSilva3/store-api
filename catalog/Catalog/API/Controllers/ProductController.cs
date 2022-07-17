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

    [HttpGet]
    [Route("product/{id}")]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        Result<Product> res = await _productService.GetProductById(id);

        if (res.IsNotSuccess)
        {
            return new NotFoundObjectResult($"Could not find product with id: {id}.");
        }
        
        return new OkObjectResult(res.Value);
    }

    [HttpGet]
    [Route("product")]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        Result<IEnumerable<Product>> res = _productService.GetProducts();

        if (res.IsNotSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, res.Error);
        }
        
        return new OkObjectResult(res.Value);
    }

    [HttpPost]
    [Route("product")]
    public async Task<ActionResult> CreateProduct(ProductModel product)
    {
        ModelValidation requestValidation = product.validation;
        if (!requestValidation.isValid)
            return new BadRequestObjectResult(requestValidation.errorMessage.FirstOrDefault());

        Result res = await _productService.SaveProduct(product);

        if (res.IsNotSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, res.Error);
        }

        return new NoContentResult();
    }

    [HttpPut]
    [Route("product/{id}")]
    public async Task<ActionResult> UpdateProduct(string id, UpdateProductModel product)
    {
        ModelValidation requestValidation = product.validation;
        if (!requestValidation.isValid)
            return new BadRequestObjectResult(requestValidation.errorMessage.FirstOrDefault());

        Result res = await _productService.UpdateProduct(id, product.Price, product.Weight);

        if (res.IsNotSuccess && res.Error == "404")
        {
            return new NotFoundObjectResult($"Could not find product with id: {id}.");
        }
        if (res.IsNotSuccess)
            return StatusCode(StatusCodes.Status500InternalServerError, res.Error);
    
        return new NoContentResult();
    }
}