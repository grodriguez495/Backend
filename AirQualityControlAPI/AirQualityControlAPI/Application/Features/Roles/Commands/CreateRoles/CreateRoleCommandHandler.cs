using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.CreateRoles;

public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand,bool>
{
    private readonly IRoleCommandRepository _roleCommandRepository;
    public CreateRoleCommandHandler(IRoleCommandRepository roleCommandRepository)
    {
        _roleCommandRepository = roleCommandRepository ?? throw new ArgumentNullException(nameof(roleCommandRepository));
    }

    public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = Role.NewRole(request.Name);
            var createdResult = await _roleCommandRepository.RegisterAsync(entity, cancellationToken);
            return createdResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}