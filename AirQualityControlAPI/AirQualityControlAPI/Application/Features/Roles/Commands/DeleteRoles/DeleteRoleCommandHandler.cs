using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.DeleteRoles;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand,bool>
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;

    public DeleteRoleCommandHandler(IRoleCommandRepository roleCommandRepository, IRoleQueryRepository roleQueryRepository)
    {
        _roleCommandRepository = roleCommandRepository ?? throw new ArgumentNullException(nameof(roleCommandRepository));
        _roleQueryRepository = roleQueryRepository ?? throw new ArgumentNullException(nameof(roleQueryRepository));
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentRole = await _roleQueryRepository.FindAsync(request.Id, false, cancellationToken);
            return await _roleCommandRepository.DeleteAsync(currentRole,cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}