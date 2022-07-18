using Shipping.Models;
namespace Shipping.Domain.Shipping;
public class ProductId : ValueObject
{
    private readonly string _value;

    public static Result<ProductId> Create(Maybe<string> productIdCode)
    {
        return productIdCode.ToResult("ProductId code cannot be an empty string.")
            .OnSuccess(productIdCode => productIdCode.Trim())
            .Ensure(productIdCode => productIdCode.Length <= 33, "productId code must have at most 5 letters.")
            .Map(productIdCode => new ProductId(productIdCode));
    }

    protected ProductId(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(ProductId currencyCode) => currencyCode._value;
    public static explicit operator ProductId(string str) => new ProductId(str);
}