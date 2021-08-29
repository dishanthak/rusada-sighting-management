using Dishantha.Domain.Entities;
using Dishantha.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dishantha.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public Repository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            if (entity.Id.Equals(default(TKey)))
            {
                entity.CreatedDateTime = DateTime.Now;

                await _dbSet.AddAsync(entity);
            }
            else
            {
                entity.UpdatedDateTime = DateTime.Now;
                _dbContext.Entry(entity).Property("RowVersion").OriginalValue = entity.RowVersion;
                _dbSet.Update(entity);
            }

            return entity;
        }        

        public async Task<bool> DeleteAsync(int id)
        {
            var sightEntity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (sightEntity == null)
            {
                return false;
            }
            _dbContext.Set<TEntity>().Remove(sightEntity);
            return true;
        }



    }
}
