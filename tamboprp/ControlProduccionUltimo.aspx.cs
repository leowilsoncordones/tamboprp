using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class ControlProduccionUltimo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LimpiarTabla();
                this.GetControlesProduccUltimo();
            }
        }
        
        private void LimpiarTabla()
        {
            this.gvControlProdUltimo.DataSource = null;
            this.gvControlProdUltimo.DataBind();
            this.lblFecha.Visible = false;
            this.lblFecha.Text = "";
            this.lblCantAnimales.Text = "";
        }

        private void GetControlesProduccUltimo()
        {
            var listTemp = Fachada.Instance.GetControlesProduccUltimo();
            this.gvControlProdUltimo.DataSource = listTemp;
            this.gvControlProdUltimo.DataBind();
            this.titCantAnimales.Visible = true;
            this.lblFecha.Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
            if (listTemp.Count > 0)
            {
                this.lblFecha.Text = listTemp[0].FechaControl.ToShortDateString();
            }
        }



        protected void GvControlProdUltimo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            GetControlesProduccUltimo();
        }

    }
}