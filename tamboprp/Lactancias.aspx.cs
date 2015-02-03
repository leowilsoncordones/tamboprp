using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public partial class Lactancias : System.Web.UI.Page
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
                    this.CargarDdlTipoListado();
                    this.CargarDdlCantidad();
                    this.panelBotonesExport.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Producción", "Produccion.aspx"));
            list.Add(new VoListItemDuplaString("Lactancias", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvLactancias.DataSource = null;
            this.gvLactancias.DataBind();
            this.lblTitulo.Visible = false;
            this.lblTitulo.Text = "";
        }
        
        private void CargarDdlTipoListado()
        {
            //var catMap = new CategoriaMapper();
            //List<Tipo> lst = catMap.GetAll();

            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "ACTUALES" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "HISTORICAS" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "MEJOR PRODUCCION 305 DIAS" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "MEJOR PRODUCCION 365 DIAS" }; lst.Add(item4);

            this.ddlTipoListado.DataSource = lst;
            this.ddlTipoListado.DataTextField = "Nombre";
            this.ddlTipoListado.DataValueField = "Id";
            this.ddlTipoListado.DataBind();
        }

        private void CargarDdlCantidad()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 10, Nombre = "10" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 20, Nombre = "20" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 50, Nombre = "50" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 100, Nombre = "100" }; lst.Add(item4);
            var item5 = new VoListItem { Id = -1, Nombre = "Todas" }; lst.Add(item5);

            this.ddlCantidad.DataSource = lst;
            this.ddlCantidad.DataTextField = "Nombre";
            this.ddlCantidad.DataValueField = "Id";
            this.ddlCantidad.SelectedValue="20";
            this.ddlCantidad.DataBind();
        }

        private void GetLactanciasActuales()
        {
            var listTemp = Fachada.Instance.GetLactanciasActuales();
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = false;
            this.gvLactancias.Columns[8].Visible = false;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
        }

        private void GetLactanciasHistoricas()
        {
            var listTemp = Fachada.Instance.GetLactanciasHistoricas();
            /* falta resolver */
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
        }

        private void GetMejorProduccion305()
        {
            int tope = -1;
            if (ddlCantidad.SelectedValue != null) int.TryParse(ddlCantidad.SelectedValue, out tope);
            var listTemp = Fachada.Instance.GetMejorProduccion305Dias(tope);
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = false;
            this.gvLactancias.Columns[6].Visible = false;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
            
        }

        private void GetMejorProduccion365()
        {
            int tope = -1;
            if (ddlCantidad.SelectedValue != null) int.TryParse(ddlCantidad.SelectedValue, out tope);
            var listTemp = Fachada.Instance.GetMejorProduccion365Dias(tope);
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = false;
            this.gvLactancias.Columns[4].Visible = false;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
        }


        protected void btnListar_Click(object sender, EventArgs e)
        {
            this.panelBotonesExport.Visible = true;
            this.LimpiarTabla();
            switch (this.ddlTipoListado.SelectedValue)
            {
                case "1": 
                    this.GetLactanciasActuales();
                    break;
                case "2":
                    this.GetLactanciasHistoricas();
                    break;
                case "3":
                    this.GetMejorProduccion305();
                    break;
                case "4":
                    this.GetMejorProduccion365();
                    break;
                default:
                    this.GetLactanciasActuales();
                    break;
            }

            this.lblTitulo.Visible = true;
            this.lblTitulo.Text = this.ddlTipoListado.SelectedItem.Text;
            this.titCantAnimales.Visible = true;
        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ddlTipoListado.SelectedValue)
            {
                case "1":
                    this.ddlCantidad.Visible = false;
                    this.titDdlCant.Visible = false;
                    break;
                case "2":
                    this.ddlCantidad.Visible = false;
                    this.titDdlCant.Visible = false;
                    break;
                case "3":
                    this.ddlCantidad.Visible = true;
                    this.titDdlCant.Visible = true;
                    break;
                case "4":
                    this.ddlCantidad.Visible = true;
                    this.titDdlCant.Visible = true;
                    break;
                default:
                    break;
            }
        }

        protected void GvLactancias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            switch (this.ddlTipoListado.SelectedValue)
            {
                case "1":
                    this.GetLactanciasActuales();
                    break;
                case "2":
                    this.GetLactanciasHistoricas();
                    break;
                case "3":
                    this.GetMejorProduccion305();
                    break;
                case "4":
                    this.GetMejorProduccion365();
                    break;
                default:
                    this.GetLactanciasActuales();
                    break;
            }
        }


        private void CargarGridParaExportar()
        {
            this.gvLactancias.AllowPaging = false;
            this.gvLactancias.EnableViewState = false;
            var listTemp = new List<VOLactancia>();
            int tope = -1;

            switch (this.ddlTipoListado.SelectedValue)
            {
                case "1":
                listTemp = Fachada.Instance.GetLactanciasActuales();              
                    break;
                case "2":
                    listTemp = Fachada.Instance.GetLactanciasHistoricas();
                    break;
                case "3":                    
                     if (ddlCantidad.SelectedValue != null) int.TryParse(ddlCantidad.SelectedValue, out tope);
                    listTemp = Fachada.Instance.GetMejorProduccion305Dias(tope);
                    break;
                case "4":
                    if (ddlCantidad.SelectedValue != null) int.TryParse(ddlCantidad.SelectedValue, out tope);
                    listTemp = Fachada.Instance.GetMejorProduccion365Dias(tope);
                    break;
                default:
                    listTemp = Fachada.Instance.GetLactanciasActuales();
                    break;

            }

            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();

        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Lactancias.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvLactancias.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=Lactancias.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvLactancias.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvLactancias);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, ddlTipoListado.SelectedItem.Text, new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = "http://www.tamboprp.uy/img_tamboprp/corporativo/logojpeg.jpg";
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        protected void print_Click(object sender, EventArgs e)
        {
            gvLactancias.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvLactancias.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>" + ddlTipoListado.SelectedItem.Text + "</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvLactancias.PagerSettings.Visible = true;
        }

    }
}