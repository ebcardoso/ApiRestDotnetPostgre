// using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestPostgre.Api.Domain.Models;

public abstract class BaseModel
{
  [Required]
  [Column("created_at")]
  public DateTime CreatedAt { get; set; }

  [Required]
  [Column("updated_at")]
  public DateTime UpdatedAt { get; set; }
}
