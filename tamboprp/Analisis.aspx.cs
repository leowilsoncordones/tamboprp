using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Analisis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LimpiarRegistro();

            // cargo tabla analitica de vacas en ordeñe
            this.CargarAnalitico();

            // cargo datos generales
            this.lblAbortosEsteAno.Text = Fachada.Instance.GetCantAbortosEsteAnio().ToString();
            this.lblCantAnUltControl.Text = Fachada.Instance.GetCantAnimalesUltControl().ToString();
            this.lblSumLecheUltControl.Text = Fachada.Instance.GetSumLecheUltControl().ToString();
            this.lblPromLecheUltControl.Text = Fachada.Instance.GetPromLecheUltControl().ToString();
            this.lblSumGrasaUltControl.Text = Fachada.Instance.GetSumGrasaUltControl().ToString();
            this.lblPromGrasaUltControl.Text = Fachada.Instance.GetPromGrasaUltControl().ToString();
            this.lblCantOrdene.Text = Fachada.Instance.GetCantOrdene().ToString();
            this.badgeCantOrdene.Text = this.lblCantOrdene.Text;
            this.lblCantEntoradas.Text = Fachada.Instance.GetCantEntoradas().ToString();
            this.lblCantSecas.Text = Fachada.Instance.GetCantSecas().ToString();
            this.lblFechaUltControl.Text = Fachada.Instance.GetFechaUltimoControl().ToString();

        }

        private void LimpiarRegistro()
        {
            this.lblCant.Text = "";
            this.lblPromProdLecheLts.Text = "";
            this.lblCantL1.Text = "";
            this.lblCantL2.CssClass = "";
            this.lblCantOtrasL.Text = "";
            this.lblConSSP.Text = "";
            this.lblPrenezConf.Text = "";
            this.lblPromL.Text = "";
        }

        public void CargarAnalitico()
        {
            var voAnalitico = Fachada.Instance.GetAnaliticoVacasEnOrdene();
            this.lblCant.Text = voAnalitico.CantVacasEnOrdene.ToString();
            //this.lblPromProdLecheLts.Text = voAnalitico.PromProdLecheLts.ToString();
            this.lblCantL1.Text = voAnalitico.CantLactancia1.ToString();
            this.lblCantL2.Text = voAnalitico.CantLactancia2.ToString();
            //this.lblCantOtrasL.Text = voAnalitico.CantLactanciaMayor2.ToString();
            //this.lblConSSP.Text = voAnalitico.ConServicioSinPreñez.ToString();
            //this.lblPrenezConf.Text = voAnalitico.PrenezConfirmada.ToString();
            //this.lblPromL.Text = voAnalitico.PromDiasLactancias.ToString();
        }
    }
}