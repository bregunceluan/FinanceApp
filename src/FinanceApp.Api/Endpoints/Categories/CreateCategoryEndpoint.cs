using FinanceApp.Api.Common.Api;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using FinanceApp.Core;
using System.Security.Claims;
using FinanceApp.Api.Models;

namespace FinanceApp.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("/",HandleAsync)
            .Produces<Response<Category?>>()
            .WithName("Categories: Create")
            .WithSummary("Create a new category.")
            .WithDescription("Creation of a new category.")
            .WithOrder(1);

        public static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler,CreateCategoryRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;


            var response = await handler.CreateAsync(request);

            if (response.IsSuccess)
            {
                return Results.Created($"/{response.Data?.Id}",response);
            }

            return Results.BadRequest(response);
        }
    }
}
