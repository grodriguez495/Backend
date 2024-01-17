using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Application.Features.Users.Queries.GetUser;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Users;

[TestClass]
public class GetUserQueryHandlerTest
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetUserQueryHandler _getUserQueryHandler;

    public GetUserQueryHandlerTest()
    {
        _userQueryRepository =  Mock.Of<IUserQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getUserQueryHandler = new GetUserQueryHandler(_userQueryRepository, _mapper);
    }

    [TestMethod]
    public async Task GivenAUserId_ReturnUser()
    {
        var user = User.NewUser("test", "test", 1, "test", true, "test");
        var userDto = new UserDto();
        var command = new GetUserQuery(1);
        Mock.Get(_userQueryRepository)
            .Setup(x => x.GetActiveUserAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user)
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<UserDto>(user ))
            .Returns( userDto )
            .Verifiable();
        var response = await _getUserQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}