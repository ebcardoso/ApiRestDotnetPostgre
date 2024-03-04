using Microsoft.EntityFrameworkCore;

namespace ApiRestPostgre.Api.Infrastructure.Context;

public class ApiDbContext: DbContext
{
  public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
  {
  }
}
