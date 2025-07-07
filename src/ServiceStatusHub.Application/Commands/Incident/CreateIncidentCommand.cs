using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Commands.Incident;

public record CreateIncidentCommand(Guid ServiceId, string description, string Status) : IRequest<Guid>;