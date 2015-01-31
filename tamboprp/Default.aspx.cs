using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            //{
            //    if (!Page.IsPostBack)
            //    {
            //        if (Session["Usuario"] != null)
            //        {
            //            var u = (VOUsuario)Session["Usuario"];
            //            if (!u.Habilitado) Response.Redirect("~/Logoff.aspx", true);
            //        }
                    
            //        if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]) Response.Redirect("~/Tablero.aspx", true);
            //        else if (Session["EsDigitador"] != null && (bool)Session["EsDigitador"]) Response.Redirect("~/NuevoEvento.aspx", true);
            //        else if (Session["EsLector"] != null && (bool)Session["EsLector"]) Response.Redirect("~/Animales.aspx", true);
            //        else Response.Redirect("~/Login.aspx", true);
            //    }
            //}
            //else Response.Redirect("~/Login.aspx", true);
            Response.Redirect("~/Proximamente.aspx", true);
        }
    }
}