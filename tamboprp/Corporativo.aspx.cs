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
                this.FLetraSistema.Value = emp.LetraSistema;
                this.fDireccion.Value = emp.Direccion;
                this.fCiudad.Value = emp.Ciudad;
                this.fCP.Value = emp.Cpostal;
                this.fCelular.Value = emp.Celular;
                this.fTelefono.Value = emp.Telefono;
                this.fWeb.Value = emp.Web;
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            // pop-up el modal y mostrar mensaje resultado de guardar en la base de datos
            if (this.ActualizarDatosCorporativos())
            {
                this.lblStatus.Text = "Los datos se han guardado con éxito";
            }
            else
            {
                this.lblStatus.Text = "Los datos no se han podido guardar";
            }
            //this.LimpiarFormulario();
            this.CargarDatosCorporativos();
        }


        private bool ActualizarDatosCorporativos()
        {
            var e = (VOEmpresa)Session["Corporativo"];
            if (e != null)
            {
                var eTemp = new VOEmpresa
                {
                    Id = e.Id,
                    Nombre = this.fNombre.Value,
                    RazonSocial = this.fRazonSocial.Value,
                    Rut = this.fRut.Value,
                    LetraSistema = this.FLetraSistema.Value,
                    Direccion = this.fDireccion.Value,
                    Ciudad = this.fCiudad.Value,
                    Telefono = this.fTelefono.Value,
                    Celular = this.fCelular.Value,
                    Web = this.fWeb.Value,
                    Cpostal = this.fCP.Value,
                    Logo = e.Logo,
                    LogoCh = e.LogoCh,
                    Actual = e.Actual
                };
                return Fachada.Instance.UpdateDatosCorporativos(eTemp);
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
            this.fRazonSocial.Value = "";
            this.fRut.Value = "";
            this.fDireccion.Value = "";
            this.fCiudad.Value = "";
            this.fCP.Value = "";
            this.fCelular.Value = "";
            this.fTelefono.Value = "";
        }

        protected void btn_CambiarImagen(object sender, EventArgs e)
        {

        }
    }
}