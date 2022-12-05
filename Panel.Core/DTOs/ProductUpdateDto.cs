namespace Panel.DTOs;

public class ProductUpdateDto
{
    public Guid id { get; set; }
    
    public string name { get; set; }

    public decimal price { get; set; }

    public int stock { get; set; }
}