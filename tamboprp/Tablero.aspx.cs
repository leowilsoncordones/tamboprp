using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class Tablero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]))
            {
                if (!Page.IsPostBack)
                {
                    //this.SetMyProfileName();
                    this.SetPageBreadcrumbs();
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Tablero", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        //protected void SetMyProfileName()
        //{
        //    var u = (VOUsuario)Session["Usuario"];
        //    if (u != null)
        //    {
        //        var strMyName = "Hola, " + u.Nombre;// + " " + u.Apellido;
        //        if (Master != null)
        //        {
        //            var fProf = Master.FindControl("fProfile") as System.Web.UI.HtmlControls.HtmlAnchor;
        //            if (fProf != null) fProf.InnerHtml = strMyName.ToString();
        //        }   
        //    }
        //}

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAll()
        {
            return Fachada.Instance.ControlTotalGetAll();
        }
    }
}