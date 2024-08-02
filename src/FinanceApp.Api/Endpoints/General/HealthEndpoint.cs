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
        var res = await context.Database.CanConnectAsync();

        return Results.Ok(
            new
            {
                Api = "API is up and happier than a dog with two tails.",
                Db = !res ? "Database connection failure. The database went out for lunch." : "Database is up and running like a caffeinated squirrel."
            });
            
    }
}

