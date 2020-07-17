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
    public enum DocumentType
    {
        PDF,
        WORD
    }
    public interface IDocumentService
    {
        //public Task<byte[]> CreatedDocument(string htmlString);
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

        //public Task<byte[]> CreatedDocument(string htmlString)
        //{
           
        //    return Task.Factory.StartNew<byte[]>(()=>
        //    {           

        //        //Selectpdf
        //        HtmlToPdf converter = new HtmlToPdf();
        //        converter.Options.PdfPageSize = PdfPageSize.A4;
        //        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
               
        //        var doc = converter.ConvertHtmlString($"{htmlString}");
        //        var bt=doc.Save();
        //        doc.Close();
        //        return bt;
        //    });
        //}

        
    }
}
