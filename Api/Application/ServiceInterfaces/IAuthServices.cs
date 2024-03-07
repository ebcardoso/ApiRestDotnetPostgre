using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Application.ServiceInterfaces;

public interface IAuthServices
{
  Task<bool> AuthenticateASync(string email, string password);
  Task<bool> UserExists(string email);
  public string GenerateToken(int id, string email);
  Task<UserDTO> GetUserByEmail(string email);
}
