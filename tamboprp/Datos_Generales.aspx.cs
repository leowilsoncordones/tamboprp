using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Datos_Generales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblAbortosEsteAno.Text = Fachada.Instance.GetCantAbortosEsteAnio().ToString();
            this.lblCantAnUltControl.Text = Fachada.Instance.GetCantAnimalesUltControl().ToString();
            this.lblSumLecheUltControl.Text = Fachada.Instance.GetSumLecheUltControl().ToString();
            this.lblPromLecheUltControl.Text = Fachada.Instance.GetPromLecheUltControl().ToString();
            this.lblSumGrasaUltControl.Text = Fachada.Instance.GetSumGrasaUltControl().ToString();
            this.lblPromGrasaUltControl.Text = Fachada.Instance.GetPromGrasaUltControl().ToString();

        }


    }
}