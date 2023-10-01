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
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new UserDto();
        }
    }
}