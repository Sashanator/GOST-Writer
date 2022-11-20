using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xceed.Words.NET;

namespace Application.Services;

public class WordService : IWordService
{
    public Task<FileStreamResult> UpdateWordDocument(IFormFile document, CancellationToken cancellationToken)
    {
        // Принять Word документ
        // Поменять весь шрифт на Times New Roman
        // Вернуть документ
        var ms = new MemoryStream();
        document.CopyTo(ms);
        var doc = DocX.Load(ms);
        doc.InsertParagraph("Hello!");
        doc.SaveAs("NewDocument.docx");
        var stream = typeof(DocX).GetField("_memoryStream", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(doc);

        return Task.FromResult(new FileStreamResult(new MemoryStream(), "application/octet-stream")
        {
            FileDownloadName = "Testing.docx"
        });
    }
}