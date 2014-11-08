using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

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
                this.lblVivo.Text = (a.Vivo) ? "VIVO" : "MUERTO";
                this.lblVivo.CssClass = (a.Vivo) ? "label label-success" : "label label-danger";
            }
            else this.lblAnimal.Text = "No existe :(";
        }

        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            Animal a = new Animal();
            a.Registro = this.regBuscar.Value;
            var amap = new AnimalMapper(a);
            //Animal a2 = amap.GetAnimalbyId();
            //Animal a2 = amap.GetAnimalbyId();
            //this.CargarFicha(a2);

            List<Animal> animals = amap.GetSearch(this.regBuscar.Value, 0);
            if (animals.Count > 0)
            {
                if (animals.Count > 1)
                {

                }
                this.CargarFichaAnimal(animals[0]);
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
            this.lblVivo.CssClass = "label label-default";
        }
    }
}