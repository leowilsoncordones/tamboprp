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
using ListItem = System.Web.UI.WebControls.ListItem;

namespace tamboprp
{
    public partial class AnalisisInseminadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.CargarListadoRankingInseminadores();
                    CargarDdl();
                    panel01.Visible = false;
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Ranking de inseminadores", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }


        public void CargarListadoRankingInseminadores()
        {
            //var lstFechas = Fachada.Instance.GetFechasDiagnosticoPorAnio(2014);

            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");
            var lst = Fachada.Instance.GetRankingInseminadores2fechas(fecha1 , fecha2);
            Session["listaTemporal"] = lst;
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantPrenadas;
                }

                this.lblTotalServicios.Text = totalServ.ToString();
                this.lblTotalPrenadas.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double)totalPren / totalServ * 100, 1);
                    this.lblPorcEfectividad.Text = porcEfect.ToString() + "%";
                }

            }
            this.lblCantInsem.Text = lst.Count.ToString();
            this.lblCantInsem.Visible = true;
            this.titCantInsem.Visible = true;

            this.gvRanking.DataSource = lst;
            this.gvRanking.DataBind();
            
        }


        private void LimpiarTabla()
        {
            this.gvRanking.DataSource = null;
            this.gvRanking.DataBind();

            this.titCantInsem.Visible = false;
            this.lblCantInsem.Text = "";

            this.lblTotalServicios.Text = "";
            this.lblTotalPrenadas.Text = "";
            this.lblPorcEfectividad.Text = "";
        }


        #region Export y print
        private void CargarGridParaExportar()
        {
            this.gvRanking.AllowPaging = false;
            this.gvRanking.EnableViewState = false;
            int anio = 2014;
            var lst = Fachada.Instance.GetRankingInseminadores(anio);
            Session["listaTemporal"] = lst;
            this.gvRanking.DataSource = lst;
            this.gvRanking.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=RankingInseminadores.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvRanking.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvRanking);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Ranking de inseminadores", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvRanking.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvRanking.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Ranking de inseminadores</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvRanking.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=RankingInseminadores.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvRanking.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        #endregion


        public void CargarDdl()
        {
            this.ddlFechas.Items.Add(new ListItem("Último año", "0"));
            this.ddlFechas.Items.Add(new ListItem("Seleccionar fechas", "1"));
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            DateTime fecha;
            string fecha1;
            string fecha2;

            switch (this.ddlFechas.SelectedValue)
            {
                case "0":
                    CargarListadoRankingInseminadores();
                    this.panel01.Visible = false;
                    break;
                case "1":

                    this.panel01.Visible = true;
                    break;
            }
        }

        protected void btnListar_Inseminadores(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechas"];
            if (strValue.Contains(",")) { strValue = strValue.Replace(",", ""); }
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var lst = Fachada.Instance.GetRankingInseminadores2fechas(fecha1, fecha2);
            Session["listaTemporal"] = lst;
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantPrenadas;
                }

                this.lblTotalServicios.Text = totalServ.ToString();
                this.lblTotalPrenadas.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double)totalPren / totalServ * 100, 1);
                    this.lblPorcEfectividad.Text = porcEfect.ToString() + "%";
                }

            }
            this.lblCantInsem.Text = lst.Count.ToString();
            this.lblCantInsem.Visible = true;
            this.titCantInsem.Visible = true;

            this.gvRanking.DataSource = lst;
            this.gvRanking.DataBind();

        }

        private string FormatoFecha(string fecha)
        {
            var res = fecha.Split(Convert.ToChar("/"));
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }
    }
}