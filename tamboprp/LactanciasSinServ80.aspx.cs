using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class LactanciasSinServ80 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.CargarGrilla();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Vacas con 80 días en lactancia y sin servicio", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarGrilla()
        {
            var list = Fachada.Instance.GetLactanciasSinServicio80();
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvServicios.DataSource = list;
            this.gvServicios.DataBind();
        }

        protected void GvLactanciasSinServ80_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarGrilla();
        }
    }
}