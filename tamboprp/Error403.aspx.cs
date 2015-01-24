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
    public partial class Error403 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Error 403", ""));
            var sb = new StringBuilder();
            sb.Append("<ul class='breadcrumb'>");
            sb.Append("<li><i class='ace-icon fa fa-home home-icon'></i> <a href='../Default.aspx'>Home</a></li>");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Valor2 == "")
                    sb.Append("<li class='active'>" + list[i].Valor1 + "</li>");
                else sb.Append("<li><a href='" + list[i].Valor2 + "'>" + list[i].Valor1 + "</a></li>");
            }
            sb.Append("</ul>");

            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = sb.ToString();
            }
        }
    }
}