using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Data;
using FinanceApp.Core.Handlers;
using Newtonsoft.Json;

namespace FinanceApp.Api.Endpoints.General;

public class HealthEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
        .WithName("General: Health")
        .WithSummary("Check if the service is running");

    public static  async Task<IResult> HandleAsync(AppDbContext context)
    {

        return Results.Ok("");
    }
}

