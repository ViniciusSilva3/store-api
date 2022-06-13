using Store.Models;
namespace Store.Domain.User;
public class UserCpf : ValueObject {
    private readonly string _value;

    private UserCpf(string value)
    {
        _value = value;
    }
    public static Result<UserCpf> Create(Maybe<string> userCpfOrNothing)
    {
        return userCpfOrNothing.ToResult("cpf cannot be empty.")
            .OnSuccess(cpf => cpf.Trim())
            .Ensure(cpf => cpf.Length == 9, "cpf must contain 9 digits.")
            .Ensure(cpf => cpf.All(char.IsDigit), "cpf can only contain numbers.")
            .Map(cpf => new UserCpf(cpf));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
    public static implicit operator string(UserCpf cpf) => cpf._value;
    public static explicit operator UserCpf(string str) => new UserCpf(str);
}