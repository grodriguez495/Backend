using System.Security.Cryptography;
using System.Text;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.CreateUsers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,bool>
{
    private readonly IUserCommandRepository _userCommandRepository;
    public CreateUserCommandHandler(IUserCommandRepository userCommandRepository)
    {
        _userCommandRepository = userCommandRepository ?? throw new ArgumentNullException(nameof(userCommandRepository));
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hash = string.Empty;
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(request.Password);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                 hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
            
            var entity = User.NewUser(request.Name, hash, request.RoleId,request.Email,  true,
                request.Phone);

            var entityCreated = await _userCommandRepository.RegisterAsync(entity, cancellationToken);
            return entityCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}