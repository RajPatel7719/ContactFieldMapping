using System.ComponentModel.DataAnnotations;

namespace ContactFieldMapping.DAL.Model;

public class Contact
{
    [Key]
    public Guid ContactKey { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime JoiningDate { get; set; }

    [Required]
    [StringLength(50)]
    public string MemberType { get; set; } = string.Empty;

    
    public string? JobTitle { get; set; }
    public bool IsMember { get; set; }
}
