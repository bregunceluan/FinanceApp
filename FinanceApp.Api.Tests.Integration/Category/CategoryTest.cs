
using Bogus;
using FinanceApp.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Api.Tests.Integration.Category;

public class CategoryTest : IClassFixture<FinanceApiFactory>
{
    private readonly FinanceApiFactory _financeApiFactory;

    private readonly Faker<User> _faker = new Faker<User>()
        .RuleFor(u => u.Email, faker => faker.Person.Email)
        .RuleFor(u => u.PasswordHash, faker => faker.Internet.Password(10, prefix: "@"));

    private readonly HttpClient _httpClient;

    public CategoryTest(FinanceApiFactory financeApiFactory)
    {
        _financeApiFactory = financeApiFactory;
        _httpClient = financeApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ReturnsCategory_WhenDataIsValid()
    {
        var user = _faker.Generate();
        dynamic value = new
        {
            email = $"{user.Email}",
            password = $"{user.PasswordHash}"
        };
        _financeApiFactory.UserCreated = user;

        var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("/v1/identity/register/", content);

       await _httpClient.PostAsync("/v1/identity/login?useCookies=true", content);

        var categoryBody = new
        {
            title = $"Alimentação",
            description = $"Gastos com alimentação"
        };
        var categoryContent = new StringContent(JsonConvert.SerializeObject(categoryBody), Encoding.UTF8, "application/json");

        var categoryResponse = await _httpClient.PostAsync("/v1/categories", categoryContent);
        categoryResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    }

}
