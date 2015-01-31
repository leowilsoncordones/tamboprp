using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Proximamente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var cantAnimUltControl = Fachada.Instance.GetCantAnimalesUltControl();
                //var cantAnimUltControl = Fachada.Instance.GetCantAbortosEsteAnio();
                //this.lblStatus.Text = cantAnimUltControl.ToString();
            }
        }
    }
}