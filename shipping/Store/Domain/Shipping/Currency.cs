using Store.Models;
namespace Store.Domain.Shipping;
public class Currency : ValueObject
{
    private readonly string _value;

    public static Result<Currency> Create(Maybe<string> currencyCode)
    {
        return currencyCode.ToResult("Currency code cannot be an empty string.")
            .OnSuccess(currencyCode => currencyCode.Trim())
            .Ensure(currencyCode => currencyCode.Length <= 5, "Currency code must have at most 5 letters.")
            .Map(currencyCode => new Currency(currencyCode));
    }

    protected Currency(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(Currency currencyCode) => currencyCode._value;
    public static explicit operator Currency(string str) => new Currency(str);
}