using FinanceApp.Api.Common.Api;
using FinanceApp.Core.Responses;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Requests.Transactions;
using System.Security.Claims;

namespace FinanceApp.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Get a transaction by id.")
            .WithDescription("Retrieve a transaction by id")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    public static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
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
