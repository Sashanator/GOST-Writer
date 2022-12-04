using Gost.Services.GostService;
using NUnit.Framework;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Gost.UnitTesting;

public class GostServiceUnitTests
{
    private DocX _document;
    private GostService _gostService;

    [SetUp]
    public void Setup()
    {
        _document = DocX.Load(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + @"\Resources\testIn.docx");
        _gostService = new GostService();
    }

    [TearDown]
    public void TearDown()
    {
        _document.Dispose();
    }

    [Test]
    public void Get_Basic_Paragraphs()
    {
        // Arrange
        

        // Act
        var result = _gostService.GetBasicParagraphs(_document);

        // Assert
        Assert.AreEqual(201, result.Count);
    }

    [Test]
    public void Apply_Margin_Properties()
    {
        _document.MarginLeft = 11;
        _document.MarginRight = 14;
        _gostService.ApplyDocumentMarginInCm(_document);
        Assert.AreEqual(_document.MarginLeft, 85.0f);
        Assert.AreEqual(_document.MarginRight, 28.0f);
    }

    [Test]
    public void Apply_Basic_Paragraph_Properties()
    {
        var paragraph = _document.Paragraphs[0];
        paragraph.Alignment = Alignment.left;
        paragraph.LineSpacing = 1;

        _gostService.ApplyBasicParagraphsProperties(_document, paragraph);

        Assert.AreEqual(paragraph.Alignment, Alignment.both);
        Assert.AreEqual(paragraph.LineSpacing, 18.0f);
    }

    [Test]
    public void Add_Page_Numeration()
    {
        _gostService.AddPageNumeration(_document);
        
        Assert.AreNotEqual(_document.Footers, null);
        Assert.AreEqual(_document.Footers.Odd.PageNumbers, false);
        Assert.AreEqual(_document.Footers.Even.PageNumbers, false);
        Assert.AreEqual(_document.Footers.First.PageNumbers, false);
    }
}