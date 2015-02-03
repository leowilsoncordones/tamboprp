using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Datos;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocio;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace tamboprp
{
    public partial class Remitos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarRegistro();
                    this.CargarRemitos();
                    this.CargarDdl();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = false;

                    CargarDdl1();
                    panel01.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Remitos a planta", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }
        private void LimpiarRegistro()
        {
            this.lblTituloListado.Text = "";
            this.lblTituloGrafica.Text = "";
        }

        public void CargarRemitos()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

            var empresaActual = Fachada.Instance.GetEmpresaRemisoraActual();
            // CARGO LISTADO
            if (empresaActual.Count > 0) // hay al menos una empresa actual, se lista la primera
            {
                var emp = empresaActual[0];
                this.lblTituloListado.Text = emp.ToString();
                this.lblTituloGrafica.Text = emp.ToString();

                
                var list = Fachada.Instance.GetRemitoByEmpresa2fechas(emp.Id, fecha1, fecha2);
                Session["listaTemporal"] = list;
                this.gvRemitos.DataSource = list;
                this.gvRemitos.DataBind();  
            }
            // CARGO GRAFICA
        }

        //[WebMethod]
        //public static List<RemitoMapper.VORemitoGrafica> RemitosGraficasGetAll()
        //{
        //    return Fachada.Instance.GetRemitosGraficas();
        //}


        [WebMethod]
        public static List<RemitoMapper.VORemitoGrafica> RemitosGetAnioCorriente()
        {
            var hoy = DateTime.Now.ToString("yyyy-MM-dd");
            int year = DateTime.Now.Year;
            var primerDia = new DateTime(year, 1, 1);
            string fecha1 = primerDia.ToString("yyyy-MM-dd");
            return Fachada.Instance.GetRemitosEntreDosFechas(fecha1, hoy);
        }

        [WebMethod]
        public static List<RemitoMapper.VORemitoGrafica> RemitosGetUltimoAnio()
        {
            var hoy = DateTime.Now.ToString("yyyy-MM-dd");
            var newDate = DateTime.Now.AddYears(-1);
            string fecha1 = newDate.ToString("yyyy-MM-dd");
            return Fachada.Instance.GetRemitosEntreDosFechas(fecha1, hoy);
        }

        [WebMethod]
        public static List<RemitoMapper.VORemitoGrafica> GetRemitosEntreDosFechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetRemitosEntreDosFechas(fecha1, fecha2);
        }


        protected void btnListar_Click(object sender, EventArgs e)
        {
            switch (this.ddlFechasGraf.SelectedValue)
            {
                case "0":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "1":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(1)", true);
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "2":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = true;
                    break;
            }
        }

        public void CargarDdl()
        {
            this.ddlFechasGraf.Items.Add(new ListItem("Este año", "0"));
            this.ddlFechasGraf.Items.Add(new ListItem("Último año", "1"));
            this.ddlFechasGraf.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }

        #region Export y print

        private void CargarGridParaExportar()
        {
            this.gvRemitos.AllowPaging = false;
            this.gvRemitos.EnableViewState = false;
            var lst = Session["listaTemporal"];
            this.gvRemitos.DataSource = lst;
            this.gvRemitos.DataBind();
        }
     

        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=Remitos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvRemitos.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvRemitos);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Listado de remitos a planta", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvRemitos.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvRemitos.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Listado de remitos a planta</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvRemitos.PagerSettings.Visible = true;
        }
        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Remitos.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            this.gvRemitos.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        #endregion


        #region DDL y Datepicker

        public void CargarDdl1()
        {
            this.ddlFechas.Items.Add(new ListItem("Último año", "0"));
            this.ddlFechas.Items.Add(new ListItem("Este mes", "1"));
            this.ddlFechas.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }




        protected void btnListar_Click1(object sender, EventArgs e)
        {
            DateTime fecha;
            string fecha1;
            string fecha2;

            switch (this.ddlFechas.SelectedValue)
            {
                case "0":
                    CargarRemitos();
                    panel01.Visible = false;
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var empresaActual = Fachada.Instance.GetEmpresaRemisoraActual();
                    // CARGO LISTADO
                    if (empresaActual.Count > 0) // hay al menos una empresa actual, se lista la primera
                    {
                        var emp = empresaActual[0];
                        this.lblTituloListado.Text = emp.ToString();
                        this.lblTituloGrafica.Text = emp.ToString();

                
                        var list = Fachada.Instance.GetRemitoByEmpresa2fechas(emp.Id, fecha1, fecha2);
                        Session["listaTemporal"] = list;
                        this.gvRemitos.DataSource = list;
                        this.gvRemitos.DataBind();  
                    }
                    panel01.Visible = false;
                    break;
                case "2":
                    this.panel01.Visible = true;
                    break;
            }
        }

        protected void btnListar_Remitos(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechas"];
            if (strValue.Contains(",")) { strValue = strValue.Replace(",", ""); }
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var empresaActual = Fachada.Instance.GetEmpresaRemisoraActual();
            // CARGO LISTADO
            if (empresaActual.Count > 0) // hay al menos una empresa actual, se lista la primera
            {
                var emp = empresaActual[0];
                this.lblTituloListado.Text = emp.ToString();
                this.lblTituloGrafica.Text = emp.ToString();


                var list = Fachada.Instance.GetRemitoByEmpresa2fechas(emp.Id, fecha1, fecha2);
                Session["listaTemporal"] = list;
                this.gvRemitos.DataSource = list;
                this.gvRemitos.DataBind();
            }

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