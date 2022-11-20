using System.Reflection;
using Microsoft.AspNetCore.Http;
using Xceed.Document.NET;
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
        var wordDocument = DocX.Load(ms);
        ApllyGOST(wordDocument);
        var stream = typeof(DocX).GetField("_memoryStream", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(wordDocument);

        return Task.FromResult(stream as Stream);
    }

    private void ApllyGOST(DocX document)
    {
        document.MarginTop = 30;
        document.MarginBottom = 0;
        document.MarginLeft = 20;
        document.MarginRight = 10;

        foreach (var paragraph in document.Paragraphs)
        {
            paragraph.
                Font("Times New Roman").
                FontSize(14);

            paragraph.Alignment = Alignment.both;
            paragraph.LineSpacing = (float)15;
        }
        document.Save();
    }
}