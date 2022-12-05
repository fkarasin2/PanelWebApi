using Microsoft.EntityFrameworkCore;
using Panel.Repository;

namespace Panel.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{

    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    
    public async Task<List<Product>> GetProductByCategory()
    {
        return await _context.Products.Include(x => x.Category).ToListAsync();

    }

    public async Task<List<Product>> GetProductCategoryId(Guid categoryId)
    {
        return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
    }
}