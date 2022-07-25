using Shipping.Models;
namespace Shipping.Domain.Shipping;
public class Price : ValueObject
{
    private readonly double _value;

    public static Result<Price> Create(double priceValue)
    {
        if (priceValue > 0)
            return Result.Ok<Price>(new Price(priceValue));
        return Result.Fail<Price>("Price cannot be a negative value.");
    }

    protected Price(double value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator double(Price PriceCode) => PriceCode._value;
    public static explicit operator Price(double str) => new Price(str);
}