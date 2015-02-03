using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Entidades;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;

namespace tamboprp
{
    public partial class Enfermedades : System.Web.UI.Page
    {
        private List<Enfermedad> _listTemp = new List<Enfermedad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.CargarEnfermedades();
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sanidad", "Sanidad.aspx"));
            list.Add(new VoListItemDuplaString("Tabla de Enfermedades", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvEnfermedades.DataSource = null;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
            this.lblStatus.Text = "";
            this.newEnfermedad.Text = "";
        }

        private void CargarEnfermedades()
        {
            _listTemp = Fachada.Instance.GetEnfermedades();
            this.gvEnfermedades.DataSource = _listTemp;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = _listTemp.Count.ToString();
        }

        protected void GvEnfermedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            CargarEnfermedades();
        }

        protected void btn_GuardarEnfermedad(object sender, EventArgs e)
        {
            try
            {
                if (this.GuardarEnfermedad())
                {
                    this.lblStatus.Text = "La enfermedad se ha guardado con éxito";
                    this.CargarEnfermedades();
                }
                else
                {
                    this.lblStatus.Text = "La enfermedad no se ha podido guardar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool GuardarEnfermedad()
        {
            if (this.newEnfermedad.Text != "")
            {
                var usu = (VOUsuario)Session["Usuario"];
                var enf = new Enfermedad(this.newEnfermedad.Text);
                return Fachada.Instance.EnfermedadInsert(enf, usu.Nickname);
            }
            else
            {
                this.lblStatus.Text = "Ingrese un nombre";
            }
            return false;
        }


        private void CargarGridParaExportar()
        {
            this.gvEnfermedades.AllowPaging = false;
            this.gvEnfermedades.EnableViewState = false;
            _listTemp = Fachada.Instance.GetEnfermedades();
            this.gvEnfermedades.DataSource = _listTemp;
            this.gvEnfermedades.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=Enfermedades.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvEnfermedades.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvEnfermedades);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Enfermedades", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvEnfermedades.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvEnfermedades.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Enfermedades</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvEnfermedades.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Enfermedades.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvEnfermedades.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

    }
}