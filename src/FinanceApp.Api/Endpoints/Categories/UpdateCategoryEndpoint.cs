using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Models;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using System.Security.Claims;

namespace FinanceApp.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Update a new category.")
            .WithDescription("Updating a new category")
            .WithOrder(2);


    public static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;

        var response = await handler.UpdateAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);

    }


}
