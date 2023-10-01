using System.Security.Cryptography;
using System.Text;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Logins.Query.LogIns;

public class LoginQueryHandler :IRequestHandler<LoginQuery,LoginDto?>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    public LoginQueryHandler(IUserQueryRepository userQueryRepository, IMapper mapper)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async  Task<LoginDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
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
            var user = await _userQueryRepository.ListAsync(x => x.Email == request.Email &&
                                                                      x.Password == hash &&
                                                                      x.IsActive == true, false, cancellationToken);
           Console.WriteLine($"salio: {user.FirstOrDefault().Name} ");
            return user.Any() ? _mapper.Map<LoginDto>(user.FirstOrDefault()) : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}