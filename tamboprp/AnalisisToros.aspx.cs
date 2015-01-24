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
    public partial class AnalisisToros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarListadoTorosUtilizados();
                this.CargarListadoTorosNacimPorGenero();
                this.pnlFechasGraf.Visible = false;
                CargarDdl();
                this.Panel1.Visible = false;
                CargarDdl1();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Análisis de toros utilizados y su efectividad", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoTorosUtilizados()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");
            var lst = Fachada.Instance.GetTorosUtilizadosPorAnio2fechas(fecha1, fecha2);
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                this.ImprimirTopUtilizados(lst);
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantDiagP;
                }

                this.lblTotalServ.Text = totalServ.ToString();
                this.lblTotalEfect.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double) totalPren / totalServ * 100, 2);
                    this.lblPorcEfect.Text = porcEfect.ToString() + "% efect.";
                }

            }
            this.lblCantToros.Text = lst.Count.ToString();
            Session["listaTemporal"] = lst;
            this.gvTorosUtilizados.DataSource = lst;
            this.gvTorosUtilizados.DataBind();
            this.titCant.Visible = true;
            this.lblCant.Text = lst.Count.ToString();
         
        }

        private void ImprimirTopUtilizados(List<VOToroUtilizado> lst)
        {
            var lstOrderByCantServ = Fachada.Instance.GetTopUtilizados(lst);
            if (lstOrderByCantServ.Count > 0)
            {
                lstOrderByCantServ.Sort();
                this.lblRegMasUtiliz1.Text = lstOrderByCantServ[0].Registro;
                this.lblMasUtiliz1.Text = lstOrderByCantServ[0].CantServicios.ToString();
                this.lblEfect1.Text = lstOrderByCantServ[0].PorcEfectividad.ToString() + "% efect.";
                if (lstOrderByCantServ.Count > 1)
                {
                    this.lblRegMasUtiliz2.Text = lstOrderByCantServ[1].Registro;
                    this.lblMasUtiliz2.Text = lstOrderByCantServ[1].CantServicios.ToString();
                    this.lblEfect2.Text = lstOrderByCantServ[1].PorcEfectividad.ToString() + "% efect.";
                }
                if (lstOrderByCantServ.Count > 2)
                {
                    this.lblRegMasUtiliz3.Text = lstOrderByCantServ[2].Registro;
                    this.lblMasUtiliz3.Text = lstOrderByCantServ[2].CantServicios.ToString();
                    this.lblEfect3.Text = lstOrderByCantServ[2].PorcEfectividad.ToString() + "% efect.";
                }
            }

        }

        protected void GvTorosUtilizados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoTorosUtilizados();
        }

        protected void GvTorosNacimPorGenero_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoTorosNacimPorGenero();
        }

        private void CargarListadoTorosNacimPorGenero()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");
            var lst = Fachada.Instance.GetTorosNacimPorGenero2fechas(fecha1, fecha2);
            Session["listaTemporal"] = lst;
            this.gvTorosNacimPorGenero.DataSource = lst;
            this.gvTorosNacimPorGenero.DataBind();

            this.titCant2.Visible = true;
            this.lblCant2.Text = lst.Count.ToString();
        }

        private void LimpiarTabla()
        {
            this.gvTorosUtilizados.DataSource = null;
            this.gvTorosUtilizados.DataBind();
            this.gvTorosNacimPorGenero.DataSource = null;
            this.gvTorosNacimPorGenero.DataBind();

            this.titCant.Visible = false;
            this.lblCant.Text = "";
            this.titCant2.Visible = false;
            this.lblCant2.Text = "";
            this.lblCantToros.Text = "";

            this.lblRegMasUtiliz1.Text = "";
            this.lblMasUtiliz1.Text = "";
            this.lblEfect1.Text = "";
            this.lblRegMasUtiliz2.Text = "";
            this.lblMasUtiliz2.Text = "";
            this.lblEfect2.Text = "";
            this.lblRegMasUtiliz3.Text = "";
            this.lblMasUtiliz3.Text = "";
            this.lblEfect3.Text = "";

            this.lblTotalServ.Text = "";
            this.lblTotalEfect.Text = "";
            this.lblPorcEfect.Text = "";
        }


        #region Export y print

        private void CargarGridParaExportar()
        {
            this.gvTorosUtilizados.AllowPaging = false;
            this.gvTorosUtilizados.EnableViewState = false;
            var lst = Session["listaTemporal"];
            this.gvTorosUtilizados.DataSource = lst;
            this.gvTorosUtilizados.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=RankingToros.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvTorosUtilizados.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=RankingToros.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvTorosUtilizados.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvTorosUtilizados);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Ranking de toros", new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = ConfigurationManager.AppSettings["logoTamboprpJpg"];
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        protected void print_Click(object sender, EventArgs e)
        {
            gvTorosUtilizados.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvTorosUtilizados.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Ranking de toros</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvTorosUtilizados.PagerSettings.Visible = true;
        }


        private void CargarGridParaExportar1()
        {
            this.gvTorosNacimPorGenero.AllowPaging = false;
            this.gvTorosNacimPorGenero.EnableViewState = false;
            var lst = Session["listaTemporal"];
            this.gvTorosNacimPorGenero.DataSource = lst;
            this.gvTorosNacimPorGenero.DataBind();
        }

        protected void excelExport_Click1(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=TorosNacimPorGenero.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar1();
            this.gvTorosNacimPorGenero.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        protected void pdfExport_Click1(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=TorosNacimPorGenero.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvTorosNacimPorGenero.Parent.Controls.Add(frm);
            CargarGridParaExportar1();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvTorosNacimPorGenero);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Nacimientos por género", new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = ConfigurationManager.AppSettings["logoTamboprpJpg"];
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void print_Click1(object sender, EventArgs e)
        {
            gvTorosNacimPorGenero.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar1();
            gvTorosNacimPorGenero.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Nacimientos por género</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvTorosNacimPorGenero.PagerSettings.Visible = true;
        }

        #endregion


        #region DDL y datepicker Ranking Toros
        

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
                    CargarListadoTorosUtilizados();
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var lst = Fachada.Instance.GetTorosUtilizadosPorAnio2fechas(fecha1, fecha2);
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                this.ImprimirTopUtilizados(lst);
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantDiagP;
                }

                this.lblTotalServ.Text = totalServ.ToString();
                this.lblTotalEfect.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double) totalPren/totalServ*100, 2);
                    this.lblPorcEfect.Text = porcEfect.ToString() + "% efect.";
                }

            }
            this.lblCantToros.Text = lst.Count.ToString();
            Session["listaTemporal"] = lst;
            this.gvTorosUtilizados.DataSource = lst;
            this.gvTorosUtilizados.DataBind();
            this.titCant.Visible = true;
            this.lblCant.Text = lst.Count.ToString();
            this.pnlFechasGraf.Visible = false;
                    break;
                case "2":
                    this.pnlFechasGraf.Visible = true;
                    break;
            }
        }

        protected void btnListar_rankingToros(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechasVentas"];
            if(strValue.Contains(",")) {strValue =  strValue.Replace(",", "");}
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var lst = Fachada.Instance.GetTorosUtilizadosPorAnio2fechas(fecha1, fecha2);
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                this.ImprimirTopUtilizados(lst);
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantDiagP;
                }

                this.lblTotalServ.Text = totalServ.ToString();
                this.lblTotalEfect.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double)totalPren / totalServ * 100, 2);
                    this.lblPorcEfect.Text = porcEfect.ToString() + "% efect.";
                }

            }
            this.lblCantToros.Text = lst.Count.ToString();
            Session["listaTemporal"] = lst;
            this.gvTorosUtilizados.DataSource = lst;
            this.gvTorosUtilizados.DataBind();
            this.titCant.Visible = true;
            this.lblCant.Text = lst.Count.ToString();

        }

        private string FormatoFecha(string fecha)
        {
            var res = fecha.Split(Convert.ToChar("/"));
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }

        #endregion

        #region DDL y datepicker Nacimientos por genero
        

        public void CargarDdl1()
        {
            this.ddlFechas1.Items.Add(new ListItem("Último año", "0"));
            this.ddlFechas1.Items.Add(new ListItem("Este mes", "1"));
            this.ddlFechas1.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }


        protected void btnListar_Click1(object sender, EventArgs e)
        {
            DateTime fecha;
            string fecha1;
            string fecha2;

            switch (this.ddlFechas1.SelectedValue)
            {
                case "0":
                    CargarListadoTorosNacimPorGenero();
                    this.Panel1.Visible = false;
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var lst = Fachada.Instance.GetTorosNacimPorGenero2fechas(fecha1, fecha2);
                    Session["listaTemporal"] = lst;
                    this.gvTorosNacimPorGenero.DataSource = lst;
                    this.gvTorosNacimPorGenero.DataBind();

                    this.titCant2.Visible = true;
                    this.lblCant2.Text = lst.Count.ToString();
                    this.Panel1.Visible = false;
                    break;
                case "2":
                    this.Panel1.Visible = true;
                    break;
            }
        }

        protected void btnListar_nacimGenero(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechasVentas1"];
            if (strValue.Contains(",")) { strValue = strValue.Replace(",", ""); }
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var lst = Fachada.Instance.GetTorosNacimPorGenero2fechas(fecha1, fecha2);
            Session["listaTemporal"] = lst;
            this.gvTorosNacimPorGenero.DataSource = lst;
            this.gvTorosNacimPorGenero.DataBind();

            this.titCant2.Visible = true;
            this.lblCant2.Text = lst.Count.ToString();

        }


        #endregion
    }
}