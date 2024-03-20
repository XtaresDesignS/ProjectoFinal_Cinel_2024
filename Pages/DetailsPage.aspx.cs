using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using ProjectoFinal_Cinel_2024.Assets.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages
{
    public partial class DetailsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_PDF_Click(object sender, EventArgs e)
        {

            string nomeFicheiro = $"ficheiroUtilizador";


            EncriptDesencript fileEncript = new EncriptDesencript();

            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlText = new HtmlTextWriter(sw);

            this.Page.RenderControl(htmlText);
            StringReader sreader = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc,Response.OutputStream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sreader);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment;filename={fileEncript.Encriptar(nomeFicheiro)}.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}