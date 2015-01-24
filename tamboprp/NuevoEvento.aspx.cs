using System;
using System.Collections.Generic;
using System.Data;
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
                this.CargarDdlNomConcurso();
                this.LimpiarFormulario();
                this.PrepararFormulario();
                //this.OcultarInputs();
                this.lblRegistro.Visible = false;

            }
           if(this.ddlEvento.SelectedIndex != 13) CargarListaRegistrosParaTypeahead(int.Parse(this.ddlEvento.SelectedValue));
            
        }

        private static string _dato="";
        private static string _datoAbortoRegistro = "";
        private static string _datoAbortoRegistroPadre = "";
        private static List<VoListItem>  _listaRegistrosTypeahead = new List<VoListItem>();
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
            var guardarEvento = GuardarEvento();
            if (guardarEvento != null)
            {
                if ((bool) guardarEvento)
                {
                    this.lblStatus.Text = "Evento ingresado correctamente";
                    this.lblStatus.Visible = true;
                }
                else
                {
                    this.lblStatus.Text = "No se pudo ingresar el evento";
                    this.lblStatus.Visible = true;
                }
            }
            else
            {
                this.lblStatus.Text = "Debe seleccionar un evento para ingresar";
                this.lblStatus.Visible = true;
            }
        }


        private bool? GuardarEvento()
        {
        string strDate = Request.Form["mydate"];

            if (ddlEvento.SelectedIndex == 13)
            {              
                return null;
            }

            switch (int.Parse(this.ddlEvento.SelectedValue))
            {
                case 0: // ABORTO
                    return GuardarAborto();
                case 1: // PARTO
                    break;
                case 2: // CELO SIN SERVICIO
                    try
                    {
                        return GuardarCeloSinServicio();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 3: // SERVICIO
                    try
                    {
                        return GuardarServicio();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 4: // SECADO
                    try
                    {
                        return GuardarSecado();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 5: // CONTROL SANITARIO (YA NO SE USA)
                    break;
                case 6: // C.M.T. (YA NO SE USA)
                    break;
                case 7: // DIAGNOSTICO DE PRENEZ
                    try
                    {
                        return GuardarDiagPrenez();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 8: // CONTROL DE PRODUCCION
                    return GuardarControlProduccion();
                case 9: // CALIFICACION
                    try
                    {
                        return GuardarCalificacion();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 10: // CONCURSO
                    try
                    {
                        return GuardarConcurso();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 11: // BAJA POR VENTA
                    try
                    {
                        return GuardarVenta();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                case 12: // BAJA POR MUERTE
                    try
                    {
                        return GuardarMuerte();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                default:
                    return false;
            }


            return false;
        }


        private void CheckDatosRegAborto()
        {
            if (!CheckAnimalConDiagPrenezActual(fRegistro.Text))
            {
                lblRegistro.InnerText = "Este registro no tiene preñez confirmada";
                this.lblRegistro.Visible = true;
            }
            else
            {
                var padre = GetAbortoServicioPadre(fRegistro.Text);
                fRegistroServ.Value = padre;
                this.lblRegistro.Visible = false;
            }
        }


        protected void EventosRegistro(object sender, EventArgs e)
        {
            if (ddlEvento.SelectedIndex != 13)
            {                
            switch (int.Parse(this.ddlEvento.SelectedValue))
            {
                case 0: // ABORTO
                    CheckDatosRegAborto();
                    break;
                case 1: // PARTO
                    break;
                case 2: // CELO SIN SERVICIO
                    break;
                case 3: // SERVICIO
                    break;
                case 4: // SECADO
                    break;
                case 5: // CONTROL SANITARIO (YA NO SE USA)
                    break;
                case 6: // C.M.T. (YA NO SE USA)
                    break;
                case 7: // DIAGNOSTICO DE PRENEZ
                    break;
                case 8: // CONTROL DE PRODUCCION
                    break;
                case 9: // CALIFICACION
                    break;
                case 10: // CONCURSO
                    break;
                case 11: // BAJA POR VENTA
                    break;
                case 12: // BAJA POR MUERTE
                    break;
                default:
                    break;
            }
            }
        }


        public static bool CheckAnimalConDiagPrenezActual(string registro)
        {
            return Fachada.Instance.CheckAnimalConDiagPrenezActual(registro);
        }

        public static string GetAbortoServicioPadre(string registro)
        {
            return Fachada.Instance.GetAbortoServicioPadre(registro);
        }


        private bool GuardarAborto()
        {
            string strDate = Request.Form["mydate"];
            var fecha = DateTime.Parse(strDate, new CultureInfo("en-US"));
            var aborto = new Aborto
            {
                Id_evento = 0,
                Registro = fRegistro.Text,
                Fecha = fecha,
                Comentarios = fComentario.Value,
                Reg_padre = fRegistroServ.Value
            };
            return Fachada.Instance.InsertarEvento(aborto);
        }





        private bool GuardarCeloSinServicio()
        {
            string strDate = Request.Form["mydate"];
            var celoSinServ = new Celo_Sin_Servicio
            {
                Id_evento = 2,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
            };
            return Fachada.Instance.InsertarEvento(celoSinServ);
        }

        private bool GuardarServicio()
        {
            string strDate = Request.Form["mydate"];
            var idEmple = Request.Form["selectEmpleados"];
            var insemin = new Empleado { Id_empleado = Int16.Parse(idEmple) };
            var monta = checkMontaNat.Checked ? 'S' : 'N';
            var serv = new Servicio
            {
                Id_evento = 3,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Serv_monta_natural = monta,
                Reg_padre = fRegPadre.Value,
                Inseminador = insemin
            };
            return Fachada.Instance.InsertarEvento(serv);
        }

        private bool GuardarSecado()
        {
            string strDate = Request.Form["mydate"];
            string enf = _dato;

            var sec = new Secado
            {
                Id_evento = 4,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Motivos_secado =  Int16.Parse(ddlMotivoSec.SelectedValue),
                Enfermedad = enf == "" ? (short?) null: Int16.Parse(enf) 
            };
            _dato = "";
            return Fachada.Instance.InsertarEvento(sec);
        }

        private bool GuardarDiagPrenez()
        {
            string strDate = Request.Form["mydate"];
            var diagp = new Diag_Prenez
            {
                Id_evento = 7,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Diagnostico = ConvertirDiagnostico(int.Parse(ddlDiagnostico.SelectedValue))
            };
            return Fachada.Instance.InsertarEvento(diagp);
        }

        private bool GuardarControlProduccion()
        {
            string strDate = Request.Form["mydate"];
            var control = new Control_Producc
            {
                Id_evento = 8,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Leche = double.Parse(fLeche.Value),
                Grasa = double.Parse(fGrasa.Value),
            };
            return Fachada.Instance.InsertarEvento(control);
        }

        private bool GuardarCalificacion()
        {
            string strDate = Request.Form["mydate"];
            string letra = Request.Form["selectLetras"];
            string num = Request.Form["selectNumeros"];
            var calif = new Calificacion
            {
                Id_evento = 9,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Letras = letra,
                Puntos = Int32.Parse(num)
            };
            return Fachada.Instance.InsertarEvento(calif);
        }

        private bool GuardarConcurso()
        {
            string strDate = Request.Form["mydate"];
            var cat = new CategoriaConcurso {Id_categ = Int16.Parse(ddlCategConcurso.SelectedValue)};
            var lugconc = new LugarConcurso {Id = int.Parse(ddlNomConcurso.SelectedValue)};
            var concurso = new Concurso
            {
                Id_evento = 10,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                ElPremio = fPremio.Value,
                NombreLugarConcurso = lugconc,
                Categoria = cat
            };
            return Fachada.Instance.InsertarEvento(concurso);
        }

        private bool GuardarVenta()
        {
            string strDate = Request.Form["mydate"];
            string enf = _dato == "" ? null : _dato;

            var venta = new Venta
            {
                Id_evento = 11,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Enfermedad = enf != null ? Int16.Parse(enf) : (short?) null

            };
            _dato = "";
            return Fachada.Instance.InsertarEvento(venta);
        }

        private bool GuardarMuerte()
        {
            string strDate = Request.Form["mydate"];
            string enf = _dato;
            var muerte = new Muerte
            {
                Id_evento = 12,
                Registro = fRegistro.Text,
                Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR")),
                Comentarios = fComentario.Value,
                Enfermedad = Int16.Parse(enf)
            };
            _dato = "";
            return Fachada.Instance.InsertarEvento(muerte);
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        //private void OcultarInputs()
        //{
        //    divInputAborto.Visible = false;
        //    divInputBajas.Visible = false;
        //    divInputCalificaciones.Visible = false;
        //    divInputConcurso.Visible = false;
        //    divInputControles.Visible = false;
        //    divInputDiagnostico.Visible = false;
        //    divInputSecado.Visible = false;
        //    divInputServicio.Visible = false;
        //}

        public void CargarListaRegistrosParaTypeahead(int idEvento)
        {
            switch (idEvento)
            {
                case 0:
                    _listaRegistrosTypeahead = Fachada.Instance.GetAbortosAnimalesConServicios();
                    ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadAbortos", "cargarTypeaheadAbortos();", true); 
                break;

                case 3: // SERVICIO
                ScriptManager.RegisterStartupScript(this, GetType(), "cargarSelectEmpleados", "cargarSelectEmpleados();", true);
                break;

                case 4: // SECADO
                ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadEnfermedades", "cargarTypeaheadEnfermedades();", true);
                break;

                case 9:
                _listaRegistrosTypeahead = Fachada.Instance.GetAbortosAnimalesConServicios();
                //ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadCategorias", "cargarTypeaheadCategorias();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "cargarSelectLetras", "cargarSelectLetras();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "cargaSelectNumeros", "cargaSelectNumeros();", true);
                break;

                case 11: // BAJA POR VENTA
                ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadEnfermedades", "cargarTypeaheadEnfermedades();", true);
                break;

                case 12: // BAJA POR MUERTE
                ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadEnfermedades", "cargarTypeaheadEnfermedades();", true);
                break;
            }
        }

        private void LimpiarFormulario()
        {
            this.fRegistro.Text = "";
            //this.mydate.Value = "";
            this.fComentario.Value = "";
            this.fRegistroServ.Value = "";
            this.fControl.Value = "";
            this.fGrasa.Value = "";
            this.fLeche.Value = "";
            //this.fEnfermedad.Value = "";
            this.fRegPadre.Value = "";
            //this.fInseminador.Value = "";
            this.fPremio.Value = "";
            //this.fRegistro.Attributes.Remove("disabled");
            //this.mydate.Attributes.Remove("disabled");
            this.fComentario.Attributes.Remove("disabled");
           // this.fEnfermedad.Attributes.Remove("disabled");
            this.lblStatus.Text = "";
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
            int iCount = this.ddlEvento.Items.Count;
            this.ddlEvento.Items.Add("Seleccione");
            this.ddlEvento.SelectedIndex = iCount;
        }
        private void CargarDdlCalificacionLetras()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "EX" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "MB" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "BM" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "B" }; lst.Add(item4);
            var item5 = new VoListItem { Id = 5, Nombre = "R" }; lst.Add(item5);
            this.ddlCalificacion.DataSource = lst;
            this.ddlCalificacion.DataTextField = "Nombre";
            this.ddlCalificacion.DataValueField = "Nombre";
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
            switch ((this.ddlCalificacion.SelectedValue))
            {
                case "EX":
                    for (int i = 0; i <= 10; i++)
                    {
                        num = 100 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case "MB":
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 90 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case "BM":
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 85 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case "B":
                    for (int i = 1; i <= 5; i++)
                    {
                        num = 80 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
                case "R":
                    for (int i = 1; i <= 6; i++)
                    {
                        num = 76 - i;
                        var item = new VoListItem { Id = i, Nombre = num.ToString() };
                        lst.Add(item);
                    }
                    break;
            }

            this.ddlCalificacionPts.DataSource = lst;
            this.ddlCalificacionPts.DataTextField = "Nombre";
            this.ddlCalificacionPts.DataValueField = "Nombre";
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

        private char ConvertirDiagnostico(int id)
        {
            if (id == 1) return 'P';
            if (id == 2) return 'D';
            if (id == 3) return 'V';
            else return 'X';
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

        private void CargarDdlNomConcurso()
        {
            var lst = Fachada.Instance.GetLugaresConcurso();
            var lista = new List<VoListItem>();
            foreach (var lg in lst)
            {
                var item = new VoListItem();
                item.Id = lg.Id;
                item.Nombre = lg.NombreExpo + " / " + lg.Lugar;
                lista.Add(item);
            }
            ddlNomConcurso.DataSource = lista;
            ddlNomConcurso.DataTextField = "Nombre";
            ddlNomConcurso.DataValueField = "Id";
            ddlNomConcurso.DataBind();
        }

        protected void ddlMotivoSec_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (int.Parse(this.ddlMotivoSec.SelectedValue) == 2)
            //    this.fEnfermedad.Attributes.Remove("disabled");
            //else this.fEnfermedad.Attributes.Add("disabled", "disabled");
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
            this.lblRegistro.Visible = false;
            //this.OcultarInputs();

            if (ddlEvento.SelectedIndex != 13) { 

            switch (int.Parse(this.ddlEvento.SelectedValue))
            {
                case 0: // ABORTO
                    this.pnlAborto.Visible = true;
                    //this.divInputAborto.Visible = true;
                    CargarListaRegistrosParaTypeahead(0);
                    
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
                    //this.fEnfermedad.Attributes.Add("disabled", "disabled");
                    this.pnlSecado.Visible = true;
                    this.pnlBajas.Visible = true;
                    break;
                case 5: // CONTROL SANITARIO (YA NO SE USA)
                   // this.fRegistro.Attributes.Add("disabled", "disabled");
                    //this.mydate.Attributes.Add("disabled", "disabled");
                    this.fComentario.Attributes.Add("disabled", "disabled");
                    break;
                case 6: // C.M.T. (YA NO SE USA)
                    //this.fRegistro.Attributes.Add("disabled", "disabled");
                    //this.mydate.Attributes.Add("disabled", "disabled");
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
                    //this.divInputCalificaciones.Visible = true;
                    this.fComentario.Attributes.Add("disabled", "disabled");
                    CargarListaRegistrosParaTypeahead(9);
                    break;
                case 10: // CONCURSO
                    this.pnlConcurso.Visible = true;
                    break;
                case 11: // BAJA POR VENTA                   
                    this.pnlBajas.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadEnfermedades", "cargarTypeaheadEnfermedades();", true);
                    break;
                case 12: // BAJA POR MUERTE
                    this.pnlBajas.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "cargarTypeaheadEnfermedades", "cargarTypeaheadEnfermedades();", true);
                    break;
                default:
                    break;
            }
            }
        }

        [WebMethod]
        public static List<Enfermedad> GetEnfermedades()
        {
            return Fachada.Instance.GetEnfermedades();
        }

        [WebMethod]
        public static string RecibirDato(dynamic dato)
        {
            _dato = dato;
            return dato;
        }

        [WebMethod]
        public static List<Empleado> GetListaEmpleados()
        {
            return Fachada.Instance.GetInseminadores();
        }

        //[WebMethod]
        //public static List<VoListItem> GetAbortosAnimalesConServicios()
        //{
        //    return _listaRegistrosTypeahead;
        //}

        //[WebMethod]
        //public static string RecibirDatoAbortoRegistro(dynamic dato)
        //{         
        //    _datoAbortoRegistro = dato.Replace("\"", "");
        //    var padre = Fachada.Instance.GetAbortoServicioPadre(_datoAbortoRegistro);
        //    _datoAbortoRegistroPadre = padre;
        //    return padre;
        //}


    }
}