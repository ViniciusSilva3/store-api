using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;
using Store.API.Entities;
using Store.Domain.User;


namespace Store.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<UserModal> GetUserById(string id)
    {
        Result<UserCpf> cpf = UserCpf.Create(id);
        if (cpf.IsNotSuccess)
            return new BadRequestObjectResult(cpf.Error);
        
        Result<User> user = _userService.GetUser(cpf.Value);
        if (user.IsNotSuccess)
            return new NotFoundObjectResult(user.Error);
        
        return new OkObjectResult(user.Value.toUserModal());
    }

    [HttpGet]
    public ActionResult<List<UserModal>> GetAllUsers()
    { 
        Result<List<User>> userList = _userService.GetAllUsers();
        if (userList.IsNotSuccess)
            return new NotFoundObjectResult(userList.Error);
        
        List<UserModal> userModalList = userList.Value.Select(user => user.toUserModal()).ToList();
        
        return new OkObjectResult(userModalList);
    }

    [HttpPost]
    public ActionResult CreateUser(UserModal user)
    {
        Result<UserCpf> cpf = UserCpf.Create(user.cpf);
        Result<UserPassword> password = UserPassword.Create(user.password);
        Result<UserCardNumber> cardNumber = UserCardNumber.Create(user.cardNumber);
        Result<UserCardDigits> cardDigits = UserCardDigits.Create(user.cardDigits);
        Result<UserCardDate> cardDate = UserCardDate.Create(user.cardDate);

        Result result = Result.Combine(cpf, password, cardDate, cardDigits, cardNumber);

        if (result.IsNotSuccess)
            return new BadRequestObjectResult(result.Error);
        
        User newUser = new User(cpf.Value, cardDate.Value, cardDigits.Value, cardNumber.Value, password.Value);

        var createUserOperation = _userService.CreateUser(newUser);
        if (createUserOperation.IsNotSuccess)
            return new BadRequestObjectResult(createUserOperation.Error);

        return new OkResult();
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult UpdateUser(UserModal user, string id)
    {
        // must get user cpf by the route parameter, cannot update CPF field!
        Result<UserCpf> cpf = UserCpf.Create(id);
        Result<UserPassword> password = UserPassword.Create(user.password);
        Result<UserCardNumber> cardNumber = UserCardNumber.Create(user.cardNumber);
        Result<UserCardDigits> cardDigits = UserCardDigits.Create(user.cardDigits);
        Result<UserCardDate> cardDate = UserCardDate.Create(user.cardDate);

        Result result = Result.Combine(cpf, password, cardDate, cardDigits, cardNumber);

        if (result.IsNotSuccess)
            return new BadRequestObjectResult(result.Error);

        User newUser = new User(cpf.Value, cardDate.Value, cardDigits.Value, cardNumber.Value, password.Value);

        OperationType updateUserOperation = _userService.UpdateUser(newUser);
        if (updateUserOperation == OperationType.UPDATE)
            return new NoContentResult();
        
        return new OkResult();
    }
}
