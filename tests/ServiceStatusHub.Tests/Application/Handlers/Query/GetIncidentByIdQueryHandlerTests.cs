using AutoMapper;
using FluentAssertions;
using Moq;
using ServiceStatusHub.Application.Handlers.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Mappings;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Enums;
using ServiceStatusHub.Domain.Interfaces;


namespace ServiceStatusHub.Tests.Application.Handlers.Query;

public class GetIncidentByIdQueryHandlerTests
{
    [Fact]
    public async Task Should_Return_Incident_From_Repository_When_Not_Cached()
    {
        var incidentId = Guid.NewGuid();

        var mockRepo = new Mock<IIncidentRepository>();
        var mockCache = new Mock<IRedisCacheService>();
        var mockPolicy = new Mock<ICachePolicyService>();

        var incident = new Incident(
            serviceId: Guid.NewGuid(),
            status: IncidentAction.Resolved.ToString(),
            description: "Sistema ok"
        );

        // Setup do repositório
        mockRepo.Setup(r => r.GetByIdAsync(incidentId))
                .ReturnsAsync(incident);

        var handler = new GetIncidentByIdQueryHandler(
            mockRepo.Object,
            mockCache.Object,
            mockPolicy.Object
        );

        // Act
        var result = await handler.Handle(new GetIncidentByIdQuery(incidentId), default);

        // Assert
        result.Should().NotBeNull();
        result!.id.Should().Be(incident.Id);
    }
}
