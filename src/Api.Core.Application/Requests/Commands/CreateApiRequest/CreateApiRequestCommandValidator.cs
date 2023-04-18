using FluentValidation;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public class CreateApiRequestCommandValidator : AbstractValidator<CreateApiRequestCommand>
{
    public CreateApiRequestCommandValidator()
    {
        RuleFor(r => r.SubscriptionKey).NotEmpty();

        RuleFor(r => r.EmpresaRfc).NotNull();
    }
}
