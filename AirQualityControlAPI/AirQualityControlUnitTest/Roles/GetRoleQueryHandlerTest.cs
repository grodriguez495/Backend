using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Application.Features.Roles.Queries.GetRole;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Roles;

[TestClass]
public class GetRoleQueryHandlerTest
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetRoleQueryHandler _getRoleQueryHandler;

    public GetRoleQueryHandlerTest()
    {
        _roleQueryRepository =  Mock.Of<IRoleQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getRoleQueryHandler = new GetRoleQueryHandler(_roleQueryRepository, _mapper);
    }
    [TestMethod]
    public async Task GivenARoleId_ReturnRole()
    {
        var role = new Role(1,"test");
        var roleDto = new RoleDto();
        var command = new GetRoleQuery(1);
        
        Mock.Get(_roleQueryRepository)
            .Setup(x => x.FindAsync(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(role)
            .Verifiable();
        
        Mock.Get(_mapper)
            .Setup(x => x.Map<RoleDto>(role))
            .Returns(roleDto)
            .Verifiable();
        var response = await _getRoleQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}