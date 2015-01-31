using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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
    public partial class Inseminaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.CargarGrilla();
                    CargarDdl();
                    this.panel01.Visible = false;
                }
            }
            else Response.Redirect("~/Login.aspx", true);            
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Inseminaciones con diágnostico de preñez confirmado", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarGrilla()
        {
            // DATEPICKER, por defecto diagnosticos del ultimo mes
            var fecha = DateTime.Now.AddYears(-1);
            var fecha1 = fecha.ToString("yyyy-MM-dd");
            var fecha2 = DateTime.Now.ToString("yyyy-MM-dd");
            var list = Fachada.Instance.GetInseminacionesExitosas2fechas(fecha1, fecha2);
            Session["listaTemporal"] = list;
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvInseminaciones.DataSource = list;
            this.gvInseminaciones.DataBind();

            this.ConsolidarResumen(list);
        }

        public void ConsolidarResumen(List<Diag_PrenezMapper.VODiagnostico> lst)
        {
            //lst.Sort();
            var lstInseminadores = new List<VoListItem2>();
            bool esta = false;
            // Recorro la lista para consolidar la inseminaciones por Inseminador
            for (int i = 0; i < lst.Count; i++)
            {
                esta = false;
                for (int j = 0; j < lstInseminadores.Count; j++)
                {
                    if (lst[i].IdInseminador.Equals(lstInseminadores[j].Id))
                    {
                        lstInseminadores[j].Num++;
                        esta = true;
                        break;
                    }
                }
                if (!esta)
                {
                    var it = new VoListItem2(lst[i].IdInseminador, 1, lst[i].Inseminador);
                    lstInseminadores.Add(it);
                }
            }
            
            this.ImprimirResumen(lstInseminadores);
            this.lblTotal.Text = lst.Count.ToString();
        }

        public void ImprimirResumen(List<VoListItem2> lstInseminadores)
        {
            lstInseminadores.Sort();
            // agrego al cuadro de resumen la lista de inseminadores consolidada
            var sb = new StringBuilder();
            this.listaInseminadores.InnerHtml = "";
            for (int j = 0; j < lstInseminadores.Count; j++)
            {
                sb.Append("<li class='bigger-110'><i class='ace-icon fa fa-caret-right blue'></i>");
                sb.Append("<span><strong>" + lstInseminadores[j].Num + "</strong> &nbsp;&nbsp;</span>");
                sb.Append(lstInseminadores[j].Nombre + "</li>");
            }
            this.listaInseminadores.InnerHtml += sb.ToString();
        }


        protected void GvInseminaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarGrilla();
        }

        #region Export y print

        private void CargarGridParaExportar()
        {
            this.gvInseminaciones.AllowPaging = false;
            this.gvInseminaciones.EnableViewState = false;
            var list = Session["listaTemporal"];
            this.gvInseminaciones.DataSource = list;
            this.gvInseminaciones.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=InseminacionesDiagPrenezConfirmada.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvInseminaciones.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvInseminaciones);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Inseminaciones con diágnostico de preñez confirmado", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvInseminaciones.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvInseminaciones.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Inseminaciones con diágnostico de preñez confirmado</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvInseminaciones.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=InseminacionesDiagPrenezConfirmada.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvInseminaciones.RenderControl(htmlText);
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
                    CargarGrilla();
                    this.panel01.Visible = false;
                    break;
                case "1":

                    fecha = DateTime.Now.AddMonths(-1);
                    fecha1 = fecha.ToString("yyyy-MM-dd");
                    fecha2 = DateTime.Now.ToString("yyyy-MM-dd");

                    var list = Fachada.Instance.GetInseminacionesExitosas2fechas(fecha1, fecha2);
                    Session["listaTemporal"] = list;
                    this.lblCantAnimales.Text = list.Count.ToString();
                    this.lblCantAnimales.Visible = true;
                    this.gvInseminaciones.DataSource = list;
                    this.gvInseminaciones.DataBind();
                    this.ConsolidarResumen(list);
                    this.panel01.Visible = false;
                    break;
                case "2":
                    this.panel01.Visible = true;
                    break;
            }
        }

        protected void btnListar_Inseminaciones(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechas"];
            if (strValue.Contains(",")) { strValue = strValue.Replace(",", ""); }
            string str1 = strValue.Replace(" ", "");
            var res = str1.Split(Convert.ToChar("-"));
            var fecha1 = FormatoFecha(res[0]);
            var fecha2 = FormatoFecha(res[1]);

            var list = Fachada.Instance.GetInseminacionesExitosas2fechas(fecha1, fecha2);
            Session["listaTemporal"] = list;
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvInseminaciones.DataSource = list;
            this.gvInseminaciones.DataBind();
            this.ConsolidarResumen(list);
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