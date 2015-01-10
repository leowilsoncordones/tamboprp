using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class AnalisisVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.LimpiarRegistro();
            this.CargarAnalisisVentas();
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
    }
}