using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiRestPostgre.Api.Application.DTO;

public class UserDTO
{
  [IgnoreDataMember]
  public int Id { get; set; }
  
  [Required(ErrorMessage = "Name is required")]
  [MinLength(5, ErrorMessage = "This field should have at least 5 characters")]
  [StringLength(100)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "Email is required")]
  [MinLength(5, ErrorMessage = "This field should have at least 5 characters")]
  [StringLength(100)]
  public string? Email { get; set; }
  
  [JsonIgnore]
  public bool IsAdmin { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [MinLength(5, ErrorMessage = "This field should have at least 5 characters")]
  [StringLength(100)]
  [NotMapped]
  public string? Password { get; set; }
  
}