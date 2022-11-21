﻿using System.Net;
using API.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
[Route("[controller]")]
public class WordController : ControllerBase
{
    private readonly IWordService _wordService;

    public WordController(IWordService wordService)
    {
        _wordService = wordService;
    }

    [HttpPost("format-document")]
    public async Task<IActionResult> FormatDocument([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            var document = await _wordService.FormatWordDocument(file, cancellationToken);
            var result = File((document as MemoryStream).ToArray(), "application/octet-stream", "HELLO.docx");
            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}