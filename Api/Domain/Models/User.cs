using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestPostgre.Api.Domain.Models;

public class User
{
  public int Id { get; private set; }
  
  [Required]
  [Column("name")]
  [StringLength(100)]
  public string Name { get; private set; }
  
  [Required]
  [Column("email")]
  [StringLength(100)]
  public string Email { get; private set; }
  public bool IsAdmin { get; private set; }
  public byte[]? PasswordHash { get; set; }
  public byte[]? PasswordSalt { get; set; }
}
