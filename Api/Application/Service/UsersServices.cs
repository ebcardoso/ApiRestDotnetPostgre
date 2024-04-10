using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using ApiRestPostgre.Api.Application.ServiceInterfaces;
using ApiRestPostgre.Api.Domain.RepositoryInterfaces;
using ApiRestPostgre.Api.Application.DTO;
using ApiRestPostgre.Api.Domain.Models;
using ApiRestPostgre.Api.Domain.Pagination;

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

  public async Task<PagedList<UserDTO>> GetAllAsync(int pageNumber, int pageSize)
  {
    var models = await _usersRepository.GetAllAsync(pageNumber, pageSize);
    var modelsDTO = _mapper.Map<IEnumerable<UserDTO>>(models);
    return new PagedList<UserDTO>(modelsDTO, pageNumber, pageSize, models.TotalCount);
  }

  public async Task<UserDTO> GetByID(int id)
  {
    var model = await _usersRepository.GetByID(id);
    return _mapper.Map<UserDTO>(model);
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

  public async Task<UserDTO> Update(UserDTO modelDTO)
  {
    var model = _mapper.Map<User>(modelDTO);
    var modelChanged = await _usersRepository.Update(model);
    return _mapper.Map<UserDTO>(modelChanged);
  }

  public async Task<UserDTO> Delete(int id)
  {
    var modelDeleted = await _usersRepository.Delete(id);
    return _mapper.Map<UserDTO>(modelDeleted);
  }

  public bool UserExists(int id)
  {
    return _usersRepository.UserExists(id);
  }
}
