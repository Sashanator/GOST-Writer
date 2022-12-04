using Microsoft.AspNetCore.Http;

namespace Application.Services.WordService.WordService;

public interface IWordService
{
    Task<Stream> FormatWordDocument(IFormFile document, CancellationToken cancellationToken);
}