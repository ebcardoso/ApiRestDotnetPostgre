using Microsoft.AspNetCore.Mvc;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Application.DTO;
using ApiRestPostgre.Api.Presentation.Responses.Auth;

namespace ApiRestPostgre.Api.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUsersServices _usersServices;
  private readonly IAuthServices _authServices;

  public UsersController(IUsersServices usersServices, IAuthServices authServices)
  {
    _usersServices = usersServices;
    _authServices = authServices;
  }

  [HttpGet]
  public async Task<IEnumerable<UserDTO>> GetUsers()
  {
    var modelsDTO = await _usersServices.GetAllAsync();
    return modelsDTO;
  }

  [HttpPost]
  public async Task<ActionResult<UserDTO>> CreateUser(UserDTO modelDTO)
  {
    var emailExists = await _authServices.UserExists(modelDTO.Email);
    if (emailExists)
    {
      var response = new ErrorResponse{ Message = "This email already exists."};
      return BadRequest(response);
    }

    var model = await _usersServices.Create(modelDTO);
    return model;
  }

  [HttpPut]
  public async Task<ActionResult<UserDTO>> UpdateModel(UserDTO modelDTO)
  {
    if (modelDTO.Id == 0)
    {
      var response = new ErrorResponse{ Message = "Id is necessary to update a user."};
      return BadRequest(response);
    }

    if(!_usersServices.UserExists(modelDTO.Id))
    {
      var response = new ErrorResponse{ Message = "User not found."};
      return NotFound(response);
    }

    var model = await _usersServices.Update(modelDTO);
    return model;
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<UserDTO>> DeleteUser(int id)
  {
    if(!_usersServices.UserExists(id))
    {
      var response = new ErrorResponse{ Message = "User not found."};
      return NotFound(response);
    }

    var model = await _usersServices.Delete(id);
    return model;
  }
}
