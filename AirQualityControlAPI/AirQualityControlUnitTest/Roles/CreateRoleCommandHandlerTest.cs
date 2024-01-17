using AirQualityControlAPI.Application.Features.Roles.Commands.CreateRoles;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using Moq;

namespace AirQualityControlUnitTest.Roles;
[TestClass]
public class CreateRoleCommandHandlerTest
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly CreateRoleCommandHandler _createRoleCommandHandler;

    public CreateRoleCommandHandlerTest()
    {
        _roleCommandRepository = Mock.Of<IRoleCommandRepository>(MockBehavior.Strict);
        _createRoleCommandHandler = new CreateRoleCommandHandler(_roleCommandRepository);
    }

    [TestMethod]
    public async Task GivenACommand_CreateNewRole()
    {
        var command = new CreateRoleCommand()
        {
            Name = "test"
        };
        Mock.Get(_roleCommandRepository)
            .Setup(x => x.RegisterAsync(It.IsAny<Role>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        var result = await _createRoleCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsTrue(result);
    }
}