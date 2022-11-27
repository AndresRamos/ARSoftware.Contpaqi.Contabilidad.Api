using Api.Core.Application.Cuentas.Commands;
using Api.Core.Application.Cuentas.Queries.GetCreateCuentaRequestForTesting;
using Api.SharedKernel.Requests;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

/// <summary>
///     Cuentas controller
/// </summary>
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
    /// <param name="request">Request with cuenta model and options.</param>
    /// <returns>Guid of created request.</returns>
    /// <response code="201">Cuenta was successfully created.</response>
    /// <response code="400">Request has validation errors.</response>
    /// <response code="500">Internal server error.</response>
    /// <returns>Request id.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePoliza(CreateCuentaRequest request)
    {
        try
        {
            Guid requestId = await _mediator.Send(new CreateCuentaCommand(request));

            string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

            return Created(pathByAction, requestId);
        }
        catch (ValidationException validationException)
        {
            _logger.LogWarning(validationException, "Validation error.");

            foreach (ValidationFailure? error in validationException.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return ValidationProblem();
        }
    }

    /// <summary>
    ///     Creates and returns a CreateCuentaRequest used for testing.
    /// </summary>
    /// <response code="200">Requests created.</response>
    /// <returns>A CreateCuentaRequest used for testing.</returns>
    [HttpGet("GetCreateCuentaRequestForTesting")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateCuentaRequest>> GetCreateCuentaRequestForTesting()
    {
        CreateCuentaRequest request = await _mediator.Send(new GetCreateCuentaRequestForTestingQuery());

        return Ok(request);
    }
}
