using System.Linq.Expressions;

namespace Panel.Repository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(Guid id, bool lazyLoading = false);

    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    
    Task AddAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    
    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void Delete(T entity);
    
    void RemoveRange(IEnumerable<T> entities);
}