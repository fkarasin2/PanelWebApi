using System.ComponentModel.DataAnnotations.Schema;

namespace Panel.DTOs;

public abstract class BaseDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order = 0)]
    public Guid id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}