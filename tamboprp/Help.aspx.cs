using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.CargarDdlTipoProblema();
            //this.LimpiarFormulario();
            this.PreCargarFormulario();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Ayuda y soporte", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void PreCargarFormulario()
        {
            var corporativo = Fachada.Instance.GetDatosCorporativos();
            this.fEstablecimiento.Value = corporativo.ToString();
            //this.fNomApe.Value = ;
            //this.fEmail.Value = "";
        }

        private void CargarDdlTipoProblema()
        {
            var lst = new List<VoListItem>();
            //var item0 = new VoListItem { Id = 0, Nombre = "Seleccione" }; lst.Add(item0);
            var item1 = new VoListItem { Id = 1, Nombre = "PERDIDA DE FUNCIONALIDAD" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "DATOS INCORRECTOS" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "DIFICULTADES DE LENTITUD" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "PROBLEMAS DE DISPONIBILIDAD" }; lst.Add(item4);
            var item5 = new VoListItem { Id = 5, Nombre = "SUGERENCIA DE MEJORA" }; lst.Add(item5);
            var item6 = new VoListItem { Id = 6, Nombre = "OTROS" }; lst.Add(item6);
            this.ddlTipo.DataSource = lst;
            this.ddlTipo.DataTextField = "Nombre";
            this.ddlTipo.DataValueField = "Id";
            this.ddlTipo.DataBind();

            int iCount = this.ddlTipo.Items.Count;
            this.ddlTipo.Items.Add("Seleccione");
            this.ddlTipo.SelectedIndex = iCount;
        }

        private void LimpiarFormulario()
        {
            this.fNomApe.Value = "";
            this.fEmail.Value = "";
            this.fTelef.Value = "";
            this.fEstablecimiento.Value = "";
            this.fTitulo.Value = "";
            this.fComentario.Value = "";

            this.PreCargarFormulario();
        }

        protected void btn_EnviarFormulario(object sender, EventArgs e)
        {
            try
            {
                 var caso = new CasoSoporte
                {
                    Establecimiento = this.fEstablecimiento.Value,
                    Nickname = "Usuario_Test",
                    NombreApellido = this.fNomApe.Value,
                    Telefono = this.fTelef.Value,
                    Email = this.fEmail.Value,
                    Titulo = this.fTitulo.Value,
                    Tipo = this.ddlTipo.SelectedValue.ToString(),
                    Descripcion = this.fComentario.Value,
                    //Adjunto = 
                };

                if (Fachada.Instance.EnviarCasoSoporte(caso))
                {
                    this.lblStatus.Text = "Los datos se han guardado con éxito";
                    this.LimpiarFormulario();
                }
                else
                {
                    this.lblStatus.Text = "Los datos no se han podido guardar";
                }
            }
            catch (SqlException exc)
            {
                
            }
            
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }
    }
}