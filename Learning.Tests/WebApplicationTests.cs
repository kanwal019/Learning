extern alias DataServiceApp;
extern alias BikeStoreWebApp;
extern alias NetCoreApiApp;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using DataServiceProgram = DataServiceApp::Program;
using BikeStoreWebProgram = BikeStoreWebApp::Program;
using NetCoreApiProgram = NetCoreApiApp::Program;

namespace Learning.Tests;

public class WebApplicationTests
{
    [Fact]
    public async Task SwaggerPreservesTheApiRoutes()
    {
        await using var factory = new WebApplicationFactory<DataServiceProgram>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Development"));
        using var client = factory.CreateClient();
        using var document = JsonDocument.Parse(await client.GetStringAsync("/swagger/v1/swagger.json"));
        var root = document.RootElement;
        Assert.Equal("DataService", root.GetProperty("info").GetProperty("title").GetString());
        var paths = root.GetProperty("paths");
        Assert.True(paths.TryGetProperty("/api/Products/{sequence}", out _));
        Assert.True(paths.TryGetProperty("/api/Orders/getOrder/{orderId}", out _));
    }

    [Fact]
    public async Task AuthenticationChallengesBeforeAccessingTheDatabase()
    {
        await using var factory = new WebApplicationFactory<DataServiceProgram>();
        using var client = factory.CreateClient();
        using var response = await client.GetAsync("/api/orders/getOrder/1");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CorsStillAllowsTheConfiguredFrontend()
    {
        await using var factory = new WebApplicationFactory<DataServiceProgram>();
        using var client = factory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Options, "/weatherforecast");
        request.Headers.Add("Origin", "http://localhost:8080");
        request.Headers.Add("Access-Control-Request-Method", "GET");
        using var response = await client.SendAsync(request);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal("http://localhost:8080", response.Headers.GetValues("Access-Control-Allow-Origin").Single());
        Assert.Equal("true", response.Headers.GetValues("Access-Control-Allow-Credentials").Single());
    }

    [Fact]
    public async Task BikeStoreApiRunsBeforeTheSpaFallback()
    {
        // Production avoids starting Angular's development server during backend tests.
        await using var factory = new WebApplicationFactory<BikeStoreWebProgram>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
        using var client = factory.CreateClient();
        using var document = JsonDocument.Parse(await client.GetStringAsync("/weatherforecast"));
        Assert.Equal(5, document.RootElement.GetArrayLength());
        Assert.True(document.RootElement[0].TryGetProperty("temperatureC", out _));
    }

    [Fact]
    public async Task NetCoreApiPreservesMiddlewareAndDirectoryBrowsing()
    {
        await using var factory = new WebApplicationFactory<NetCoreApiProgram>();
        using var client = factory.CreateClient();
        Assert.Equal("Logger Middleware Invoked !!\n\nApplication Running !!", await client.GetStringAsync("/"));
        using var response = await client.GetAsync("/files/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("BikeStores", await response.Content.ReadAsStringAsync());
    }
}
