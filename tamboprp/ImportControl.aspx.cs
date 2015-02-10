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
    public partial class ImportControl : System.Web.UI.Page
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
                    this.panelFallidas.Visible = false;
               	 	this.panelExitosas.Visible = false;
                	lblStatus.Visible = false;
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Producción", "Produccion.aspx"));
            list.Add(new VoListItemDuplaString("Importar archivo de controles de producción", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            LimpiarFormulario();
            
            // pop-up el modal y mostrar mensaje resultado de guardar en la base de datos
            if (this.fupTxt.HasFile)
            {
                try
                {
                    // ruta de las textos de controles en el sitio
                    string filename = Path.GetFileName(fupTxt.FileName);
                    var extension = Path.GetExtension(filename);
                    if (extension == "._hy")
                    {
                        var rutaSiteTxt = "~/controlesMU/" + filename;
                        //var rutaSiteTxtcheck = "http://www.tamboprp.uy/controlesMU/" + filename;
                        var websiteLocalPath =
                            "d:\\DZHosts\\LocalUser\\tamboprp_admin\\www.tamboprp.somee.com\\controlesMU\\" + filename;
                        if (!File.Exists(websiteLocalPath))
                        {
                            var usu = (VOUsuario) Session["Usuario"];
                            fupTxt.SaveAs(Server.MapPath(rutaSiteTxt));
                            var ruta = Server.MapPath("~/controlesMU/" + filename);
                            var resultado = Fachada.Instance.LeerArchivoControl(ruta, usu.Nickname);
                            lblTotales.Text = "Se procesaron un total de " + resultado.CantTotales + " controles.";
                            this.panelExitosas.Visible = true;
                            var cantFallidas = resultado.ControlesFallidos.Count;
                            if (cantFallidas > 0)
                            {
                                lblExitosas.Text = resultado.CantExitosas + " controles se procesaron con éxito.";
                                lblFallidas.Text = cantFallidas + " controles no pudieron ser procesados.";
                                CargarGrillaFallidas(resultado.ControlesFallidos);
                                this.panelFallidas.Visible = true;
                            }
                        }
                        else
                        {
                            lblStatus.Visible = true;
                            lblStatus.Text = "El archivo : '" + filename + "' ya fue cargado";
                        }
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "Este tipo de archivo: '" + filename + "' no puede ser cargado.";
                    }

                }
                catch (Exception)
                {
                    lblStatus.Visible = true;
                    lblStatus.Text = "El archivo no se pudo procesar";                    
                }

            }

            
        }


        private void CargarGrillaFallidas(List<Control_Producc> lista )
        {
            this.gvControles.DataSource = lista;
            this.gvControles.DataBind();
        }


        private void LimpiarFormulario()
        {
            lblStatus.Visible = false;
            panelExitosas.Visible = false;
            panelFallidas.Visible = false;
            //this.fNombre.Value = "";
            //this.fApellido.Value = "";
            //this.fEmail.Value = "";
            //this.username.Value = "";
            //this.password.Value = "";
        }
       
    }
}