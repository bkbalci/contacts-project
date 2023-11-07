using System.Linq.Expressions;

namespace ContactService.Domain.Core;

public interface IRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
        EntityPaging? paging = null);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
        EntityPaging? paging = null);

    Task AddAsync(TEntity entity);
    void BeginAdd(TEntity entity);

    Task UpdateAsync(TEntity entity);
    void BeginUpdate(TEntity entity);

    Task DeleteAsync(TEntity entity);
    void BeginDelete(TEntity entity);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
}