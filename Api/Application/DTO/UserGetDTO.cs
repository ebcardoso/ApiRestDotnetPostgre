namespace ApiRestPostgre.Api.Application.DTO;

public class UserGetDTO
{
  public int Id { get; set; }

  public string? Name { get; set; }

  public string? Email { get; set; }

  public bool IsAdmin { get; set; }
}