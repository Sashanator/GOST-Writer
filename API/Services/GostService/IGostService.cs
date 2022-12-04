using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Gost.Services.GostService;

public interface IGostService
{
    void ApplyGostSettings(DocX document);
    
    IList<Paragraph> GetBasicParagraphs(DocX document);

    void ApplyCommonProperties(DocX document);

    void ApplyDocumentMarginInCm(DocX document);

    void ApplyBasicSectionsProperties(DocX document);

    void ApplyDocumentProperties(DocX document);

    void ReplaceEmptyWithPageBreak(DocX document);

    void ApplyBasicParagraphsProperties(DocX document, Paragraph paragraph);

    void AddPageNumeration(DocX document);

    void ApplyFootersProperties(DocX document);

    void ApplyHeadersProperties(DocX document);
}