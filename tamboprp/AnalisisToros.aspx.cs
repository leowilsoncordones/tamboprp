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
    public partial class AnalisisToros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarListadoTorosUtilizados();
                this.CargarListadoTorosNacimPorGenero();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Análisis de toros utilizados y su efectividad", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarListadoTorosUtilizados()
        {
            int anio = 2014;
            var lst = Fachada.Instance.GetTorosUtilizadosPorAnio(anio);
            int totalServ = 0;
            int totalPren = 0;
            if (lst.Count > 0)
            {
                this.ImprimirTopUtilizados(lst);
                for (int i = 0; i < lst.Count; i++)
                {
                    totalServ += lst[i].CantServicios;
                    totalPren += lst[i].CantDiagP;
                }

                this.lblTotalServ.Text = totalServ.ToString();
                this.lblTotalEfect.Text = totalPren.ToString();
                if (totalServ > 0)
                {
                    var porcEfect = Math.Round((double) totalPren / totalServ * 100, 2);
                    this.lblPorcEfect.Text = porcEfect.ToString() + "% efect.";
                }

            }
            this.lblCantToros.Text = lst.Count.ToString();

            this.gvTorosUtilizados.DataSource = lst;
            this.gvTorosUtilizados.DataBind();
            this.titCant.Visible = true;
            this.lblCant.Text = lst.Count.ToString();
         
        }

        private void ImprimirTopUtilizados(List<VOToroUtilizado> lst)
        {
            var lstOrderByCantServ = Fachada.Instance.GetTopUtilizados(lst);
            if (lstOrderByCantServ.Count > 0)
            {
                lstOrderByCantServ.Sort();
                this.lblRegMasUtiliz1.Text = lstOrderByCantServ[0].Registro;
                this.lblMasUtiliz1.Text = lstOrderByCantServ[0].CantServicios.ToString();
                this.lblEfect1.Text = lstOrderByCantServ[0].PorcEfectividad.ToString() + "% efect.";
                if (lstOrderByCantServ.Count > 1)
                {
                    this.lblRegMasUtiliz2.Text = lstOrderByCantServ[1].Registro;
                    this.lblMasUtiliz2.Text = lstOrderByCantServ[1].CantServicios.ToString();
                    this.lblEfect2.Text = lstOrderByCantServ[1].PorcEfectividad.ToString() + "% efect.";
                }
                if (lstOrderByCantServ.Count > 2)
                {
                    this.lblRegMasUtiliz3.Text = lstOrderByCantServ[2].Registro;
                    this.lblMasUtiliz3.Text = lstOrderByCantServ[2].CantServicios.ToString();
                    this.lblEfect3.Text = lstOrderByCantServ[2].PorcEfectividad.ToString() + "% efect.";
                }
            }

        }

        protected void GvTorosUtilizados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoTorosUtilizados();
        }

        protected void GvTorosNacimPorGenero_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarListadoTorosNacimPorGenero();
        }

        private void CargarListadoTorosNacimPorGenero()
        {
            int anio = 2014;
            var lst = Fachada.Instance.GetTorosNacimPorGenero(anio);
            this.gvTorosNacimPorGenero.DataSource = lst;
            this.gvTorosNacimPorGenero.DataBind();

            this.titCant2.Visible = true;
            this.lblCant2.Text = lst.Count.ToString();
        }

        private void LimpiarTabla()
        {
            this.gvTorosUtilizados.DataSource = null;
            this.gvTorosUtilizados.DataBind();
            this.gvTorosNacimPorGenero.DataSource = null;
            this.gvTorosNacimPorGenero.DataBind();

            this.titCant.Visible = false;
            this.lblCant.Text = "";
            this.titCant2.Visible = false;
            this.lblCant2.Text = "";
            this.lblCantToros.Text = "";

            this.lblRegMasUtiliz1.Text = "";
            this.lblMasUtiliz1.Text = "";
            this.lblEfect1.Text = "";
            this.lblRegMasUtiliz2.Text = "";
            this.lblMasUtiliz2.Text = "";
            this.lblEfect2.Text = "";
            this.lblRegMasUtiliz3.Text = "";
            this.lblMasUtiliz3.Text = "";
            this.lblEfect3.Text = "";

            this.lblTotalServ.Text = "";
            this.lblTotalEfect.Text = "";
            this.lblPorcEfect.Text = "";
        }
    }
}