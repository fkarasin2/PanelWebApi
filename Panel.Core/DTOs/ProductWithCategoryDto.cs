namespace Panel.DTOs;

public class ProductWithCategoryDto : ProductDto
{
    public CategoryDto Category { get; set; }
}