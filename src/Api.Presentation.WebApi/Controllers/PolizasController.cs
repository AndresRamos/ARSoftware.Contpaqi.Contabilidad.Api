using Api.Core.Application.Polizas.Commands.CreatePoliza;
using Api.Core.Application.Polizas.Queries.GetCreatePolizaRequestForTesting;
using Api.SharedKernel.Requests;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

/// <summary>
///     Polizas controller
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
    /// <param name="request">Request with poliza model and options.</param>
    /// <returns>Guid of created request.</returns>
    /// <response code="201">Poliza was successfully created.</response>
    /// <response code="400">Request has validation errors.</response>
    /// <response code="500">Internal server error.</response>
    /// <returns>Request id.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePoliza(CreatePolizaRequest request)
    {
        try
        {
            Guid requestId = await _mediator.Send(new CreatePolizaCommand(request));

            string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

            return Created(pathByAction, requestId);
        }
        catch (ValidationException validationException)
        {
            _logger.LogWarning(validationException, "Validation error.");

            foreach (ValidationFailure error in validationException.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return ValidationProblem();
        }
    }

    /// <summary>
    ///     Creates and returns a CreatePolizaRequest used for testing.
    /// </summary>
    /// <response code="200">Requests created.</response>
    /// <returns>A CreatePolizaRequest used for testing.</returns>
    [HttpGet("GetCreatePolizaRequestForTesting")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreatePolizaRequest>> GetCreatePolizaRequestForTesting()
    {
        CreatePolizaRequest request = await _mediator.Send(new GetCreatePolizaRequestForTestingQuery());

        return Ok(request);
    }
}
