using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireHouse.DTO;

public class Product : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Manufactorer { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Size { get; set; } = null!;

    public bool IsDeleted {  get; set; }

    public DateTime ProductionDate { get; set; }

    public Category? Category { get; set; }
}
