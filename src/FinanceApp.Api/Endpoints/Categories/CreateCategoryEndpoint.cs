using FinanceApp.Api.Common.Api;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using FinanceApp.Core;

namespace FinanceApp.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("/",HandleAsync)
            .Produces<Response<Category?>>()
            .WithName("Categories: Create")
            .WithSummary("Create a new category.")
            .WithDescription("Creation of a new category")
            .WithOrder(1);

        public static async Task<IResult> HandleAsync(ICategoryHandler handler,CreateCategoryRequest request)
        {
            var response = await handler.CreateAsync(request);

            if (response.IsSuccess)
            {
                return Results.Created($"/{response.Data?.Id}",response);
            }

            return Results.BadRequest(response.Data);
        }
    }
}
