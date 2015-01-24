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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;

namespace tamboprp
{
    public partial class EmpresasRemisoras : System.Web.UI.Page
    {
        private List<VOEmpresa> _lstRemisoras;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarModal();
                this.CargarEmpresasRemisoras();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Empresas remisoras", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvRemisoras.DataSource = null;
            this.gvRemisoras.DataBind();
        }

        private void CargarEmpresasRemisoras()
        {
            _lstRemisoras = Fachada.Instance.GetEmpresasRemisoras();
            this.CargarGvEmpresasRemisoras();
            this.CargarDdlEmpresas();
        }

        private void CargarGvEmpresasRemisoras()
        {
            this.gvRemisoras.DataSource = _lstRemisoras;
            this.gvRemisoras.DataBind();
        }

        protected void GvRemisoras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
        }

        private void CargarDdlEmpresas()
        {
            this.ddlEmpresas.DataSource = _lstRemisoras;
            this.ddlEmpresas.DataTextField = "Nombre";
            this.ddlEmpresas.DataValueField = "Id";
            this.ddlEmpresas.DataBind();
        }

        protected void btn_GuardarRemisora(object sender, EventArgs e)
        {
            if (this.fNombre.Text != "")
            {
                var nuevaRemisora = new VOEmpresa();
                nuevaRemisora.Nombre = this.fNombre.Text;
                nuevaRemisora.RazonSocial = this.fRazonSocial.Text;
                nuevaRemisora.Rut = this.fRut.Text;
                nuevaRemisora.Telefono = this.fTelefono.Text;
                nuevaRemisora.Direccion = this.fDireccion.Text;
                if (Fachada.Instance.GuardarEmpresaRemisora(nuevaRemisora))
                {
                    this.lblStatus.Text = "La empresa se guardó con éxito";
                    this.CargarEmpresasRemisoras();
                }
                else
                {
                    this.lblStatus.Text = "La empresa no se pudo guardar";
                }
            }
            else this.lblStatus.Text = "Ingrese una nombre para la nueva empresa";
            this.LimpiarModal();
        }

        public void LimpiarModal()
        {
            this.fNombre.Text = "";
            this.fRazonSocial.Text = "";
            this.fRut.Text = "";
            this.fTelefono.Text = "";
            this.fDireccion.Text = "";
        }

        protected void btn_ModificarActiva(object sender, EventArgs e)
        {
            var idEmpRem = Int32.Parse(this.ddlEmpresas.SelectedValue);
            if (Fachada.Instance.UpdateEmpresaRemisoraActual(idEmpRem))
            {
                this.lblStatus.Text = "La empresa actual se cambió con éxito";
                this.CargarEmpresasRemisoras();
            }
            else
            {
                this.lblStatus.Text = "La empresa actual no se pudo cambiar";
            }
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=Remisoras.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvRemisoras.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvRemisoras);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Empresas remisoras", new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = ConfigurationManager.AppSettings["logoTamboprpJpg"];
            //pdfDoc.Add(new Chunk(new Jpeg(new Uri("D:/ORT laptop/2014-S5-Proyecto/tamboprp-git/tamboprp/img_tamboprp/corporativo/logojpeg.jpg")),300f,-10f));
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        protected void print_Click(object sender, EventArgs e)
        {
            gvRemisoras.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvRemisoras.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Empresas remisoras</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvRemisoras.PagerSettings.Visible = true;
        }
        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Remisoras.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            this.gvRemisoras.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }
    }
}