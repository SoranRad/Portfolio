using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using Domain.SeedWork;
using Domain.SharedKernel;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{

    public class ReadRepository<TEntity> : IGenericDependency, IReadRepository<TEntity>where TEntity : class,IAggregateRoot


    {
        private readonly PostContext _dbContext;

        public ReadRepository(PostContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync 
            (
                Expression<Func<TEntity, bool>> predicate,
                string include
            )
        {
            var query = _dbContext.Set<TEntity>().Where(predicate);

            if (string.IsNullOrWhiteSpace(include))
            {
                foreach (var item in include.Split(","))
                    query = query.Include(item);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public  PagedResult<TEntity> GetPaged  
            (
                PagedResult<TEntity> Param,
                Expression<Func<TEntity, bool>> predicate,
                string include
            )
            
        {
            var query = _dbContext.Set<TEntity>().Where(predicate);

            if (string.IsNullOrWhiteSpace(include))
            {
                foreach (var item in include.Split(","))
                    query = query.Include(item);
            }

            var skip = (Param.CurrentPage - 1) * Param.PageSize;
            Param.Results = query.Skip(skip).Take(Param.PageSize).ToList();

            return Param;
        }


    }
}
