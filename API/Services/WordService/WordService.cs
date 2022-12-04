using System.Reflection;
using Gost.Services.GostService;
using Xceed.Words.NET;

namespace Gost.Services.WordService;

public class WordService : IWordService
{
    private readonly IGostService _gostService;
    public WordService(IGostService gostService)
    {
        _gostService = gostService;
    }

    public Task<Stream> FormatWordDocument(IFormFile document, CancellationToken cancellationToken)
    {
        // Принять Word документ
        // Поменять весь шрифт на Times New Roman
        // Вернуть документ
        var ms = new MemoryStream();
        document.CopyTo(ms);
        var wordDocument = DocX.Load(ms);
        _gostService.ApplyGostSettings(wordDocument);
        var stream = GetDocumentStream(wordDocument);
        return Task.FromResult(stream);
    }

    private Stream GetDocumentStream(DocX document)
    {
        return document.GetType().
            GetField("_memoryStream", BindingFlags.NonPublic | BindingFlags.Instance).
            GetValue(document) as Stream;
    }
}