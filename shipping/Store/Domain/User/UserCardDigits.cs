using Store.Models;
namespace Store.Domain.User;
public class UserCardDigits : ValueObject
{
    private readonly string _value;
    public static Result<UserCardDigits> Create(Maybe<string> cardDigits)
    {
        return cardDigits.ToResult("Card number cannot be an empty string.")
            .OnSuccess(cardDigits => cardDigits.Trim())
            .Ensure(cardDigits => cardDigits.All(char.IsDigit), "Card number must only contain numbers.")
            .Ensure(cardDigits => cardDigits.Length == 3, "Card number must contain 3 digits.")
            .Map(cardDigits => new UserCardDigits(cardDigits));
    }

    protected UserCardDigits(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(UserCardDigits cardDigits) => cardDigits._value;
    public static explicit operator UserCardDigits(string str) => new UserCardDigits(str);
}