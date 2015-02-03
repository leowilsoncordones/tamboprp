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
    public partial class NuevoRemito : System.Web.UI.Page
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
                    this.CargarDdlEmpresasRemisoras();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Nuevo remito a planta", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            try
            {
                if (this.GuardarRemito())
                {
                    this.lblStatus.Text = "El remito se ha guardado con éxito";
                    this.LimpiarFormulario();
                }
                else
                {
                    this.lblStatus.Text = "El remito no se ha podido guardar";
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }


        private bool GuardarRemito()
        {
            //string strDate = this.mydate.Value;
            string strDate = Request.Form["mydate"];
            var idEmp = int.Parse(this.ddlEmpresa.SelectedValue);
            var emp = new EmpresaRemisora(idEmp);
            var remito = new Remito
            {
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Empresa = emp,
                Factura = fFactSerie.Value + " " + fFactNum.Value,
                Matricula = fMatricula.Value,
                Litros = Double.Parse(fLitros.Value),
                Encargado = fEncargado.Value,
                Temp_1 = Double.Parse(fTemp1.Value),
                Temp_2 = Double.Parse(fTemp2.Value),
                Observaciones = fObservaciones.Value,
            };
            return Fachada.Instance.InsertarRemito(remito);
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fFactSerie.Value = "";
            this.fFactNum.Value = "";
            this.fMatricula.Value = "";
            this.fLitros.Value = "";
            this.fTemp1.Value = "";
            this.fTemp2.Value = "";
            this.fEncargado.Value = "";
            this.fObservaciones.Value = "";
        }

        private void CargarDdlEmpresasRemisoras()
        {
            var lst = Fachada.Instance.GetEmpresaRemisoraActual();
            this.ddlEmpresa.DataSource = lst;
            this.ddlEmpresa.DataTextField = "Nombre";
            this.ddlEmpresa.DataValueField = "Id";
            this.ddlEmpresa.DataBind();
        }


        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        
    }
}