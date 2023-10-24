using System.Security.Cryptography;
using System.Text;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler( IUserQueryRepository userQueryRepository, IMapper mapper)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userQueryRepository.GetActiveUserAsync(x => x.UserId == request.UserId, cancellationToken);
            if (user == null)
                return new UserDto();
            user = DecodeUserPassword(user);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new UserDto();
        }
    }

    private User DecodeUserPassword(User user)
    {
        string hash = String.Empty;
        ;
        byte[] data = Convert.FromBase64String(user.Password);
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();
        tripDES.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
        tripDES.Mode = CipherMode.ECB;
        ICryptoTransform transform = tripDES.CreateDecryptor();
        byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
        user.Password = Encoding.UTF8.GetString(result);
        return user;
    }
}