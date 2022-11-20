using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services;

public interface IWordService
{
    Task<Stream> UpdateWordDocument(IFormFile document, CancellationToken cancellationToken);
}