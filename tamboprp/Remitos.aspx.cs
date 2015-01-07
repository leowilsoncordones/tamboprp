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
            this.SetPageBreadcrumbs();
            this.LimpiarRegistro();
            this.CargarRemitos();
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

        [WebMethod]
        public static List<RemitoMapper.VORemitoGrafica> RemitosGraficasGetAll()
        {
            return Fachada.Instance.GetRemitosGraficas();
        }
    }
}