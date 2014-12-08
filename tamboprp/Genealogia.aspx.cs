using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class Genealogia : System.Web.UI.Page
    {
        private Animal _animal;
        private List<Animal> _similares = new List<Animal>();
        private VOAnimal voA;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LimpiarRegistro();
            //this.GetAnimalArbolGenealogico("5582");
        }

        private void CargarArbolGenealogico()
        {

            this.Animal.Text = voA.Registro;
            this.CargarEstadoAnimal(voA.Padre, 1);
            if (voA.Madre != null)
            {
                this.Madre.Text = voA.Madre.Registro;
                this.CargarEstadoAnimal(voA.Madre, 2);
                if (voA.Madre.Madre != null)
                {
                    this.CargarEstadoAnimal(voA.Madre.Madre, 4);
                    this.AbuelaM.Text = voA.Madre.Madre.Registro;
                }
                if (voA.Madre.Padre != null)
                {
                    this.CargarEstadoAnimal(voA.Madre.Padre, 5);
                    this.AbueloM.Text = voA.Madre.Padre.Registro;
                }
            }
            if (voA.Padre != null)
            {
                this.CargarEstadoAnimal(voA.Padre, 3);
                this.Padre.Text = voA.Padre.Registro;
                if (voA.Padre.Madre != null)
                {
                    this.CargarEstadoAnimal(voA.Padre.Madre, 6);
                    this.AbuelaP.Text = voA.Padre.Madre.Registro;
                }
                if (voA.Padre.Padre != null)
                {
                    this.CargarEstadoAnimal(voA.Padre.Padre, 7);
                    this.AbueloP.Text = voA.Padre.Padre.Registro;
                }
            }
            
        }

        public void CargarEstadoAnimal(VOAnimal voAnim, int numPanel)
        {
            switch (numPanel)
            {
                case 1:
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstado.Text = "MUERTO";
                        this.lblEstado.Visible = true;
                        this.lblEstado.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
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
                    break;
                }
                case 2: //madre
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoMadre.Text = "MUERTO";
                        this.lblEstadoMadre.Visible = true;
                        this.lblEstadoMadre.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoMadre.Text = "VENDIDO";
                            this.lblEstadoMadre.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoMadre.Text = "VIVO";
                            this.lblEstadoMadre.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoMadre.Visible = true;
                    }
                    break;
                }
                case 3: //padre
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoPadre.Text = "MUERTO";
                        this.lblEstadoPadre.Visible = true;
                        this.lblEstadoPadre.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoPadre.Text = "VENDIDO";
                            this.lblEstadoPadre.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoPadre.Text = "VIVO";
                            this.lblEstadoPadre.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoPadre.Visible = true;
                    }
                    break;
                }
                case 4: //abuela materna
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoAbuelaM.Text = "MUERTO";
                        this.lblEstadoAbuelaM.Visible = true;
                        this.lblEstadoAbuelaM.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoAbuelaM.Text = "VENDIDO";
                            this.lblEstadoAbuelaM.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoAbuelaM.Text = "VIVO";
                            this.lblEstadoAbuelaM.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoAbuelaM.Visible = true;
                    }
                    break;
                }
                case 5: //abuelo materno
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoAbueloM.Text = "MUERTO";
                        this.lblEstadoAbueloM.Visible = true;
                        this.lblEstadoAbueloM.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoAbueloM.Text = "VENDIDO";
                            this.lblEstadoAbueloM.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoAbueloM.Text = "VIVO";
                            this.lblEstadoAbueloM.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoAbueloM.Visible = true;
                    }
                    break;
                }
                case 6: //abuela paterna
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoAbuelaP.Text = "MUERTO";
                        this.lblEstadoAbuelaP.Visible = true;
                        this.lblEstadoAbuelaP.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoAbuelaP.Text = "VENDIDO";
                            this.lblEstadoAbuelaP.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoAbuelaP.Text = "VIVO";
                            this.lblEstadoAbuelaP.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoAbuelaP.Visible = true;
                    }
                    break;
                }
                case 7: //abuelo paterno
                {
                    if (!voAnim.Vivo)
                    {
                        this.lblEstadoAbueloP.Text = "MUERTO";
                        this.lblEstadoAbueloP.Visible = true;
                        this.lblEstadoAbueloP.CssClass = "label label-danger arrowed";
                    }
                    else
                    {
                        if (Fachada.Instance.FueVendidoAnimal(voAnim.Registro))
                        {
                            this.lblEstadoAbueloP.Text = "VENDIDO";
                            this.lblEstadoAbueloP.CssClass = "label label-info arrowed";
                        }
                        else
                        {
                            this.lblEstadoAbueloP.Text = "VIVO";
                            this.lblEstadoAbueloP.CssClass = "label label-success arrowed";
                        }
                        this.lblEstadoAbueloP.Visible = true;
                    }
                    break;
                }

            }
            
        }

        public void GetAnimalArbolGenealogico()
        {
            //_animal = Fachada.Instance.GetAnimalByRegistro(registro);
            voA = new VOAnimal();

            if (_animal != null)
            {
                voA = Fachada.Instance.CopiarVOAnimal(_animal);
                if (_animal.Reg_madre != "H-DESCONOC")
                {
                    voA.Madre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(_animal.Reg_madre));
                    voA.Madre.Vivo = !Fachada.Instance.EstaMuertoAnimal(_animal.Reg_madre);
                    if (voA.Madre != null && voA.Madre.Registro != "H-DESCONOC")
                    {
                        string strAbuelaM = voA.Madre.Reg_madre;
                        string strAbueloM = voA.Madre.Reg_padre;
                        if (strAbuelaM != "H-DESCONOC")
                        {
                            voA.Madre.Madre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(strAbuelaM));
                            voA.Madre.Madre.Vivo = !Fachada.Instance.EstaMuertoAnimal(strAbuelaM);
                        }
                        if (strAbueloM != "M-DESCONOC")
                        {
                            voA.Madre.Padre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(strAbueloM));
                            voA.Madre.Padre.Vivo = !Fachada.Instance.EstaMuertoAnimal(strAbueloM);
                        }
                    }
                }
                if (_animal.Reg_padre != "M-DESCONOC")
                {
                    voA.Padre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(_animal.Reg_padre));
                    voA.Padre.Vivo = !Fachada.Instance.EstaMuertoAnimal(_animal.Reg_padre);
                    if (voA.Padre != null && voA.Padre.Registro != "M-DESCONOC")
                    {
                        string strAbuelaP = voA.Padre.Reg_madre;
                        string strAbueloP = voA.Padre.Reg_padre;
                        if (strAbuelaP != "H-DESCONOC")
                        {
                            voA.Padre.Madre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(strAbuelaP));
                            voA.Padre.Madre.Vivo = !Fachada.Instance.EstaMuertoAnimal(strAbuelaP);
                        }
                        if (strAbueloP != "M-DESCONOC")
                        {
                            voA.Padre.Padre = Fachada.Instance.CopiarVOAnimal(Fachada.Instance.GetAnimalByRegistro(strAbueloP));
                            voA.Padre.Padre.Vivo = !Fachada.Instance.EstaMuertoAnimal(strAbueloP);
                        }
                    }
                }
                if (voA != null)this.CargarArbolGenealogico();
            }
            
        }

        protected void btnBuscarAnimal_Click(object sender, EventArgs e)
        {
            this.BuscarAnimal(this.regBuscar.Value);
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
                    //this.CargarDdlListSimilares(_similares); VER ANIMALES
                }
                if (_animal != null)
                {
                    var animalTemp = new Animal();
                    animalTemp.Registro = _animal.Registro;
                    animalTemp.Sexo = _animal.Sexo;
                    _animal.Vivo = !Fachada.Instance.EstaMuertoAnimal(_animal.Registro);
                    //_animal.Eventos = Fachada.Instance.GetEventosAnimal(animalTemp);
                    this.GetAnimalArbolGenealogico();
                }
            }
            else
            {
                this.LimpiarRegistro();
            }
        }

        //private void CargarDdlListSimilares(List<Animal> list)
        //{
        //    // Large button group dinámico para resultados similares
        //    this.ddlSimilares.DataSource = list;
        //    this.ddlSimilares.DataTextField = "Nombre";
        //    this.ddlSimilares.DataValueField = "Nombre";
        //    this.ddlSimilares.DataBind();
        //}

        private void LimpiarRegistro()
        {
            // reseteo valores ficha de animal
            this.Animal.Text = "";
            this.lblEstado.Text = "";
            this.lblEstado.Visible = false;

            // padres
            this.Madre.Text = "";
            this.lblEstadoMadre.Text = "";
            this.lblEstadoMadre.Visible = false;
            this.Padre.Text = "";
            this.lblEstadoPadre.Text = "";
            this.lblEstadoPadre.Visible = false;

            //abuelos maternos
            this.AbuelaM.Text = "";
            this.lblEstadoAbuelaM.Text = "";
            this.lblEstadoAbuelaM.Visible = false;
            this.AbueloM.Text = "";
            this.lblEstadoAbueloM.Text = "";
            this.lblEstadoAbueloM.Visible = false;
 
            //abuelos paternos
            this.AbuelaP.Text = "";
            this.lblEstadoAbuelaP.Text = "";
            this.lblEstadoAbuelaP.Visible = false;
            this.AbueloP.Text = "";
            this.lblEstadoAbueloP.Text = "";
            this.lblEstadoAbueloP.Visible = false;
        }

    }
}