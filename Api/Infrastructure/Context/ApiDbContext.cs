using Microsoft.EntityFrameworkCore;
using ApiRestPostgre.Api.Domain.Models;

namespace ApiRestPostgre.Api.Infrastructure.Context;

public class ApiDbContext: DbContext
{
  public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
}
