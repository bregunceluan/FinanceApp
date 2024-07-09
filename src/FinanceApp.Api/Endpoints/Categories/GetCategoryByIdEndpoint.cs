using FinanceApp.Api.Common.Api;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;

namespace FinanceApp.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Get by category")
            .WithDescription("Get a category by id")
            .WithOrder(5)
            .Produces<Response<Category?>>();

    public static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            Id = id,
        };

        var response = await handler.GetByIdAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);
    }
}
