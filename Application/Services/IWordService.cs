using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services;

public interface IWordService
{
    Task<FileStreamResult> UpdateWordDocument(IFormFile document, CancellationToken cancellationToken);
}