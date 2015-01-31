using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.IO;
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
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.CargarMiPerfilUsuario();
                }
            }
            else Response.Redirect("~/Login.aspx", true);
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
                };
                //if (this.password.Value != "") usuario.Password = this.password.Value;
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

        //protected void btn_CambiarImagen(object sender, EventArgs e)
        //{

        //}

        protected void btn_ResetearPassword(object sender, EventArgs e)
        {
            try
            {
                if (this.ResetearPassword())
                {
                    this.lblStatus.Text = "La contraseña se actualizó";
                }
                else
                {
                    this.lblStatus.Text = "La contraseña no se pudo actualizar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool ResetearPassword()
        {
            if (this.password.Value != "")
            {
                var user = (VOUsuario)Session["Usuario"];
                var userPerfil = user.Nickname;
                return Fachada.Instance.ResetearPassword(userPerfil, userPerfil, this.password.Value);
            }
            else
            {
                this.lblStatus.Text = "La contraseña no puede ser vacía";
            }
            return false;
        }

        
        protected void btn_CambiarImagen(object sender, EventArgs e)
        {
            var u = (VOUsuario)Session["Usuario"];
            if (this.fupFoto.HasFile && u != null)
            {
                try
                {
                    // ruta de las imagenenes de animales en el sitio
                    string filename = u.Nickname + "_" + Path.GetFileName(fupFoto.FileName);
                    var carpetaAvatar = "img_tamboprp/usuarios/";

                    // ruta para save as en el sitio
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fupFoto.PostedFile.InputStream);

                    // resize
                    float imgWidth = image.PhysicalDimension.Width;
                    float imgHeight = image.PhysicalDimension.Height;
                    float imgSize = imgHeight > imgWidth ? imgHeight : imgWidth;
                    float imgResize = imgSize <= 200 ? (float) 1.0 : 200/imgSize;
                    imgWidth *= imgResize;
                    imgHeight *= imgResize;
                    System.Drawing.Image thumb = image.GetThumbnailImage((int) imgWidth, (int) imgHeight, delegate() { return false; }, (IntPtr) 0);

                    var filenamePath = Path.Combine(
                    Server.MapPath("~/img_tamboprp/usuarios/"),
                    string.Format("{0}{1}",
                    Path.GetFileNameWithoutExtension(filename),
                    Path.GetExtension(filename)
                    )
                    );

                    if (File.Exists(filenamePath)) File.Delete(filenamePath);
                    thumb.Save(filenamePath);

                    // ruta para la base de datos
                    var rutaDbImg = "../" + carpetaAvatar + filename;
                    var user = new Usuario();
                    user.Nickname = u.Nickname;
                    user.Foto = rutaDbImg;
                    if (Fachada.Instance.SubirFotoPerfilUsuario(user))
                    {
                        u.Foto = user.Foto;
                        lblStatus.Text = "Archivo subido";
                        this.CargarMiPerfilUsuario();
                    }
                    
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "El archivo no se pudo subir";
                }
            }
        }

         
         
    }
}