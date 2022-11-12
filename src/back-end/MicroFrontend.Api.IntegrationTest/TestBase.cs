using System.Net;
using FluentAssertions;
using MicroFrontend.Api.IntegrationTest.Common.Models;
using MicroFrontend.Api.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MicroFrontend.Api.IntegrationTest;

using static Testing;

[TestFixture]
public abstract class TestBase
{
    protected HttpClient HttpClient = null!;
    protected IServiceScope Scope = null!;
    protected AppDbContext DatabaseContext = null!;
    
    [SetUp]
    public async Task BaseSetUp()
    {
        await ResetStateAsync();
        await SeedDefaultDataAsync();
        HttpClient = GetHttpClient();
        Scope = GetScopeFactory().CreateScope();
        DatabaseContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
    
    protected IServiceScopeFactory GetScopeFactory() => ScopeFactory;
    
    protected HttpClient GetHttpClient() => GetClient();
    
    protected Uri GetUrl(HttpClient client, string postfix)
    {
        if (postfix.StartsWith("/"))
            postfix = postfix.Substring(1);
        string url = (client.BaseAddress?.ToString() ?? String.Empty) + postfix;
        return new Uri(url);
    }
    
    protected async Task<ServerResponse<T>> DeserializeResponseAsync<T>(HttpResponseMessage? httpResponseMessage)
    {
        if (httpResponseMessage == null)
            throw new ArgumentNullException();
        string rawResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
        {
            if (typeof(T) == typeof(String))
                return new ServerResponse<T>((T)(object)(rawResponse.Trim('"')));
            if (rawResponse is T)
                return new ServerResponse<T>((T)Convert.ChangeType(rawResponse, typeof(T)));
            T? response = JsonConvert.DeserializeObject<T>(rawResponse);
            if (response != null)
                return new ServerResponse<T>(response);
        }
        return new ServerResponse<T>(new ErrorResponse(rawResponse));
    }
    
    protected async Task<ServerResponse<TResult>> CallApiAsync<TResult>(HttpClient client, HttpMethod method, string url,
        HttpContent? content)
    {
        Uri uri = GetUrl(client, url);
        HttpRequestMessage request = new(method, uri);
        if (content != null)
            request.Content = content;
        HttpResponseMessage response = await client.SendAsync(request);
        return await DeserializeResponseAsync<TResult>(response);
    }
    
    protected async Task<ServerResponse<TResult>> CallApiAsync<TResult>(HttpClient client, string url, string? query)
    {
        Uri uri = GetUrl(client, url);
        if (!String.IsNullOrWhiteSpace(query))
            uri = GetUrl(client, url + "?" + query);
        HttpRequestMessage request = new(HttpMethod.Get, uri);
        HttpResponseMessage response = await client.SendAsync(request);
        return await DeserializeResponseAsync<TResult>(response);
    }
    
    protected void AssertServerResponse<T>(ServerResponse<T>? response, bool expectedResult)
    {
        response.Should().NotBeNull();
        response!.IsSuccessful.Should().Be(expectedResult);
        if (expectedResult)
            response.Content.Should().NotBeNull();
        else
            response.Error.Should().NotBeNull();
    }
}