using FinanceApp.Api.Data;
using FinanceApp.Api.Handlers;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(connectionStr);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.CustomSchemaIds(c => c.FullName);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
{
    var request = new GetCategoryByIdRequest { Id = id,UserId="luan@gmail.com" };
    return await handler.GetByIdAsync(request);
})
    .WithName("Categories: Get by Id")
    .WithSummary("Get a category")
    .Produces<Response<Category?>>(); 

app.MapGet("/v1/categories/", async (ICategoryHandler handler) =>
{
    var request = new GetAllCategoriesRequest { UserId="luan@gmail.com" };
    return await handler.GetAllAsync(request);
})
    .WithName("Categories: Get by all")
    .WithSummary("Get all categories of a user")
    .Produces<PagedResponse<List<Category?>>>(); ;

app.MapPost("/v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler) =>
{
    return await handler.CreateAsync(request);
})
    .WithName("Categories: Create")
    .WithSummary("Create a new category")
    .Produces<Response<Category?>>();


app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
{
    request.Id = id;
    return await handler.UpdateAsync(request);
})
    .WithName("Categories: Update")
    .WithSummary("Update a category")
    .Produces<Response<Category?>>();


app.MapDelete("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
{
    var request = new DeleteCategoryRequest { Id = id };
    return await handler.DeleteAsync(request);
})
    .WithName("Categories: Delete")
    .WithSummary("Delete a category")
    .Produces<Response<Category?>>(); ;

app.Run();
