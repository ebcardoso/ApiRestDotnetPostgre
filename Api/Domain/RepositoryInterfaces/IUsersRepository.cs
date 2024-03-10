using ApiRestPostgre.Api.Domain.Models;

namespace ApiRestPostgre.Api.Domain.RepositoryInterfaces;

public interface IUsersRepository
{
  Task<IEnumerable<User>> GetAllAsync();
  Task<User> GetByID(int id);
  Task<User> GetByEmail(string email);
  Task<User> Create(User model);
  Task<User> Update(User model);
  Task<User> Delete(int id);
  bool UserExists(int id);
}
