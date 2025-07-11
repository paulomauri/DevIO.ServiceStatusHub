using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.WebApi.Models;

namespace ServiceStatusHub.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncidentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IncidentDto>> GetById(Guid id)
        {
            var query = new GetIncidentByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncident(Guid id)
        {
            var command = new RemoveIncidentCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIncident(Guid id, [FromBody] UpdateIncidentCommand command)
        {
            if(id != command.incidentId)
        {
                return BadRequest("O ID do Incidente não confere com o ID da requisição.");
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent([FromQuery] int count = 50)
        {
            var query = new GetRecentIncidentQuery(count);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
