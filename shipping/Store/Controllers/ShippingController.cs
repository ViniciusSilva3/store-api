using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;
using Store.API.Entities;
using Store.Domain.Shipping;


namespace Store.Controllers;

[ApiController]
[Route("api/shipping")]
public class ShippingController : ControllerBase
{
    private readonly IShippingService _shippingService;
    public ShippingController(IShippingService shippingService)
    {
        _shippingService = shippingService;
    }
    /*
        rotas
        getQuote -> receives items, based on number of items returns price for route
    */
        [HttpPost]
        [Route("quote")]
        public ActionResult<ShippingQuote> CalculateShippingQuote(CartModal cart)
        {
            List<Product> validProductList = new List<Product>();
            foreach (Product product in cart.ProductList)
            {
                Result<ProductId> productId = ProductId.Create(product.ProductId);
                Result<ProductQuantity> productQuantity = ProductQuantity.Create(product.Quantity);

                Result result = Result.Combine(productId, productQuantity);

                if (result.IsNotSuccess)
                {
                    return new BadRequestObjectResult(result.Error);
                }
                validProductList.Add(new Product() { ProductId = productId.Value, Quantity = productQuantity.Value});
            }

            Result<ShippingQuote> quote = _shippingService.CalculateShippingQuote(validProductList);

            if (quote.IsNotSuccess)
            {
                return StatusCode(500, quote.Error);
            }
            
            return new OkObjectResult(quote.Value.ToShippingQuoteModal());
        }

    /*
    shipOrder -> receives delivery address, cart -> createes package and returns tracking information
    */

}