using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Application.ServiceInterfaces;

public interface IUsersServices
{
  Task<IEnumerable<UserDTO>> GetAllAsync();
  Task<UserDTO> Create(UserDTO model);
}