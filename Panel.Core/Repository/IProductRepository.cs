namespace Panel.Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetProductByCategory();

    Task<List<Product>> GetProductCategoryId(Guid categoryId);
}