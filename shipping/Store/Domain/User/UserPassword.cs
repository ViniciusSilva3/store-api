using Store.Models;
namespace Store.Domain.User;

public class UserPassword : ValueObject
{
    private readonly string _value;

    public static Result<UserPassword> Create(Maybe<string> password)
    {
        return password.ToResult("Password cannot be an empty string.")
            .OnSuccess(password => password.Trim())
            .Ensure(password => password.Length >= 6, "Password must have at least 6 characters.")
            .Map(password => new UserPassword(password));
    }

    protected UserPassword(string value)
    {
        _value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(UserPassword password) => password._value;
    public static explicit operator UserPassword(string str) => new UserPassword(str);
}