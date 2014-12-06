using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Datos;
using Negocio;
using Entidades;

namespace tamboprp
{
    public partial class Animales : System.Web.UI.Page
    {
        private Animal _animal;
        private List<Animal> _similares = new List<Animal>(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LimpiarRegistro();
        }

        public void CargarFichaAnimal()
        {
            if (_animal != null)
            {
                this.lblAnimal.Text = _animal.Registro;
                this.lblIdentif.Text = _animal.Identificacion;
                this.lblGen.Text = (_animal.Gen != -1) ? _animal.Gen.ToString() : "";
                Categoria catAnimal = Fachada.Instance.GetCategoriaById(_animal.IdCategoria);
                this.panelFicha.CssClass = "panel panel-default";
                if (catAnimal != null)
                {
                    // doy estilo a la label de categoria segun la misma
                    switch (catAnimal.Id_categ)
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

                    this.lblCategoria.Text = catAnimal.ToString();
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
                    int y = DateTime.Now.Year - _animal.Fecha_nacim.Year;
                    int m = Math.Abs(DateTime.Now.Month - _animal.Fecha_nacim.Month);
                    string output = y + "a " + m + "m"; 
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
                    this.CargarFichaHembra();
                }
                // eventos del animal al historico
                ProcesarEventosAnimal();
            }
            else this.lblAnimal.Text = "No existe :(";
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
                        break;
                    }
                    case 4: // secados
                    {
                        var sec = (Secado)list[i];
                        varMotivoUltSecado = sec.Motivos_secado.ToString();
                        varFechaUltSecado = sec.Fecha.ToShortDateString();
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
                        break;
                    }
                }
            }

            this.lblServicios.Text = varCantServicios.ToString();
            this.lblRegServicio.Text = varRegUltimoServicio;
            this.lblFechaUltServ.Text = varFechaUltServicio;
            //var strHembras = "";
            //var strMachos = "";
            //var strUnion = "";
            //if (partosH > 0 && partosM > 0) strUnion=" y ";
            //if (partosH > 0) strHembras = partosH.ToString() + " H";
            //if (partosM > 0) strMachos = partosM.ToString() + " M";
            this.lblFechaUltParto.Text = varFechaUltParto;
            //this.lblParidos.Text = strHembras + strUnion + strMachos;

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
            this.lblProdLeche.Text = varProdLeche.ToString();
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
        }

        public void BindearEventosAnimalGridView(List<VOEvento> list)
        {
            this.gvHistoria.DataSource = list;
            this.gvHistoria.DataBind();
        }

        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            this.BuscarAnimal(this.regBuscar.Value);
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
       
            // reseteo grilla de eventos historial
            this.gvHistoria.DataSource = null;
            this.gvHistoria.DataBind();
            this.panelFicha.CssClass = "panel panel-default";
            this.phFichaHembra.Visible = false;
            this.phHistorial.Visible = false;
            this.LimpiarDdlResultadosSimilares();

            // reseteo valores de ficha de hembras
            this.lblServicios.Text = "";
            this.lblRegServicio.Text = "";
            this.lblFechaUltServ.Text = "";
            this.lblFechaUltParto.Text = "";
            //this.lblParidos.Text = "";
            this.lblH.Text = "";
            this.lblH.Visible = false;
            this.lblM.Text = "";
            this.lblM.Visible = false;
            this.lblControles.Text = "";
            this.lblLecheUltControl.Text = "";
            this.lblProdLeche.Text = "";
            this.lblProdGrasa.Text = "";
            this.lblAvgGrasa.Text = "";
            this.lblMotivoSecado.Text = "";
            this.lblFechaSecado.Text = "";
            this.lblDiag.Text = "";

        }

        private void LimpiarDdlResultadosSimilares()
        {
            this.divContenedorDdl.InnerHtml = "";
            this._similares.Clear();
        }

        protected void ddlSimilares_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ddlSimilares.SelectedIndex > 0 )
                //this.BuscarAnimal(this.ddlSimilares.SelectedValue);
                //this.BuscarAnimal(this.ddlSimilares.SelectedItem.Value);
                //this.BuscarAnimal(this.ddlSimilares.SelectedItem.Text);

            //var btn = (LinkButton)sender;
            //if (btn != null && btn.Text != "")
            //{
            //    this.BuscarAnimal(btn.Text);
            //}
        }


        protected void btnSimilares_click(object sender, EventArgs e)
        {
            /*if (this.ddlSimilares.SelectedIndex > 0)
                this.BuscarAnimal(this.ddlSimilares.SelectedValue);*/
                
        }


        protected void BuscarAnimal(string registro)
        {
            this.LimpiarRegistro();
            List<Animal> animals = Fachada.Instance.GetSearchAnimal(registro);
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
                    this.BootstrapDropDownListLargeList(_similares);
                }
                if (_animal != null)
                {
                    var animalTemp = new Animal();
                    animalTemp.Registro = _animal.Registro;
                    animalTemp.Sexo = _animal.Sexo;
                    _animal.Eventos = Fachada.Instance.GetEventosAnimal(animalTemp);
                    this.CargarFichaAnimal();
                }
            }
            else
            {
                this.LimpiarRegistro();
                this.lblAnimal.Text = "No existe";
            }
        }


        private void BootstrapDropDownListLargeList(List<Animal> list)
        {
            // Large button group dinámico para resultados similares
            var sb = new StringBuilder();
            sb.Append("<div class='btn-group btn-group-lg'>");
            sb.Append("<button data-toggle='dropdown' class='btn btn-default btn-white dropdown-toggle' aria-expanded='false'>");
            sb.Append("Resultados similares ");
            sb.Append("<i class='ace-icon fa fa-angle-down icon-on-right'></i></button>");
            sb.Append("</button>");
            sb.Append("<ul class='dropdown-menu dropdown-info dropdown-menu-right'>");
            // cargo list items recorriendo la lista
            for (int i = 0 ; i < list.Count; i++)
            {
                sb.Append("<li class='btn-lg'><a id='item_" + i + "' href='#' onserverclick='ddlSimilares_SelectedIndexChanged();'>");
                sb.Append(list[i].Registro.ToString());
                sb.Append("</a></li>");
            }
            //sb.Append("<li class='divider'></li>");
            // list items abajo de la línea divider
            sb.Append("</ul>");
            sb.Append("</div>");
            this.divContenedorDdl.InnerHtml += sb.ToString();
        }

        //protected void btn_update_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gvr in GridView1.Rows)
        //    {
        //        if (((CheckBox)gvr.findcontrol("Checkbox")).Checked == true)
        //        {
        //            //Do stuff with checked row
        //            gvr.Visible = false;
        //        }

        //    }
        //}

        public void cBoxControles_CheckedChanged(Object sender, EventArgs e)
        {
            //MessageBox.Show("checked");
            foreach (GridViewRow gvr in gvHistoria.Rows)
            {
                if (gvr.DataItem == "Control de Producción")
                {
                    if (cboxControles.Checked)
                    {
                        gvr.Visible = true;
                    }
                    else gvr.Visible = false;
                }
            }
        }
       
    }
}