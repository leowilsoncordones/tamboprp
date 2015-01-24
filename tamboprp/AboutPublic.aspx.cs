using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class AboutPublic : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                Response.Redirect("~/About.aspx", true);
            }
            this.SetPageBreadcrumbs();
        }

        protected void SetPageBreadcrumbs()
        {
            var strB = new StringBuilder();
            strB.Append("<span class='pull-right'><strong>" + "+ Producción + Reproducción + Performance = + RENTABILIDAD!" + "</strong></span>");
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }
    }
}