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
using Datos;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;
using Entidades;

namespace tamboprp
{
    public partial class ListPorCategoria : System.Web.UI.Page
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
                    this.CargarDdlCategorias();
                    this.panelBotonesExport.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Listado por categoría", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }
        
        private void LimpiarTabla()
        {
            this.gvAnimales.DataSource = null;
            this.gvAnimales.DataBind();
            this.lblCateg.Text = "";
        }

        private void CargarDdlCategorias()
        {
            var catMap = new CategoriaMapper();
            //List<Categoria> lst = catMap.GetAll();
            List<Categoria> lst = Fachada.Instance.GetCategoriasAnimalAll();
            this.ddlCategorias.DataSource = lst;
            this.ddlCategorias.DataTextField = "Nombre";
            this.ddlCategorias.DataValueField = "Id_categ";
            this.ddlCategorias.DataBind();
        }

        private void CargarAnimalesPorCategoria()
        {
            int idCategoria = int.Parse(this.ddlCategorias.SelectedValue);
            var listTemp = Fachada.Instance.GetAnimalesByCategoria(idCategoria);
            Session["listaTempListCategoria"] = listTemp;
            this.gvAnimales.DataSource = listTemp;
            this.gvAnimales.DataBind();
            this.panelBotonesExport.Visible = true;
            this.titCantAnimales.Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            this.gvAnimales.PageIndex = 0;
            this.CargarAnimalesPorCategoria();
            //this.lblCateg.Text = this.ddlCategorias.SelectedItem.Text;
        }

        protected void GvAnimales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarAnimalesPorCategoria();
        }

        #region Export y print

        private void CargarGridParaExportar()
        {
            this.gvAnimales.AllowPaging = false;
            this.gvAnimales.EnableViewState = false;
            //int idCategoria = int.Parse(this.ddlCategorias.SelectedValue);
            //var listTemp = Fachada.Instance.GetAnimalesByCategoria(idCategoria);
            this.gvAnimales.DataSource = Session["listaTempListCategoria"];
            this.gvAnimales.DataBind();
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Categorias_Animales.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvAnimales.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=Categorias_Animales.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvAnimales.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvAnimales);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, ddlCategorias.SelectedItem.Text, new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = "http://www.tamboprp.uy/img_tamboprp/corporativo/logojpeg.jpg";
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        protected void print_Click(object sender, EventArgs e)
        {
            gvAnimales.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvAnimales.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>" + ddlCategorias.SelectedItem.Text + "</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvAnimales.PagerSettings.Visible = true;
        }

        #endregion

    }
}