using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Entidades;
using Negocio;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace tamboprp
{
    public partial class ListPersonal : System.Web.UI.Page
    {
        private List<VOEmpleado> _listEmpleados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.CargarEmpleadosActivos();
                //this.CargarEmpleados();
                this.CargarDdlEmpleados();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Personal", "Personal.aspx"));
            list.Add(new VoListItemDuplaString("Lista de personal", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarEmpleados()
        {
            //var emp = new EmpleadoMapper();
            //List<Empleado> lst = emp.GetAll();
            var lst = Fachada.Instance.GetAllEmpleados();
            this.gvEmpleados.DataSource = lst;
            this.gvEmpleados.DataBind();

            _listEmpleados = lst;

        }

        public void CargarEmpleadosActivos()
        {
            var lst = Fachada.Instance.GetEmpleadosActivos();
            this.gvEmpleados.DataSource = lst;
            this.gvEmpleados.DataBind();

            _listEmpleados = lst;
        }

        protected void btn_VerActivos(object sender, EventArgs e)
        {
            this.CargarEmpleadosActivos();
        }

        protected void btn_VerTodos(object sender, EventArgs e)
        {
            this.CargarEmpleados();
        }

        protected void btn_ModificarDatos(object sender, EventArgs e)
        {

        }

        private void CargarDdlEmpleados()
        {
            this.ddlEmpleados.DataSource = _listEmpleados;
            this.ddlEmpleados.DataTextField = "NombreCompletoIniciales";
            this.ddlEmpleados.DataValueField = "Id";
            this.ddlEmpleados.DataBind();
        }


        public override void VerifyRenderingInServerForm(Control control) { }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf"; Response.AddHeader("content-disposition", "attachment;filename=Personal.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.gvEmpleados.AllowPaging = false;
            this.gvEmpleados.DataBind();
            this.gvEmpleados.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            //pdfDoc.Add(new Paragraph("My first PDF"));
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            
            //Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 30, 1);
            //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/pdf/" + fname), FileMode.Create));
            //doc.Open();
            //Image image = Image.GetInstance(Server.MapPath("~/img_tamboprp/corporativo/logo_ch.png"));
            //image.SetAbsolutePosition(12, 300);
            //writer.DirectContent.AddImage(image, false);
            //doc.Close();
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Personal.xls");
            Response.ContentType = "application/vnd.xls";
            var writeItem = new StringWriter();
            var htmlText = new HtmlTextWriter(writeItem);
            this.gvEmpleados.RenderControl(htmlText);
            Response.Write(writeItem.ToString());
            Response.End();
        }
    }
}