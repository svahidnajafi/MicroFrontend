using FluentAssertions;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;
using MicroFrontend.Api.IntegrationTest.Common.Models;

namespace MicroFrontend.Api.IntegrationTest.Users;

public class GetUserListTests : TestBase
{
    [Test]
    public async Task ShouldSuccessfullyGetUsersAsync()
    {
        // Arrange
        // currently empty
        
        // Act
        ServerResponse<IEnumerable<UserDto>> response = await CallEndpointAsync();
        List<User> users = DatabaseContext.Users.ToList(); 
        
        // Assert
        AssertServerResponse(response, true);
        users.Should().NotBeNull();
        response.Content!.Should().HaveCount(users.Count);
    }

    private Task<ServerResponse<IEnumerable<UserDto>>> CallEndpointAsync() =>
        CallApiAsync<IEnumerable<UserDto>>(HttpClient, "/users", null);
}