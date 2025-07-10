using AutoMapper;
using Moq;
using ServiceStatusHub.Application.Handlers.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Enums;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServiceStatusHub.Tests.Application.Handlers.Query;

public class GetAllIncidentsQueryHandlerTests
{
    [Fact]
    public async Task Should_Return_All_Incidents_From_Repository()
    {
        var incidentList = new List<Incident>
        {
            new(Guid.NewGuid(), IncidentAction.Created.ToString(), "Falha 1"),
            new(Guid.NewGuid(), IncidentAction.Resolved.ToString(), "Tudo ok")
        };

        var mockRepo = new Mock<IIncidentRepository>();
        var mockCache = new Mock<IRedisCacheService>();
        var mockPolicy = new Mock<ICachePolicyService>();

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(incidentList);

        var handler = new GetAllIncidentsQueryHandler( mockRepo.Object, mockCache.Object, mockPolicy.Object);

        var result = await handler.Handle(new GetAllIncidentsQuery(), default);

        result.Should().NotBeNull();
        result.Count().Should().Be(2);
    }
}
