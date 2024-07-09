using FinanceApp.Api.Common.Api;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Libraries.Consts;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapGet("/", HandleAsync)
            .Produces<PagedResponse<List<Category>?>>()
            .WithName("Categories: Get All")
            .WithSummary("Get all categories")
            .WithDescription("Retrive all categories")
            .WithOrder(4);
    public static async Task<IResult> HandleAsync(
        ICategoryHandler handler, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize=Configuration.DefaultPageSize)
    {

        var request = new GetAllCategoriesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var response = await handler.GetAllAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response.Data);
        }

        return Results.BadRequest(response.Data);
    }
}
