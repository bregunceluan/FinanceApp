using FinanceApp.Api.Common.Api;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;

namespace FinanceApp.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Update a transaction")
            .WithSummary("Update a transaction by id.")
            .WithDescription("Retrieve a transaction by id and update it.")
            .WithOrder(5)
            .Produces<Response<Transaction?>>();

    public static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = "luan@gmail.com";
        request.Id = id;

        var response = await handler.UpdateAsync(request);

        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }

        return Results.BadRequest(response);
    }

}
