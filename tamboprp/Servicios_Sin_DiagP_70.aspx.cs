using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Servicios_Sin_DiagP_70 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarGrilla();
            }
        }




        public void cargarGrilla() 
        {
            var list = Fachada.Instance.GetServicios70SinDiagPrenezVaqEnt();
            this.lblCantAnimales.Text = list.Count.ToString();
            this.titCantAnimales.Visible = true;
            this.lblCantAnimales.Visible = true;
            this.gvServicios.DataSource = list;
            this.gvServicios.DataBind();
        }
    }
}