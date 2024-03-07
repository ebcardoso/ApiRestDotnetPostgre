using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Application.ServiceInterfaces;

public interface IUsersServices
{
  Task<UserDTO> Create(UserDTO model);
}