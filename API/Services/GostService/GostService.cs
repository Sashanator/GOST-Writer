using System.Drawing;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Gost.Services.GostService;

public class GostService : IGostService
{

    #region Units

    private const float CmPerIn = 2.54f;
    private const float PtPerIn = 72;
    private const float CmPerPt = CmPerIn / PtPerIn;
    private const float PtPerLine = 12f;

    #endregion

    #region Units Transactions

    private float CmToPt(float cm) => cm / CmPerPt;
    private float LineToPt(float line) => line * PtPerLine;

    #endregion

    public void ApplyGostSettings(DocX document)
    {
        ApplyDocumentProperties(document);
        ApplyCommonProperties(document);
        ApplyBasicSectionsProperties(document);
        document.Save();
    }

    public IList<Paragraph> GetBasicParagraphs(DocX document)
    {
        var isTitleEnded = false;
        var res = new List<Paragraph>();
        foreach (Paragraph paragraph in document.Paragraphs)
        {
            if (paragraph.Text.ToLower().Contains("содержание"))
                isTitleEnded = true;

            if (isTitleEnded)
                res.Add(paragraph);
        }
        return res;
    }


    public void ApplyDocumentProperties(DocX document)
    {
        ApplyDocumentMarginInCm(document);
        AddPageNumeration(document);
        ApplyFootersProperties(document);
        ApplyHeadersProperties(document);
        ReplaceEmptyWithPageBreak(document);
    }

    public void ApplyCommonProperties(DocX document)
    {
        foreach (var paragraph in document.Paragraphs)
        {
            paragraph.
                Font("Times New Roman").
                Color(Color.Black);
        }
    }


    public void ApplyBasicSectionsProperties(DocX document)
    {
        foreach (var section in GetBasicParagraphs(document))
        {
            ApplyBasicParagraphsProperties(document, section);
        }
    }


    public void ApplyDocumentMarginInCm(DocX document)
    {
        const float left = 3;
        const float top = 2;
        const float right = 1;
        const float bottom = 2;
        document.MarginLeft = CmToPt(left);
        document.MarginTop = CmToPt(top);
        document.MarginRight = CmToPt(right);
        document.MarginBottom = CmToPt(bottom);
    }

    public void ReplaceEmptyWithPageBreak(DocX document)
    {
        List<Paragraph> emptyParagraphsSeries = new();
        foreach (var paragraph in GetBasicParagraphs(document))
        {
            if (IsSectionStart(paragraph))
            {
                emptyParagraphsSeries.ForEach(p => document.RemoveParagraph(p));
                emptyParagraphsSeries.Clear();
                paragraph.InsertParagraphBeforeSelf("").InsertPageBreakBeforeSelf();
                continue;
            }

            if (IsText(paragraph.Text))
            {
                emptyParagraphsSeries.Clear();
            }
            else
            {
                emptyParagraphsSeries.Add(paragraph);
            }
        }
    }


    public void ApplyBasicParagraphsProperties(DocX document, Paragraph paragraph)
    {
        paragraph.FontSize(14);
        if (!IsHeader(paragraph))
            paragraph.Alignment = Alignment.both;

        if (paragraph.PreviousParagraph is not null && IsSectionStart(paragraph))
        {
            document.RemoveParagraph(paragraph.PreviousParagraph);
        }

        paragraph.SetLineSpacing(LineSpacingType.Line, LineToPt(1.5f));
        paragraph.IndentationFirstLine = CmToPt(1.25f);
    }

    private bool IsText(string? line) => line?.Any(c => !char.IsControl(c)) == true;

    private bool IsHeader(Paragraph paragraph)
    {
        return paragraph.MagicText.FirstOrDefault()?.formatting?.Bold == true &&
               paragraph.Alignment == Alignment.center;
    }
    private bool IsSectionStart(Paragraph paragraph)
    {
        return IsHeader(paragraph)
               && paragraph.Text.Where(c => char.IsLetter(c)).All(c => char.IsUpper(c));
    }

    public void AddPageNumeration(DocX document)
    {
        document.AddFooters();
        document.Footers.Odd.PageNumbers = true;
        document.Footers.Even.PageNumbers = true;
        document.Footers.First.PageNumbers = false;
    }

    public void ApplyFootersProperties(DocX document)
    {
        ApplyNotesProperties(document.Footers?.Odd);
        ApplyNotesProperties(document.Footers?.Even);
    }
    public void ApplyHeadersProperties(DocX document)
    {
        ApplyNotesProperties(document.Headers?.Odd);
        ApplyNotesProperties(document.Headers?.Even);
    }

    private void ApplyNotesProperties(Container? note)
    {
        if (note?.Paragraphs is null)
            return;

        foreach (var paragraph in note.Paragraphs)
        {
            paragraph.FontSize(12);
        }
    }
}