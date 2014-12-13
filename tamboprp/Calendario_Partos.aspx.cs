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
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.cargarDdl();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Calendario de Partos", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            LimpiarTabla();
            var idFecha = DateTime.Parse(this.ddlMeses.SelectedValue);
            var listServ = Fachada.Instance.GetProximosPartos(idFecha);
            this.gvPartos.DataSource = listServ;
            this.gvPartos.DataBind();
            this.lblCantAnimales.Text = listServ.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.titCantAnimales.Visible = true;
        }

        private void cargarDdl() 
        {
            var list = Fachada.Instance.GetMeses();
            this.ddlMeses.DataSource = list;
            this.ddlMeses.DataTextFormatString = "{0:MMMM - yyyy}";
            this.ddlMeses.DataBind();

        }


        private void LimpiarTabla()
        {
            this.gvPartos.DataSource = null;
            this.gvPartos.DataBind();
            this.lblCantAnimales.Visible = false;
            this.lblCantAnimales.Text = "";
            this.titCantAnimales.Visible = false;
        }

    }
}