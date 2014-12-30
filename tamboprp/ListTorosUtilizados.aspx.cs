using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class ListTorosUtilizados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarListadoTorosUtilizados();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Listado de Toros utilizados", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoTorosUtilizados()
        {
            int anio = 2014;
            //List<VOAnimal> lst = Fachada.Instance.GetTorosUtilizadosPorAnio(anio);
            //this.gvTorosUtilizados.DataSource = lst;
            //this.gvTorosUtilizados.DataBind();
            //this.titCant.Visible = true;
            //this.lblCant.Text = lst.Count.ToString();
         
        }

        protected void GvTorosUtilizados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoTorosUtilizados();
        }

        private void LimpiarTabla()
        {
            this.gvTorosUtilizados.DataSource = null;
            this.gvTorosUtilizados.DataBind();
            this.lblCant.Text = "";
        }
    }
}