using FinanceApp.Api.Common.Api;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;

namespace FinanceApp.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Delete a category.")
            .WithDescription("Deletion of a category")
            .WithOrder(3)
            .Produces<Response<Category?>>();
    public static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = "luan@gmail.com",
            Id = id
        };

        var response = await handler.DeleteAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);
    }
}
