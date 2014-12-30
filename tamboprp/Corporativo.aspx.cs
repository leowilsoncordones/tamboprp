using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Corporativo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.CargarDatosCorporativos();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Datos corporativos", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarDatosCorporativos()
        {
            var emp = Fachada.Instance.GetDatosCorporativos();
            if (emp != null)
            {
                this.fNombre.Value = emp.Nombre;
                this.fRazonSocial.Value = emp.RazonSocial;
                this.fRut.Value = emp.Rut;
                this.fDireccion.Value = emp.Direccion;
                this.fCP.Value = emp.Cpostal;
                this.fCelular.Value = emp.Celular;
                this.fTelefono.Value = emp.Telefono;
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            // pop-up el modal y mostrar mensaje resultado de guardar en la base de datos
            if (this.GuardarDatos())
            {
                //this.bodySaveModal.InnerText = "El usuario se ha guardado con éxito!";
            }
            //else this.bodySaveModal.InnerText = "El usuario no se ha podido guardar";
            this.LimpiarFormulario();
        }


        private bool GuardarDatos()
        {
            //var usuario = new Usuario
            //{
            //    Nombre = fNombre.Value,
            //    Apellido = fApellido.Value,
            //    Nickname = username.Value,
            //    Password = password.Value,
            //    Email = fEmail.Value,
            //    Foto = "",
            //    Rol = rol
            //};
            //return Fachada.Instance.InsertarUsuario(usuario);
            return true;
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fNombre.Value = "";
            this.fRazonSocial.Value = "";
            this.fRut.Value = "";
            this.fDireccion.Value = "";
            this.fCP.Value = "";
            this.fCelular.Value = "";
            this.fTelefono.Value = "";
        }
    }
}