using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Logins;
using AirQualityControlAPI.Application.Features.Logins.Commands.LogIns;
using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Application.Features.Users.Queries.GetUser;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace AirQualityControlUnitTest.Logins;

[TestClass]
public class LoginCommandsHandlerTest
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    private readonly LoginCommandsHandler _loginCommandsHandler;

    public LoginCommandsHandlerTest()
    {
        _userQueryRepository = Mock.Of<IUserQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _loginCommandsHandler = new LoginCommandsHandler(_userQueryRepository, _mapper);

    }

    [TestMethod]
    public async Task GivenAUser_WhenTryLogin_RejectLogin()
    {
        var user = GetUser();
        var Dto = new UserDto();

        Mock.Get(_userQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(user))
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<UserDto>( user.First() ))
            .Returns( Dto)
            .Verifiable();
        var request = new LoginCommand("test@gmail.com", "test");
        var userDto = await _loginCommandsHandler.Handle(request, CancellationToken.None);
        Assert.IsNull(userDto);
        Assert.AreNotEqual(Dto, userDto);

    }

    private static IEnumerable<User> GetUser()
    {
        var userList = new List<User>();
        User user = new User(1, "test", "test", 1, "test@gmail.com", true, "1234567890");
        userList.Add(user);
        return userList;
    }

    [TestMethod]
    public async Task GivenAUser_WhenTryLogin_AllowLogin()
    {
        var user = GetUser();
        var dto = new LoginDto();

        Mock.Get(_userQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(user))
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<LoginDto>( user.First() ))
            .Returns( dto)
            .Verifiable();
        var request = new LoginCommand("test@gmail.com", "test");
        var userDto = await _loginCommandsHandler.Handle(request, CancellationToken.None);
        Assert.IsNotNull(userDto);
        Assert.AreEqual(dto, userDto);
    }
}