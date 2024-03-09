using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiRestPostgre.Api.Infrastructure.Exceptions;

namespace ApiRestPostgre.Api.Domain.Models;

public class User : BaseModel
{
  public int Id { get; private set; }
  
  [Required]
  [Column("name")]
  [StringLength(100)]
  public string Name { get; private set; }
  
  [Required]
  [Column("email")]
  [StringLength(100)]
  public string Email { get; private set; }
  public bool IsAdmin { get; private set; }
  public byte[]? PasswordHash { get; set; }
  public byte[]? PasswordSalt { get; set; }

  public User(int id, string name, string email)
  {
    ModelValidationException.When(id < 0, "Field Id should be greater than 0");
    Id = id;

    ValidateModel(name, email);
  }

  public User(string name, string email)
  {
    ValidateModel(name, email);
  }

  public void SetAdmin(bool isAdmin) {
    IsAdmin = isAdmin;
  }

  public void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
  {
    PasswordHash = passwordHash;
    PasswordSalt = passwordSalt;
  }

  private void ValidateModel(string name, string email)
  {
    ModelValidationException.When(name == null, "Field Name is required");
    ModelValidationException.When(email == null, "Field Email is required");

    ModelValidationException.When(name.Length > 100, "Maximum size for Name is 100 characters");
    ModelValidationException.When(email.Length > 100, "Maximum size for LegalName is 100 characters");

    Name = name;
    Email = email;
    IsAdmin = false;
  }
}
