using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tamboprp
{
    public partial class Servicios_Sin_DiagP_70 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.gvServicios.DataBind();
        }
    }
}