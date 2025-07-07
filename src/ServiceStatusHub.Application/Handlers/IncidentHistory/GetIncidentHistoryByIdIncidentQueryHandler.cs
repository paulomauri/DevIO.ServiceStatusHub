using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Handlers.IncidentHistory
{
    public class GetIncidentHistoryByIdIncidentQueryHandler : IRequestHandler<GetIncidentHistoryByIdIncidentQuery, List<IncidentHistoryDto>>
    {
        private readonly IIncidentHistoryRepository _repository;
        public GetIncidentHistoryByIdIncidentQueryHandler(IIncidentHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<IncidentHistoryDto>> Handle(GetIncidentHistoryByIdIncidentQuery request, CancellationToken cancellationToken)
        {
            var incidentHistories = await _repository.GetRecentAsync(request.idIncident);
         
            if (incidentHistories == null || !incidentHistories.Any())
            {
                throw new KeyNotFoundException("No incident history found for the given incident ID.");
            }

            return incidentHistories.Select(ih => new IncidentHistoryDto(ih.Id, ih.IncidentId, ih.Action.ToString(), ih.Description, ih.PerformedBy, ih.Timestamp)).ToList();
        }
    }
}
