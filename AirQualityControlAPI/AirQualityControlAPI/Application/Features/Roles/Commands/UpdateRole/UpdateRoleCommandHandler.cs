using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler: IRequestHandler<UpdateRoleCommand,RoleDto>
{
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IMapper _mapper;
    
    
    public UpdateRoleCommandHandler(IRoleQueryRepository roleQueryRepository,IRoleCommandRepository roleCommandRepository,IMapper mapper)
    {
        _roleQueryRepository = roleQueryRepository ?? throw new ArgumentNullException(nameof(roleQueryRepository));
        _roleCommandRepository = roleCommandRepository ?? throw new ArgumentNullException(nameof(roleCommandRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var currentRole = await _roleQueryRepository.FindAsync(request.RoleId, false, cancellationToken);
        currentRole.Name = request.Name;
        var response = await _roleCommandRepository.EditAsync(currentRole, cancellationToken);
        if (response)
        {
            currentRole = await _roleQueryRepository.FindAsync(request.RoleId, false, cancellationToken);

        }
        var result = _mapper.Map<RoleDto>(currentRole);
        return result;
    }
}