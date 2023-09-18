using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.DeleteUsers;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand,bool>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    
    public DeleteUserCommandHandler(IUserQueryRepository userQueryRepository, IUserCommandRepository userCommandRepository 
         )
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _userCommandRepository = userCommandRepository ?? throw new ArgumentNullException(nameof(userCommandRepository));
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = await _userQueryRepository.FindAsync(request.Id,true, cancellationToken);
            currentUser.IsActive = !currentUser.IsActive;
            return await _userCommandRepository.EditAsync(currentUser, cancellationToken: cancellationToken);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}