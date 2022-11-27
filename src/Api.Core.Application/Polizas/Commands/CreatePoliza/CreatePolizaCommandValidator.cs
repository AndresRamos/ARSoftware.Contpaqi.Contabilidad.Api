using Api.Core.Application.Polizas.Validators;
using FluentValidation;

namespace Api.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommandValidator : AbstractValidator<CreatePolizaCommand>
{
    public CreatePolizaCommandValidator()
    {
        RuleFor(m => m.ApiRequest).NotNull();

        RuleFor(m => m.ApiRequest.Model).NotNull();

        RuleFor(m => m.ApiRequest.Model).SetValidator(new CreatePolizaModelValidator());
    }
}
