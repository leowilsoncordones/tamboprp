using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.fUsuario.Value = "";
            //this.fContrasena.Value = "";

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string nick = this.fUsuario.Value;
            string pwd = this.fContrasena.Value;
            if (nick != "" && pwd != "")
            {
                VOUsuario usuario = Fachada.Instance.AskForLogin(nick, pwd);
                if (usuario != null && usuario.Habilitado)
                {
                    HttpContext.Current.Session["EstaLogueado"] = true;
                    HttpContext.Current.Session["EsAdmin"] = usuario.Rol.Nivel == 1;
                    HttpContext.Current.Session["EsDigitador"] = usuario.Rol.Nivel == 2;
                    HttpContext.Current.Session["EsLector"] = usuario.Rol.Nivel == 3;
                    HttpContext.Current.Session["Usuario"] = usuario;
                    var corporativo = Fachada.Instance.GetDatosCorporativos();
                    HttpContext.Current.Session["Corporativo"] = corporativo;

                    this.lblResLogin.Text = "Bienvenido! " + usuario.Nombre + " " + usuario.Apellido;

                    if ((bool)Session["EsAdmin"])
                    {
                        //var u = (VOUsuario)Session["Usuario"];
                        //this.lblResLogin.Text += " " + u.Nombre + " " + u.Apellido;
                        Response.Redirect("~/Tablero.aspx", true);
                    }
                    else if ((bool)Session["EsDigitador"]) Response.Redirect("~/NuevoEvento.aspx", true);
                    else if ((bool)Session["EsLector"]) Response.Redirect("~/Animales.aspx", true);
                    //else Response.Redirect("~/Login.aspx", true);
                }
                else
                {
                    this.lblResLogin.Visible = true;
                    this.lblResLogin.Text = "Login inválido";
                }
            }
            
        }

        protected void btn_MeOlvide(object sender, EventArgs e)
        {

        }

    }
}