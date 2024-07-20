using FinanceApp.Api.Common.Api;
using System.Security.Claims;

namespace FinanceApp.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder) => builder.MapGet("/roles",HandleAsync);
    

    public static async Task<IResult> HandleAsync(ClaimsPrincipal user)
    {
		try
		{
			if (user.Identity is null || !user.Identity.IsAuthenticated) return Results.Unauthorized();

			var identity = user.Identity as ClaimsIdentity;
			var roles = identity?.FindAll(identity.RoleClaimType)
				.Select(u => new
				{
					u.Issuer,
					u.OriginalIssuer,
					u.Type,
					u.Value,
					u.ValueType
				});

			return TypedResults.Json(roles);
		}
		catch (Exception)
		{
			throw;
		}
    }
}
