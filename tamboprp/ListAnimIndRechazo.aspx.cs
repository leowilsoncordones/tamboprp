using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class ListAnimIndRechazo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.CargarListadoAnimConIndicRechazo();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Listado de Animales con indicación de rechazo", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoAnimConIndicRechazo()
        {
            int anio = 2014;
            //List<Parto> lst = Fachada.Instance.GetPartosPorAnio(anio);
            //this.gvAnimConIndRechazo.DataSource = lst;
            //this.gvAnimConIndRechazo.DataBind();
            //this.titCant.Visible = true;
            //this.lblCant.Text = lst.Count.ToString();
            //this.lblTotalPartos.Text = lst.Count.ToString();
          
        }

        protected void GvAnimConIndRechazo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoAnimConIndicRechazo();
        }

        private void LimpiarTabla()
        {
            this.gvAnimConIndRechazo.DataSource = null;
            this.gvAnimConIndRechazo.DataBind();
            this.lblCant.Text = "";
        }
    }
}