using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Endpoints.Categories;
using FinanceApp.Api.Endpoints.General;
using FinanceApp.Api.Endpoints.Identity;
using FinanceApp.Api.Endpoints.Transactions;
using FinanceApp.Api.Models;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Requests.Transactions;

namespace FinanceApp.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpointsCategories = app.MapGroup("v1/categories")
            .WithTags("Categories")
            .WithOrder(1)
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>();

        app.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .WithOrder(2)
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByPeriodEndpoint>();

        app.MapGroup("v1/identity")
            .WithTags("Identity")
            .WithOrder(3)
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>()
            .MapIdentityApi<User>();

        app.MapGroup("health")
            .WithTags("General")
            .MapEndpoint<HealthEndpoint>();

    }

    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(builder);
        return builder;
    }

}
