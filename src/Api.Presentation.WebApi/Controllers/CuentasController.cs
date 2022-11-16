using Api.Core.Application.Cuentas.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CuentasController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly ILogger<CuentasController> _logger;
    private readonly IMediator _mediator;

    public CuentasController(IMediator mediator, LinkGenerator linkGenerator, ILogger<CuentasController> logger)
    {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
        _logger = logger;
    }

    /// <summary>
    ///     Creates a new cuenta.
    /// </summary>
    /// <param name="command">Request with cuenta model and options.</param>
    /// <returns>Guid of created request.</returns>
    /// <response code="201">Cuenta was successfully created.</response>
    /// <response code="400">Request has validation errors.</response>
    /// <response code="500">Internal server error.</response>
    /// <returns>Id of newly created request.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePoliza(CreateCuentaCommand command)
    {
        try
        {
            Guid requestId = await _mediator.Send(command);

            string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

            return Created(pathByAction, requestId);
        }
        catch (ValidationException validationException)
        {
            foreach (ValidationFailure? error in validationException.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return ValidationProblem();
        }
    }
}
