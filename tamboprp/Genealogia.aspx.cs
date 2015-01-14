using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Entidades;
using Negocio;
using Label = System.Web.UI.WebControls.Label;

namespace tamboprp
{
    public partial class Genealogia : System.Web.UI.Page
    {
        private Animal _animal;
        private List<Animal> _similares = new List<Animal>();
        private VOAnimal voA;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.LimpiarRegistro();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Cabaña", "Cabana.aspx"));
            list.Add(new VoListItemDuplaString("Genealogía", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void CargarArbolGenealogico()
        {
            if (voA != null)
            {
                this.verResultado.Visible = true;
                this.Animal.Text = voA.Registro;
                this.CargarNombreAnimal(voA, this.lblNom);
                this.CargarFechaNacAnimal(voA, this.titFNac, this.lblFNac);
                CargarGenAnimal(voA, this.titGen, this.lblGen);
                this.CargarIdAnimal(voA, this.titId, this.lblId);
                this.CargarTrazabAnimal(voA, this.titTraz, this.lblTraz);
                //this.CargarCalifAnimal(voA, this.titCalif, this.lblCalif);
                this.CargarEstadoAnimal(voA, this.lblEst);
                this.CargarCategoriaAnimal(voA, this.lblCat);
                if (voA.esHembra() && voA.Lactancias != null)
                {
                    this.CargarLactancias(voA.Lactancias, this.gvLactancias);
                    this.CargarProdTotalLecheLactancias(voA, this.titPL, this.lblPL);
                }
                if (voA.esMacho())
                {
                    this.lblSexo.Text = "M";
                    this.lblSexo.CssClass = "badge badge-primary";
                }
                if (voA.Madre != null)
                {
                    this.Madre.Text = voA.Madre.Registro;
                    this.CargarNombreAnimal(voA.Madre, this.lblNomMadre);
                    this.CargarFechaNacAnimal(voA.Madre, this.titFNacMadre, this.lblFNacMadre);
                    this.CargarIdAnimal(voA.Madre, this.titIdMadre, this.lblIdMadre);
                    this.CargarTrazabAnimal(voA.Madre, this.titTrazMadre, this.lblTrazMadre);
                    this.CargarEstadoAnimal(voA.Madre, this.lblEstMadre);
                    this.CargarCategoriaAnimal(voA.Madre, this.lblCatMadre);
                    if (voA.Madre.Lactancias != null)
                    {
                        this.CargarLactancias(voA.Madre.Lactancias, this.gvLactMadre);
                        this.CargarProdTotalLecheLactancias(voA.Madre, this.titPLMadre, this.lblPLMadre);
                    }
                    if (voA.Madre.Madre != null)
                    {
                        this.AbuelaM.Text = voA.Madre.Madre.Registro;
                        this.CargarNombreAnimal(voA.Madre.Madre, this.lblNomAbuelaM);
                        this.CargarFechaNacAnimal(voA.Madre.Madre, this.titFNacAbuelaM, this.lblFNacAbuelaM);
                        this.CargarIdAnimal(voA.Madre.Madre, this.titIdAbuelaM, this.lblIdAbuelaM);
                        this.CargarTrazabAnimal(voA.Madre.Madre, this.titTrazAbuelaM, this.lblTrazAbuelaM);
                        this.CargarEstadoAnimal(voA.Madre.Madre, this.lblEstAbuelaM);
                        this.CargarCategoriaAnimal(voA.Madre.Madre, this.lblCatAbuelaM);
                        if (voA.Madre.Madre.Lactancias != null)
                        {
                            this.CargarLactancias(voA.Madre.Madre.Lactancias, this.gvLactAbuelaM);
                            this.CargarProdTotalLecheLactancias(voA.Madre.Madre, this.titPLAbuelaM, this.lblPLAbuelaM);
                        }
                    }
                    if (voA.Madre.Padre != null)
                    {
                        this.AbueloM.Text = voA.Madre.Padre.Registro;
                        this.CargarNombreAnimal(voA.Madre.Padre, this.lblNomAbueloM);
                        this.CargarFechaNacAnimal(voA.Madre.Padre, this.titFNacAbueloM, this.lblFNacAbueloM);
                        this.CargarIdAnimal(voA.Madre.Padre, this.titIdAbueloM, this.lblIdAbueloM);
                        this.CargarTrazabAnimal(voA.Madre.Padre, this.titTrazAbueloM, this.lblTrazAbueloM);
                        this.CargarEstadoAnimal(voA.Madre.Padre, lblEstAbueloM);
                        this.CargarCategoriaAnimal(voA.Madre.Padre, this.lblCatAbueloM);
                    }
                }
                if (voA.Padre != null)
                {
                    this.Padre.Text = voA.Padre.Registro;
                    this.CargarNombreAnimal(voA.Padre, this.lblNomPadre);
                    this.CargarFechaNacAnimal(voA.Padre, this.titFNacPadre, this.lblFNacPadre);
                    this.CargarIdAnimal(voA.Padre, this.titIdPadre, this.lblIdPadre);
                    this.CargarTrazabAnimal(voA.Padre, this.titTrazPadre, this.lblTrazPadre);
                    this.CargarEstadoAnimal(voA.Padre, lblEstPadre);
                    this.CargarCategoriaAnimal(voA.Padre, this.lblCatPadre);
                    if (voA.Padre.Madre != null)
                    {
                        this.AbuelaP.Text = voA.Padre.Madre.Registro;
                        this.CargarNombreAnimal(voA.Padre.Madre, this.lblNomAbuelaP);
                        this.CargarFechaNacAnimal(voA.Padre.Madre, this.titFNacAbuelaP, this.lblFNacAbuelaP);
                        this.CargarIdAnimal(voA.Padre.Madre, this.titIdAbuelaP, this.lblIdAbuelaP);
                        this.CargarTrazabAnimal(voA.Padre.Madre, this.titTrazAbuelaP, this.lblTrazAbuelaP);
                        this.CargarEstadoAnimal(voA.Padre.Madre, this.lblEstAbuelaP);
                        this.CargarCategoriaAnimal(voA.Padre.Madre, this.lblCatAbuelaP);
                        if (voA.Padre.Madre.Lactancias != null)
                        {
                            this.CargarLactancias(voA.Padre.Madre.Lactancias, this.gvLactAbuelaP);
                            this.CargarProdTotalLecheLactancias(voA.Padre.Madre, this.titPLAbuelaP, this.lblPLAbuelaP);
                        }
                    }
                    if (voA.Padre.Padre != null)
                    {
                        this.AbueloP.Text = voA.Padre.Padre.Registro;
                        this.CargarNombreAnimal(voA.Padre.Padre, this.lblNomAbueloP);
                        this.CargarFechaNacAnimal(voA.Padre.Padre, this.titFNacAbueloP, this.lblFNacAbueloP);
                        this.CargarIdAnimal(voA.Padre.Padre, this.titIdAbueloP, this.lblIdAbueloP);
                        this.CargarTrazabAnimal(voA.Padre.Padre, this.titTrazAbueloP, this.lblTrazAbueloP);
                        this.CargarEstadoAnimal(voA.Padre.Padre, this.lblEstAbueloP);
                        this.CargarCategoriaAnimal(voA.Padre.Padre, this.lblCatAbueloP);
                    }
                }
            }
            
        }

        public void CargarLactancias(List<VOLactancia> lst, GridView gvLact)
        {
            if (lst.Count > 0)
            {
                gvLact.Visible = true;
                gvLact.DataSource = lst;
                gvLact.DataBind();
            }
        }

        public void CargarProdTotalLecheLactancias(VOAnimal voAnim, Label titPLTotal, Label lblPLTotal)
        {
            var pLTotal = voAnim.GetProdTotalLecheLactancias();
            if (pLTotal > 0)
            {
                lblPLTotal.Visible = true;
                lblPLTotal.Text = pLTotal.ToString();
                titPLTotal.Visible = true;
                if (pLTotal >= 50000)
                {
                    titPLTotal.CssClass = "label label-yellow arrowed-right";
                }
            }
        }

        public void CargarGenAnimal(VOAnimal voAnim, Label lblTitulo, Label lblData)
        {
            if (voAnim != null && voAnim.Gen > 0)
            {
                lblData.Text = voAnim.Gen.ToString();
                lblData.Visible = true;
                lblTitulo.Visible = true;
            }
        }

        public void CargarFechaNacAnimal(VOAnimal voAnim, Label lblTitulo, Label lblData)
        {
            var minDate = new DateTime(0001, 01, 01);
            if (voAnim != null && voAnim.Fecha_nacim != minDate)
            {
                lblData.Text = voAnim.Fecha_nacim.ToShortDateString();
                lblData.Visible = true;
                lblTitulo.Visible = true;
            }
        }

        public void CargarNombreAnimal(VOAnimal voAnim, Label lblData)
        {
            if (voAnim != null && voAnim.Nombre != "")
            {
                lblData.Text = voAnim.Nombre;
                lblData.Visible = true;
            }
        }

        public void CargarIdAnimal(VOAnimal voAnim, Label lblTitulo, Label lblData)
        {
            if (voAnim != null && voAnim.Identificacion != "")
            {
                lblData.Text = voAnim.Identificacion;
                lblData.Visible = true;
                lblTitulo.Visible = true;
            }
        }

        public void CargarTrazabAnimal(VOAnimal voAnim, Label lblTitulo, Label lblData)
        {
            if (voAnim != null && voAnim.Reg_trazab != "")
            {
                lblData.Text = voAnim.Reg_trazab;
                lblData.Visible = true;
                lblTitulo.Visible = true;
            }
        }

        public void CargarCalifAnimal(VOAnimal voAnim, Label lblTitulo, Label lblData)
        {
            if (voAnim != null && voAnim.Calific != "")
            {
                lblData.Text = voAnim.Calific;
                lblData.Visible = true;
                lblTitulo.Visible = true;
            }
        }

        public void CargarEstadoAnimal(VOAnimal voAnim, Label lblEstado)
        {
            if (!voAnim.Vivo)
            {
                lblEstado.Text = "MUERTO";
                lblEstado.Visible = true;
                lblEstado.CssClass = "label label-danger";
            }
            else
            {
                if (voAnim.Vendido)
                {
                    lblEstado.Text = "VENDIDO";
                    lblEstado.CssClass = "label label-info";
                }
                else
                {
                    lblEstado.Text = "VIVO";
                    lblEstado.CssClass = "label label-success";
                }
                lblEstado.Visible = true;
            }
            
        }

        public void CargarCategoriaAnimal(VOAnimal voAnim, Label lblCateg)
        {
            if (voAnim != null)
            {
                var catAnimal = voAnim.IdCategoria;
                // doy estilo a la label de categoria segun la misma
                switch (catAnimal)
                {
                    case 1: // Ternera
                        {
                            lblCateg.CssClass = "label label-default";
                            break;
                        }
                    case 2: // Vaquillona
                        {
                            lblCateg.CssClass = "label label-pink";
                            break;
                        }
                    case 3: // Vaquillona entorada
                        {
                            lblCateg.CssClass = "label label-yellow";
                            break;
                        }
                    case 4: // En ordeñe
                        {
                            lblCateg.CssClass = "label label-success";
                            break;
                        }
                    case 5: // Seca
                        {
                            lblCateg.CssClass = "label label-warning";
                            break;
                        }
                    case 6: // Descarte
                        {
                            lblCateg.CssClass = "label label-grey";
                            break;
                        }
                    case 7: // Ternero
                        {
                            lblCateg.CssClass = "label label-default";
                            break;
                        }
                    case 8: // Toro
                        {
                            lblCateg.CssClass = "label label-inverse";
                            break;
                        }
                    case 9: // Eliminada
                        {
                            lblCateg.CssClass = "label label-grey";
                            break;
                        }
                    case 10: // Semen
                        {
                            lblCateg.CssClass = "label label-purple";
                            break;
                        }
                    default:
                        {
                            lblCateg.CssClass = "label label-default";
                            break;
                        }
                }
                lblCateg.Visible = true;
                lblCateg.Text = voAnim.Categoria;
            }
        
        }
        

        public void GetAnimalArbolGenealogico()
        {
            if (_animal != null) 
            {
                voA = Fachada.Instance.GetArbolGenealogico(_animal);
                this.CargarArbolGenealogico();
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
            //this.lblSexo.Visible = false;
            this.titFNac.Visible = false;
            this.lblFNac.Visible = false;
            this.titGen.Visible = false;
            this.lblGen.Visible = false;
            this.titId.Visible = false;
            this.lblId.Visible = false;
            this.titTraz.Visible = false;
            this.lblTraz.Visible = false;
            this.lblEst.Text = "";
            this.lblEst.Visible = false;
            this.lblCat.Visible = false;
            this.titPL.Visible = false;
            this.titPL.CssClass = "label label-default arrowed-right";
            this.lblPL.Visible = false;
            this.gvLactancias.Visible = false;

            // madre
            this.Madre.Text = "";
            this.lblEstMadre.Text = "";
            this.lblEstMadre.Visible = false;

            //padre
            this.Padre.Text = "";
            this.lblEstPadre.Text = "";
            this.lblEstPadre.Visible = false;

            //abuelos maternos
            this.AbuelaM.Text = "";
            this.lblEstAbuelaM.Text = "";
            this.lblEstAbuelaM.Visible = false;
            this.AbueloM.Text = "";
            this.lblEstAbueloM.Text = "";
            this.lblEstAbueloM.Visible = false;
 
            //abuelos paternos
            this.AbuelaP.Text = "";
            this.lblEstAbuelaP.Text = "";
            this.lblEstAbuelaP.Visible = false;
            this.AbueloP.Text = "";
            this.lblEstAbueloP.Text = "";
            this.lblEstAbueloP.Visible = false;
        }

    }
}