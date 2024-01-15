using System.ComponentModel.DataAnnotations;

namespace TireHouse.DTO;

public class Category : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public IEnumerable<Product>? Products { get; set; }

}
