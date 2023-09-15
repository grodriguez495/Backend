using AirQualityControlAPI.Domain.Repositories.Roles;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Queries.GetRoles
{
    public class GetRoleQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly IRoleQueryRepository _roleQueryRepository;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IRoleQueryRepository repository, IMapper mapper)
        {

            _roleQueryRepository = repository;
            _mapper = mapper;

        }
        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
               
                var entities = await _roleQueryRepository.ListAsync(cancellationToken: cancellationToken);
                var result = _mapper.Map<List<RoleDto>>(entities);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<RoleDto>();
            }
        }
    }
}
