using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Endpoints.Categories;
using FinanceApp.Api.Endpoints.Transactions;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Requests.Transactions;

namespace FinanceApp.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpointsCategories = app.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>();



        app.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByPeriodEndpoint>();
    }

    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(builder);
        return builder;
    }

}
