using Microsoft.EntityFrameworkCore.ChangeTracking;
using Parxlab.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parxlab.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<EntityEntry<TEntity>> Add(TEntity entity);
        Task<object> AddAsync(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        Task<int> RemoveAsync(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<int> Count();
        Task<long> CountAsync();
        ValueTask<TEntity> Find(Guid id);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(Guid id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> Exists(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetPage(int skip, int offset);
        Task<List<TEntity>> GetPage(Expression<Func<TEntity, bool>> expression, int skip, int offset);
    }
}
