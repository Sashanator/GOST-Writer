using System.Net;
using API.Extensions;
using API.RequestHandling.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
[Route("[controller]")]
public class WordController : ControllerBase
{
    private readonly IMediator _mediator;

    public WordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("word-count")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWordCount(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWordCountRequest(), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}