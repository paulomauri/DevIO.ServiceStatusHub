using FluentAssertions;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Validators.Incident;
using ServiceStatusHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Tests.Application.Validators;

public class CreateIncidentCommandValidatorTests
{
    [Fact]
    public void Validator_Should_Have_Errors_When_Description_Is_Empty()
    {
        var validator = new CreateIncidentCommandValidator();
        var command = new CreateIncidentCommand(Guid.NewGuid(), "", IncidentAction.Resolved.ToString());

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "description");
    }
}
