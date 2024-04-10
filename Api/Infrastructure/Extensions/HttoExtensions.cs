using System.Text.Json;
using ApiRestPostgre.Api.Presentation.Controllers;

namespace ApiRestPostgre.Api.Infrastructure.Extensions;

public static class HttpExtensions
{
  public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
  {
    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
    response.Headers.Append("Pagination", JsonSerializer.Serialize(header, jsonOptions));
    response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
  }
}
