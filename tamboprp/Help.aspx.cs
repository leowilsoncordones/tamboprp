using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        //private string pestanaActiva = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            //{
                //selected_tab.Value = Request.Form[selected_tab.UniqueID];
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.CargarFaqs();
                    this.CargarDdlTipoProblema();
                    //this.LimpiarFormulario();
                    this.PreCargarFormulario();
                }
                //this.lblTabValue.Text = pestanaActiva;
            //}
            //else Response.Redirect("~/Default.aspx", true);
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
            var u = (VOUsuario)Session["Usuario"];
            if (u != null)
            {
                this.fNomApe.Value = u.Nombre + " " + u.Apellido;
                this.fEmail.Value = u.Email;
            }
        }

        private void CargarDdlTipoProblema()
        {
            var lst = new List<VoListItem>();
            var item0 = new VoListItem { Id = 0, Nombre = "Seleccione" }; lst.Add(item0);
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

            //int iCount = this.ddlTipo.Items.Count;
            //this.ddlTipo.Items.Add("Seleccione");
            this.ddlTipo.SelectedIndex = 0;
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
            this.PreCargarFormulario();
            var u = (VOUsuario)Session["Usuario"];
            var caso = new VOCaso();
            caso.Establecimiento = this.fEstablecimiento.Value;
            if (u != null) caso.Nickname = u.Nickname;
            caso.NombreApellido = this.fNomApe.Value;
            caso.Telefono = this.fTelef.Value;
            caso.Email = this.fEmail.Value;
            caso.Titulo = this.fTitulo.Value;
            //caso.Tipo = this.ddlTipo.SelectedValue.ToString();
            caso.Tipo = this.ddlTipo.SelectedItem.ToString();
            caso.Descripcion = this.fComentario.Value;

            if (Fachada.Instance.EnviarCasoSoporte(caso))
            {
                this.lblStatus.Text = "Los datos se han enviado con éxito";
                this.LimpiarFormulario();
            }
            else
            {
                this.lblStatus.Text = "Los datos no se han podido enviar";
            }
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        public void CargarFaqs()
        {
            this.faqList.InnerHtml = "";
            var lstFaq = Fachada.Instance.GetAllFaqs();
            var sb = new StringBuilder();
            for (int i = 0; i < lstFaq.Count; i++)
            {
                var voF = lstFaq[i];
                string anchor = "faq-1-" + voF.Id.ToString();
                sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                        sb.Append("<a href='#"+ anchor + "' data-parent='#faq-list-1' data-toggle='collapse' class='accordion-toggle collapsed' aria-expanded='false'>");
                            sb.Append("<i class='ace-icon fa fa-chevron-left pull-right' data-icon-hide='ace-icon fa fa-chevron-down' data-icon-show='ace-icon fa fa-chevron-left'></i>");
                            sb.Append("<i class='ace-icon fa " + voF.Icono + " bigger-130'></i>");
                            sb.Append("&nbsp; " + voF.Pregunta);
                        sb.Append("</a>");
                    sb.Append("</div>");
                    sb.Append("<div class='panel-collapse collapse' id='" + anchor + "' aria-expanded='false'>");
                        sb.Append("<div class='panel-body'>");
                            sb.Append(voF.Respuesta);
                        sb.Append("</div>");
                    sb.Append("</div>");
                sb.Append("</div>");
            }
            this.faqList.InnerHtml = sb.ToString();
        }
    }
}