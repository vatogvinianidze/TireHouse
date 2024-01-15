using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireHouse.DTO;

public class Customer : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(100)]
    public string? Email { get; set; }
    public string? Address { get; set; }

    [Column(TypeName ="varchar")]
    public string? PhoneNumber { get; set; } = null!;

    [Column(TypeName ="datetime")]
    public DateTime? DateOfBirth { get; set; }

    public Account? Account { get; set; } = null!;

    public bool IsDeleted {  get; set; }
}
