using System.ComponentModel.DataAnnotations;

namespace TireHouse.DTO;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = null!;

    public bool IsDeleted {  get; set; }

}
