using Store.API.Entities;
namespace Store.Domain.User;

public class User
{
    public UserCpf Cpf { get; set; }
    public UserCardDate CardDate { get; set; }
    public UserCardDigits CardDigits { get; set; }
    public UserCardNumber CardNumber { get; set; }
    public UserPassword Password { get; set; }

    public User(UserCpf cpf, UserCardDate cardDate, UserCardDigits cardDigits,
        UserCardNumber cardNumber, UserPassword password)
    {
        Cpf = cpf;
        CardDate = cardDate;
        CardDigits = cardDigits;
        CardNumber = cardNumber;
        Password = password;
    }

    public UserModal toUserModal()
    {
        return new UserModal()
        {
            cpf = (string)Cpf,
            cardDate = (string)CardDate,
            cardDigits = (string)CardDigits,
            cardNumber = (string)CardNumber,
            password = (string)Password
        };
    }
}
