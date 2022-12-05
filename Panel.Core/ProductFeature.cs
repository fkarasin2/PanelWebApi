namespace Panel;

public class ProductFeature
{
    public Guid id { get; set; }

    public string color { get; set; }

    public int height { get; set; }

    public int width { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; }

}