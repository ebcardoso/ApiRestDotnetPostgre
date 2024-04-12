using AutoMapper;
using ApiRestPostgre.Api.Domain.Models;
using ApiRestPostgre.Api.Application.DTO;

namespace ApiRestPostgre.Api.Presentation.Mapping;

public class UsersMapping : Profile
{
  public UsersMapping()
  {
    CreateMap<User, UserDTO>().ReverseMap();
    CreateMap<User, UserGetDTO>().ReverseMap();
  }
}
