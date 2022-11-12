using FluentAssertions;
using MicroFrontend.Api.Domain.Entities;
using MicroFrontend.Api.IntegrationTest.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroFrontend.Api.IntegrationTest.Users;

public class DeleteUserTests : TestBase
{
    private User _user = null!;
    
    [SetUp]
    public async Task SetUp()
    {
        _user = await SeedExampleUserAsync();
    }

    [Test]
    public async Task ShouldSuccessfullyDeleteUserFromDatabaseAsync()
    {
        // Arrange
        bool doesUserExistBeforeCall = DoesUserExists(_user.Id);
        
        // Act
        ServerResponse<string> result = await CallEndpointAsync(_user.Id);
        bool doesUserExistAfterCall = DoesUserExists(_user.Id);
        
        // Assertion
        AssertServerResponse(result, true);
        doesUserExistBeforeCall.Should().BeTrue();
        doesUserExistAfterCall.Should().BeFalse();
    }

    [Test]
    [TestCase("InvalidId")]
    [TestCase("")]
    [TestCase(null)]
    public async Task ShouldFailToDeleteUserFromDatabaseAsync(string? id)
    {
        // Arrange
        bool doesUserExistBeforeCall = DoesUserExists(id); 
        
        // Act
        ServerResponse<string> result = await CallEndpointAsync(id);
        
        // Assertion
        AssertServerResponse(result, false);
        doesUserExistBeforeCall.Should().BeFalse();
    }

    private Task<ServerResponse<string>> CallEndpointAsync(string id) => 
        CallApiAsync<string>(HttpClient, HttpMethod.Delete, $"users/{id}", null);
}