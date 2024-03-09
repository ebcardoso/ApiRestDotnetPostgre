using Microsoft.EntityFrameworkCore;
using ApiRestPostgre.Api.Domain.RepositoryInterfaces;
using ApiRestPostgre.Api.Domain.Models;
using ApiRestPostgre.Api.Infrastructure.Context;

namespace ApiRestPostgre.Api.Domain.Repositories;

public class UsersRepository : IUsersRepository
{
  private readonly ApiDbContext _context;

  public UsersRepository(ApiDbContext context)
  {
    _context = context;
  }

  public async Task<User> GetByEmail(string email)
  {
    var model = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    return model;
  }

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User> Create(User model)
  {
    _context.Users.Add(model);
    await _context.SaveChangesAsync();
    return model;
  }
}
