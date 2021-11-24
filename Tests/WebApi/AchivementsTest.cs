using AchiveClub.Server;
using AchiveClub.Shared.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace Tests.WebApi;

public class AchivementsTest
{
    private readonly HttpClient _httpClient;

    public AchivementsTest()
    {
        var server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<Startup>());
        _httpClient = server.CreateClient();
    }

    [Fact]
    public async void UsersNotEmptyTestAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<List<UserInfo>>("api/Users");
        Assert.True(users.Any());
    }
}