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
    public partial class AnalisisVentas : System.Web.UI.Page
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
                    this.CargarAnalisisVentas();
                    CargarDdl();
                    this.pnlFechasGraf.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Análisis de ventas", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarRegistro()
        {
            this.lblCantVentas.Text = "";
            this.lblAFrigorifico.Text = "";
            this.lblRecienNac.Text = "";
        }

        public void CargarAnalisisVentas()
        {
            //var anio = DateTime.Today.Year;
            var anio = 2014;   // TESTING ----------------------------
            this.lblCantVentas.Text = Fachada.Instance.GetCantVentasPorAnio(anio).ToString();
            this.lblAFrigorifico.Text = Fachada.Instance.GetCantVentasAFrigPorAnio(anio).ToString();
            this.lblRecienNac.Text = Fachada.Instance.GetCantVentasRecienNacidosPorAnio(anio).ToString();
            this.lblPorVieja.Text = Fachada.Instance.GetCantVentasViejasPorAnio(anio).ToString();
        }

        public void CargarDdl()
        {
            this.ddlFechasGraf.Items.Add(new ListItem("Último año", "0"));
            this.ddlFechasGraf.Items.Add(new ListItem("Este mes", "1"));
            this.ddlFechasGraf.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            switch (this.ddlFechasGraf.SelectedValue)
            {
                case "0":
                    var anio = DateTime.Now.Year - 1;  
                    this.lblCantVentas.Text = Fachada.Instance.GetCantVentasPorAnio(anio).ToString();
                    this.lblAFrigorifico.Text = Fachada.Instance.GetCantVentasAFrigPorAnio(anio).ToString();
                    this.lblRecienNac.Text = Fachada.Instance.GetCantVentasRecienNacidosPorAnio(anio).ToString();
                    this.lblPorVieja.Text = Fachada.Instance.GetCantVentasViejasPorAnio(anio).ToString();
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "1":
                    var mes = DateTime.Now.Month;  
                    this.lblCantVentas.Text = Fachada.Instance.GetCantVentasPorMes(mes).ToString();
                    this.lblAFrigorifico.Text = Fachada.Instance.GetCantVentasAFrigPorMes(mes).ToString();
                    this.lblRecienNac.Text = Fachada.Instance.GetCantVentasRecienNacidosPorMes(mes).ToString();
                    this.lblPorVieja.Text = Fachada.Instance.GetCantVentasViejasPorMes(mes).ToString();
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "2":
                    this.pnlFechasGraf.Visible = true;
                    break;
            }
        }


        [WebMethod]
        public static string GetCantVentasPor2fechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetCantVentasPor2fechas(fecha1, fecha2).ToString();
        }

        [WebMethod]
        public static string GetCantVentasAFrigPor2fechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetCantVentasAFrigPor2fechas(fecha1, fecha2).ToString();
        }

        [WebMethod]
        public static string GetCantVentasRecienNacidosPor2fechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetCantVentasRecienNacidosPor2fechas(fecha1, fecha2).ToString();
        }

        [WebMethod]
        public static string GetCantVentasViejasPor2fechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetCantVentasViejasPor2fechas(fecha1, fecha2).ToString();
        }


        protected void btnListar_Click1(object sender, EventArgs e)
        {
            string strValue = Request.Form["fechasVentas"];
            string value1 = strValue;

        }
    }
}