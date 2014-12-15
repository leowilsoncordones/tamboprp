using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class CategConcurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.GetCategConcurso();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Cabaña", "Cabana.aspx"));
            list.Add(new VoListItemDuplaString("Categorías de concurso", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvCategConcurso.DataSource = null;
            this.gvCategConcurso.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
        }

        private void GetCategConcurso()
        {
            var lst = Fachada.Instance.GetCategoriasConcursoAll();
            this.gvCategConcurso.DataSource = lst;
            this.gvCategConcurso.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = lst.Count.ToString();
        }

        protected void GvCategConcurso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetCategConcurso();
            //gv.DataSource = _listTemp;
            //gv.DataBind();
        }
    }
}