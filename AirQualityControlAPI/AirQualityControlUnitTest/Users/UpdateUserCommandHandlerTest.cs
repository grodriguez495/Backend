using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Application.Features.Users.Commands.UpdateUsers;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Users;

[TestClass]
public class UpdateUserCommandHandlerTest
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    private readonly UpdateUserCommandHandler _updateUserCommandHandler;
    
    public UpdateUserCommandHandlerTest()
    {
        _userCommandRepository = Mock.Of<IUserCommandRepository>(MockBehavior.Strict);
        _userQueryRepository = Mock.Of<IUserQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _updateUserCommandHandler = new UpdateUserCommandHandler(_userCommandRepository, _userQueryRepository, _mapper);
    }

    [TestMethod]
    public async Task GivenAExistingUser_UpdateUser()
    {
        var user = User.NewUser("test", "test", 1, "test", true, "test");
        var userDto = new UserDto();
        var command = new UpdateUserCommand()
        {
            UserId = 1,
            Name = "test",
            Password = "test",
            Email = "test",
            RoleId = 1,
            Phone = "test",
            IsActive = true
        };
        Mock.Get(_userQueryRepository)
            .Setup(x=>x.FindAsync(
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
        Mock.Get(_mapper)
            .Setup(x => x.Map<UserDto>(user ))
            .Returns( userDto )
            .Verifiable();
        var response = await _updateUserCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}