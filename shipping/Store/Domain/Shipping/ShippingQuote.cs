using Store.API.Entities;
namespace Store.Domain.Shipping;

public class ShippingQuote
{
    public Price Value { get; set; }
    public Currency CurrencyCode { get; set; }
}