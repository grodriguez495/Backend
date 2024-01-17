using AirQualityControlAPI.Application.Features.Users.Commands.DeleteUsers;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using Moq;

namespace AirQualityControlUnitTest.Users;

[TestClass]
public class DeleteUserCommandHandlerTest
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly DeleteUserCommandHandler _deleteUserCommandHandler;
   
    public DeleteUserCommandHandlerTest()
    {
        _userQueryRepository = Mock.Of<IUserQueryRepository>(MockBehavior.Strict);
        _userCommandRepository = Mock.Of<IUserCommandRepository>(MockBehavior.Strict);
        _deleteUserCommandHandler = new DeleteUserCommandHandler(_userQueryRepository, _userCommandRepository);
    }

    [TestMethod]
    public async Task GivenExistingUser_DeleteUser()
    {
        var user = User.NewUser("test", "test", 1, "test", true, "test");

        var command = new DeleteUserCommand()
        {
            Id = 1
        };
        Mock.Get(_userQueryRepository)
            .Setup(x =>x.FindAsync(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user)
            .Verifiable();
        Mock.Get(_userCommandRepository)
            .Setup(x => x.EditAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()))
            .Verifiable();
        var response = await _deleteUserCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}