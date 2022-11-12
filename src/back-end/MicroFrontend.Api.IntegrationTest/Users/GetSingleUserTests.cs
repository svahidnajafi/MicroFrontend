using FluentAssertions;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;
using MicroFrontend.Api.IntegrationTest.Common.Models;

namespace MicroFrontend.Api.IntegrationTest.Users;

public class GetSingleUserTests : TestBase
{
    private User _user = null!;
    
    [SetUp]
    public async Task SetUp()
    {
        _user = await SeedExampleUserAsync();
    }

    [Test]
    public async Task ShouldSuccessfullyGetUserAsync()
    {
        // Arrange
        
        // Act
        ServerResponse<UserDto?> result = await CallEndpointAsync(_user.Id);
        
        // Assertion
        AssertServerResponse(result, true);
        result.Content.Should().NotBeNull();
    }
    
    [Test]
    [TestCase("InvalidId")]
    public async Task ShouldFailToGetUserAsync(string? id)
    {
        // Arrange 
        
        // Act
        ServerResponse<UserDto?> result = await CallEndpointAsync(id);
        
        // Assertion
        result.IsSuccessful.Should().BeTrue();
        result.Content.Should().BeNull();
    }
    
    private Task<ServerResponse<UserDto?>> CallEndpointAsync(string? id) =>
        CallApiAsync<UserDto?>(HttpClient, $"/users/{id}", null);
}