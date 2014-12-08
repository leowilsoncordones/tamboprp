using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Microsoft.Ajax.Utilities;
using Negocio;

namespace tamboprp
{
    public partial class Tablero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAll()
        {
            return Fachada.Instance.ControlTotalGetAll();
        }

        [WebMethod]
        public static string TestAjax(string dato1, string dato2)
        {
            return dato1 + " leonardo " + dato2;
        }

    }
}