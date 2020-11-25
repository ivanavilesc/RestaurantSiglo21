using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AppRestaurantSiglo21.Controllers
{
    public class GeneraPDFController : Controller
    {
        // GET: GeneraPDF
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post()
        {

            //crea documento y asigna tam pagina y margenes
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            //crea instancia de PDFWriter
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            // ############# FORMATEO DE DOCUMENTO #########################

            Chunk chunk = new Chunk("DETALLE DE TU PAGO", FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(chunk);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            //Cell no 1
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            Image image = Image.GetInstance(Server.MapPath("~/Content/Images/RestSiglo21Logo.png"));
            image.ScaleAbsolute(200, 150);
            cell.AddElement(image);
            table.AddCell(cell);

            //Cell no 2
            chunk = new Chunk("Restaurant SIGLO XXI\nDirección: Avda Siempreviva 1340\nComuna: Santiago\nTelefono: 56 22 5703212\nemail: reservarestaurantsigloxxi@gmail.com", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK));
            cell = new PdfPCell();
            cell.Border = 0;
            cell.AddElement(chunk);
            table.AddCell(cell);

            //Add table to document    
            pdfDoc.Add(table);
                                   
            //Image image = Image.GetInstance(Server.MapPath("~/Content/Images/RestSiglo21Logo.png") );
            image.ScalePercent(50f);
            image.SetAbsolutePosition(440, 10);
            image.ScaleToFit(120f, 155.25f);

            

           

            Paragraph para = new Paragraph("Gracias por preferir RESTAURANT SIGLO XXI, a continuación tienes el detalle de tu boleta:");
            pdfDoc.Add(para);
            //#################   FIN DE FORMATEO DE DOCUMENTO  ###############

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Comprobante_Pago.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return View();
        }

    }
}