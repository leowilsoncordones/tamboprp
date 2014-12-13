using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class NuevoEvento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.CargarDdlTipoEvento();
                this.CargarDdlCalificacionLetras();
                this.CargarDdlDiagnostico();
                this.CargarDdlMotivoSecado();
                this.CargarDdlCategConcurso();
                this.LimpiarFormulario();
                this.PrepararFormulario();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Producción", "Produccion.aspx"));
            list.Add(new VoListItemDuplaString("Nuevo Evento", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void SetBreadcrumbs(List<VoListItemDuplaString> list)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='breadcrumb'>");
            sb.Append("<li><i class='ace-icon fa fa-home home-icon'></i> <a href='Default.aspx'>Home</a></li>");
            // cargo list items recorriendo la lista
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append("<li><a href='" + list[i].Valor2 + "'>" + list[i].Valor1 + "</a></li>");
            }
            sb.Append("</ul>");

            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml += sb.ToString();
            }
        }

        private List<Empleado> GetInseminadores()
        {
            return Fachada.Instance.GetInseminadores();
        }

        private void PrepararFormulario()
        {
            this.pnlCalif.Visible = false;
            this.pnlAborto.Visible = false;
            this.pnlBajas.Visible = false;
            this.pnlConcurso.Visible = false;
            this.pnlControles.Visible = false;
            this.pnlDiagnostico.Visible = false;
            this.pnlSecado.Visible = false;
            this.pnlServicio.Visible = false;
        }

        protected void btn_GuardarEvento(object sender, EventArgs e)
        {
            this.lblVer.Text = this.fRegistro.Value + " " + this.mydate.Value + " " + this.fComentario.Value;

            var celo = new Celo_Sin_Servicio();
            celo.Registro = this.fRegistro.Value.ToString();
            string strDate = this.mydate.Value.ToString();
            if (strDate != string.Empty) celo.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            celo.Comentarios = this.fComentario.Value.ToString();

            bool ret = Fachada.Instance.CeloSinServicioInsert(celo);
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fRegistro.Value = "";
            //this.mydate.Value = "";
            this.fComentario.Value = "";
            this.fRegistroServ.Value = "";
            this.fControl.Value = "";
            this.fGrasa.Value = "";
            this.fLeche.Value = "";
            this.fEnfermedad.Value = "";
            this.fRegPadre.Value = "";
            this.fInseminador.Value = "";
            this.fPremio.Value = "";
            this.fRegistro.Attributes.Remove("disabled");
            this.mydate.Attributes.Remove("disabled");
            this.fComentario.Attributes.Remove("disabled");
            this.fEnfermedad.Attributes.Remove("disabled");

            this.lblVer.Text = "";
        }

        private void CargarDdlTipoEvento()
        {
            var lst = new List<TipoEvento>();
            lst = Fachada.Instance.GetTipoEventosAnimal();
            this.ddlEvento.DataSource = lst;
            this.ddlEvento.DataTextField = "Nombre";
            this.ddlEvento.DataValueField = "Id";
            this.ddlEvento.DataBind();
        }
        private void CargarDdlCalificacionLetras()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "EX" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "MB" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "BM" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "B" }; lst.Add(item4);
            this.ddlCalificacion.DataSource = lst;
            this.ddlCalificacion.DataTextField = "Nombre";
            this.ddlCalificacion.DataValueField = "Id";
            this.ddlCalificacion.DataBind();

            this.ddlCalificacion.SelectedValue = "EX";
            this.CargarDdlCalificacionPuntos();
        }

        protected void ddlCalif_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CargarDdlCalificacionPuntos();
        }


        private void CargarDdlCalificacionPuntos()
        {
            var lst = new List<VoListItem>();
            int num;
            switch (int.Parse(this.ddlCalificacion.SelectedValue))
            {
                case 1:
                    for (int i = 0; i <= 10; i++)
                    {
                        num = 100 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case 2:
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 90 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case 3:
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 85 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case 4:
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 80 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
            }

            this.ddlCalificacionPts.DataSource = lst;
            this.ddlCalificacionPts.DataTextField = "Nombre";
            this.ddlCalificacionPts.DataValueField = "Id";
            this.ddlCalificacionPts.DataBind();
        }

        private void CargarDdlDiagnostico()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "PREÑADA" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "DUDOSA" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "VACIA" }; lst.Add(item3);

            this.ddlDiagnostico.DataSource = lst;
            this.ddlDiagnostico.DataTextField = "Nombre";
            this.ddlDiagnostico.DataValueField = "Id";
            this.ddlDiagnostico.DataBind();
            this.ddlDiagnostico.SelectedValue = "Preñada";
        }

        private void CargarDdlMotivoSecado()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "LACTANCIA COMPLETA" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "RAZONES SANITARIAS" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "BAJA PRODUCCION" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "PREÑEZ AVANZADA" }; lst.Add(item4);

            this.ddlMotivoSec.DataSource = lst;
            this.ddlMotivoSec.DataTextField = "Nombre";
            this.ddlMotivoSec.DataValueField = "Id";
            this.ddlMotivoSec.DataBind();
        }


        private void CargarDdlCategConcurso()
        {
            var lst = new List<CategoriaConcurso>();
            lst = Fachada.Instance.GetCategoriasConcurso();
            this.ddlCategConcurso.DataSource = lst;
            this.ddlCategConcurso.DataTextField = "Nombre";
            this.ddlCategConcurso.DataValueField = "Id_categ";
            this.ddlCategConcurso.DataBind();
        }

        protected void ddlMotivoSec_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (int.Parse(this.ddlMotivoSec.SelectedValue) == 2)
                this.fEnfermedad.Attributes.Remove("disabled");
            else this.fEnfermedad.Attributes.Add("disabled", "disabled");
        }


        protected void ddlEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
            this.pnlAborto.Visible = false;
            this.pnlBajas.Visible = false;
            this.pnlCalif.Visible = false;
            this.pnlControles.Visible = false;
            this.pnlDiagnostico.Visible = false;
            this.pnlSecado.Visible = false;
            this.pnlServicio.Visible = false;
            this.pnlConcurso.Visible = false;

            switch (int.Parse(this.ddlEvento.SelectedValue))
            {
                case 0: // ABORTO
                    this.pnlAborto.Visible = true;
                    break;
                case 1: // PARTO
                    break;
                case 2: // CELO SIN SERVICIO
                    break;
                case 3: // SERVICIO
                    var lst = this.GetInseminadores();
                    this.pnlServicio.Visible = true;
                    break;
                case 4: // SECADO
                    this.fEnfermedad.Attributes.Add("disabled", "disabled");
                    this.pnlSecado.Visible = true;
                    this.pnlBajas.Visible = true;
                    break;
                case 5: // CONTROL SANITARIO (YA NO SE USA)
                    this.fRegistro.Attributes.Add("disabled", "disabled");
                    this.mydate.Attributes.Add("disabled", "disabled");
                    this.fComentario.Attributes.Add("disabled", "disabled");
                    break;
                case 6: // C.M.T. (YA NO SE USA)
                    this.fRegistro.Attributes.Add("disabled", "disabled");
                    this.mydate.Attributes.Add("disabled", "disabled");
                    this.fComentario.Attributes.Add("disabled", "disabled");
                    break;
                case 7: // DIAGNOSTICO DE PRENEZ
                    this.pnlDiagnostico.Visible = true;
                    break;
                case 8: // CONTROL DE PRODUCCION
                    this.pnlControles.Visible = true;
                    break;
                case 9: // CALIFICACION
                    this.pnlCalif.Visible = true;
                    break;
                case 10: // CONCURSO
                    this.pnlConcurso.Visible = true;
                    break;
                case 11: // BAJA POR VENTA
                    this.pnlBajas.Visible = true;
                    break;
                case 12: // BAJA POR MUERTE
                    this.pnlBajas.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
}