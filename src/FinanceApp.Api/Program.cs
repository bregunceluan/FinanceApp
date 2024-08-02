using FinanceApp.Api;
using FinanceApp.Api.Data;
using FinanceApp.Api.Endpoints;
using FinanceApp.Api.Handlers;
using FinanceApp.Api.Models;
using FinanceApp.Api.Properties.Options;
using FinanceApp.Api.Services;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

var isProduction = builder.Environment.IsProduction();

string[]? allowedOrigins = builder.Configuration.GetSection("Cors").Get<string[]>();

if (isProduction)
{
    builder.Configuration.AddEnvironmentVariables("FinanceApp__");
}

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddHealthChecks();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<SendGridOptions>(builder.Configuration);

builder.Services.AddAuthorization();

var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(connectionStr);
});

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (isProduction)
{
    builder.Services.AddCors(c =>
    {
        c.AddPolicy("AllowedOrigins", p => p.WithOrigins(   )
        .AllowAnyHeader()
        .AllowAnyMethod());
    });
}

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.CustomSchemaIds(c => c.FullName);
});


var app = builder.Build();

if (isProduction)
{
    app.UseCors("AllowedOrigins");
}


app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceApp");
    s.RoutePrefix = string.Empty;
});

app.MapEndpoints();

app.MapHealthChecks("/healthz");

app.Run();
