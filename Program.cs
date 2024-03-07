using System.Text;
using ApiRestPostgre.Api.Application.Service;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Domain.Repositories;
using ApiRestPostgre.Api.Domain.RepositoryInterfaces;
using ApiRestPostgre.Api.Infrastructure.Context;
using ApiRestPostgre.Api.Presentation.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var Configuration = builder.Configuration;

    builder.Services.AddDbContext<ApiDbContext>(options =>
      options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
    );
    builder.Services.AddAuthentication(opt =>
    {
      opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(opt =>
    {
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true, //Who generates the token 
        ValidateAudience = true, //Who consumes the token
        ValidateLifetime = true, //Deadline
        ValidateIssuerSigningKey = true, //Validates login

        ValidIssuer = Configuration.GetValue<string>("jwt:issuer"),
        ValidAudience = Configuration.GetValue<string>("jwt:audience"),
        IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(Configuration.GetValue<string>("jwt:secretKey"))),
        ClockSkew = TimeSpan.Zero
      };
    });
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddScoped<IAuthServices, AuthServices>();
    builder.Services.AddScoped<IUsersServices, UsersServices>();
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
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
  }
}
