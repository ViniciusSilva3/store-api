using Shipping.Models;
namespace Shipping.Domain.Shipping;
public class Price : ValueObject
{
    private readonly float _value;

    public static Result<Price> Create(float priceValue)
    {
        if (priceValue > 0)
            return Result.Ok<Price>(new Price(priceValue));
        return Result.Fail<Price>("Price cannot be a negative value.");
    }

    protected Price(float value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator float(Price PriceCode) => PriceCode._value;
    public static explicit operator Price(float str) => new Price(str);
}