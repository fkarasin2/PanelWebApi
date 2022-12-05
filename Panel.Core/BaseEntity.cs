using System.ComponentModel.DataAnnotations.Schema;

namespace Panel;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order = 0)]
    public Guid? id { get; set; } = Guid.NewGuid();

    public DateTime? CretedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }
}