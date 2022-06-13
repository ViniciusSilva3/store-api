using Store.Models;
namespace Store.Domain.User;
public class UserCardDate : ValueObject
{
    private readonly string _value;

    public static Result<UserCardDate> Create(Maybe<string> cardDate)
    {
        DateTime dateValue;
        return cardDate.ToResult("Card Date cannot be an empty string.")
            .OnSuccess(cardDate => cardDate.Trim())
            .Ensure(cardDate => DateTime.TryParse(cardDate, out dateValue), "Card Date must only contain Dates.")
            .OnSuccess(cardDate => DateTime.Parse(cardDate).ToShortDateString())
            .Map(cardDate => new UserCardDate(cardDate));
    }

    protected UserCardDate(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(UserCardDate cardDate) => cardDate._value;
    public static explicit operator UserCardDate(string str) => new UserCardDate(str);
}