using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Repositories;

public class UserRepository :
    BaseRepository<User, int, IAirQualityControlDbContext>,
    IUserQueryRepository,
    IUserCommandRepository
{
    public UserRepository(IAirQualityControlDbContext dBContext) : base(dBContext)
    {
    }

    protected override DbSet<User> Entities => _dbContext.User;
    protected override IQueryable<User> DefaultIQueryable() => Entities
        .Include(x=>x.Role).AsQueryable();

    protected override Expression<Func<User, bool>> FindExpression(int key)
    {
        return user => user.UserId == key;
    }

    public async Task<User> GetActiveUserAsync(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = Entities
            .Include(x=>x.Role).AsNoTracking();
        return await query.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
}