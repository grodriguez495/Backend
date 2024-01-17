using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Application.Features.Users.Queries.GetUsers;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Users;

[TestClass]
public class GetUsersQueryHandlerTest
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetUsersQueryHandler _getUsersQueryHandler;

    public GetUsersQueryHandlerTest()
    {
        _userQueryRepository =  Mock.Of<IUserQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getUsersQueryHandler = new GetUsersQueryHandler(_userQueryRepository, _mapper);

    }

    [TestMethod]
    public async Task GivenAUserId_ReturnUser()
    {
        var user = User.NewUser("test", "test", 1, "test", true, "test");
        var userDto = new UserDto();
        var command = new GetUsersQuery();
        Mock.Get(_userQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<User>(){user})
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<List<UserDto>>(new List<User>(){user} ))
            .Returns( new List<UserDto>(){userDto} )
            .Verifiable();
        var response = await _getUsersQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
            
    }
}