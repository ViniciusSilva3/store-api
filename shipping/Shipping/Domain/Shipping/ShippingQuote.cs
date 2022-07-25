namespace Shipping.Domain.Shipping;

public class ShippingQuote
{
    public Price Value { get; set; }
    public Currency CurrencyCode { get; set; }

    public ShippingQuoteModal ToShippingQuoteModal()
    {
        return new ShippingQuoteModal() { Value = (double)Value, CurrencyCode = (string) CurrencyCode };
    }
}

public class ShippingQuoteModal
{
    public double Value { get; set; }
    public string CurrencyCode { get; set; }
}