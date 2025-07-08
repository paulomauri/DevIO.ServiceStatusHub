using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;

namespace ServiceStatusHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllServicesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetServiceByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand command)
    {
        if (id != command.serviceId)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new RemoveServiceCommand(id));
        return NoContent();
    }
}
