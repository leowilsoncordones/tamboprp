using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.GetCategorias();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Categorías", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvCategorias.DataSource = null;
            this.gvCategorias.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
        }

        private void GetCategorias()
        {
            var lst = Fachada.Instance.GetCategoriasAnimalAll();
            this.gvCategorias.DataSource = lst;
            this.gvCategorias.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = lst.Count.ToString();
        }

        protected void GvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetCategorias();
            //gv.DataSource = _listTemp;
            //gv.DataBind();
        }
    }
}