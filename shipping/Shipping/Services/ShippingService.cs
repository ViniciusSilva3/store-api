using Shipping.Domain.Shipping;
using Shipping.API.Entities;
using Shipping.Models;
using Shipping.Clients;
using Shipping.Domain;

namespace Shipping.Services;

public interface IShippingService
{
    Task<Result<ShippingQuote>> CalculateShippingQuote(List<CartProduct> productList);
}

public class ShippingService : IShippingService
{
    
    private readonly CatalogClient _catalogClient;

    public ShippingService(CatalogClient catalogClient)
    {
        _catalogClient = catalogClient;
    }

    public async Task<Result<ShippingQuote>> CalculateShippingQuote(List<CartProduct> productList)
    {
        double totalCost = 0;
        foreach(CartProduct item in productList)
        {
            Result<Product> product = await _catalogClient.GetProductById(item.ProductId);
            if (product.IsNotSuccess)
            {
                return Result.Fail<ShippingQuote>(product.Error);
            }
            totalCost += (double)(item.Quantity * product.Value.Weight);
        }
        Result<Currency> currencyCode = Currency.Create("Real");
        Result<Price> price = Price.Create(totalCost);

        Result result = Result.Combine(currencyCode, price);

        if (result.IsNotSuccess)
        {
            return Result<ShippingQuote>.Fail<ShippingQuote>(result.Error);
        }

        return Result.Ok<ShippingQuote>(new ShippingQuote() { CurrencyCode = currencyCode.Value, Value = price.Value });
    }
}