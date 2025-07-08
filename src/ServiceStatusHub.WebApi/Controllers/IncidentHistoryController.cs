using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.WebApi.Models;

namespace ServiceStatusHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncidentHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("incident/{incidentId:guid}")]
        public async Task<IActionResult> GetByIncidentId(Guid incidentId)
        {
            var query = new GetIncidentHistoryByIdIncidentQuery(incidentId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent([FromQuery] int count = 50)
        {
            var query = new GetRecentIncidentHistoryQuery(count);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory([FromBody] AddIncidentHistoryRequest request)
        {
            var command = new AddIncidentHistoryCommand(
                request.IncidentId,
                request.Action,
                request.Description,
                request.PerformedBy
            );

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIncidentId), new { incidentId = request.IncidentId }, new { id });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHistory(DeleteIncidentHistoryRequest request)
        {
            var command = new RemoveIncidentHistoryCommand(request.IncidentId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
