using FluentAssertions;
using MicroFrontend.Api.IntegrationTest.Common.Mocks;
using MicroFrontend.Api.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFrontend.Api.IntegrationTest.Basic;

public class DatabaseTests : TestBase
{
    [Test]
    public void ShouldSuccessfullySeedDataToDatabase()
    {
        // Arrange
        AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // Act
        // currently empty
        
        // Assert
        context.Users.ToList().Should().NotBeNullOrEmpty();
        context.Users.ToList().Should().HaveCount(UsersMock.Users.Count);
    }
}