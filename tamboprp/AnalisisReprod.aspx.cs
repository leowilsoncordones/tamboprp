using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class AnalisisReprod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.LimpiarRegistro();
            this.CargarAnalisisReproductivo();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Análisis", "Analisis.aspx"));
            list.Add(new VoListItemDuplaString("Análisis reproductivo", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarRegistro()
        {
            this.lblCantPartos.Text = "";
            this.lblCantCSS.Text = "";
            this.lblCantSecas.Text = "";
            this.lblCantServSemen.Text = "";
            this.lblAbortos.Text = "";
            this.lblCantLactComp.Text = "";
            this.lblCantRazSanit.Text = "";
            this.lblCantBajaProd.Text = "";
            this.lblCantPrenezAv.Text = "";
        }

        public void CargarAnalisisReproductivo()
        {
            //var voAnalitico = Fachada.Instance.GetAnaliticoVacasEnOrdene();
            //this.lblCant.Text = voAnalitico.CantVacasEnOrdene.ToString();
            //this.lblPromProdLecheLts.Text = voAnalitico.PromProdLecheLts.ToString();
            //this.lblCantL1.Text = voAnalitico.CantLactancia1.ToString();
            //this.lblCantL2.Text = voAnalitico.CantLactancia2.ToString();
            //this.lblCantOtrasL.Text = voAnalitico.CantLactanciaMayor2.ToString();
            //this.lblConSSP.Text = voAnalitico.ConServicioSinPreñez.ToString();
            //this.lblPrenezConf.Text = voAnalitico.PrenezConfirmada.ToString();
            //this.lblPromL.Text = voAnalitico.PromDiasLactancias.ToString();
        }
    }
}