using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class Remitos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarRegistro();
                this.CargarRemitos();
                this.CargarDdl();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                this.pnlFechasGraf.Visible = false;
            }
            
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
            var empresaActual = Fachada.Instance.GetEmpresaRemisoraActual();
            // CARGO LISTADO
            if (empresaActual.Count > 0) // hay al menos una empresa actual, se lista la primera
            {
                var emp = empresaActual[0];
                this.lblTituloListado.Text = emp.ToString();
                this.lblTituloGrafica.Text = emp.ToString();
                var list = Fachada.Instance.GetRemitoByEmpresa(emp.Id);
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

    }
}