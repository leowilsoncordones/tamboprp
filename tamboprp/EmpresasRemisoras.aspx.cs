using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class EmpresasRemisoras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.GetEmpresasRemisoras();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Producción", "Produccion.aspx"));
            list.Add(new VoListItemDuplaString("Empresas remisoras", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvRemisoras.DataSource = null;
            this.gvRemisoras.DataBind();
        }

        private void GetEmpresasRemisoras()
        {
            var lst = Fachada.Instance.GetEmpresasRemisorasAll();
            this.gvRemisoras.DataSource = lst;
            this.gvRemisoras.DataBind();
        }

        protected void GvRemisoras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetEmpresasRemisoras();
        }
    }
}