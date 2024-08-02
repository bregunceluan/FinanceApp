using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FinanceApp.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.Hosting.Server;
using DotNet.Testcontainers.Builders;
using System.Data.Common;
using FinanceApp.Api.Libraries.Extensions;
using FinanceApp.Api.Models;


namespace FinanceApp.Api.Tests.Integration
{
    public class FinanceApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {

        public AppDbContext Context;
        public DbConnection DbConnection;
        public User UserCreated;

        public readonly PostgreSqlContainer _dbContainer =
            new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithUsername("user")
            .WithPassword("pass123!")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
            .WithCleanUp(true)
            .Build();

        async Task IAsyncLifetime.InitializeAsync()
        {
            await _dbContainer.StartAsync();

            Context = Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            DbConnection = Context.Database.GetDbConnection();
            await DbConnection.OpenAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await DbConnection.CloseAsync();
            await Context.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveDbContext<AppDbContext>();


                var connection = _dbContainer.GetConnectionString();

                services.AddDbContext<AppDbContext>(c => c.UseNpgsql(_dbContainer.GetConnectionString(), op =>
                {
                    op.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), new List<string>());
                }), ServiceLifetime.Transient);

                services.EnsureDbCreated<AppDbContext>();
            });

        }

    }
}
