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
    public partial class Calificaciones : System.Web.UI.Page
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
                    this.CargarCalificaciones();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Cabaña", "Cabana.aspx"));
            list.Add(new VoListItemDuplaString("Calificaciones", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarCalificaciones()
        {
            List<Calificacion> lst = Fachada.Instance.GetCalificacionesAll();
            this.gvCalificaciones.DataSource = lst;
            this.gvCalificaciones.DataBind();
            this.titCantCalif.Visible = true;
            this.lblCantCalif.Text = lst.Count.ToString();
            this.lblTotal.Text = lst.Count.ToString();
            int cant = lst.Count;
            if (cant > 0)
            {
                //this.lblEX.Text = Fachada.Instance.GetCantCalificacionesEx().ToString();
                //this.lblMB.Text = Fachada.Instance.GetCantCalificacionesMb().ToString();
                //this.lblBM.Text = Fachada.Instance.GetCantCalificacionesBm().ToString();
                //this.lblB.Text = Fachada.Instance.GetCantCalificacionesB().ToString();
                //this.lblProm.Text = Fachada.Instance.GetCalificacionProm().ToString();
                //this.lblMax.Text = Fachada.Instance.GetCalificacionesMax().ToString();

                int sum = 0; int maxPts = 0; string maxLetras = "";
                int cantEx = 0; int cantMb = 0; int cantBm = 0; 
                int cantB = 0; int cantR = 0;

                for (int i = 0; i < cant; i++)
                {
                    if (lst[i].Puntos > maxPts)
                    {
                        maxPts = lst[i].Puntos;
                        maxLetras = lst[i].Letras;
                    }
                    switch (lst[i].Letras)
                    {
                        case "EX":
                            cantEx++;
                            break;
                        case "MB":
                            cantMb++;
                            break;
                        case "BM":
                            cantBm++;
                            break;
                        case "B":
                            cantB++;
                            break;
                        case "R":
                            cantR++;
                            break;
                    }
                    sum += lst[i].Puntos;
                }

                this.lblEX.Text = cantEx.ToString();
                this.lblMB.Text = cantMb.ToString();
                this.lblBM.Text = cantBm.ToString();
                this.lblB.Text = cantB.ToString();
                this.lblR.Text = cantR.ToString();
                this.lblMax.Text = (maxLetras + " " + maxPts).ToString();
                this.lblProm.Text = (sum / cant).ToString();
            }
            
        }

        protected void GvCalificaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarCalificaciones();
        }

        private void LimpiarTabla()
        {
            this.gvCalificaciones.DataSource = null;
            this.gvCalificaciones.DataBind();
            this.lblCantCalif.Text = "";
            this.lblEX.Text = "";
            this.lblMB.Text = "";
            this.lblBM.Text = "";
            this.lblB.Text = "";
            this.lblR.Text = "";
            this.lblTotal.Text = "";
            this.lblMax.Text = "";
            this.lblProm.Text = "";
        }

        private void CargarGridParaExportar()
        {
            this.gvCalificaciones.AllowPaging = false;
            this.gvCalificaciones.EnableViewState = false;
            List<Calificacion> lst = Fachada.Instance.GetCalificacionesAll();
            this.gvCalificaciones.DataSource = lst;
            this.gvCalificaciones.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=Calificaciones.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();
            gvCalificaciones.Parent.Controls.Add(frm);
            CargarGridParaExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvCalificaciones);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(40f, "Calificaciones", new Font(Font.FontFamily.HELVETICA, 14f)));
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
            gvCalificaciones.PagerSettings.Visible = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            CargarGridParaExportar();
            gvCalificaciones.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"<label>Calificaciones</label>\");");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvCalificaciones.PagerSettings.Visible = true;
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Calificaciones.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            CargarGridParaExportar();
            this.gvCalificaciones.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }
    }
}