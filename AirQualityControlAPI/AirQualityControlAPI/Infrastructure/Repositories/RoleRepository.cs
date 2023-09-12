using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirQualityControlAPI.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role, int, IAirQualityControlDbContext>, IRoleQueryRepository
    {
        public RoleRepository(IAirQualityControlDbContext dbContext):base(dbContext)
        {

        }
        protected override DbSet<Role> Entities => _dbContext.Roles;

        protected override IQueryable<Role> DefaultIQueryable() => Entities.AsQueryable();

        protected override Expression<Func<Role, bool>> FindExpression(int key)
        {
            return roles => roles.RoleId == key;
        }
    }
}
