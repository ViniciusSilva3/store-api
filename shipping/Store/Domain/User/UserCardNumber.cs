using Store.Models;
namespace Store.Domain.User;
public class UserCardNumber : ValueObject
{
    private readonly string _value;

    public static Result<UserCardNumber> Create(Maybe<string> cardNumber)
    {
        return cardNumber.ToResult("Card number cannot be an empty string.")
            .OnSuccess(cardNumber => cardNumber.Trim())
            .Ensure(cardNumber => cardNumber.All(char.IsDigit), "Card number must only contain numbers.")
            .Ensure(cardNumber => cardNumber.Length == 10, "Card number must contain 10 digits.")
            .Map(cardNumber => new UserCardNumber(cardNumber));
    }

    protected UserCardNumber(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(UserCardNumber cardNumber) => cardNumber._value;
    public static explicit operator UserCardNumber(string str) => new UserCardNumber(str);
}