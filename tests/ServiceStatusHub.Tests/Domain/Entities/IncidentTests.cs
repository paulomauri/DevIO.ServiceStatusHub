using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Tests.Domain.Entities;

public class IncidentTests
{
    [Fact]
    public void Incident_Should_Set_CreatedAt_To_UtcNow()
    {
        var serviceId = Guid.NewGuid();
        var status = IncidentAction.Created.ToString();
        var description = "Falha na API";

        var incident = new Incident(serviceId, description, status);

        incident.Id.Should().NotBeEmpty();
        incident.Status.Should().Be(status);
        incident.StartedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }
}
