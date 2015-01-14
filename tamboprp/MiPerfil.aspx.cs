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
            if (this.ActualizarUsuario())
            {
                this.lblStatus.Text = "Los datos se han guardado con éxito";
                var u = (VOUsuario)Session["Usuario"];
                VOUsuario uRefresh = Fachada.Instance.GetDatosUsuario(u.Nickname);
                HttpContext.Current.Session["Usuario"] = uRefresh;
            }
            else
            {
                this.lblStatus.Text = "Los datos no se han podido guardar";
            }
            //this.LimpiarFormulario();
            this.CargarMiPerfilUsuario();
        }


        private bool ActualizarUsuario()
        {
            var u = (VOUsuario) Session["Usuario"];
            if (u != null)
            {
                var usuario = new Usuario
                {
                    Nombre = this.fNombre.Value,
                    Apellido = this.fApellido.Value,
                    Nickname = u.Nickname,
                    Email = this.fEmail.Value,
                    Habilitado = u.Habilitado,
                    Rol = u.Rol,
                    Foto = u.Foto,
                    //Password = ,
                };
                if (this.password.Value != "") usuario.Password = this.password.Value;
                return Fachada.Instance.UpdateUsuario(usuario);
                //return true;
            }
            return false;
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

        protected void btn_CambiarImagen(object sender, EventArgs e)
        {

        }
    }
}