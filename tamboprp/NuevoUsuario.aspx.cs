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
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarFormulario();
                this.CargarDdlRoldeUsuario();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Nuevo usuario", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            // pop-up el modal y mostrar mensaje resultado de guardar en la base de datos
            if (this.GuardarUsuario())
            {
                //this.bodySaveModal.InnerText = "El usuario se ha guardado con éxito!";
            }
            //else this.bodySaveModal.InnerText = "El usuario no se ha podido guardar";
            this.LimpiarFormulario();
        }


        private bool GuardarUsuario()
        {
            var idRol = int.Parse(this.ddlRolUsuario.SelectedValue);
            var rol = new RolUsuario
            {
                Nivel = int.Parse(this.ddlRolUsuario.SelectedValue),
                NombreRol = this.ddlRolUsuario.SelectedItem.ToString()
            };
            var usuario = new Usuario
            {
                Nombre = fNombre.Value,
                Apellido = fApellido.Value,
                Nickname = username.Value,
                Password = password.Value,
                Email = fEmail.Value,
                Foto = avatar.Src,
                Rol = rol
            };
            return Fachada.Instance.InsertarUsuario(usuario);
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fNombre.Value = "";
            this.fApellido.Value = "";
            this.fEmail.Value = "";
            this.username.Value = "";
            this.password.Value = "";
        }

        private void CargarDdlRoldeUsuario()
        {
            var lst = Fachada.Instance.GetRolesDeUsuario();
            this.ddlRolUsuario.DataSource = lst;
            this.ddlRolUsuario.DataTextField = "NombreRol";
            this.ddlRolUsuario.DataValueField = "Nivel";
            this.ddlRolUsuario.DataBind();
            // por defecto seleccionado el rol DIGITADOR
            this.ddlRolUsuario.SelectedIndex = 1;
        }


        protected void ddlRolUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var rol = this.ddlRolUsuario.SelectedIndex;
            //var rol1 = this.ddlRolUsuario.SelectedItem;
            //var rol2 = this.ddlRolUsuario.SelectedValue;
        }

        protected void btn_CambiarImagen(object sender, EventArgs e)
        {

        }
        
    }
}