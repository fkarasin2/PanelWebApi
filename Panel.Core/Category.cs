namespace Panel;

public class Category: BaseEntity
{
    public string title { get; set; }

    public ICollection<Product> Products { get; set; }
}