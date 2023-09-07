using AirQualityControlAPI.Domain.Repositories.Roles;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RoleDto>>
    {
        private readonly IRoleQueryRepository _roleQueryRepository;
        private readonly IMapper _mapper;
        public GetRoleQueryHandler(IRoleQueryRepository repository, IMapper mapper) 
        {
            _mapper = mapper;
            _roleQueryRepository = repository;

        }    
        public Task<List<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
