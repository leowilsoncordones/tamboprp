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
        protected void Page_Load(object sender, EventArgs e)
        {
            LimpiarRegistro();
            //if (!Page.IsPostBack)
            //{
            //    //Animal a = new Animal();
            //    ////a.Registro = "3110";
            //    //a.Registro = "01430";
            //    //var amap = new AnimalMapper(a);
            //    //Animal a2 = amap.GetAnimalbyId();
            //    //this.CargarFicha(a2);
            //}
        }

        public void CargarFichaAnimal(Animal a)
        {
            if (a != null)
            {
                this.lblAnimal.Text = a.Registro;
                this.lblIdentif.Text = a.Identificacion;
                this.lblGen.Text = (a.Gen != -1) ? a.Gen.ToString() : " - ";
                //this.lblCategoria.Text = a.Categoria.ToString();
                this.lblNombre.Text = (a.Nombre != "") ? a.Nombre : " - ";
                this.lblTraz.Text = a.Reg_trazab;
                this.lblSexo.Text = a.Sexo.ToString();
                this.lblFechaNac.Text = a.Fecha_nacim.ToShortDateString();
                this.lblOrigen.Text = a.Origen;
                this.lblRegPadre.Text = a.Reg_padre;
                this.lblRegMadre.Text = a.Reg_madre;
                a.Vivo = !Fachada.Instance.EstaMuertoAnimal(a.Registro);
                if (!a.Vivo)
                {
                    this.lblVivo.Text = "MUERTO";
                    this.lblVivo.CssClass = "label label-danger";
                }
                else
                {
                    if (Fachada.Instance.FueVendidoAnimal(a.Registro))
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
                EventosAGrilla(a);
            }
            else this.lblAnimal.Text = "No existe :(";
        }

        public void EventosAGrilla(Animal a)
        {
            List<EventoString> list = new List<EventoString>();
            if (a.Eventos.Count > 0)
            {                
                for (int i = 0; i < a.Eventos.Count; i++)
                {
                    var eventStr = new EventoString();
                    eventStr.Fecha = a.Eventos[i].Fecha.ToShortDateString();
                    eventStr.NombreEvento = a.Eventos[i].Nombre;
                    eventStr.Comentario = a.Eventos[i].ToString();
                    list.Add(eventStr);
                }
                this.gvHistoria.DataSource = list;
                this.gvHistoria.DataBind();
            }
            this.titHistorico.Visible = true;
            this.lblHistorico.Text = a.Eventos.Count.ToString();
        }


        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            List<Animal> animals =  Fachada.Instance.GetSearchAnimal(regBuscar.Value);
           
            if (animals.Count > 0)
            {
                if (animals.Count > 1)
                {
                    /* hay mas de un resultado */
                }
                /* hay un solo resultado, es el animal que buscamos */
                var unAnimal = animals[0];
                var retAnimal = Fachada.Instance.GetEventosAnimal(unAnimal.Registro);
                unAnimal.Eventos = retAnimal.Eventos;
                this.CargarFichaAnimal(unAnimal);
            }
            else
            {
                LimpiarRegistro();
                this.lblAnimal.Text = "No existe :(";
            }
        }

        private void LimpiarRegistro()
        {
            this.lblAnimal.Text = "Registro";
            this.lblIdentif.Text = "";
            this.lblGen.Text = "";
            this.lblCategoria.Text = "";
            this.lblNombre.Text = "";
            this.lblTraz.Text = "";
            this.lblSexo.Text = "";
            this.lblFechaNac.Text = "";
            this.lblOrigen.Text = "";
            this.lblRegPadre.Text = "";
            this.lblRegMadre.Text = "";
            this.lblVivo.Text = "VIVO/MUERTO";
            this.titHistorico.Visible = false;
            this.lblHistorico.Text = "";
            this.lblVivo.CssClass = "label label-default";
            this.gvHistoria.DataSource = null;
            this.gvHistoria.DataBind();
        }

        public class EventoString
        {
            public EventoString() { }

            public EventoString(string fecha, string nombreEvento, string comentario)
            {
                Fecha = fecha;
                NombreEvento = nombreEvento;
                Comentario = comentario;
            }

            public string Fecha { get ; set; }

            public string NombreEvento { get ; set; }

            public string Comentario { get ; set; }

          }
    }
}