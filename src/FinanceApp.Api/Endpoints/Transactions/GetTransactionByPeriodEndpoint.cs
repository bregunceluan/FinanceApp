using FinanceApp.Api.Common.Api;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;
using System.Transactions;

namespace FinanceApp.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
    .Produces<PagedResponse<List<Transaction>>>()
    .WithName("Transactions: Get Transaction By Period")
    .WithSummary("Get transactions by a period of date.")
    .WithDescription("Get transactions by a period of date.")
    .WithOrder(1);

    public static async Task<IResult> HandleAsync(ITransactionHandler handler, GetTransactionByPeriodRequest request)
    {
        request.UserId = "luan@gmail.com";

        var response = await handler.GetByPeriodAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);
    }

}
