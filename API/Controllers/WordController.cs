using System.Net;
using API.BaseRequestHandling;
using API.Extensions;
using API.RequestHandling.Requests;
using Application.Services;
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
    private readonly IWordService _wordService;

    public WordController(IMediator mediator, IWordService wordService)
    {
        _mediator = mediator;
        _wordService = wordService;
    }

    [HttpGet("word-count")]
    public async Task<IActionResult> GetWordCount([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            var document = await _wordService.UpdateWordDocument(file, cancellationToken);
            var result = File((document as MemoryStream).ToArray(), "application/octet-stream", "HELLO.docx");
            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}