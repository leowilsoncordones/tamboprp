using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace tamboprp
{
    public partial class Personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CargarEmpleados();
            }
        }

        public void CargarEmpleados()
        {
            var emp = new EmpleadoMapper();
            List<Empleado> lst = emp.GetAll();
            this.gvEmpleados.DataSource = lst;
            this.gvEmpleados.DataBind();
        }
    }
}