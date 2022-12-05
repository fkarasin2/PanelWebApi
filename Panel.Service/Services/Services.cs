using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Panel.Repository;
using Panel.Service.Exceptions;
using Panel.Services;
using Panel.UnitOfWork;

namespace Panel.Service.Services;

public class Services<T> : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;
    public Services(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    
    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _repository.AnyAsync(expression);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAll().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var hasProduct = await _repository.GetByIdAsync(id);

        if (hasProduct == null)
        {
            throw new NotFoundException($"{typeof(T).Name}({id}) not found");
        }
        return hasProduct;
    }

    public async Task RemoveAsync(T entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _repository.Where(expression);
    }
        
}