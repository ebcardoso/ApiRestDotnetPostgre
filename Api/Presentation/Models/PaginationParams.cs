using System.ComponentModel.DataAnnotations;

namespace ApiRestPostgre.Api.Presentation.Controllers;

public class PaginationParams
{
  [Range(1, int.MaxValue)]
  public int PageNumber { get; set; }

  [Range(1, 50, ErrorMessage="Maximum size per page is 50.")]
  public int PageSize { get; set; }
}
