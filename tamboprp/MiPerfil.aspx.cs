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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.CargarMiPerfilUsuario();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Mi perfil", ""));
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
            if (this.ActualizarUsuario())
            {
                //this.bodySaveModal.InnerText = "El usuario se ha guardado con éxito!";
            }
            //else this.bodySaveModal.InnerText = "El usuario no se ha podido guardar";
        }


        private bool ActualizarUsuario()
        {
            /*var idRol = int.Parse(this.ddlRolUsuario.SelectedValue);
            var rol = new RolUsuario
            {
                Nivel = int.Parse(this.ddlRolUsuario.SelectedValue),
                NombreRol = this.ddlRolUsuario.SelectedItem.ToString()
            };*/
            var usuario = new Usuario
            {
                Nombre = fNombre.Value,
                Apellido = fApellido.Value,
                Nickname = username.Value,
                Password = password.Value,
                Email = fEmail.Value,
                //Foto = "",
                //Rol = rol
            };
            //return Fachada.Instance.UpdateUsuario(usuario);
            return true;
        }

        private void CargarMiPerfilUsuario()
        {
            //var lst = Fachada.Instance.GetMiPerfilUsuario();
            var u = (VOUsuario)Session["Usuario"];
            if (u != null)
            {
                this.fNombre.Value = u.Nombre;
                this.fNomFoto.InnerHtml = u.ToString();
                this.fApellido.Value = u.Apellido;
                var e = (VOEmpresa)Session["Corporativo"];
                if (e != null)
                {
                    this.fEmpresa.Value = e.ToString();
                }
                this.fEmail.Value = u.Email;
                this.username.Value = u.Nickname;
                this.fRol.Value = u.Rol.ToString();
                if (u.Foto != "")
                {
                    this.avatar.Alt = u.ToString();
                    this.avatar.Src = u.Foto;
                }
            }
        }
        
    }
}