using Api.Core.Application.Cuentas.Validators;
using FluentValidation;

namespace Api.Core.Application.Cuentas.Commands;

public sealed class CreateCuentaCommandValidator : AbstractValidator<CreateCuentaCommand>
{
    public CreateCuentaCommandValidator()
    {
        RuleFor(m => m.ApiRequest).NotNull();

        RuleFor(m => m.ApiRequest.Model).NotNull();

        RuleFor(m => m.ApiRequest.Model).SetValidator(new CreateCuentaModelValidator());
    }
}
