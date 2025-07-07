using MediatR;
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
        public RemoveIncidentHistoryCommandHandler(IIncidentHistoryRepository incidentHistoryRepository)
        {
            _incidentHistoryRepository = incidentHistoryRepository;
        }
        public async Task Handle(RemoveIncidentHistoryCommand request, CancellationToken cancellationToken)
        {
            var incidentHistory = await _incidentHistoryRepository.GetByIdAsync(request.IncidentHistoryId);
            
            if (incidentHistory == null)
            {
                throw new KeyNotFoundException("Histórico de incidente não encontrado.");
            }
            
            await _incidentHistoryRepository.DeleteAsync(incidentHistory.Id);

        }
    }
}
