using Microsoft.AspNetCore.Mvc;
using ApiRestPostgre.Api.Application.DTO;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Presentation.Responses.Auth;
using ApiRestPostgre.Api.Presentation.Requests.Auth;

namespace ApiRestPostgre.Api.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthServices _authServices;
  private readonly IUsersServices _usersServices;

  public AuthController(IAuthServices authServices, IUsersServices usersServices)
  {
    _authServices = authServices;
    _usersServices = usersServices;
  }

  [HttpPost("signup")]
  public async Task<ActionResult<SignupResponse>> Signup(UserDTO modelDTO)
  {
    if (modelDTO == null)
    {
      var response = new ErrorResponse{ Message = "Invalid data."};
      return BadRequest(response);
    }

    var emailExists = await _authServices.UserExists(modelDTO.Email);
    if (emailExists)
    {
      var response = new ErrorResponse{ Message = "This email already exists."};
      return BadRequest(response);
    }

    var model = await _usersServices.Create(modelDTO);
    if (model == null)
    {
      var response = new ErrorResponse{ Message = "Error to register user."};
      return BadRequest(response);
    }
    var token = _authServices.GenerateToken(model.Id, model.Email);
    var signupResponse = new SignupResponse { Token = token };
    return signupResponse;
  }

  [HttpPost("signin")]
  public async Task<ActionResult<SigninResponse>> Signin(SigninRequest loginReq)
  {
    var emailExists = await _authServices.UserExists(loginReq.Email);
    if (!emailExists)
    {
      var response = new ErrorResponse{ Message = "User not found"};
      return NotFound(response);
    }

    var result = await _authServices.AuthenticateASync(loginReq.Email, loginReq.Password);
    if (!result)
    {
      var response = new ErrorResponse{ Message = "User or password invalid."};
      return Unauthorized(response);
    }

    var user = await _authServices.GetUserByEmail(loginReq.Email);
    var token = _authServices.GenerateToken(user.Id, user.Email);
    var signinResponse = new SigninResponse { Token = token };
    return signinResponse;
  }
}
