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
using ListItem = System.Web.UI.WebControls.ListItem;

namespace tamboprp
{
    public partial class AnalisisMuertes : System.Web.UI.Page
    {
        private List<BajaMapper.VOEnfermedad> listaTemporalResumenMuertes = new List<BajaMapper.VOEnfermedad>(); 
        private List<VOBaja> listaTempMuertes = new List<VOBaja>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
            //   (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            //{
                if (!Page.IsPostBack)
                {                    
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.GetResumenMuertes();
                    this.GetListaMuertes();
                    CargarDdl();
                    this.pnlFechasGraf.Visible = false;
                    CargarDdl1();
                    this.Panel1.Visible = false;
                }
            //}
            //else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Información sobre muertes", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvMuertes.DataSource = null;
            this.gvMuertes.DataBind();
            this.gvMuertesResumen.DataSource = null;
            this.gvMuertesResumen.DataBind();
            this.lblCantAnimales.Text = "";
        }

        private void GetListaMuertes()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

            var listTemp = Fachada.Instance.GetMuertesPor2fechas(fecha1, fecha2);
            Session["listaTempMuertes"] = listTemp;
            this.gvMuertes.DataSource = listTemp;
            this.gvMuertes.DataBind();
            this.titCantAnimales.Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
            this.titTotalMuertes.Visible = true;
            this.lblTotalMuertes.Text = listTemp.Count.ToString();
        }

        private void GetResumenMuertes()
        {
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

            var listTemp = Fachada.Instance.GetCantidadMuertesPorEnfermedadPor2fechas(fecha1, fecha2);
            Session["listaTempREsumenMuerte"] = listTemp;
            this.gvMuertesResumen.DataSource = listTemp;
            this.gvMuertesResumen.DataBind();
            this.titEnfDif.Visible = true;
            this.lblEnfDif.Text = listTemp.Count.ToString();

        }

        protected void GvMuertes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetListaMuertes();
        }

        protected void GvMuertesResumen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetResumenMuertes();

        }


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
                    fecha = DateTime.Now.AddYears(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var listTemp = Fachada.Instance.GetCantidadMuertesPorEnfermedadPor2fechas(fecha1, fecha2);
                    Session["listaTempREsumenMuerte"] = listTemp;
                    this.gvMuertesResumen.DataSource = listTemp;
                    this.gvMuertesResumen.DataBind();
                    this.titEnfDif.Visible = true;
                    this.lblEnfDif.Text = listTemp.Count.ToString();
                    break;
                case "1":
                    
                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var listTemp1 = Fachada.Instance.GetCantidadMuertesPorEnfermedadPor2fechas(fecha1, fecha2);
                    Session["listaTempREsumenMuerte"] = listTemp1;
                    this.gvMuertesResumen.DataSource = listTemp1;
                    this.gvMuertesResumen.DataBind();
                    this.titEnfDif.Visible = true;
                    this.lblEnfDif.Text = listTemp1.Count.ToString();
                    break;
                case "2":
                    this.pnlFechasGraf.Visible = true;
                    break;
            }
        }

        #region Export y print
       

        private void CargarGridParaExportar()
        {
            this.gvMuertesResumen.AllowPaging = false;
            this.gvMuertesResumen.EnableViewState = false;
            int anio = 2014;
            var listTemp = Session["listaTempREsumenMuerte"];
            this.gvMuertesResumen.DataSource = listTemp;
            this.gvMuertesResumen.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=Muertes.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvMuertesResumen.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvMuertesResumen);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Análisis sanitario de muertes", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvMuertesResumen.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvMuertesResumen.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Muertes</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvMuertesResumen.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Muertes.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvMuertesResumen.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        private void CargarGridParaExportar1()
        {
            this.gvMuertes.AllowPaging = false;
            this.gvMuertes.EnableViewState = false;
            int anio = 2014;
            var listTemp = Session["listaTempMuertes"];
            this.gvMuertes.DataSource = listTemp;
            this.gvMuertes.DataBind();
        }
        

        protected void pdfExport_Click1(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=ResumenMuertes.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvMuertes.Parent.Controls.Add(frm);
            CargarGridParaExportar1();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvMuertes);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Resumen de muertes", new Font(Font.FontFamily.HELVETICA, 14f)));
            var pathLogo = "http://www.tamboprp.uy/img_tamboprp/corporativo/logojpeg.jpg";
            //pdfDoc.Add(new Chunk(new Jpeg(new Uri("D:/ORT laptop/2014-S5-Proyecto/tamboprp-git/tamboprp/img_tamboprp/corporativo/logojpeg.jpg")),300f,-10f));
            pdfDoc.Add(new Chunk(new Jpeg(new Uri(pathLogo)), 300f, -10f));
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void print_Click1(object sender, EventArgs e)
        {
            gvMuertes.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar1();
            gvMuertes.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Resumen de muertes</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvMuertes.PagerSettings.Visible = true;
        }

        protected void excelExport_Click1(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=ResumenMuertes.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar1();
            this.gvMuertes.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }

        #endregion

        protected void btnListar_gvMuertesResumen(object sender, EventArgs e)
        {
            if (Request.Form["fechasVentas"]!="")
            {
                string strValue = Request.Form["fechasVentas"];
                string str1 = strValue.Replace(" ", "");
                var res = str1.Split(Convert.ToChar("-"));
                var fecha1 = FormatoFecha(res[0]);
                var fecha2 = FormatoFecha(res[1]);

                var listTemp1 = Fachada.Instance.GetCantidadMuertesPorEnfermedadPor2fechas(fecha1, fecha2);
                Session["listaTempREsumenMuerte"] = listTemp1;
                this.gvMuertesResumen.DataSource = listTemp1;
                this.gvMuertesResumen.DataBind();
                this.titEnfDif.Visible = true;
                this.lblEnfDif.Text = listTemp1.Count.ToString();
            }
            

        }

        private string  FormatoFecha(string fecha) {
            var res = fecha.Split(Convert.ToChar("/"));
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }

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
                    fecha = DateTime.Now.AddYears(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var listTemp = Fachada.Instance.GetMuertesPor2fechas(fecha1 , fecha2);
                    Session["listaTempREsumenMuerte"] = listTemp;
                    this.gvMuertes.DataSource = listTemp;
                    this.gvMuertes.DataBind();
                    this.titCantAnimales.Visible = true;
                    this.lblCantAnimales.Text = listTemp.Count.ToString();
                    this.titTotalMuertes.Visible = true;
                    this.lblTotalMuertes.Text = listTemp.Count.ToString();
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var listTemp1 = Fachada.Instance.GetMuertesPor2fechas(fecha1 , fecha2);
                    Session["listaTempREsumenMuerte"] = listTemp1;
                    this.gvMuertes.DataSource = listTemp1;
                    this.gvMuertes.DataBind();
                    this.titCantAnimales.Visible = true;
                    this.lblCantAnimales.Text = listTemp1.Count.ToString();
                    this.titTotalMuertes.Visible = true;
                    this.lblTotalMuertes.Text = listTemp1.Count.ToString();
                    break;
                case "2":
                    this.Panel1.Visible = true;
                    break;
            }
        }

        protected void btnListar_gvMuertesResumen1(object sender, EventArgs e)
        {
            if (Request.Form["fechasVentas1"] !="")
            {
                string strValue = Request.Form["fechasVentas1"];
                string str1 = strValue.Replace(" ", "");
                var res = str1.Split(Convert.ToChar("-"));
                var fecha1 = FormatoFecha(res[0]);
                var fecha2 = FormatoFecha(res[1]);

                var listTemp1 = Fachada.Instance.GetMuertesPor2fechas(fecha1, fecha2);
                Session["listaTempREsumenMuerte"] = listTemp1;
                this.gvMuertes.DataSource = listTemp1;
                this.gvMuertes.DataBind();
                this.titCantAnimales.Visible = true;
                this.lblCantAnimales.Text = listTemp1.Count.ToString();
                this.titTotalMuertes.Visible = true;
                this.lblTotalMuertes.Text = listTemp1.Count.ToString();
            }
            
        }

    }
}
