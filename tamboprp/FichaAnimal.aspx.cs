using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Datos;
using Negocio;
using Entidades;


namespace tamboprp
{
    public partial class FichaAnimal : System.Web.UI.Page
    {
        private VOAnimal _animal;
        private List<VOAnimal> _similares = new List<VOAnimal>();
        private static string _reg = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarRegistro();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Ficha de Animal", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarFichaAnimal()
        {
            if (_animal != null)
            {
                _reg = regBuscar.Value;
                this.lblAnimal.Text = _animal.Registro;
                this.lblRegistroModal.Text = _animal.Registro;
                this.lblRegistroModalFotos.Text = _animal.Registro;
                this.lblRegistroModalModificar.Text = _animal.Registro;
                this.lblRegistroModalSubirFoto.Text = _animal.Registro;
                this.lblIdentif.Text = _animal.Identificacion;
                this.lblGen.Text = (_animal.Gen != -1) ? _animal.Gen.ToString() : "";
                //Categoria catAnimal = Fachada.Instance.GetCategoriaById(_animal.IdCategoria);
                //Categoria catAnimal = _animal.Categoria;
                this.panelFicha.CssClass = "panel panel-default";
                if (_animal.Categoria != "")
                {
                    // doy estilo a la label de categoria segun la misma
                    switch (_animal.IdCategoria)
                    {
                        case 1: // Ternera
                        {
                            this.lblCategoria.CssClass = "label label-default arrowed";
                            break;
                        }
                        case 2: // Vaquillona
                        {
                            this.lblCategoria.CssClass = "label label-pink arrowed";
                            break;
                        }
                        case 3: // Vaquillona entorada
                        {
                            this.lblCategoria.CssClass = "label label-yellow arrowed";
                            break;
                        }
                        case 4: // En ordeñe
                        {
                            this.lblCategoria.CssClass = "label label-success arrowed";
                            break;
                        }
                        case 5: // Seca
                        {
                            this.lblCategoria.CssClass = "label label-warning arrowed";
                            break;
                        }
                        case 6: // Descarte
                        {
                            this.lblCategoria.CssClass = "label label-grey arrowed";
                            break;
                        }
                        case 7: // Ternero
                        {
                            this.lblCategoria.CssClass = "label label-default arrowed";
                            break;
                        }
                        case 8: // Toro
                        {
                            this.lblCategoria.CssClass = "label label-inverse arrowed";
                            break;
                        }
                        case 9: // Eliminada
                        {
                            this.lblCategoria.CssClass = "label label-grey arrowed";
                            break;
                        }
                        case 10: // Semen
                        {
                            this.lblCategoria.CssClass = "label label-purple arrowed";
                            break;
                        }
                        default:
                        {
                            this.lblCategoria.CssClass = "label label-default arrowed";
                            break;
                        }
                    }
                    //this.titFooterPanel.Visible = true;
                    this.lblFooterPanel.Visible = true;
                    this.lblFooterPanel.Text = _animal.Registro;
                    this.lblFooterPanel.CssClass = this.lblCategoria.CssClass;
                    this.lblCategoria.Text = _animal.Categoria;
                }
                // nombre
                if (_animal.Nombre != "")
                {
                    this.lblNombre.Visible = true;
                    this.lblNombre.Text = _animal.Nombre;
                }
                // registro de trazabilidad
                if (_animal.Reg_trazab != "")
                {
                    this.titTraz.Visible = true;
                    this.lblTraz.Visible = true;
                    this.lblTraz.Text = _animal.Reg_trazab;
                }
                else
                {
                    this.titTraz.Visible = false;
                }
                // sexo
                this.lblSexo.Text = _animal.Sexo.ToString();
                // fecha de nacimiento
                this.lblFechaNac.Text = _animal.Fecha_nacim.ToShortDateString();
                if (_animal.Fecha_nacim != DateTime.MinValue)
                {
                    int[] age = Fachada.Instance.CalcularEdadYMD(_animal.Fecha_nacim);
                    //int y = DateTime.Now.Year - _animal.Fecha_nacim.Year;
                    //int m = Math.Abs(DateTime.Now.Month - _animal.Fecha_nacim.Month);
                    string output = age[0] + "a " + age[1] + "m"; 
                    this.lblEdad.Text = output;
                }
                // origen, registro de padre y madre
                this.lblOrigen.Text = _animal.Origen;
                this.lblRegPadre.Text = _animal.Reg_padre;
                this.lblRegMadre.Text = _animal.Reg_madre;
                // estado del animal, vivo y baja por muerte y venta
                _animal.Vivo = !Fachada.Instance.EstaMuertoAnimal(_animal.Registro);
                if (!_animal.Vivo)
                {
                    this.lblEstado.Text = "MUERTO";
                    this.lblEstado.Visible = true;
                    this.lblEstado.CssClass = "label label-danger arrowed";
                    //this.panelFicha.CssClass = "panel panel-danger";
                }
                else
                {
                    if (Fachada.Instance.FueVendidoAnimal(_animal.Registro))
                    {
                        this.lblEstado.Text = "VENDIDO";
                        this.lblEstado.CssClass = "label label-info arrowed";
                    }
                    else
                    {
                        this.lblEstado.Text = "VIVO";
                        this.lblEstado.CssClass = "label label-success arrowed";
                    }
                    this.lblEstado.Visible = true;
                }
                // si es hembra cargo la ficha correspondiente a hembras
                if (_animal.esHembra())
                {
                    this.cboxControles.Enabled = true;
                    this.CargarFichaHembra();
                }
                // eventos del animal al historico
                this.ProcesarEventosAnimal();
                //if (_animal.Fotos != null) this.CargarFotosAnimal(_animal.Fotos, this.ULFotos);
                this.CargarFotosDelAnimal();
            }
            else this.lblAnimal.Text = "No existe :(";
        }

        
        private void CargarFotosDelAnimal()
        {
            if (_animal.Fotos != null) 
                this.CargarFotosAnimal(_animal.Fotos, this.ULFotos);
        }

        private void ReCargarFotosDelAnimal()
        {
            if (_animal != null)
            {
                _animal.Fotos = Fachada.Instance.GetFotosAnimal(_animal.Registro);
                this.CargarFotosDelAnimal();
            }
        }

        public void CargarFichaHembra()
        {
            this.phFichaHembra.Visible = true;
        }

        public void ProcesarEventosAnimal()
        {
            if (_animal != null)
            {
                var list = new List<VOEvento>();
                if (_animal.Eventos.Count > 0)
                {
                    for (int i = 0; i < _animal.Eventos.Count; i++)
                    {
                        var voEv = new VOEvento();
                        voEv.Fecha = _animal.Eventos[i].Fecha.ToShortDateString();
                        voEv.NombreEvento = _animal.Eventos[i].Nombre;
                        voEv.Observaciones = _animal.Eventos[i].ToString();
                        voEv.Comentarios = _animal.Eventos[i].Comentarios;
                        list.Add(voEv);
                        if (_animal.Eventos[i].Id_evento == 9)
                        {
                            var califUlt = (Calificacion) _animal.Eventos[i];
                            _animal.Calific = califUlt.Letras + " " + califUlt.Puntos;
                            this.titCalif.Visible = true;
                            this.lblCalif.Visible = true;
                            this.lblCalif.Text = _animal.Calific;
                        }
                    }
                    //this.gvHistoria.DataSource = list;
                    //this.gvHistoria.DataBind();
                    this.ProcesarFichaHembra(_animal.Eventos);
                    this.BindearEventosAnimalGridView(list);
                }
                this.phHistorial.Visible = true;
                this.gvHistoria.Visible = true;
                //this.titCantEventos.Visible = true;
                this.lblCantEventos.Text = _animal.Eventos.Count.ToString();
            }
        }

        private void ProcesarFichaHembra(List<Evento> list)
        {
            var partosM = 0;
            var partosH = 0;
            var varFechaUltParto = "";
            var varCantServicios = 0;
            var varCantCeloSinServicio = 0;
            var varRegUltimoServicio = "";
            var varFechaUltServicio = "";
            var varControles = 0;
            double varLecheUltControl = 0;
            double varProdLeche = 0;
            double varProdGrasa = 0;
            var varCantDias = 0;
            var varMotivoUltSecado = "";
            var varFechaUltSecado = "";
            var varUltDiag = "";
            bool estaCalificada = false;

            for (int i = 0; i < list.Count; i++)
            {
                list.Sort();
                // actualizo las variables segun el tipo de evento
                switch (list[i].Id_evento)
                {
                    case 1: // partos
                    {
                        var pa = (Parto)list[i];
                        if (pa.Sexo_parto == 'M') partosM++;
                        else partosH++;
                        varFechaUltParto = pa.Fecha.ToShortDateString();
                        //string strDate = pa.Fecha.ToShortDateString();
                        //if (strDate != string.Empty) varFechaUltParto = DateTime.Parse(strDate, new CultureInfo("fr-FR")).ToShortDateString();
                        break;
                    }
                    case 2: // celos sin servicio
                    {
                        varCantCeloSinServicio++;
                        break;
                    }
                    case 3: // servicios
                    {
                        varCantServicios++;
                        var sv = (Servicio)list[i];
                        varRegUltimoServicio = sv.Reg_padre;
                        varFechaUltServicio = sv.Fecha.ToShortDateString();
                        //string strDate = sv.Fecha.ToShortDateString();
                        //if (strDate != string.Empty) varFechaUltServicio = DateTime.Parse(strDate, new CultureInfo("fr-FR")).ToShortDateString();
                        break;
                    }
                    case 4: // secados
                    {
                        var sec = (Secado)list[i];
                        // traigo el nombre del motivo de secado con id dado
                        varMotivoUltSecado = sec.MotivoSecado;
                        varFechaUltSecado = sec.Fecha.ToShortDateString();
                        //string strDate = sec.Fecha.ToShortDateString();
                        //if (strDate != string.Empty) varFechaUltSecado = DateTime.Parse(strDate, new CultureInfo("fr-FR")).ToShortDateString();
                        break;
                    }
                    case 7: // diagnosticos
                    {
                        var di = (Diag_Prenez)list[i];
                        varUltDiag = di.Diagnostico.ToString();
                        break;
                    }
                    case 8: // controles
                    {
                        varControles++;
                        var cp = (Control_Producc) list[i];
                        varProdLeche += cp.Leche * cp.Dias_para_control;
                        varProdGrasa += cp.Grasa * cp.Dias_para_control;
                        varCantDias += cp.Dias_para_control;
                        varLecheUltControl = cp.Leche;
                        break;
                    }
                    case 9: // calificacion
                    {
                        estaCalificada = true;
                        var califUlt = (Calificacion) list[i];
                        _animal.Calific = califUlt.Letras + " " + califUlt.Puntos;
                        break;
                    }
                    case 10: // concursos
                    {
                        break;
                    }
                    case 11: // baja por venta
                    {
                        break;
                    }
                    case 12: // baja por muerte
                    {
                        //var bajaMuerte = (Baja)list[i];
                        break;
                    }
                }
            }

            this.lblServicios.Text = varCantServicios.ToString();
            this.lblRegServicio.Text = varRegUltimoServicio;
            this.lblFechaUltServ.Text = varFechaUltServicio;
            this.lblFechaUltParto.Text = varFechaUltParto;

            if (partosH > 0)
            {
                this.lblH.Text = partosH.ToString();
                this.lblH.Visible = true;
            }
            if (partosM > 0)
            {
                this.lblM.Text = partosM.ToString();
                this.lblM.Visible = true;
            }
            
            this.lblControles.Text = varControles.ToString();
            this.lblLecheUltControl.Text = varLecheUltControl.ToString() + " lts";
            if (varProdLeche >= 50000)
            {
                // es "vitalicia"
                this.lblProdLeche.CssClass = "label label-yellow arrowed bolder";
            }
            this.lblProdLeche.Text = varProdLeche.ToString(); // NO DA IGUAL QUE CUANDO SUMAMOS LAS LACTANCIAS (VER VITALICIAS)
            this.lblProdGrasa.Text = varProdGrasa.ToString();
            this.lblAvgGrasa.Text = (varCantDias.Equals(0)) ? "0" : Math.Round(varProdGrasa / varCantDias, 2).ToString();
            this.lblMotivoSecado.Text = varMotivoUltSecado;
            this.lblFechaSecado.Text = varFechaUltSecado;
            this.lblDiag.Text = varUltDiag;
            if (estaCalificada)
            {
                this.titCalif.Visible = true;
                this.lblCalif.Visible = true;
                this.lblCalif.Text = _animal.Calific;
            }

            // cargo y proceso las lactancias
            var lst_lact = Fachada.Instance.GetLactanciaByRegistro(_animal.Registro);
            int maxLact = 0;
            int maxInd = 0;
            var diasLact = 0;
            var prodLecheUlt = 0.0;
            var prodGrasaUlt = 0.0;
            var avgGrasaUlt = 0.0;
            int tam = lst_lact.Count;
            if (tam > 0)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (lst_lact[j].Numero > maxLact)
                    {
                        if (lst_lact[j].Dias > 0 && lst_lact[j].ProdLeche > 0)
                        {
                            maxLact = lst_lact[j].Numero;
                            maxInd = j;
                        }
                    }
                }
                diasLact = lst_lact[maxInd].Dias;
                prodLecheUlt = lst_lact[maxInd].ProdLeche;
                prodGrasaUlt = lst_lact[maxInd].ProdGrasa;
                if (lst_lact[maxInd].ProdLeche > 0)
                    avgGrasaUlt = Math.Round(lst_lact[maxInd].ProdGrasa/lst_lact[maxInd].ProdLeche*100, 2);
                else avgGrasaUlt = 0;
            }
            // Si esta en ordeñe me traigo la lactancia actual calculada
            // (no esta en la tabla de lactancias consolidadas al momento del secado)
            if (_animal.IdCategoria == 4)
            {
                var lactActual = Fachada.Instance.ConsolidarLactancia(_animal.Registro);
                maxLact = lactActual.Numero;
                diasLact = lactActual.Dias;
                prodLecheUlt = lactActual.ProdLeche;
                prodGrasaUlt = lactActual.ProdGrasa;
                if (lactActual.ProdLeche > 0)
                    avgGrasaUlt = Math.Round(lactActual.ProdGrasa / lactActual.ProdLeche * 100, 2);
                else avgGrasaUlt = 0;
            }

            this.lblDiasLact.Text = diasLact.ToString();
            this.lblNumLact.Text = maxLact.ToString();
            this.lblProdLecheUlt.Text = prodLecheUlt.ToString();
            this.lblProdGrasaUlt.Text = prodGrasaUlt.ToString();
            this.lblAvgGrasaUlt.Text = Math.Round(avgGrasaUlt, 2).ToString();
            
            //GetControlesByRegistro(_animal.Registro);
        }

        public void BindearEventosAnimalGridView(List<VOEvento> list)
        {
            this.gvHistoria.DataSource = list;
            this.gvHistoria.DataBind();
        }

        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            if (this.regBuscar.Value != "")
            {
                this.BuscarAnimal(this.regBuscar.Value);
            }
        }

        private void LimpiarRegistro()
        {
            // reseteo valores ficha de animal
            this.lblAnimal.Text = "Registro";
            this.lblIdentif.Text = "";
            this.lblGen.Text = "";
            this.lblCategoria.Text = "";
            this.lblCategoria.CssClass = "";
            this.lblNombre.Text = "";
            this.lblNombre.Visible = false;
            this.lblRegistroModal.Text = "";
            this.lblRegistroModalFotos.Text = "";
            this.lblRegistroModalModificar.Text = "";
            this.lblRegistroModalSubirFoto.Text = "";
            this.lblTraz.Visible = false;
            this.lblTraz.Text = "";
            this.lblSexo.Text = "";
            this.titCalif.Visible = false;
            this.lblCalif.Text = "";
            this.lblFechaNac.Text = "";
            this.lblEdad.Text = "";
            this.lblOrigen.Text = "";
            this.lblRegPadre.Text = "";
            this.lblRegMadre.Text = "";
            this.lblEstado.Visible = false;
            this.lblEstado.Text = "ESTADO";
            this.lblEstado.CssClass = "label label-default arrowed";
            this.lblCantEventos.Text = "";
            
            this.fNombre.Value = "";
            this.fComentario.Value = "";
            this.fIdentif.Value = "";
            this.fOrigen.Value = "";
            this.fPie.Value = "";
            this.fTraz.Value = "";
            
            this.ULFotos.InnerHtml = "";
       
            // reseteo grilla de eventos historial
            this.gvHistoria.DataSource = null;
            this.gvHistoria.DataBind();
            this.panelFicha.CssClass = "panel panel-default";
            this.phFichaHembra.Visible = false;
            this.phHistorial.Visible = false;
            this.LimpiarDdlResultadosSimilares();

            this.cboxControles.Enabled = false;

            // reseteo valores de ficha de hembras
            this.lblServicios.Text = "";
            this.lblRegServicio.Text = "";
            this.lblFechaUltServ.Text = "";
            this.lblFechaUltParto.Text = "";
            this.lblH.Text = "";
            this.lblH.Visible = false;
            this.lblM.Text = "";
            this.lblM.Visible = false;
            this.lblControles.Text = "";
            this.lblLecheUltControl.Text = "";
            this.lblProdLeche.Text = "";
            this.lblProdLeche.CssClass = "";
            this.lblProdGrasa.Text = "";
            this.lblAvgGrasa.Text = "";
            this.lblProdLecheUlt.Text = "";
            this.lblProdGrasaUlt.Text = "";
            this.lblAvgGrasaUlt.Text = "";
            this.lblMotivoSecado.Text = "";
            this.lblFechaSecado.Text = "";
            this.lblDiag.Text = "";

            //this.titFooterPanel.Visible = false;
            this.lblFooterPanel.Visible = false;

            this.fNombre.Value = "";
            this.fIdentif.Value = "";
            this.fTraz.Value = "";
            this.fOrigen.Value = "";

            _reg = "";
            _animal = null;
        }

        private void LimpiarDdlResultadosSimilares()
        {
            this._similares.Clear();
            this.ddlSimil.Visible = false;
            this.ddlSimil.DataSource = null;
            this.ddlSimil.DataBind();
        }

        protected void ddlSimilares_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSimil.SelectedIndex > 0)
                this.BuscarAnimal(this.ddlSimil.SelectedValue);
                
        }
        
        protected void BuscarAnimal(string registro)
        {
            this.LimpiarRegistro();
            this.LimpiarDdlResultadosSimilares();
            this.regBuscar.Value = registro;
            List<VOAnimal> animals = Fachada.Instance.GetSearchAnimal(registro);
            if (animals.Count > 0)
            {
                for (int i = 0; i < animals.Count; i++)
                {
                    if (animals[i].Registro.ToUpper().Equals(registro.ToUpper()))
                    {
                        _animal = animals[i];
                    }
                    else
                    {
                        _similares.Add(animals[i]);
                    }
                }

                // hay resultados similares y se presentan en el ddl como ayuda
                if (_similares.Count > 0)
                {
                    this.CargarDropDownListSimilares(_similares);
                }
                if (_animal != null)
                {
                    var animalTemp = new Animal();
                    animalTemp.Registro = _animal.Registro;
                    animalTemp.Sexo = _animal.Sexo;
                    _animal.Eventos = Fachada.Instance.GetEventosAnimal(animalTemp);
                    this.CargarFichaAnimal();
                    
                    // cargo datos del modal
                    this.fNombre.Value = _animal.Nombre;
                    this.fIdentif.Value = _animal.Identificacion;
                    this.fTraz.Value = _animal.Reg_trazab;
                    this.fOrigen.Value = _animal.Origen;
                }
            }
            else
            {
                this.LimpiarRegistro();
                this.lblAnimal.Text = "No existe";
            }
        }

        private void CargarDropDownListSimilares(List<VOAnimal> list)
        {
            if (list.Count > 0)
            {
                this.ddlSimil.Visible = true;
                this.ddlSimil.DataSource = list;
                this.ddlSimil.DataBind();
                this.ddlSimil.Items.Insert(0, new ListItem("Resultados Similares", "Resultados Similares"));
            }
        }

        protected void cBoxControles_CheckedChanging(object sender, EventArgs e)
        {
            if (this.cboxControles.Checked)
            {
                var list = new List<VOEvento>();
                for (int i = 0; i < _animal.Eventos.Count; i++)
                {
                    if (_animal.Eventos[i].Id_evento != 8)
                    {
                        var voEv = new VOEvento();
                        voEv.Fecha = _animal.Eventos[i].Fecha.ToShortDateString();
                        voEv.NombreEvento = _animal.Eventos[i].Nombre;
                        voEv.Observaciones = _animal.Eventos[i].ToString();
                        voEv.Comentarios = _animal.Eventos[i].Comentarios;
                        list.Add(voEv);
                    }
                }
                this.BindearEventosAnimalGridView(list);
                this.lblCantEventos.Text = list.Count.ToString();
            }
            else this.ProcesarEventosAnimal();
        }

        protected void gvHistoria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //OnRowDataBound="gvHistoria_RowDataBound" en aspx

            //foreach (GridViewRow rw in gvHistoria.Rows)
            //{
            //    if (rw.Cells[1].Text == "Control de Producción")
            //        e.Row.Visible = false;
            //}
        }

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> GetControlesByRegistro()
        {
            var list = Fachada.Instance.ControlesByRegistroGetUltAnio(_reg);
            return list;
        }

        public void CargarFotosAnimal(List<AnimalMapper.VOFoto> lst, HtmlGenericControl ul)
        {
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                // cargo list items recorriendo la lista
                for (int i = 0; i < lst.Count; i++)
                {
                    sb.Append("<li>");
                    var pie = "";
                    if (lst[i].PieDeFoto != "") pie = lst[i].PieDeFoto + ". ";
                    else pie = lst[i].Registro;
                    var titulo = pie + lst[i].Comentario;
                    sb.Append("<a data-rel='colorbox' title='" + titulo + "' href='" + lst[i].Ruta + "' >");
                    // la primera en grande, las demas como thumbnails
                    if (i == 0)
                    {
                        sb.Append("<img src='" + lst[i].Ruta + "' style='max-width: 520px;' /></a>");
                    }
                    else
                    {
                        sb.Append("<img src='" + lst[i].Thumb + "' style='max-width: 150px;' /></a>");
                    }
                    sb.Append("</li>");
                }
                ul.InnerHtml += sb.ToString();
            }
        }


        protected void btn_ModificarAnimal(object sender, EventArgs e)
        {
            if (ActualizarAnimal())
            {
                if (this.regBuscar.Value != "") this.BuscarAnimal(this.regBuscar.Value);
            }
        }

        protected bool ActualizarAnimal()
        {
            if (this.regBuscar.Value != "")
            {
                var nom = this.fNombre.Value;
                var id = this.fIdentif.Value;
                var traz = this.fTraz.Value;
                var orig = this.fOrigen.Value;
                var animTemp = new Animal();
                animTemp.Registro = this.regBuscar.Value;
                animTemp.Nombre = nom;
                animTemp.Identificacion = id;
                animTemp.Reg_trazab = traz;
                animTemp.Origen = orig;
                var u = (VOUsuario)Session["Usuario"];
                if (u != null)
                {
                    return Fachada.Instance.UpdateDatosAnimal(animTemp, u.Nickname);
                }
            }
            return false;
        }

        protected void btnFotoUpload_Click(object sender, EventArgs e)
        {
            var foto = this.btnFotoUpload.Text;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (this.fupFoto.HasFile)
            {
                try
                {
                    // ruta de las imagenenes de animales en el sitio
                    string filename = this.regBuscar.Value + "_" + Path.GetFileName(fupFoto.FileName);
                    var img_tamboprp = "img_tamboprp/animales/";
                    var img_tamboprp_thumb = "img_tamboprp/animales/animales_thumbs/";

                    // ruta para save as en el sitio
                    var rutaSiteImg = "~/" + img_tamboprp + filename;
                    
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fupFoto.PostedFile.InputStream);

                    // seteo máximo de 420px de ancho para imagen subida
                    //float imgWidth0 = image.PhysicalDimension.Width;
                    //float imgHeight0 = image.PhysicalDimension.Height;
                    //float imgSize0 = imgHeight0 > imgWidth0 ? imgHeight0 : imgWidth0;
                    //float imgResize0 = imgSize0 <= 420 ? (float)1.0 : 420 / imgSize0;
                    //imgWidth0 *= imgResize0; imgHeight0 *= imgResize0;
                    //System.Drawing.Image imageResized = image.GetThumbnailImage((int)imgWidth0, (int)imgHeight0, delegate() { return false; }, (IntPtr)0);


                    // creo thumbnail
                    float imgWidth = image.PhysicalDimension.Width;
                    float imgHeight = image.PhysicalDimension.Height;
                    float imgSize = imgHeight > imgWidth ? imgHeight : imgWidth;
                    float imgResize = imgSize <= 150 ? (float)1.0 : 150 / imgSize;
                    imgWidth *= imgResize; imgHeight *= imgResize;
                    System.Drawing.Image thumb = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
                    //System.Drawing.Image thumb = image.GetThumbnailImage(150, 150, null, IntPtr.Zero);


                    var filenameth = Path.Combine(
                    Server.MapPath("~/img_tamboprp/animales/animales_thumbs/"),
                    string.Format("{0}_th{1}",
                    Path.GetFileNameWithoutExtension(filename),
                    Path.GetExtension(filename)
                    )
                    );

                    // guardo imagen thumbnail
                    if (File.Exists(filenameth)) File.Delete(filenameth);
                    thumb.Save(filenameth);

                    // ruta para la base de datos
                    var rutaDbImg = "../" + img_tamboprp + filename;
                    var rutaDbThu = "../" + img_tamboprp_thumb + Path.GetFileNameWithoutExtension(filenameth) + Path.GetExtension(filename);
                    
                    // guardo en el sitio web
                    fupFoto.SaveAs(Server.MapPath(rutaSiteImg));
                    //if (File.Exists(rutaSiteImg)) File.Delete(rutaSiteImg);
                    //imageResized.Save(rutaSiteImg);

                    // creo objeto VOFoto y lo guardo en la base de datos (rutas a ambos archivos)
                    var foto = new AnimalMapper.VOFoto();
                    foto.Registro = this.regBuscar.Value;
                    foto.Ruta = rutaDbImg;
                    foto.Thumb = rutaDbThu;
                    foto.PieDeFoto = this.fPie.Value;
                    foto.Comentario = this.fComentario.Value;

                    Fachada.Instance.SubirFotoAnimal(foto);
                    lblStatus.Text = "Archivo subido";

                    this.ReCargarFotosDelAnimal();
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "El archivo no se pudo subir";
                }
            }
        }
        
    }
}