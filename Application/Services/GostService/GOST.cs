using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Application.Services.GostService
{
    public class GOST
    {
        private readonly DocX _document;

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

        public GOST(DocX aDocument)
        {
            _document = aDocument;
        }

        public void ApllyGOST()
        {
            ApplyDocumentProperties();
            ApplyCommonProperties();
            ApplyBasicSectionsProperies();
            _document.Save();
        }

        private IList<Paragraph> GetBasicParagraphs()
        {
            bool isTitleEnded = false;
            var res = new List<Paragraph>();
            foreach (Paragraph paragraph in _document.Paragraphs)
            {
                if (paragraph.Text.ToLower().Contains("содержание"))
                    isTitleEnded = true;

                if (isTitleEnded)
                    res.Add(paragraph);
            }
            return res;
        }


        private void ApplyDocumentProperties()
        {
            ApplyDocumentMarginInCm(3, 2, 1, 2);
            AddPageNumeration();
            ApplyFootersProperies();
            ApplyHeadersProperies();
            ReplaceEmptyWithPageBreak();
        }

        private void ApplyCommonProperties()
        {
            foreach (var paragraph in _document.Paragraphs)
            {
                paragraph.
                    Font("Times New Roman").
                    Color(Color.Black);
            }
        }


        private void ApplyBasicSectionsProperies()
        {
            foreach (var section in GetBasicParagraphs())
            {
                ApplyBasicParagraphsProperties(section);
            }
        }


        private void ApplyDocumentMarginInCm(float left, float top, float right, float bottom)
        {
            _document.MarginLeft = CmToPt(left);
            _document.MarginTop = CmToPt(top);
            _document.MarginRight = CmToPt(right);
            _document.MarginBottom = CmToPt(bottom);
        }


        private List<Paragraph> EmptyParagraphsSeries = new();
        private void ReplaceEmptyWithPageBreak()
        {
            foreach (var paragraph in GetBasicParagraphs())
            {
                if (IsSectionStart(paragraph))
                {
                    EmptyParagraphsSeries.ForEach(p => _document.RemoveParagraph(p));
                    EmptyParagraphsSeries.Clear();
                    paragraph.InsertParagraphBeforeSelf("").InsertPageBreakBeforeSelf();
                    continue;
                }

                if (IsText(paragraph.Text))
                {
                    EmptyParagraphsSeries.Clear();
                }
                else
                {
                    EmptyParagraphsSeries.Add(paragraph);
                }
            }
        }


        private void ApplyBasicParagraphsProperties(Paragraph paragraph)
        {
            paragraph.FontSize(14);
            if (!IsHeader(paragraph))
                paragraph.Alignment = Alignment.both;

            if (paragraph.PreviousParagraph is not null && IsSectionStart(paragraph))
            {
                _document.RemoveParagraph(paragraph.PreviousParagraph);
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

        private void AddPageNumeration()
        {
            _document.AddFooters();
            _document.Footers.Odd.PageNumbers = true;
            _document.Footers.Even.PageNumbers = true;
            _document.Footers.First.PageNumbers = false;
        }

        private void ApplyFootersProperies()
        {
            ApplyNotesProperies(_document.Footers?.Odd);
            ApplyNotesProperies(_document.Footers?.Even);
        }
        private void ApplyHeadersProperies()
        {
            ApplyNotesProperies(_document.Headers?.Odd);
            ApplyNotesProperies(_document.Headers?.Even);
        }

        private void ApplyNotesProperies(Container? note)
        {
            if (note?.Paragraphs is null)
                return;

            foreach (var paragraph in note.Paragraphs)
            {
                paragraph.FontSize(12);
            }
        }
    }
}
