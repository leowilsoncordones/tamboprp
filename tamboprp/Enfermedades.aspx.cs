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
    public partial class Enfermedades : System.Web.UI.Page
    {
        private List<Enfermedad> _listTemp = new List<Enfermedad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.GetEnfermedades();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Tabla de Enfermedades", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvEnfermedades.DataSource = null;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
        }

        private void GetEnfermedades()
        {
            _listTemp = Fachada.Instance.GetEnfermedades();
            this.gvEnfermedades.DataSource = _listTemp;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = _listTemp.Count.ToString();
        }

        protected void GvEnfermedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            GetEnfermedades();
            //gv.DataSource = _listTemp;
            //gv.DataBind();
        }

        //protected void GvEnfermedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    var gv = (GridView)sender;
        //    gv.PageIndex = e.NewPageIndex;
        //    gv.DataSource = _listTemp;
        //    gv.DataBind();
        //}

    }
}