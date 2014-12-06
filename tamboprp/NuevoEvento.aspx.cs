using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class NuevoEvento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            this.lblVer.Text = this.fRegistro.Value + " " + this.datepicker.Value + " " + this.fComentario.Value;

            Celo_Sin_Servicio celo = new Celo_Sin_Servicio();
            celo.Registro = this.fRegistro.Value.ToString();
            string strDate = this.datepicker.Value.ToString();
            if (strDate != string.Empty) celo.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            celo.Comentarios = this.fComentario.Value.ToString();

        }
    }
}