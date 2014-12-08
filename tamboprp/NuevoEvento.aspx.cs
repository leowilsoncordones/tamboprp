using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                this.CargarDdlTipoEvento();
                this.CargarDdlCalificacionLetras();
                this.CargarDdlDiagnostico();
                this.CargarDdlMotivoSecado();
                this.CargarDdlCategConcurso();
                this.LimpiarFormulario();
                this.PrepararFormulario();
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
            this.lblVer.Text = this.fRegistro.Value + " " + this.datepicker.Value + " " + this.fComentario.Value;

            var celo = new Celo_Sin_Servicio();
            celo.Registro = this.fRegistro.Value.ToString();
            string strDate = this.datepicker.Value.ToString();
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
            this.datepicker.Value = "";
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
            this.datepicker.Attributes.Remove("disabled");
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
                    this.datepicker.Attributes.Add("disabled", "disabled");
                    this.fComentario.Attributes.Add("disabled", "disabled");
                    break;
                case 6: // C.M.T. (YA NO SE USA)
                    this.fRegistro.Attributes.Add("disabled", "disabled");
                    this.datepicker.Attributes.Add("disabled", "disabled");
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