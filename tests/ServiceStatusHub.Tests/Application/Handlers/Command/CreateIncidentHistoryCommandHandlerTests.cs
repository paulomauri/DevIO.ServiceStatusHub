using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Enums;

namespace ServiceStatusHub.Tests.Application.Handlers.Command;

public class AddIncidentHistoryCommandHandlerTests
{
    [Fact]
    public async Task Should_Add_IncidentHistory()
    {
        var repoMock = new Mock<IIncidentHistoryRepository>();
        var handler = new AddIncidentHistoryCommandHandler(
            repoMock.Object,
            Mock.Of<ILogger<AddIncidentHistoryCommandHandler>>()
        );

        var command = new AddIncidentHistoryCommand(
            Guid.NewGuid(),
            IncidentAction.CommentAdded,
            "Adicionado comentário",
            "user@example.com"
        );

        var result = await handler.Handle(command, default);

        repoMock.Verify(r => r.AddAsync(It.IsAny<IncidentHistory>()), Times.Once);
        result.Should().NotBe(Guid.Empty);
    }
}
