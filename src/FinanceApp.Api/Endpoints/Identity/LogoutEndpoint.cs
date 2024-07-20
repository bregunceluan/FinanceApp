using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FinanceApp.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder) => 
        builder.MapPost("/logout", HandleAsync)
        .WithName("Identity: Logout")
        .WithSummary("Logout the current user")
        .WithDescription("Logout the logged user")
        .WithOrder(4)
        .RequireAuthorization();

    public static async Task<IResult> HandleAsync(SignInManager<User> sign)
    {
		try
		{
            await sign.SignOutAsync();
            return Results.Ok();

        }
		catch (Exception)
		{

			throw;
		}

    }


}
