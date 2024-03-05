using ApiRestPostgre.Api.Infrastructure.Context;
using ApiRestPostgre.Api.Presentation.Mapping;
using Microsoft.EntityFrameworkCore;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var Configuration = builder.Configuration;

    builder.Services.AddDbContext<ApiDbContext>(options =>
      options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
    );
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(UsersMapping));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
  }
}
