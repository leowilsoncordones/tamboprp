using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;
using Font = System.Drawing.Font;

namespace tamboprp
{
    public partial class DiagEcograficos : System.Web.UI.Page
    {

        private List<VOServicio> _listEnt = Fachada.Instance.GetServicios35SinDiagPrenezVaqEnt();
        private List<VOServicio> _listOrd = Fachada.Instance.GetServicios35SinDiagPrenezVacOrdene();
        private List<VOServicio> _listSec = Fachada.Instance.GetServicios35SinDiagPrenezVacSecas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                this.SetPageBreadcrumbs();
                cargarGrilla();
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Vacas con 35 días de servicio y sin diagnóstico de preñez", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void cargarGrilla()
        {
            var list = new List<VOServicio>();
            list.AddRange(_listEnt);
            list.AddRange(_listOrd);
            list.AddRange(_listSec);
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvServicios.DataSource = list;
            this.gvServicios.DataBind();
        }

        protected void gvServicios_created(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 1)
            {
                GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc = new TableCell();
                tc.Text = "VAQUILLONAS ENTORADAS  " + "( " + _listEnt.Count + " )";
                tc.ColumnSpan = 7;
                tc.Font.Bold = true;
                tc.BackColor = Color.LightBlue;
                tc.HorizontalAlign = HorizontalAlign.Center;
                gvRow.Cells.Add(tc);
                gvServicios.Controls[0].Controls.AddAt(1, gvRow);
            }

            if (e.Row.RowIndex == _listEnt.Count + 2)
            {
                GridViewRow gvRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc1 = new TableCell();
                tc1.Text = "VACAS EN ORDEÑE  " + "( " + _listOrd.Count + " )";
                tc1.ColumnSpan = 7;
                tc1.Font.Bold = true;
                tc1.BackColor = Color.LightBlue;
                tc1.HorizontalAlign = HorizontalAlign.Center;
                gvRow1.Cells.Add(tc1);
                gvServicios.Controls[0].Controls.AddAt(_listEnt.Count + 2, gvRow1);
            }

            if (e.Row.RowIndex == _listEnt.Count + _listOrd.Count + 3)
            {
                GridViewRow gvRow2 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc2 = new TableCell();
                tc2.Text = "VACAS SECAS  " + "( " + _listSec.Count + " )";
                tc2.ColumnSpan = 7;
                tc2.Font.Bold = true;
                tc2.BackColor = Color.LightBlue;
                tc2.HorizontalAlign = HorizontalAlign.Center;
                gvRow2.Cells.Add(tc2);
                gvServicios.Controls[0].Controls.AddAt(_listEnt.Count + _listOrd.Count + 3, gvRow2);
            }
        }


        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=DiagEcograficos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvServicios.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvServicios);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Diagnósticos ecográficos", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = "http://www.tamboprp.uy/img_tamboprp/corporativo/logojpeg.jpg";
            //pdfDoc.Add(new Chunk(new Jpeg(new Uri("D:/ORT laptop/2014-S5-Proyecto/tamboprp-git/tamboprp/img_tamboprp/corporativo/logojpeg.jpg")),300f,-10f));
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        protected void print_Click(object sender, EventArgs e)
        {
            gvServicios.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvServicios.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Diagnósticos ecográficos</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvServicios.PagerSettings.Visible = true;
        }
        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=DiagEcograficos.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            this.gvServicios.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }
    }
}