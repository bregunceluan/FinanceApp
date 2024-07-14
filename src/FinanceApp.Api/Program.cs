using FinanceApp.Api.Data;
using FinanceApp.Api.Endpoints;
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
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.CustomSchemaIds(c => c.FullName);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();
