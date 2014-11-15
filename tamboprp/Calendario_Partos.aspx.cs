using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tamboprp
{
    public partial class Calendario_Partos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDdl();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {

        }

        private void cargarDdl() 
        {
            var list = Fachada.Instance.GetMeses();
            this.ddlMeses.DataSource = list;
            this.ddlMeses.DataBind();

        }

    }
}