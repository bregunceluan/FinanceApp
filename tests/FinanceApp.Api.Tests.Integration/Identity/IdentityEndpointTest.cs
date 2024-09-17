using Bogus;
using FinanceApp.Api.Data;
using FinanceApp.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinanceApp.Api.Tests.Integration.Identity;

public class IdentityEndpointTest
{
    private readonly Faker<User> _faker = new Faker<User>()
        .RuleFor(u => u.Email, faker => faker.Person.Email)
        .RuleFor(u => u.PasswordHash, faker => faker.Internet.Password(10, prefix: "@"));
    private readonly HttpClient _httpClient;


    public IdentityEndpointTest(FinanceApiFactory financeApiFactory)
    {
        _httpClient = financeApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreateUser_WhenDataIsValid()
    {
        var user = _faker.Generate();
        dynamic value = new
        {
            email = $"{user.Email}",
            password = $"{user.PasswordHash}"
        };

        var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/v1/identity/register/", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_LoginUser_WhenDataIsValid()
    {
        var user = _faker.Generate();

        var userDynamic = new
        {
            email = $"{user.Email}",
            password = $"{user.PasswordHash}"
        };

        var content = new StringContent(JsonConvert.SerializeObject(userDynamic), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/v1/identity/register/", content);

        var contentLogin = new StringContent(JsonConvert.SerializeObject(userDynamic), Encoding.UTF8, "application/json");
        var responseLogin = await _httpClient.PostAsync("/v1/identity/login?useCookies=true", content);

        responseLogin.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
