using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Handlers.IncidentHistory
{
    public class RemoveIncidentHistoryCommandHandler : IRequestHandler<RemoveIncidentHistoryCommand>
    {
        private readonly IIncidentHistoryRepository _incidentHistoryRepository;
        private readonly ILogger<RemoveIncidentHistoryCommandHandler> _logger;
        public RemoveIncidentHistoryCommandHandler(IIncidentHistoryRepository incidentHistoryRepository, ILogger<RemoveIncidentHistoryCommandHandler> logger)
        {
            _incidentHistoryRepository = incidentHistoryRepository;
            _logger = logger;
        }
        public async Task Handle(RemoveIncidentHistoryCommand request, CancellationToken cancellationToken)
        {
            var incidentHistory = await _incidentHistoryRepository.GetByIdAsync(request.IncidentHistoryId);
            
            if (incidentHistory == null)
            {
                throw new KeyNotFoundException("Histórico de incidente não encontrado.");
            }
            
            await _incidentHistoryRepository.DeleteAsync(incidentHistory.Id);

            _logger.LogWarning("Removed incident history entry with ID: {IncidentHistoryId}", request.IncidentHistoryId);
        }
    }
}
