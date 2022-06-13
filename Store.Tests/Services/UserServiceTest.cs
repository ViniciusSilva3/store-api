using Xunit;
using Store.Db;
using Store.Models;
using Moq;
using Store.Services;
using Store.Domain.User;
public class UserServiceTests
{
    private readonly Mock<IUserDatabaseManager> userDbManager = new Mock<IUserDatabaseManager>();
    private readonly Mock<IPersister> persister = new Mock<IPersister>();
    private IUserService userService;

    public UserServiceTests()
    {
        userService = new UserService(persister.Object, userDbManager.Object);
    }
    [Fact]
    public void should_return_ok_result_if_user_is_created_successfully()
    {
        userDbManager.Setup(userDbManager => userDbManager.AddUser(It.IsAny<FileContent>(), It.IsAny<User>()))
            .Returns(new FileOperation(new string[]{}, "newFile", OperationType.WRITE));
        persister.Setup(persister => persister.ApplyChanges(It.IsAny<FileOperation>()));

        Result result = userService.CreateUser(It.IsAny<User>());

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void should_return_failed_result_if_user_is_not_created_successfully()
    {
        userDbManager.Setup(userDbManager => userDbManager.AddUser(It.IsAny<FileContent>(), It.IsAny<User>()))
            .Returns<FileContent>(null);
        persister.Setup(persister => persister.ApplyChanges(It.IsAny<FileOperation>()));

        Result result = userService.CreateUser(It.IsAny<User>());

        Assert.True(result.IsNotSuccess);
    }
}
