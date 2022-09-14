using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Domain.SharedKernel;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> :ReadRepository<TEntity>, IGenericDependency, IGenericRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly PostContext _dbContext;

        public GenericRepository(PostContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
