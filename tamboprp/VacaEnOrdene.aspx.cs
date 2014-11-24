using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class VacaEnOrdene : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LimpiarRegistro();
            this.CargarAnalitico();
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