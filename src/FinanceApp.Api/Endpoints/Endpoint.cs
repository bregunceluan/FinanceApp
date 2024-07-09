using FinanceApp.Api.Common.Api;
using FinanceApp.Api.Endpoints.Categories;
using FinanceApp.Core.Requests.Categories;

namespace FinanceApp.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>();
    }

    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(builder);
        return builder;
    }

}
