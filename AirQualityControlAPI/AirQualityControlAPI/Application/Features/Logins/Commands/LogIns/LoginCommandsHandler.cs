using System.Security.Cryptography;
using System.Text;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Logins.Commands.LogIns;

public class LoginCommandsHandler :IRequestHandler<LoginCommand,LoginDto?>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
   
    public LoginCommandsHandler(IUserQueryRepository userQueryRepository, IMapper mapper)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async  Task<LoginDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string hash = String.Empty;
            byte[] data = Encoding.UTF8.GetBytes(request.Password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();
            tripDES.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            hash = Convert.ToBase64String(result);
            var user = await _userQueryRepository.ListAsync(x => x.Email == request.Email &&
                                                                      x.Password == hash &&
                                                                      x.IsActive == true, false, cancellationToken);
            return user.Any() ? _mapper.Map<LoginDto>(user.FirstOrDefault()) : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}