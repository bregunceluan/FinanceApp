using FinanceApp.Api.Common.Api;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
    .WithName("Transactions: Delete")
    .WithSummary("Delete a transaction.")
    .WithDescription("Deletion of a transaction.")
    .WithOrder(3)
    .Produces<Response<Transaction?>>();

    public static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest
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
