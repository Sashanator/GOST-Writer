using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface IWordService
{
    Task<Stream> FormatWordDocument(IFormFile document, CancellationToken cancellationToken);
}