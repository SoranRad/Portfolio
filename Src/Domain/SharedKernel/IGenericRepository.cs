using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.SharedKernel
{
    public interface IGenericRepository<TEntity> : IReadRepository<TEntity> where TEntity : IAggregateRoot
    {
        void    Add        (TEntity entity);

        void    Remove     (TEntity entity);

        void    Update     (TEntity entity);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
