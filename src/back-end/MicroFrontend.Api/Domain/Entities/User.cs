using System.ComponentModel.DataAnnotations;

namespace MicroFrontend.Api.Domain.Entities;

public class User
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    public string Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Email { get; set;}
}