using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Enums;
using ServiceStatusHub.Domain.Interfaces;


namespace ServiceStatusHub.Tests.Application.Handlers.Command;

public class CreateIncidentCommandHandlerTests
{
    private readonly Mock<IIncidentRepository> _repositoryMock;
    private readonly CreateIncidentCommandHandler _handler;
    private readonly IRedisCacheService _cacheMock = Mock.Of<IRedisCacheService>();

    public CreateIncidentCommandHandlerTests()
    {
        _repositoryMock = new Mock<IIncidentRepository>();
        _handler = new CreateIncidentCommandHandler(_repositoryMock.Object, Mock.Of<ILogger<CreateIncidentCommandHandler>>(), _cacheMock);
    }

    [Fact]
    public async Task Handle_Should_Create_Incident_And_Return_Id()
    {
        // Arrange
        var command = new CreateIncidentCommand(Guid.NewGuid(), "API caiu", IncidentAction.Created.ToString());

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Incident>()), Times.Once);

        result.Should().NotBe(Guid.Empty);
    }
}
