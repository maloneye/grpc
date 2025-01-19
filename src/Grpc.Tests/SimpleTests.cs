using Grpc.Client;
using Grpc.Server;
using Shouldly;

namespace Grpc.Tests;

[TestClass]
public sealed class SimpleTests
{
    [TestMethod("Given an known employee, then the service should correctly return there skills")]
    public async Task T0()
    { 
        // Arrange
        var employee = new Employee("Ewan");

        var server = new Host();
        var client = new Edge("http://localhost:5000");

        try
        {
            await server.StartAsync(CancellationToken.None);

            // Act
            var skills = await client.RequestSkill(employee);

            // Assert
            skills.Count.ShouldBe(2);
        }
        finally
        {
            await server.StopAsync(CancellationToken.None);
        }
    }
}