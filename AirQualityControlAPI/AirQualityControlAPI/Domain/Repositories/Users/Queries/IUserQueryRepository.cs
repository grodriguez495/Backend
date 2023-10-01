using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Users.Queries;

public interface IUserQueryRepository:IQueryRepository<User,int>
{
    Task<User> GetActiveUserAsync(Expression<Func<User,bool>> predicate,CancellationToken cancellationToken);
}