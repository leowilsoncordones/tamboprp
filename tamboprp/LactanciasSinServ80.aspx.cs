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
            cargarGrilla();
        }

        public void cargarGrilla()
        {
            var list = Fachada.Instance.GetLactanciasSinServicio80();
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvServicios.DataSource = list;
            this.gvServicios.DataBind();
        }
    }
}