using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Datos;
using Entidades;
using Negocio;
using Label = System.Web.UI.WebControls.Label;

namespace tamboprp
{
    public partial class Genealogia : System.Web.UI.Page
    {
        private VOAnimal _animal;
        private List<VOAnimal> _similares = new List<VOAnimal>();
        private VOAnimal voA;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
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
                // REGISTRO
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
                this.CargarSexo(voA, this.lblSexo);
                //if (voA.esMacho())
                //{
                //    this.lblSexo.Text = "M";
                //    this.lblSexo.CssClass = "badge badge-primary";
                //}
                //else
                //{
                //    this.lblSexo.Text = "H";
                //    this.lblSexo.CssClass = "badge badge-pink";
                //}
                if (voA.Concursos != null) this.CargarConcursosAnimal(voA.Concursos, this.gvConcursos);
                if (voA.Fotos != null) this.CargarFotosAnimalPrincipal(voA.Fotos, this.lblFotos, this.ULFotos);

                // LINEA MATERNA
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
                    if (voA.Madre.Fotos != null) this.CargarFotosAnimal(voA.Madre.Fotos, this.lblFotosMadre, this.ULFotosMadre);
                    // ABUELA MATERNA
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
                    // ABUELO MATERNO
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

                // LINEA PATERNA
                if (voA.Padre != null)
                {
                    this.Padre.Text = voA.Padre.Registro;
                    this.CargarNombreAnimal(voA.Padre, this.lblNomPadre);
                    this.CargarFechaNacAnimal(voA.Padre, this.titFNacPadre, this.lblFNacPadre);
                    this.CargarIdAnimal(voA.Padre, this.titIdPadre, this.lblIdPadre);
                    this.CargarTrazabAnimal(voA.Padre, this.titTrazPadre, this.lblTrazPadre);
                    this.CargarEstadoAnimal(voA.Padre, lblEstPadre);
                    this.CargarCategoriaAnimal(voA.Padre, this.lblCatPadre);
                    if (voA.Padre.Fotos != null) this.CargarFotosAnimal(voA.Padre.Fotos, this.lblFotosPadre, this.ULFotosPadre);
                    // ABUELA PATERNA
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
                    // ABUELO PATERNO
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

        public void CargarSexo(VOAnimal voAnim, Label lbl)
        {
            if (voAnim.esMacho())
            {
                lbl.Text = "M";
                lbl.CssClass = "badge badge-primary";
            }
            else
            {
                lbl.Text = "H";
                lbl.CssClass = "badge badge-pink";
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

        public void CargarConcursosAnimal(List<VOConcurso> lst, GridView gvConc)
        {
            if (lst.Count > 0)
            {
                var lstResult = new List<VOConcurso>();
                // filtro solo los registros en los que concursó y además ganó premios
                foreach (VOConcurso voC in lst)
                {
                    if (voC.ElPremio != "")
                    {
                        lstResult.Add(voC);
                    }
                }
                if (lstResult.Count > 0)
                {
                    gvConc.Visible = true;
                    gvConc.DataSource = lstResult;
                    gvConc.DataBind();
                    this.lblPremios.Visible = true;
                }
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

        public void CargarFotosAnimal(List<AnimalMapper.VOFoto> lst, Label lblTitulo, HtmlGenericControl ul)
        {
            if (lst != null && lst.Count > 0)
            {
                lblTitulo.Visible = true;

                var sb = new StringBuilder();
                // cargo list items recorriendo la lista
                for (int i = 0 ; i < lst.Count; i++)
                {
                    sb.Append("<li>");
                    sb.Append("<a data-rel='colorbox' title='" + lst[i].PieDeFoto + "' href='" + lst[i].Ruta + "' >");
                    sb.Append("<img src='" + lst[i].Thumb + "' alt='150x150' /></a>");
                    sb.Append("</li>");

                    //<li>
                    //    <a data-rel="colorbox" title="YJ3110, Expo Prado 2013" href="img_tamboprp/animales/reg_3110_expoprado2013.jpg">
                    //    <img src="img_tamboprp/animales/animales_thumbs/reg_3110_expoprado2013_th.png" alt="150x150" />
                    //    <!-- optional tags here -->
                    //    <!-- optional caption here -->
                    //    </a>
                    //    <!-- optional tags here -->
                    //    <!-- optional caption here -->
                    //    <!-- optional tools -->
                    //</li>

                }
                ul.InnerHtml += sb.ToString();
            }
        }


        public void CargarFotosAnimalPrincipal(List<AnimalMapper.VOFoto> lst, Label lblTitulo, HtmlGenericControl ul)
        {
            if (lst != null && lst.Count > 0)
            {
                lblTitulo.Visible = true;

                var sb = new StringBuilder();
                // cargo list items recorriendo la lista
                for (int i = 0 ; i < lst.Count; i++)
                {
                    sb.Append("<li>");
                    var pie = "";
                    if (lst[i].PieDeFoto != "") pie = lst[i].PieDeFoto + ". ";
                    var titulo = pie + lst[i].Comentario;
                    sb.Append("<a data-rel='colorbox' title='" + titulo + "' href='" + lst[i].Ruta + "' >");
                    // la primera en grande, las demas como thumbnails
                    if (i == 0)
                    {
                        sb.Append("<img src='" + lst[i].Ruta + "' /></a>");
                    }
                    else
                    {
                        sb.Append("<img src='" + lst[i].Thumb + "' /></a>");
                    }
                    sb.Append("</li>");

                    //<li>
                    //    <a data-rel="colorbox" title="YJ3110, Expo Prado 2013" href="img_tamboprp/animales/reg_3110_expoprado2013.jpg">
                    //    <img src="img_tamboprp/animales/animales_thumbs/reg_3110_expoprado2013_th.png" alt="150x150" />
                    //    <!-- optional tags here -->
                    //    <!-- optional caption here -->
                    //    </a>
                    //    <!-- optional tags here -->
                    //    <!-- optional caption here -->
                    //    <!-- optional tools -->
                    //</li>

                }
                ul.InnerHtml += sb.ToString();
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
                    //this.CargarDdlListSimilares(_similares); VER ANIMALES
                    this.CargarDropDownListSimilares(_similares);
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

        protected void ddlSimilares_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSimil.SelectedIndex > 0)
                this.BuscarAnimal(this.ddlSimil.SelectedValue);
        }

        private void LimpiarRegistro()
        {
            // reseteo valores ficha de animal
            this.Animal.Text = "";
            this.lblNom.Visible = false;
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

            this.lblFotos.Visible = false;
            this.ULFotos.InnerHtml = "";

            this.gvConcursos.Visible = false;
            this.lblPremios.Visible = false;

            this.LimpiarDdlResultadosSimilares();

            // madre
            this.Madre.Text = "";
            this.lblNomMadre.Visible = false;
            this.titFNacMadre.Visible = false;
            this.lblFNacMadre.Visible = false;
            //this.titGenMadre.Visible = false;
            //this.lblGenMadre.Visible = false;
            this.titIdMadre.Visible = false;
            this.lblIdMadre.Visible = false;
            this.titTrazMadre.Visible = false;
            this.lblTrazMadre.Visible = false;
            this.lblEstMadre.Text = "";
            this.lblEstMadre.Visible = false;
            this.lblCatMadre.Visible = false;

            this.titPLMadre.Visible = false;
            this.titPLMadre.CssClass = "label label-default arrowed-right";
            this.lblPLMadre.Visible = false;
            this.gvLactMadre.Visible = false;

            this.lblFotosMadre.Visible = false;
            this.ULFotosMadre.InnerHtml = "";

            //padre
            this.Padre.Text = "";
            this.lblNomPadre.Visible = false;
            this.titFNacPadre.Visible = false;
            this.lblFNacPadre.Visible = false;
            //this.titGenPadre.Visible = false;
            //this.lblGenPadre.Visible = false;
            this.titIdPadre.Visible = false;
            this.lblIdPadre.Visible = false;
            this.titTrazPadre.Visible = false;
            this.lblTrazPadre.Visible = false;
            this.lblEstPadre.Text = "";
            this.lblEstPadre.Visible = false;
            this.lblCatPadre.Visible = false;

            this.lblFotosPadre.Visible = false;
            this.lblFotosPadre.Visible = false;
            this.ULFotosPadre.InnerHtml = "";

            //abuelos maternos
            this.AbuelaM.Text = "";
            this.lblNomAbuelaM.Visible = false;
            this.titFNacAbuelaM.Visible = false;
            this.lblFNacAbuelaM.Visible = false;
            //this.titGenAbuelaM.Visible = false;
            //this.lblGenAbuelaM.Visible = false;
            this.titIdAbuelaM.Visible = false;
            this.lblIdAbuelaM.Visible = false;
            this.titTrazAbuelaM.Visible = false;
            this.lblTrazAbuelaM.Visible = false;
            this.lblEstAbuelaM.Text = "";
            this.lblEstAbuelaM.Visible = false;
            this.lblCatAbuelaM.Visible = false;
            
            this.titPLAbuelaM.Visible = false;
            this.titPLAbuelaM.CssClass = "label label-default arrowed-right";
            this.lblPLAbuelaM.Visible = false;
            this.gvLactAbuelaM.Visible = false;


            this.AbueloM.Text = "";
            this.lblNomAbueloM.Visible = false;
            this.titFNacAbueloM.Visible = false;
            this.lblFNacAbueloM.Visible = false;
            //this.titGenAbueloM.Visible = false;
            //this.lblGenAbueloM.Visible = false;
            this.titIdAbueloM.Visible = false;
            this.lblIdAbueloM.Visible = false;
            this.titTrazAbueloM.Visible = false;
            this.lblTrazAbueloM.Visible = false;
            this.lblEstAbueloM.Text = "";
            this.lblEstAbueloM.Visible = false;
            this.lblCatAbueloM.Visible = false;
 
            //abuelos paternos
            this.AbuelaP.Text = "";
            this.lblNomAbuelaP.Visible = false;
            this.titFNacAbuelaP.Visible = false;
            this.lblFNacAbuelaP.Visible = false;
            //this.titGenAbuelaP.Visible = false;
            //this.lblGenAbuelaP.Visible = false;
            this.titIdAbuelaP.Visible = false;
            this.lblIdAbuelaP.Visible = false;
            this.titTrazAbuelaP.Visible = false;
            this.lblTrazAbuelaP.Visible = false;
            this.lblEstAbuelaP.Text = "";
            this.lblEstAbuelaP.Visible = false;
            this.lblCatAbuelaP.Visible = false;
            
            this.titPLAbuelaP.Visible = false;
            this.titPLAbuelaP.CssClass = "label label-default arrowed-right";
            this.lblPLAbuelaP.Visible = false;
            this.gvLactAbuelaP.Visible = false;

            this.AbueloP.Text = "";
            this.lblNomAbueloP.Visible = false;
            this.titFNacAbueloP.Visible = false;
            this.lblFNacAbueloP.Visible = false;
            //this.titGenAbueloP.Visible = false;
            //this.lblGenAbueloP.Visible = false;
            this.titIdAbueloP.Visible = false;
            this.lblIdAbueloP.Visible = false;
            this.titTrazAbueloP.Visible = false;
            this.lblTrazAbueloP.Visible = false;
            this.lblEstAbueloP.Text = "";
            this.lblEstAbueloP.Visible = false;
            this.lblCatAbueloP.Visible = false;
        }

        private void LimpiarDdlResultadosSimilares()
        {
            this._similares.Clear();
            this.ddlSimil.Visible = false;
            this.ddlSimil.DataSource = null;
            this.ddlSimil.DataBind();
        }

    }
}