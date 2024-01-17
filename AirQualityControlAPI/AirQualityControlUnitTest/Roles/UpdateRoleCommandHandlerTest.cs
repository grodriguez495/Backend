using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Application.Features.Roles.Commands.UpdateRole;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Roles;

[TestClass]
public class UpdateRoleCommandHandlerTest
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IMapper _mapper;
    private readonly UpdateRoleCommandHandler _updateRoleCommandHandler;

    public UpdateRoleCommandHandlerTest()
    {
        _roleQueryRepository = Mock.Of<IRoleQueryRepository>(MockBehavior.Strict);
        _roleCommandRepository = Mock.Of<IRoleCommandRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _updateRoleCommandHandler = new UpdateRoleCommandHandler(_roleQueryRepository, _roleCommandRepository, _mapper);
    }

    [TestMethod]
    public async Task GivenAExistingRole_UpdateRole()
    {
        var role = new Role(1,"test");
        var command = new UpdateRoleCommand()
        {
            RoleId = 1,
            Name = "test update"
        };
        var roleDto = new RoleDto();
        Mock.Get(_roleQueryRepository)
            .Setup(x => x.FindAsync(It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(role)
            .Verifiable();
        Mock.Get(_roleCommandRepository)
            .Setup(x => x.EditAsync(
                
                It.IsAny<Role>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<RoleDto>(role))
            .Returns(roleDto)
            .Verifiable();
        var response = await _updateRoleCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}