using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AsposePDF101.Pages
{
    public class IndexModel : PageModel
    {
        [ BindProperty]
        public string Html { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private DocumentServiceFactory _docServiceFactory;

        public IndexModel(ILogger<IndexModel> logger, DocumentServiceFactory asposeServiceFactory)
        {
            _logger = logger;
            _docServiceFactory = asposeServiceFactory;
            Html = @"<h1 style=""text-align:center"">&nbsp;</h1>

<h1 style=""text-align:center"">&nbsp;</h1>

<h1 style=""text-align:center"">&nbsp;</h1>

<h1 style=""text-align:center""><strong><span style=""font-family:Arial,Helvetica,sans-serif""><span style=""color:#e74c3c"">md:Title</span></span></strong></h1>

<h3 style=""text-align:center""><strong><span style=""font-family:Arial,Helvetica,sans-serif""><em><span style=""color:#95a5a6"">by md:Author</span></em></span></strong></h3>

<div style=""page-break-after:always""><span style=""display:none"">&nbsp;</span></div>

<h2 style=""text-align:center"">&nbsp;</h2>

<h2 style=""text-align:center"">&nbsp;</h2>

<h2 style=""text-align:center"">&nbsp;</h2>

<h2 style=""text-align:center"">&nbsp;</h2>

<h2 style=""text-align:center""><strong><span style=""font-family:Arial,Helvetica,sans-serif"">Table of Content</span></strong></h2>

<ol>
	<li><span style=""font-family:Arial,Helvetica,sans-serif""><span style=""font-size:16px"">Introduction&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;1</span></span></li>
	<li><span style=""font-size:16px""><span style=""font-family:Arial,Helvetica,sans-serif"">Description&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 2</span></span></li>
	<li><span style=""font-size:16px""><span style=""font-family:Arial,Helvetica,sans-serif"">Conclusion&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3</span></span></li>
</ol>

<h2 style=""text-align:center""><span style=""font-size:16px"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span></h2>

<div style=""page-break-after:always""><span style=""display:none"">&nbsp;</span></div>

<p style=""text-align:center"">&nbsp;&nbsp;</p>

<h2 style=""text-align:center""><span style=""font-family:Arial,Helvetica,sans-serif""><u>md:Page1_Title</u></span></h2>

<p>&nbsp;</p>

<p><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">md:Page1_Field1:</span></span><strong><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">&nbsp;md:Page1_Field1_Value</span></span></strong></p>

<p><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">md:Page1_Field2:</span></span><strong><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">&nbsp;md:Page1_Field2_Value</span></span></strong></p>

<p><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">md:Page1_Field3:</span></span><strong><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">&nbsp;md:Page1_Field3_Value</span></span></strong></p>

<p>&nbsp;</p>

<p><strong><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">Executive Summary</span></span></strong></p>

<p>&nbsp;</p>

<p style=""text-align:justify""><span style=""font-size:14px""><span style=""font-family:Arial,Helvetica,sans-serif"">Paragraph one&nbsp;<br />
<br />
Paragraph two<br />
<br />
Paragraph three</span></span></p>

<div style=""page-break-after:always""><span style=""display:none"">&nbsp;</span></div>

<h2 style=""text-align:center""><span style=""font-family:Arial,Helvetica,sans-serif""><u>Conclusion</u></span><br />
&nbsp;</h2>

";
        }

        public void OnGet()
        {
            
        }

        public async Task<ActionResult> OnPost()
        {
            if (string.IsNullOrWhiteSpace(Html)) Html = "<h1 style='color:red'>No data passed...</h1>";
            var service=_docServiceFactory.GetServiceInstance(DocumentType.PDF);
            var res=await service.CreatedDocument(Html);            
            return File(res, "application/pdf", "test.pdf");
        }
    }
}
