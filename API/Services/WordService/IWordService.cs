namespace Gost.Services.WordService;

public interface IWordService
{
    Task<Stream> FormatWordDocument(IFormFile document, CancellationToken cancellationToken);
}