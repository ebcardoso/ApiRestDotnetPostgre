using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Application.ServiceInterfaces;

public interface IUsersServices
{
  Task<IEnumerable<UserDTO>> GetAllAsync();
  Task<UserDTO> GetByID(int id);
  Task<UserDTO> Create(UserDTO model);
  Task<UserDTO> Update(UserDTO modelDTO);
  bool UserExists(int id);
}