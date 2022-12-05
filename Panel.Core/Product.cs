using System.ComponentModel.DataAnnotations.Schema;

namespace Panel;

public class Product: BaseEntity
{
    public string name { get; set; }

    public decimal price { get; set; }

    public int stock { get; set; }

    public Guid CategoryId { get; set; }
    
    [NotMapped] 
    public string CategoryName { get; set; }
    public virtual Category Category { get; set; }

    public ProductFeature ProductFeature { get; set; }
}