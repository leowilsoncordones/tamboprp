using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tamboprp
{
    public partial class CalendarioPartos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.cargarDdl();
                    this.panelExport.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Calendario de Partos", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            LimpiarTabla();
            this.panelExport.Visible = GetProximosPartos();
        }

        private bool GetProximosPartos()
        {
            var idFecha = DateTime.Parse(this.ddlMeses.SelectedValue);
            var listServ = Fachada.Instance.GetProximosPartos(idFecha);
            this.gvPartos.DataSource = listServ;
            this.gvPartos.DataBind();
            this.lblCantAnimales.Text = listServ.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.titCantAnimales.Visible = true;
            return listServ.Count != 0;
        }

        private void cargarDdl() 
        {
            var list = Fachada.Instance.GetMeses();
            this.ddlMeses.DataSource = list;
            this.ddlMeses.DataTextFormatString = "{0:MMMM - yyyy}";
            this.ddlMeses.DataBind();
        }


        private void LimpiarTabla()
        {
            this.gvPartos.DataSource = null;
            this.gvPartos.DataBind();
            this.lblCantAnimales.Visible = false;
            this.lblCantAnimales.Text = "";
            this.titCantAnimales.Visible = false;
        }

        protected void GvPartos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetProximosPartos();
        }


        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=CalendarioPartos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvPartos.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvPartos);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Calendario de partos", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvPartos.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvPartos.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Calendario de partos</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvPartos.PagerSettings.Visible = true;
        }
        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=CalendarioPartos.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            this.gvPartos.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

    }
}