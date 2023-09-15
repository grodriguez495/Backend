using AirQualityControlAPI.Domain.Repositories.Roles;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto>
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IMapper _mapper;
    
    
    public GetRoleQueryHandler(IRoleQueryRepository roleQueryRepository, IMapper mapper)
    {
        _roleQueryRepository = roleQueryRepository ?? throw new ArgumentNullException(nameof(roleQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleQueryRepository.FindAsync(request.RoleId, false, cancellationToken);
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}