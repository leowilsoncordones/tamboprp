using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Enfermedades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LimpiarTabla();
                this.GetEnfermedades();
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
            var listTemp = Fachada.Instance.GetEnfermedades();
            this.gvEnfermedades.DataSource = listTemp;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = listTemp.Count.ToString();
        }

        protected void GvEnfermedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            GetEnfermedades();
        }

    }
}