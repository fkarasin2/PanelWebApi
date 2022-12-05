namespace Panel.DTOs;

public class ProductDto : BaseDto
{
    public string name { get; set; }

    public int stock { get; set; }

    public decimal price { get; set; }

    public Guid CategoryId { get; set; }

}