using System.Reflection;
using Microsoft.AspNetCore.Http;
using Xceed.Words.NET;

namespace Application.Services;

public class WordService : IWordService
{
    public Task<Stream> UpdateWordDocument(IFormFile document, CancellationToken cancellationToken)
    {
        // Принять Word документ
        // Поменять весь шрифт на Times New Roman
        // Вернуть документ
        var ms = new MemoryStream();
        document.CopyTo(ms);
        var doc = DocX.Load(ms);
        doc.InsertParagraph("Hello!");
        doc.Save();
        var stream = typeof(DocX).GetField("_memoryStream", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(doc);

        return Task.FromResult(stream as Stream);
    }
}