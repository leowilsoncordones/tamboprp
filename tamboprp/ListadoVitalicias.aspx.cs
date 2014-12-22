using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;
using Entidades;

namespace tamboprp
{
    public partial class ListadoVitalicias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarVitalicias();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Listado de vacas vitalicias", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }
        
        private void LimpiarTabla()
        {
            this.gvVitalicias.DataSource = null;
            this.gvVitalicias.DataBind();
            this.lblCateg.Text = "";
            this.lblCantAnimales.Text = "";
        }


        private void CargarVitalicias()
        {
            var listTemp = Fachada.Instance.GetVitalicias();
            this.gvVitalicias.DataSource = listTemp;
            this.gvVitalicias.DataBind();

            this.titCantAnimales.Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
        }

        protected void GvVitalicias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarVitalicias();
        }


    }
}