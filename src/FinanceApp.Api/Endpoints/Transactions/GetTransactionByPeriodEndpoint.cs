using FinanceApp.Api.Common.Api;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Libraries.Consts;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace FinanceApp.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
    .Produces<PagedResponse<List<Transaction>>>()
    .WithName("Transactions: Get Transaction By Period")
    .WithSummary("Get transactions by a period of date.")
    .WithDescription("Get transactions by a period of date.")
    .WithOrder(5);

    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler, 
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {   
        var request = new GetTransactionByPeriodRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await handler.GetByPeriodAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);
    }

}
