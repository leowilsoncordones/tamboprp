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
using ListItem = System.Web.UI.WebControls.ListItem;

namespace tamboprp
{
    public partial class ListPartos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.CargarListadoPartos();
                    CargarDdl();
                    panel01.Visible = false;
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Listado de partos", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoPartos()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

            var lst = Fachada.Instance.GetPartosPor2fechas(fecha1, fecha2);
            var cantNacim = Fachada.Instance.GetCantNacimientosPor2fechas(fecha1, fecha2);
            var cantMellizos = Fachada.Instance.GetCantMellizosPor2fechas(fecha1, fecha2);
            var cantTrillizos = Fachada.Instance.GetCantTrillizosPor2fechas(fecha1, fecha2);
            int cant = lst.Count;
            int hembras = 0;
            int machos = 0;
            int muertos = 0;
            for (int i = 0; i < cant; i++)
            {
                if (lst[i].Sexo_parto == 'M') machos++;
                if (lst[i].Sexo_parto == 'H') hembras++;
                if (lst[i].Reg_hijo == "-") muertos++;
            }
            double promHembras = 0;
            double promMachos = 0;
            if (cant > 0)
            {
                promHembras = Math.Round((double)hembras / cant * 100, 1);
                promMachos = Math.Round((double)machos / cant * 100, 1);
            }

            Session["listaTemporal"] = lst;
            this.gvListPartos.DataSource = lst;
            this.gvListPartos.DataBind();
            
            this.titCant.Visible = true;
            this.lblCant.Text = cant.ToString();
           
            this.lblTotalPartos.Text = cant.ToString();
            //int nac = cant + cantMellizos + (cantTrillizos * 2) - muertos;
            this.lblTotalNac.Text = cantNacim.ToString();
            this.lblH.Text = hembras.ToString();
            this.lblPromH.Text = promHembras.ToString() + "%";
            this.lblM.Text = machos.ToString();
            this.lblPromM.Text = promMachos.ToString() + "%";

            this.lblMellizos.Text = cantMellizos.ToString();
            this.lblTrillizos.Text = cantTrillizos.ToString();
            
            this.lblTotalMuertos.Text = muertos.ToString();
            double promMuertos = Math.Round((double)muertos / cant * 100, 1);
            this.lblPromMuertos.Text = promMuertos.ToString() + "%";
            double promNac = Math.Round((double)cantNacim / cant * 100, 1);
            this.lblPromNac.Text = promNac.ToString() + "%";

        }

        protected void GvListPartos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoPartos();
        }

        private void LimpiarTabla()
        {
            this.gvListPartos.DataSource = null;
            this.gvListPartos.DataBind();
            this.lblCant.Text = "";
            this.lblTotalPartos.Text = "";
            this.lblTotalNac.Text = "";
            this.lblH.Text = "";
            this.lblM.Text = "";
        }


        #region Export y print

        private void CargarGridParaExportar()
        {
            this.gvListPartos.AllowPaging = false;
            this.gvListPartos.EnableViewState = false;
            int anio = 2014;
            var lst = Session["listaTemporal"];
            this.gvListPartos.DataSource = lst;
            this.gvListPartos.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=ListadoPartos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvListPartos.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvListPartos);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Listado de partos", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvListPartos.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvListPartos.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Listado de partos</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvListPartos.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=ListadoPartos.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvListPartos.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        #endregion


        #region DDL y datepicker

        public void CargarDdl()
        {
            this.ddlFechas.Items.Add(new ListItem("Último año", "0"));
            this.ddlFechas.Items.Add(new ListItem("Este mes", "1"));
            this.ddlFechas.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            DateTime fecha;
            string fecha1;
            string fecha2;

            switch (this.ddlFechas.SelectedValue)
            {
                case "0":
                    CargarListadoPartos();
                    this.panel01.Visible = false;
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var lst = Fachada.Instance.GetPartosPor2fechas(fecha1, fecha2);
                    var cantNacim = Fachada.Instance.GetCantNacimientosPor2fechas(fecha1, fecha2);
                    var cantMellizos = Fachada.Instance.GetCantMellizosPor2fechas(fecha1, fecha2);
                    var cantTrillizos = Fachada.Instance.GetCantTrillizosPor2fechas(fecha1, fecha2);
                    int cant = lst.Count;
                    int hembras = 0;
                    int machos = 0;
                    int muertos = 0;
                    for (int i = 0; i < cant; i++)
                    {
                        if (lst[i].Sexo_parto == 'M') machos++;
                        if (lst[i].Sexo_parto == 'H') hembras++;
                        if (lst[i].Reg_hijo == "-") muertos++;
                    }
                    double promHembras = 0;
                    double promMachos = 0;
                    if (cant > 0)
                    {
                        promHembras = Math.Round((double)hembras / cant * 100, 1);
                        promMachos = Math.Round((double)machos / cant * 100, 1);
                    }

                    Session["listaTemporal"] = lst;
                    this.gvListPartos.DataSource = lst;
                    this.gvListPartos.DataBind();
            
                    this.titCant.Visible = true;
                    this.lblCant.Text = cant.ToString();
           
                    this.lblTotalPartos.Text = cant.ToString();
                    //int nac = cant + cantMellizos + (cantTrillizos * 2) - muertos;
                    this.lblTotalNac.Text = cantNacim.ToString();
                    this.lblH.Text = hembras.ToString();
                    this.lblPromH.Text = promHembras.ToString() + "%";
                    this.lblM.Text = machos.ToString();
                    this.lblPromM.Text = promMachos.ToString() + "%";

                    this.lblMellizos.Text = cantMellizos.ToString();
                    this.lblTrillizos.Text = cantTrillizos.ToString();
            
                    this.lblTotalMuertos.Text = muertos.ToString();
                    double promMuertos = Math.Round((double)muertos / cant * 100, 1);
                    this.lblPromMuertos.Text = promMuertos.ToString() + "%";
                    double promNac = Math.Round((double)cantNacim / cant * 100, 1);
                    this.lblPromNac.Text = promNac.ToString() + "%";
                    this.panel01.Visible = false;
                    break;
                case "2":
                    this.panel01.Visible = true;
                    break;
            }
        }

        protected void btnListar_Partos(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechas"];
            if (strValue.Contains(",")) { strValue = strValue.Replace(",", ""); }
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var lst = Fachada.Instance.GetPartosPor2fechas(fecha1, fecha2);
            var cantNacim = Fachada.Instance.GetCantNacimientosPor2fechas(fecha1, fecha2);
            var cantMellizos = Fachada.Instance.GetCantMellizosPor2fechas(fecha1, fecha2);
            var cantTrillizos = Fachada.Instance.GetCantTrillizosPor2fechas(fecha1, fecha2);
            int cant = lst.Count;
            int hembras = 0;
            int machos = 0;
            int muertos = 0;
            for (int i = 0; i < cant; i++)
            {
                if (lst[i].Sexo_parto == 'M') machos++;
                if (lst[i].Sexo_parto == 'H') hembras++;
                if (lst[i].Reg_hijo == "-") muertos++;
            }
            double promHembras = 0;
            double promMachos = 0;
            if (cant > 0)
            {
                promHembras = Math.Round((double)hembras / cant * 100, 1);
                promMachos = Math.Round((double)machos / cant * 100, 1);
            }

            Session["listaTemporal"] = lst;
            this.gvListPartos.DataSource = lst;
            this.gvListPartos.DataBind();

            this.titCant.Visible = true;
            this.lblCant.Text = cant.ToString();

            this.lblTotalPartos.Text = cant.ToString();
            //int nac = cant + cantMellizos + (cantTrillizos * 2) - muertos;
            this.lblTotalNac.Text = cantNacim.ToString();
            this.lblH.Text = hembras.ToString();
            this.lblPromH.Text = promHembras.ToString() + "%";
            this.lblM.Text = machos.ToString();
            this.lblPromM.Text = promMachos.ToString() + "%";

            this.lblMellizos.Text = cantMellizos.ToString();
            this.lblTrillizos.Text = cantTrillizos.ToString();

            this.lblTotalMuertos.Text = muertos.ToString();
            double promMuertos = Math.Round((double)muertos / cant * 100, 1);
            this.lblPromMuertos.Text = promMuertos.ToString() + "%";
            double promNac = Math.Round((double)cantNacim / cant * 100, 1);
            this.lblPromNac.Text = promNac.ToString() + "%";

        }

        private string FormatoFecha(string fecha)
        {
            var res = fecha.Split(Convert.ToChar("/"));
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }

        #endregion
    }
}