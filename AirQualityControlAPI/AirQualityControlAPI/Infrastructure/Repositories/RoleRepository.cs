using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;

namespace AirQualityControlAPI.Infrastructure.Repositories
{
    public class RoleRepository :
        BaseRepository<Role, int, IAirQualityControlDbContext>,
        IRoleQueryRepository,
        IRoleCommandRepository
    {
        public RoleRepository(IAirQualityControlDbContext dbContext) : base(dbContext)
        {

        }

        protected override DbSet<Role> Entities => _dbContext.Role;

        protected override IQueryable<Role> DefaultIQueryable() => Entities.AsQueryable();

        protected override Expression<Func<Role, bool>> FindExpression(int key)
        {
            return role => role.RoleId == key;
        }
    }
}
