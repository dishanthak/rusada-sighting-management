using Dishantha.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dishantha.Domain.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);
       
    }
}
