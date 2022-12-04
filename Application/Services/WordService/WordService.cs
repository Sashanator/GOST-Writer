using System.Reflection;
using Application.Tools;
using Microsoft.AspNetCore.Http;
using Xceed.Words.NET;

namespace Application.Services.WordService;

public class WordService : IWordService
{
    public Task<Stream> FormatWordDocument(IFormFile document, CancellationToken cancellationToken)
    {
        // Принять Word документ
        // Поменять весь шрифт на Times New Roman
        // Вернуть документ
        var ms = new MemoryStream();
        document.CopyTo(ms);
        var wordDocument = DocX.Load(ms);
        var gost = new GOST(wordDocument);
        gost.ApllyGOST();
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