using System.Net;
using API.BaseRequestHandling;
using API.Extensions;
using API.RequestHandling.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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
    public async Task<IActionResult> GetWordCount([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWordCountRequest(file), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}