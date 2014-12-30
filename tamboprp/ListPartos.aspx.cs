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
    public partial class ListPartos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarListadoPartos();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reproducción", "Reproduccion.aspx"));
            list.Add(new VoListItemDuplaString("Listado de partos", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoPartos()
        {
            int anio = 2014;
            var lst = Fachada.Instance.GetPartosPorAnio(anio);
            var cantNacim = Fachada.Instance.GetCantNacimientosPorAnio(anio);
            var cantMellizos = Fachada.Instance.GetCantMellizosPorAnio(anio);
            var cantTrillizos = Fachada.Instance.GetCantTrillizosPorAnio(anio);
            int cant = lst.Count;
            int hembras = 0;
            int machos = 0;
            int muertos = 0;
            for (int i = 0; i < cant; i++)
            {
                if (lst[i].Sexo_parto == 'M') machos++;
                if (lst[i].Sexo_parto == 'H') hembras++;
                if (lst[i].Reg_hijo == "-") muertos++;
            }
            double promHembras = 0;
            double promMachos = 0;
            if (cant > 0)
            {
                promHembras = Math.Round((double)hembras / cant * 100, 2);
                promMachos = Math.Round((double)machos / cant * 100, 2);
            }

            this.gvListPartos.DataSource = lst;
            this.gvListPartos.DataBind();
            
            this.titCant.Visible = true;
            this.lblCant.Text = cant.ToString();
           
            this.lblTotalPartos.Text = cant.ToString();
            //int nac = cant + cantMellizos + (cantTrillizos * 2) - muertos;
            this.lblTotalNac.Text = cantNacim.ToString();
            this.lblH.Text = hembras.ToString();
            this.lblPromH.Text = promHembras.ToString() + "%";
            this.lblM.Text = machos.ToString();
            this.lblPromM.Text = promMachos.ToString() + "%";

            this.lblMellizos.Text = cantMellizos.ToString();
            this.lblTrillizos.Text = cantTrillizos.ToString();
            
            this.lblTotalMuertos.Text = muertos.ToString();
            double promMuertos = Math.Round((double)muertos / cant * 100, 2);
            this.lblPromMuertos.Text = promMuertos.ToString() + "%";
            double promNac = Math.Round((double)cantNacim / cant * 100, 2);
            this.lblPromNac.Text = promNac.ToString() + "%";

        }

        protected void GvListPartos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoPartos();
        }

        private void LimpiarTabla()
        {
            this.gvListPartos.DataSource = null;
            this.gvListPartos.DataBind();
            this.lblCant.Text = "";
            this.lblTotalPartos.Text = "";
            this.lblTotalNac.Text = "";
            this.lblH.Text = "";
            this.lblM.Text = "";
        }
    }
}