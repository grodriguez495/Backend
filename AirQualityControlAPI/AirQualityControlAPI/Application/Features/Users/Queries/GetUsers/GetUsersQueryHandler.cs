using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,List<UserDto>>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    
    public GetUsersQueryHandler(IUserQueryRepository userQueryRepository,IMapper mapper)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _userQueryRepository.ListAsync(cancellationToken:cancellationToken);
            var result = _mapper.Map<List<UserDto>>(entities);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<UserDto>();
        }
    }
}