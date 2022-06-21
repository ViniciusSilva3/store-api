using Xunit;
using Store.Db;
using Store.Models;
using Store.Domain.User;
public class UserDatabaseTests
{
    [Fact]
    public void should_create_new_user_if_the_user_does_not_exist_on_create_user()
    {
        var dbClient = new UserDatabaseManager();
        var testUser = new User(
            UserCpf.Create("123456789").Value, 
            UserCardDate.Create("2020-02-01").Value,
            UserCardDigits.Create("123").Value,
            UserCardNumber.Create("0123456789").Value,
            UserPassword.Create("adminpassword").Value);
        var fileContent = new FileContent(new string[] {
            "222222222;password;01/04/2021;444;0123456789;"
        }, "file");

        Maybe<FileOperation> operation = dbClient.AddUser(fileContent, testUser);

        Assert.True(operation.HasValue);
        if (operation.HasValue)
        {
            Assert.Equal(OperationType.WRITE, operation.Value.Type);
            Assert.Equal(new[] {
                "222222222;password;01/04/2021;444;0123456789",
                "123456789;adminpassword;01/02/2020;123;0123456789"
            }, operation.Value.Content);
            Assert.Equal("file", operation.Value.FileName);
        }
    }
    [Fact]
    public void should_return_null_if_creating_already_existing_user_on_create_user()
    {
        var dbClient = new UserDatabaseManager();
        var testUser = new User(
            UserCpf.Create("123456789").Value, 
            UserCardDate.Create("2020-02-01").Value,
            UserCardDigits.Create("123").Value,
            UserCardNumber.Create("0123456789").Value,
            UserPassword.Create("adminpassword").Value);
        var fileContent = new FileContent(new string[] {
            "222222222;password;01/04/2021;444;0123456789",
            "123456789;adminpassword;01/02/2020;123;0123456789"
        }, "file");

        Maybe<FileOperation> operation = dbClient.AddUser(fileContent, testUser);

        Assert.True(operation.HasNoValue);
    }

    [Fact]
    public void should_update_existing_user()
    {
        var dbClient = new UserDatabaseManager();
        var testUser = new User(
            UserCpf.Create("123456789").Value, 
            UserCardDate.Create("2022-05-06").Value,
            UserCardDigits.Create("555").Value,
            UserCardNumber.Create("9876543210").Value,
            UserPassword.Create("newPassword").Value);
        
        var fileContent = new FileContent(new string[] {
            "222222222;password;01/04/2021;444;0123456789",
            "123456789;adminpassword;01/02/2020;123;0123456789",
            "222223333;password-test;02/03/2021;333;0123454444"
        }, "file");

        Maybe<FileOperation> operation = dbClient.UpdateUser(fileContent, testUser);

        Assert.True(operation.HasValue);
        if (operation.HasValue)
        {
            Assert.Equal(OperationType.UPDATE, operation.Value.Type);
            Assert.Equal(new[] {
                "222222222;password;01/04/2021;444;0123456789",
                "123456789;newPassword;06/05/2022;555;9876543210",
                "222223333;password-test;02/03/2021;333;0123454444"
            }, operation.Value.Content);
            Assert.Equal("file", operation.Value.FileName);
        }
    }
}
