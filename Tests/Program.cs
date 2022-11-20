using Application.Tools;
using Xceed.Words.NET;

class Program
{
    static void Main(string[] args)
    {       
        string documentsDirectory = "D:\\Documents\\ITMO\\4.1 year\\Gost\\TestDocuments";
        string testIn = Path.Combine(documentsDirectory, "testIn.docx");
        string testOut = Path.Combine(documentsDirectory, "testOut.docx");
        var document = DocX.Load(testIn);
        var gost = new GOST(document);
        gost.ApllyGOST();
        document.SaveAs(testOut);
    }
}