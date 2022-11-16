using Api.Core.Application.Requests.Commands.SetResponse;
using Api.Core.Application.Requests.Queries.GetPendingRequests;
using Api.Core.Application.Requests.Queries.GetRequestById;
using Api.SharedKernel.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Gets a request by id.
    /// </summary>
    /// <param name="id">Id of request</param>
    /// <returns>The request.</returns>
    /// <response code="200">Request found.</response>
    /// <response code="404">Request not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Request>> Get(Guid id)
    {
        Request? request = await _mediator.Send(new GetRequestByIdQuery(id));

        if (request is null)
            return NotFound();

        return Ok(request);
    }

    /// <summary>
    ///     Gets request pending to be processed.
    /// </summary>
    /// <returns>Collection of pending requests.</returns>
    /// <response code="200">Request found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Request>>> GetPendingRequests()
    {
        IEnumerable<Request> requests = await _mediator.Send(new GetPendingRequestsQuery());

        return Ok(requests);
    }

    /// <summary>
    ///     Sets the response for the request.
    /// </summary>
    /// <param name="command">Reponse</param>
    /// <returns></returns>
    /// <response code="200">Request found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("{id:guid}/response")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> SetResponse(SetResponseCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}
