using Aspose.Pdf;
using DinkToPdf;
using DinkToPdf.Contracts;
using SelectPdf;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;

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
        SelectPdf,
        WkHtmlDinkToPdf,
        ItextSharpLGPLv2
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
            switch (lib)
            {
                case Library.SelectPdf:
                    {
                        return CreateDocumentSelectPDF(htmlString);
                    }
                case Library.Aspose:
                    {
                        return CreateDocumentAspose(htmlString);
                    }
                case Library.WkHtmlDinkToPdf:
                    {
                        return CreatedDocumentDinkToPDF(htmlString);
                    }
                case Library.ItextSharpLGPLv2:
                {
                    return CreatedDocumentItextSharp(htmlString);
                }
                default:
                    {
                        throw new Exception("Invalid Library");
                    }
            }
        }

        private Task<Stream> CreatedDocumentItextSharp(string htmlString)
        {
            return Task.Factory.StartNew<Stream>(() =>
            {
                var ms = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);
                //create pdf file header
                document.Open();                
             
                HtmlWorker worker = new HtmlWorker(document);
                worker.Open();
                worker.StartDocument();
                htmlString.Split("<div style=\"page-break-after:always\"><span style=\"display:none\">&nbsp;</span></div>").ToList().ForEach(page =>
                {
                    page = page.Replace("<p>&nbsp;</p>", "<p><br/></p>");
                    document.NewPage();
                    worker.NewPage();
                    var reader = new StringReader(page);
                    worker.Parse(reader);

                    foreach (IElement element in HtmlWorker.ParseToList(reader, null))
                    {
                        document.Add((IElement)element);
                    }
                });
              
                worker.EndDocument();
                worker.Close();                        
                document.Close();
                pdfWriter.Close();
                return new MemoryStream(ms.ToArray());
            });
        }

        public Task<Stream> CreatedDocumentDinkToPDF(string htmlString)
        {
            return Task.Factory.StartNew<Stream>(() =>
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlString
                    }
                }
                };

                IConverter converter = new SynchronizedConverter(new PdfTools());
                var ms = new MemoryStream(converter.Convert(doc));
                return ms;
            });
        }
       
        public Task<Stream> CreateDocumentAspose(string htmlString)
        {
            return Task.Factory.StartNew<Stream>(() =>
            {
                var ms = new MemoryStream();
                //Aspose
                Aspose.Pdf.Document document = new Aspose.Pdf.Document();

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
