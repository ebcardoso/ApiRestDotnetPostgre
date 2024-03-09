using Microsoft.AspNetCore.Mvc;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUsersServices _usersServices;

  public UsersController(IUsersServices usersServices)
  {
    _usersServices = usersServices;
  }

  [HttpGet]
  public async Task<IEnumerable<UserDTO>> GetBrands()
  {
    var modelsDTO = await _usersServices.GetAllAsync();
    return modelsDTO;
  }
}
