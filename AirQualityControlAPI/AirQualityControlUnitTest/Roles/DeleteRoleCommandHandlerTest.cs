using AirQualityControlAPI.Application.Features.Roles.Commands.DeleteRoles;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using Moq;

namespace AirQualityControlUnitTest.Roles;
[TestClass]
public class DeleteRoleCommandHandlerTest
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly DeleteRoleCommandHandler _deleteRoleCommandHandler;

    public DeleteRoleCommandHandlerTest()
    {
        _roleCommandRepository = Mock.Of<IRoleCommandRepository>(MockBehavior.Strict);
        _roleQueryRepository = Mock.Of<IRoleQueryRepository>();
        _deleteRoleCommandHandler = new DeleteRoleCommandHandler(_roleCommandRepository, _roleQueryRepository);

    }

    [TestMethod]
    public async Task GivenExistingRole_DeletRole()
    {
        var role = new Role(1,"test");
        var command = new DeleteRoleCommand()
        {
            Id = 1
        };
        Mock.Get(_roleQueryRepository)
            .Setup(x => x.FindAsync(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(role)
            .Verifiable();
        Mock.Get(_roleCommandRepository)
            .Setup(x => x.DeleteAsync(
                It.IsAny<Role>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();
        var response = await _deleteRoleCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsTrue(response);

    }
}