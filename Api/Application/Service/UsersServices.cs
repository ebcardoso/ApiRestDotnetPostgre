using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Domain.RepositoryInterfaces;
using ApiRestPostgre.Api.Application.DTO;
using ApiRestPostgre.Api.Domain.Models;

namespace ApiRestPostgre.Api.Application.Service;

public class UsersServices : IUsersServices
{
  private readonly IUsersRepository _usersRepository;
  private readonly IMapper _mapper;

  public UsersServices(IUsersRepository usersRepository, IMapper mapper)
  {
    _usersRepository = usersRepository;
    _mapper = mapper;
  }

  public async Task<UserDTO> Create(UserDTO modelDTO)
  {
    var model = _mapper.Map<User>(modelDTO);
    if (model != null)
    {
      using var hmac = new HMACSHA512();
      byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(modelDTO.Password));
      byte[] passwordSalt = hmac.Key;
      model.UpdatePassword(passwordHash, passwordSalt);
    }    
    var modelCreated = await _usersRepository.Create(model);
    return _mapper.Map<UserDTO>(modelCreated);
  }
}
