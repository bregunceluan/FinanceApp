

using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Models;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;
using System.Security.Claims;

namespace FinanceApp.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
    .Produces<Response<Transaction?>>()
    .WithName("Transactions: Create")
    .WithSummary("Create a new transaction.")
    .WithDescription("Creation of a new transaction.")
    .WithOrder(1);


    public static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, CreateTransactionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;

        var response = await handler.CreateAsync(request);

        if (response.IsSuccess)
        {
            return Results.Created($"/{response.Data?.Id}", response);
        }

        return Results.BadRequest(response);
    }

}
