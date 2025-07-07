using FluentValidation;
using ServiceStatusHub.Application.Commands.Service;

namespace ServiceStatusHub.Application.Validators.Service
{
    public class UpadateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpadateServiceCommandValidator()
        {
            RuleFor(x => x.serviceId)
                .NotEmpty().WithMessage("Id do serviço deve ser obrigatório");
            
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Nome deve ser obrigatório")
                .MaximumLength(100).WithMessage("Nome deve possuir tamanho máximo de 100 caracteres");
            
            RuleFor(x => x.url)
                .NotEmpty().WithMessage("Url do serviço deve ser obrigatório")
                .MaximumLength(255).WithMessage("Url deve possuir tamanho máximo de 255 caracteres");
            
            RuleFor(x => x.environment)
                .NotEmpty().WithMessage("Ambiente deve ser obrigatório")
                .MaximumLength(50).WithMessage("Ambiente deve possuir tamanho máximo de 50 caracteres");
            
            RuleFor(x => x.type)
                .NotEmpty().WithMessage("Tipo de serviço deve ser obrigatório")
                .Must(status => Enum.IsDefined(typeof(Domain.Enums.ServiceType), status))
                .WithMessage("Tipo de serviço deve ser um valor válido.");
        }
    }
}
