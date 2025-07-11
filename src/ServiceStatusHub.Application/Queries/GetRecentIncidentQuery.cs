using MediatR;
using ServiceStatusHub.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Queries;

public record GetRecentIncidentQuery(int count) : IRequest<List<IncidentDto>>;

