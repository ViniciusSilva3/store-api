using Shipping.Domain.Shipping;
using Shipping.API.Entities;
using Shipping.Models;

namespace Shipping.Services;

public interface IShippingService
{
    Result<ShippingQuote> CalculateShippingQuote(List<Product> productList);
}

public class ShippingService : IShippingService
{
    
    public Random _rand;

    public ShippingService()
    {
        _rand = new Random();
    }

    public Result<ShippingQuote> CalculateShippingQuote(List<Product> productList)
    {
        float totalCost = 0;
        foreach(Product product in productList)
        {
            totalCost += (float)(product.Quantity * _rand.NextDouble());
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