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
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        private Usuario _usuarioTemp = new Usuario();
        private System.Drawing.Image _fotoPerfil;
        private string _filenamePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarFormulario();
                    this.CargarDdlRoldeUsuario();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
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
            if (this.GuardarUsuario())
            {
                lblStatus.Text = "El usuario se ha guardado con éxito!";
            }
            else lblStatus.Text = "El usuario no se ha podido guardar";
            this.LimpiarFormulario();
        }


        private bool GuardarUsuario()
        {
            if (_usuarioTemp != null && username.Value != "" && password.Value != "")
            {
                var idRol = int.Parse(this.ddlRolUsuario.SelectedValue);
                var rol = new RolUsuario
                {
                    Nivel = int.Parse(this.ddlRolUsuario.SelectedValue),
                    NombreRol = this.ddlRolUsuario.SelectedItem.ToString()
                };
                _usuarioTemp.Nombre = fNombre.Value;
                _usuarioTemp.Apellido = fApellido.Value;
                _usuarioTemp.Nickname = username.Value;
                _usuarioTemp.Password = password.Value;
                _usuarioTemp.Email = fEmail.Value;
                _usuarioTemp.Rol = rol;

                var stringRutaFoto = (string)Session["FotoRuta"];
                var FotoFile = (System.Drawing.Image)Session["FotoPerfil"];
                var stringRutaFotoDB = (string)Session["FotoRutaBase"];
                if (string.IsNullOrEmpty(stringRutaFotoDB)) stringRutaFotoDB = "../avatars/user_silhouette.png";
                _usuarioTemp.Foto = stringRutaFotoDB;

                if (FotoFile != null && !string.IsNullOrEmpty(stringRutaFotoDB))
                {
                    if (File.Exists(stringRutaFoto)) File.Delete(stringRutaFoto);
                    FotoFile.Save(stringRutaFoto);
                }
                
                return Fachada.Instance.InsertarUsuario(_usuarioTemp);
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
            if (_usuarioTemp != null && this.fupFoto.HasFile)
            {
                try
                {
                    // ruta de las imagenenes de animales en el sitio
                    string filename = this.username.Value + "_" + Path.GetFileName(fupFoto.FileName);
                    var carpetaAvatar = "img_tamboprp/usuarios/";

                    // ruta para save as en el sitio
                    var rutaSiteAvatar = "~/" + carpetaAvatar + filename;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fupFoto.PostedFile.InputStream);

                    // creo thumbnail
                    float imgWidth = image.PhysicalDimension.Width;
                    float imgHeight = image.PhysicalDimension.Height;
                    float imgSize = imgHeight > imgWidth ? imgHeight : imgWidth;
                    float imgResize = imgSize <= 200 ? (float)1.0 : 200 / imgSize;
                    imgWidth *= imgResize;
                    imgHeight *= imgResize;
                    System.Drawing.Image thumb = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);

                    var filenamePath = Path.Combine(
                    Server.MapPath("~/img_tamboprp/usuarios/"),
                    string.Format("{0}{1}",
                    Path.GetFileNameWithoutExtension(filename),
                    Path.GetExtension(filename)
                    )
                    );

                    _fotoPerfil = thumb;
                    _filenamePath = filenamePath;
                    //if (File.Exists(filenamePath)) File.Delete(filenamePath);
                    //thumb.Save(filenamePath);

                    // ruta para la base de datos
                    var rutaDbImg = "../" + carpetaAvatar + filename;
                    _usuarioTemp.Foto = rutaDbImg;

                    HttpContext.Current.Session["FotoPerfil"] = _fotoPerfil;
                    HttpContext.Current.Session["FotoRuta"] = _filenamePath;
                    HttpContext.Current.Session["FotoRutaBase"] = rutaDbImg;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "El archivo no se pudo subir";
                }
            }
        }

    }
}