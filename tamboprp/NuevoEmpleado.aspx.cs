using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class NuevoEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarFormulario();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Personal", "Personal.aspx"));
            list.Add(new VoListItemDuplaString("Nuevo empleado", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            if (this.GuardarEmpleado())
            {
                this.lblStatus.Text = "El empleado se ha guardado con éxito";
            }
            else
            {
                this.lblStatus.Text = "El empleado no se ha podido guardar";
            }
            this.LimpiarFormulario();
        }


        private bool GuardarEmpleado()
        {
            try
            {
                if (this.fIniciales.Value != "")
                {
                    var emp = new Empleado
                    {
                        Nombre = this.fNombre.Value,
                        Apellido = this.fApellido.Value,
                        Iniciales = this.fIniciales.Value,
                        Activo = this.checkActivo.Checked
                    };
                    return Fachada.Instance.InsertarEmpleado(emp);
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fNombre.Value = "";
            this.fApellido.Value = "";
            this.fIniciales.Value = "";
            this.lblStatus.Text = "";
            this.checkActivo.Checked = true;
        }
        
    }
}