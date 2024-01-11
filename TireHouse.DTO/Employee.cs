using System.ComponentModel.DataAnnotations;

namespace TireHouse.DTO;

public class Employee : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    public string? Position { get; set; }

    public Account? Account { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool IsDeleted {  get; set; }
}
