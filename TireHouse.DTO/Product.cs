using System.ComponentModel.DataAnnotations;

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


    [Required]
    public DateTime ProductionDate { get; set; }

    [Required]
    public Category Category { get; set; } = null!;
}
