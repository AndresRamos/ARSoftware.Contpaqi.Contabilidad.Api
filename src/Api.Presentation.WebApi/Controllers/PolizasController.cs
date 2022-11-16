using Api.Core.Application.Polizas.Commands.CreatePoliza;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

/// <summary>
///     Work with Polizas
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PolizasController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly ILogger<PolizasController> _logger;
    private readonly IMediator _mediator;

    public PolizasController(IMediator mediator, LinkGenerator linkGenerator, ILogger<PolizasController> logger)
    {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
        _logger = logger;
    }

    /// <summary>
    ///     Creates a new poliza.
    /// </summary>
    /// <param name="command">Request with poliza model and options.</param>
    /// <returns>Guid of created request.</returns>
    /// <response code="201">Poliza was successfully created.</response>
    /// <response code="400">Request has validation errors.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePoliza([FromBody] CreatePolizaCommand command)
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
