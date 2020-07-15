using Aspose.Pdf;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//TODO
/// <summary>
/// Factory & singleton patterns are used
/// can be refactored to any pattern
/// </summary>
namespace DocumentLib
{
    public enum DocumentType
    {
        PDF,
        WORD
    }
    public interface IDocumentService
    {
        public Task<Stream> CreatedDocument(string htmlString);
    }
    public class DocumentServiceFactory
    {
        public IDocumentService GetServiceInstance(DocumentType docType)
        {
            IDocumentService ret = null;
            switch (docType)
            {
                case DocumentType.PDF:
                    {
                        ret = DocumentPDFService.Instance;
                        break;
                    }
            }
            return ret;
        }
    }

    internal class DocumentPDFService : IDocumentService
    {
        public static DocumentPDFService Instance = new DocumentPDFService();
        private DocumentPDFService()
        {

        }

        public Task<Stream> CreatedDocument(string htmlString)
        {
            return Task.Factory.StartNew<Stream>(()=>
            {
                var ms = new MemoryStream();
                // Initialize document object
                Document document = new Document();
                
                htmlString.Split("</html>").ToList().ForEach(str =>
                {
                    var pgStr = str.Replace("<html>", "").Replace("\r","").Replace("\n","").Trim();
                    if (!string.IsNullOrWhiteSpace(pgStr))
                    {
                        // Add page
                        Page page = document.Pages.Add();

                        HtmlFragment htmlFragment = new HtmlFragment(pgStr);
                        page.Paragraphs.Add(htmlFragment);
                    }
                });
                
                // Save updated PDF
                document.Save(ms);
                return ms;
            });
        }
    }
}
