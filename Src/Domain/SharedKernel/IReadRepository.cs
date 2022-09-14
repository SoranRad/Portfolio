using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;

namespace Domain.SharedKernel
{
    public interface IReadRepository<TEntity> where TEntity : IAggregateRoot
    {
         Task<TEntity> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync 
            (
                Expression<Func<TEntity, bool>> predicate,
               string include
            );

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        PagedResult<TEntity> GetPaged
        (
            PagedResult<TEntity> Param,
            Expression<Func<TEntity, bool>> predicate,
            string include
        );
    }
}
