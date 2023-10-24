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
            byte[] data = Encoding.UTF8.GetBytes(request.Password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();
            tripDES.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            hash = Convert.ToBase64String(result);
            
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