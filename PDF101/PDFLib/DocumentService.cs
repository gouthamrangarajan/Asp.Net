using Aspose.Pdf;
using SelectPdf;
using System;
using System.Data.Common;
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
    public enum Library
    {
        Aspose,
        SelectPdf
    }
    public enum DocumentType
    {
        PDF,
        WORD
    }
    public interface IDocumentService
    {        
         Task<Stream> CreateDocument(string htmlString, Library lib);
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

        public Task<Stream> CreateDocument(string htmlString, Library lib)
        {
            switch(lib){
                case Library.SelectPdf:
                    {
                        return CreateDocumentSelectPDF(htmlString);
                    }
                case Library.Aspose:
                {
                    return CreateDocumentAspose(htmlString);
                }
                default:
                    {
                        throw new Exception("Invalid Library");
                    }
            }
        }


        public Task<Stream> CreateDocumentAspose(string htmlString)
        {
            return Task.Factory.StartNew<Stream>(() =>
            {
                var ms = new MemoryStream();
                //Aspose
                Document document = new Document();

                // Add page
                Page page = document.Pages.Add();
                HtmlFragment htmlFragment = new HtmlFragment(htmlString);
                page.Paragraphs.Add(htmlFragment);

                document.Save(ms);
                return ms;
            });
        }

        
        public Task<Stream> CreateDocumentSelectPDF(string htmlString)
        {

            return Task.Factory.StartNew<Stream>(() =>
            {
               
                //Selectpdf
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

                var doc = converter.ConvertHtmlString($"{htmlString}");
                var bt = doc.Save();
                doc.Close();
                var ms = new MemoryStream(bt);
                return ms;
            });
        }


    }
}
