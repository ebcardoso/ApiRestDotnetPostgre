namespace ApiRestPostgre.Api.Infrastructure.Exceptions;

public class ModelValidationException : Exception
{
  public ModelValidationException(string error) : base(error) { }

  public static void When(bool hasError, string error)
  {
    if (hasError)
    {
      throw new ModelValidationException(error);
    }
  }
}
