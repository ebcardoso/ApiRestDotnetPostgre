using ApiRestPostgre.Api.Domain.Models;

namespace ApiRestPostgre.Api.Domain.RepositoryInterfaces;

public interface IUsersRepository
{
  Task<User> GetByEmail(string email);
  Task<User> Create(User model);
}
