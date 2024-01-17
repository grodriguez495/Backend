using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Application.Features.Roles.Queries.GetRoles;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Roles;

[TestClass]
public class GetRolesQueryHandlerTest
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetRoleQueryHandler _getRoleQueryHandler;

    public GetRolesQueryHandlerTest()
    {
        _roleQueryRepository =  Mock.Of<IRoleQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getRoleQueryHandler = new GetRoleQueryHandler(_roleQueryRepository, _mapper);

    }

    [TestMethod]
    public async Task GivenARolesIds_ReturnRoleList()
    {
        var role = new Role(1, "test");
        var roleDto = new RoleDto();
        var command = new GetRolesQuery();
        Mock.Get(_roleQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<Role, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Role>() { role })
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<List<RoleDto>>(new List<Role>() { role }))
            .Returns(new List<RoleDto>() { roleDto })
            .Verifiable();
        var response = await _getRoleQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}