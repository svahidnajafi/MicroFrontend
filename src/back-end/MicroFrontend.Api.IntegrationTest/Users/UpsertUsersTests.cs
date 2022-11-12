using System.Net.Http.Json;
using FluentAssertions;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;
using MicroFrontend.Api.IntegrationTest.Common.Models;

namespace MicroFrontend.Api.IntegrationTest.Users;

public class UpsertUsersTests : TestBase
{
    private static readonly object[] CreateTestSource =
    {
        new object[] { "Gustav", "sigrid48@wilhelm.de" }
    };

    [Test]
    [TestCaseSource(nameof(CreateTestSource))]
    public async Task ShouldSuccessfullyCreateUserAsync(string name, string email)
    {
        // Arrange
        UserDto request = new() { Name = name, Email = email };
        
        // Act
        ServerResponse<string> response = await CallEndpointAsync(request);
        User? createdUser = await DatabaseContext.Users.FindAsync(response.Content ?? string.Empty);

        // Assert
        AssertServerResponse(response, true);
        createdUser.Should().NotBeNull();
        response.Content.Should().Be(createdUser!.Id);
        createdUser.Name.Should().Be(name);
        createdUser.Email.Should().Be(email);
    }

    private static readonly object[] CreateFailureTestSource =
    {
        new object[] { "Wilma", "" },
        new object[] { "", "sandro.blum@schutz.com" },
        new object[] { "", "" },
        new object?[] { null, "sandro.blum@schutz.com" },
        new object?[] { "Wilma", null },
        new object?[] { null, null }
    };

    [Test]
    [TestCaseSource(nameof(CreateFailureTestSource))]
    public async Task ShouldFailToCreateUserAsync(string? name, string? email)
    {
        // Arrange
        UserDto request = new() { Name = name, Email = email };
        
        // Act
        ServerResponse<string> response = await CallEndpointAsync(request);
        User? createdUser = await DatabaseContext.Users.FindAsync(response.Content ?? string.Empty);

        // Assert
        AssertServerResponse(response, false);
        createdUser.Should().BeNull();
    }
    
    private static readonly object[] EditTestSource =
    {
        new object[] { "U1", "Najafi", "vahid.najafi632@gmail.com" }
    };
    
    [Test]
    [TestCaseSource(nameof(EditTestSource))]
    public async Task ShouldSuccessfullyUpdateUserAsync(string id, string name, string email)
    {
        // Arrange
        UserDto request = new() { Id = id, Name = name, Email = email };
        
        // Act
        ServerResponse<string> response = await CallEndpointAsync(request);
        User? updatedUser = await DatabaseContext.Users.FindAsync(id);

        // Assert
        AssertServerResponse(response, true);
        updatedUser.Should().NotBeNull();
        updatedUser!.Id.Should().Be(id);
        response.Content.Should().Be(updatedUser!.Id);
        updatedUser.Name.Should().Be(name);
        updatedUser.Email.Should().Be(email);
    }
    
    private static readonly object[] UpdateFailureTestSource =
    {
        new object[] { "U2", "Wilma", "" },
        new object[] { "U1", "", "sandro.blum@schutz.com" },
        new object[] { "U1", "", "" },
        new object?[] { "U1", null, "sandro.blum@schutz.com" },
        new object?[] { "U1", "Wilma", null },
        new object?[] { "U1", null, null },
        new object?[] { "InvalidId", "Wilma", "sandro.blum@schutz.com" }
    };

    [Test]
    [TestCaseSource(nameof(UpdateFailureTestSource))]
    public async Task ShouldFailToUpdateUserAsync(string id, string? name, string? email)
    {
        // Arrange
        UserDto request = new() { Id = id, Name = name, Email = email };
        
        // Act
        ServerResponse<string> response = await CallEndpointAsync(request);
        User? updatedUser = await DatabaseContext.Users.FindAsync(id);

        // Assert
        AssertServerResponse(response, false);
        if (updatedUser != null)
        {
            updatedUser.Id.Should().Be(id);
            updatedUser.Name.Should().NotBe(name);
            updatedUser.Email.Should().NotBe(email);
        }
    }
    
    private Task<ServerResponse<string>> CallEndpointAsync(UserDto request)
    {
        JsonContent body = JsonContent.Create(request);
        return CallApiAsync<string>(HttpClient, HttpMethod.Post, "users", body);
    }
}