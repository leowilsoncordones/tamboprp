using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class AnalisisProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.LimpiarRegistro();

            // cargo tabla analitica de vacas en ordeñe
            this.CargarAnalitico();

            // cargo datos generales
            this.lblAbortosEsteAno.Text = Fachada.Instance.GetCantAbortosEsteAnio().ToString();
            this.lblCantAnUltControl.Text = Fachada.Instance.GetCantAnimalesUltControl().ToString();
            this.lblSumLecheUltControl.Text = Fachada.Instance.GetSumLecheUltControl().ToString();
            this.lblPromLecheUltControl.Text = Math.Round(Fachada.Instance.GetPromLecheUltControl(), 2).ToString();
            this.lblSumGrasaUltControl.Text = Fachada.Instance.GetSumGrasaUltControl().ToString();
            this.lblPromGrasaUltControl.Text = Math.Round(Fachada.Instance.GetPromGrasaUltControl(), 2).ToString();
            this.lblCantOrdene.Text = Fachada.Instance.GetCantOrdene().ToString();
            this.badgeCantOrdene.Text = this.lblCantOrdene.Text;
            this.lblCantEntoradas.Text = Fachada.Instance.GetCantEntoradas().ToString();
            this.lblCantSecas.Text = Fachada.Instance.GetCantSecas().ToString();
            this.lblFechaUltControl.Text = Fachada.Instance.GetFechaUltimoControl().ToString();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Análisis de producción y ordeñe", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarRegistro()
        {
            this.lblCant.Text = "";
            this.lblPromProdLecheLts.Text = "";
            this.lblCantL1.Text = "";
            this.lblCantL2.Text = "";
            this.lblCantOtrasL.Text = "";
            this.lblConSSP.Text = "";
            this.lblPrenezConf.Text = "";
            this.lblPromL.Text = "";

            this.lblPorcL1.Text = "";
            this.lblPorcL2.Text = "";
            this.lblPorcOtrasL.Text = "";
            this.lblPorcPrenezConf.Text = "";
        }

        public void CargarAnalitico()
        {
            var voAnalitico = Fachada.Instance.GetAnaliticoVacasEnOrdene();
            this.lblCant.Text = voAnalitico.CantVacasEnOrdene.ToString();
            this.lblPromProdLecheLts.Text = voAnalitico.PromProdLecheLts.ToString();
            this.lblCantL1.Text = voAnalitico.CantLactancia1.ToString();
            this.lblPorcL1.Text = voAnalitico.PorcLactancia1.ToString() + "%";
            this.lblCantL2.Text = voAnalitico.CantLactancia2.ToString();
            this.lblPorcL2.Text = voAnalitico.PorcLactancia2.ToString() + "%";
            this.lblCantOtrasL.Text = voAnalitico.CantLactanciaMayor2.ToString();
            this.lblPorcOtrasL.Text = voAnalitico.PorcLactanciaMayor2.ToString() + "%";
            
            this.lblConSSP.Text = voAnalitico.ConServicioSinPreñez.ToString();
            this.lblPorcConSSP.Text = voAnalitico.PromServicioSinPrenez.ToString() + "%";
            this.lblPrenezConf.Text = voAnalitico.PrenezConfirmada.ToString();
            this.lblPorcPrenezConf.Text = voAnalitico.PromPrenezConfirmada.ToString() + "%";
            this.lblPromL.Text = voAnalitico.PromDiasLactancias.ToString();
        }
    }
}