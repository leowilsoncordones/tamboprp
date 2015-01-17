using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class AnalisisInseminadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarListadoRankingInseminadores();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Ranking de inseminadores", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }


        public void CargarListadoRankingInseminadores()
        {
            //var lstFechas = Fachada.Instance.GetFechasDiagnosticoPorAnio(2014);

            int anio = 2014;
            var lst = Fachada.Instance.GetRankingInseminadores(anio);
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantPrenadas;
                }

                this.lblTotalServicios.Text = totalServ.ToString();
                this.lblTotalPrenadas.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double)totalPren / totalServ * 100, 1);
                    this.lblPorcEfectividad.Text = porcEfect.ToString() + "%";
                }

            }
            this.lblCantInsem.Text = lst.Count.ToString();
            this.lblCantInsem.Visible = true;
            this.titCantInsem.Visible = true;

            this.gvRanking.DataSource = lst;
            this.gvRanking.DataBind();
            
        }


        private void LimpiarTabla()
        {
            this.gvRanking.DataSource = null;
            this.gvRanking.DataBind();

            this.titCantInsem.Visible = false;
            this.lblCantInsem.Text = "";

            this.lblTotalServicios.Text = "";
            this.lblTotalPrenadas.Text = "";
            this.lblPorcEfectividad.Text = "";
        }
    }
}