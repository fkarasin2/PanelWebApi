using Microsoft.EntityFrameworkCore;
using Panel.Repository;

namespace Panel.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Category> GetCategoryWithProductsByIdAsync(Guid categoryId)
    {
        return _context.Categories.Include(x => x.Products).Where(x => x.id == categoryId).SingleOrDefaultAsync();
    }
}