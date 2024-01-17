using AirQualityControlAPI.Application.Features.Users.Commands.CreateUsers;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using Moq;

namespace AirQualityControlUnitTest.Users;

[TestClass]
public class CreateUserCommandHandlerTest
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly CreateUserCommandHandler _createUserCommandHandler;
    
    public CreateUserCommandHandlerTest()
    {
        _userCommandRepository = Mock.Of<IUserCommandRepository>(MockBehavior.Strict);
        _createUserCommandHandler = new CreateUserCommandHandler(_userCommandRepository);
    }

    [TestMethod]
    public async Task GivenCreateCommand_CreateNewUser()
    {
        var command = new CreateUserCommand()
        {
            Name = "test",
            Password = "test",
            Email = "test",
            RoleId = 1,
            Phone = "test"
        };
        Mock.Get(_userCommandRepository)
            .Setup(x => x.RegisterAsync(
            It.IsAny<User>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();
        var response = await _createUserCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsTrue(response);


    }
}