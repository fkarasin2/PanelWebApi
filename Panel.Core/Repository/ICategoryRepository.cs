namespace Panel.Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCategoryWithProductsByIdAsync(Guid categoryId);
}