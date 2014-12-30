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
    public partial class Cmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarCmt();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sanidad", "Sanidad.aspx"));
            list.Add(new VoListItemDuplaString("Diágnosticos C.M.T.", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarCmt()
        {
            //List<Cmt> lst = Fachada.Instance.GetCmtAll();
            //this.gvCmt.DataSource = lst;
            var lst = Fachada.Instance.GetCmtAll();
            this.gvCmt.DataSource = lst;
            this.gvCmt.DataBind();
            this.titCant.Visible = true;
            this.lblCant.Text = lst.Count.ToString();
        }

        protected void GvCmt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarCmt();
        }

        private void LimpiarTabla()
        {
            this.gvCmt.DataSource = null;
            this.gvCmt.DataBind();
            this.lblCant.Text = "";
        }
    }
}