using Store.Models;
namespace Store.Domain.Shipping;
public class ProductQuantity : ValueObject
{
    private readonly int _value;

    public static Result<ProductQuantity> Create(int ProductQuantityValue)
    {
        if (ProductQuantityValue > 0)
            return Result.Ok<ProductQuantity>(new ProductQuantity(ProductQuantityValue));
        return Result.Fail<ProductQuantity>("ProductQuantity cannot be a negative value.");
    }

    protected ProductQuantity(int value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator int(ProductQuantity ProductQuantityCode) => ProductQuantityCode._value;
    public static explicit operator ProductQuantity(int str) => new ProductQuantity(str);
}