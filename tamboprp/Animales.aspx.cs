using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                this.lblGen.Text = (_animal.Gen != -1) ? _animal.Gen.ToString() : "-";
                Categoria catAnimal = Fachada.Instance.GetCategoriaById(_animal.IdCategoria);
                if (catAnimal != null)
                {
                    if (catAnimal.Id_categ == 4) 
                        this.lblCategoria.CssClass = "label label-success";
                    this.lblCategoria.Text = catAnimal.ToString();
                }
                else this.lblCategoria.Text = "-";
                this.lblNombre.Text = (_animal.Nombre != "") ? _animal.Nombre : "-";
                this.lblTraz.Text = _animal.Reg_trazab;
                this.lblSexo.Text = _animal.Sexo.ToString();
                this.lblFechaNac.Text = _animal.Fecha_nacim.ToShortDateString();
                this.lblOrigen.Text = _animal.Origen;
                this.lblRegPadre.Text = _animal.Reg_padre;
                this.lblRegMadre.Text = _animal.Reg_madre;
                _animal.Vivo = !Fachada.Instance.EstaMuertoAnimal(_animal.Registro);
                if (!_animal.Vivo)
                {
                    this.lblVivo.Text = "MUERTO";
                    this.lblVivo.CssClass = "label label-danger";
                }
                else
                {
                    if (Fachada.Instance.FueVendidoAnimal(_animal.Registro))
                    {
                        this.lblVivo.Text = "VENDIDO";
                        this.lblVivo.CssClass = "label label-info";
                    }
                    else
                    {
                        this.lblVivo.Text = "VIVO";
                        this.lblVivo.CssClass = "label label-success";
                    }                    
                }
                //this.btnHistorial.Visible = true;
                EventosAnimalAGrilla();
            }
            else this.lblAnimal.Text = "No existe :(";
        }

        public void EventosAnimalAGrilla()
        {
            if (_animal != null)
            {
                var list = new List<VOEvento>();
                if (_animal.Eventos.Count > 0)
                {
                    //this.gvHistoria.Visible = true;
                    for (int i = 0; i < _animal.Eventos.Count; i++)
                    {
                        var voEv = new VOEvento();
                        voEv.Fecha = _animal.Eventos[i].Fecha.ToShortDateString();
                        voEv.NombreEvento = _animal.Eventos[i].Nombre;
                        voEv.Observaciones = _animal.Eventos[i].ToString();
                        voEv.Comentarios = _animal.Eventos[i].Comentarios;
                        list.Add(voEv);
                    }
                    this.gvHistoria.DataSource = list;
                    this.gvHistoria.DataBind();
                }
                this.gvHistoria.Visible = true;
                this.titHistorico.Visible = true;
                this.lblHistorico.Text = _animal.Eventos.Count.ToString();
            }
        }

        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            this.BuscarAnimal(this.regBuscar.Value);
        }

        private void LimpiarRegistro()
        {
            this.lblAnimal.Text = "Registro";
            this.lblIdentif.Text = "";
            this.lblGen.Text = "";
            this.lblCategoria.Text = "";
            this.lblCategoria.CssClass = "";
            this.lblNombre.Text = "";
            this.lblTraz.Text = "";
            this.lblSexo.Text = "";
            this.lblFechaNac.Text = "";
            this.lblOrigen.Text = "";
            this.lblRegPadre.Text = "";
            this.lblRegMadre.Text = "";
            this.lblVivo.Text = "ESTADO";
            this.lblVivo.CssClass = "label label-default";
            this.titHistorico.Visible = false;
            this.lblHistorico.Text = "";            
            this.gvHistoria.DataSource = null;
            this.gvHistoria.DataBind();
            this.btnHistorial.Visible = false;
            this.LimpiarDdlResultadosSimilares();
        }

        private void LimpiarDdlResultadosSimilares()
        {
            this.ddlSimilares.Visible = false;
            this.ddlSimilares.DataSource = null;
            this.ddlSimilares.DataBind();
            this.ddlSimilares.Items.Clear();
            //this.ddlSimilares.Items.Insert(0, new ListItem("Resultados similares", "Resultados similares"));
            this._similares.Clear();
        }

        protected void ddlSimilares_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ddlSimilares.SelectedIndex > 0 )
                //this.BuscarAnimal(this.ddlSimilares.SelectedValue);
                //this.BuscarAnimal(this.ddlSimilares.SelectedItem.Value);
                //this.BuscarAnimal(this.ddlSimilares.SelectedItem.Text);
        }


        protected void btnSimilares_click(object sender, EventArgs e)
        {
            if (this.ddlSimilares.SelectedIndex > 0)
                this.BuscarAnimal(this.ddlSimilares.SelectedValue);
                
        }


        protected void BuscarAnimal(string registro)
        {
            this.LimpiarRegistro();
            List<Animal> animals = Fachada.Instance.GetSearchAnimal(registro);
            if (animals.Count > 0)
            {
                for (int i = 0; i < animals.Count; i++)
                {
                    if (animals[i].Registro.Equals(registro))
                    {
                        _animal = animals[i];
                    }
                    else
                    {
                        _similares.Add(animals[i]);
                        //var valor = i.ToString();
                        //var item = new ListItem(animals[i].Registro, valor);
                        //this.ddlSimilares.Items.Add(item);
                    }
                }
                /* hay resultados similares y se presentan en el ddl como ayuda */
                if (_similares.Count > 0)
                {
                    this.ddlSimilares.Visible = true;
                    this.ddlSimilares.DataSource = _similares;
                    this.ddlSimilares.DataTextField = "Registro";
                    this.ddlSimilares.DataValueField = "Registro";
                    this.ddlSimilares.Items.Insert(0, new ListItem("Resultados similares", "Resultados similares"));
                    this.ddlSimilares.DataBind();

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

        
        //protected void btnVerHistorial_Click(object sender, EventArgs e)
        //{
        //    if (_animal != null)
        //    {
        //        var retAnimal = Fachada.Instance.GetEventosAnimal(_animal.Registro);
        //        _animal.Eventos = retAnimal.Eventos;
        //        this.ProcesarEventosAnimal();
        //    }
        //    else
        //    {
        //        LimpiarRegistro();
        //        this.lblAnimal.Text = "No existe";
        //    }
        //}

        protected void btnVerHistorial_Click(object sender, EventArgs e)
        {
            if (_animal != null)
            {
                this.gvHistoria.Visible = true;
                this.titHistorico.Visible = true;
                this.lblHistorico.Text = _animal.Eventos.Count.ToString();
            }
        }
        
    }
}