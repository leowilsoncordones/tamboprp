using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Logoff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estaLogueado"] != null)
            {

                var u = (VOUsuario)Session["Usuario"];
                if (u != null)
                {
                    bool res = Fachada.Instance.Logoff(u.Nickname);
                }
                Session["EstaLogueado"] = false;
                Session["EsAdmin"] = false;
                Session["EsDigitador"] = false;
                Session["EsLector"] = false;
                Session["Usuario"] = null;
                Session.Abandon();
            }
            Response.Redirect("~/Login.aspx", true);
        }
    }
}