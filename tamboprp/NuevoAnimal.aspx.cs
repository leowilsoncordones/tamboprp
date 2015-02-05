using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class NuevoAnimal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.CargarDdlCategorias();
                    this.LimpiarFormulario();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Nuevo animal", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void CargarDdlCategorias()
        {
            //var catMap = new CategoriaMapper();
            //List<Categoria> lst = catMap.get();

            var lstCateg = Fachada.Instance.GetCategoriasAnimalAll();
            this.ddlCategorias.DataSource = lstCateg;
            this.ddlCategorias.DataTextField = "Nombre";
            this.ddlCategorias.DataValueField = "Id_categ";
            this.ddlCategorias.DataBind();
            
            // valor por defecto semen y macho
            this.ddlCategorias.SelectedIndex = 9;
        }
        
        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            this.fTraz.Value = "";
            this.fRegistro.Value = "";
            this.fRegMadre.Value = "";
            this.fRegPadre.Value = "";
            this.fNombre.Value = "";
            this.fIdentif.Value = "";
            this.fGen.Value = "";
            this.fOrigen.Value = "";
            //this.fOrigen.Value = "PROPIETARIO";
            // valor por defecto semen y macho
            this.checkSexo.Checked = false;
            this.ddlCategorias.SelectedIndex = 9;
            //this.lblStatus.Text = "";
        }


        protected void btn_GuardarAnimal(object sender, EventArgs e)
        {
            try
            {
                var registro = this.fRegistro.Value;
                if (registro != "")
                {
                    if (!Fachada.Instance.AnimalExiste(registro))
                    {
                        var nuevoAnimal = new VOAnimal();
                        nuevoAnimal.Registro = registro;
                        nuevoAnimal.Identificacion = this.fIdentif.Value;
                        int gen;
                        bool ok = int.TryParse(this.fGen.Value, out gen);
                        if (ok) nuevoAnimal.Gen = gen;
                        nuevoAnimal.Nombre = this.fNombre.Value;
                        nuevoAnimal.Reg_trazab = this.fTraz.Value;
                        nuevoAnimal.Sexo = this.checkSexo.Checked ? 'H' : 'M';
                        nuevoAnimal.IdCategoria = int.Parse(this.ddlCategorias.SelectedItem.Value);
                        string strDate = Request.Form["mydate"];
                        var fecha = new DateTime(1900,1,1);
                        if (strDate != "") fecha = DateTime.Parse(strDate, new CultureInfo("en-US"));
                        nuevoAnimal.Fecha_nacim = fecha;
                        //nuevoAnimal.Fecha_nacim = DateTime.Parse(this.mydate.Value);
                        nuevoAnimal.Origen = this.fOrigen.Value;
                        nuevoAnimal.Reg_padre = this.fRegPadre.Value == "" ? "M-DESCONOC" : this.fRegPadre.Value;
                        nuevoAnimal.Reg_madre = this.fRegMadre.Value == "" ? "H-DESCONOC" : this.fRegMadre.Value;
                        var u = (VOUsuario)Session["Usuario"];
                        var existeMadre = Fachada.Instance.AnimalExiste(nuevoAnimal.Reg_madre);
                        var existePadre = Fachada.Instance.AnimalExiste(nuevoAnimal.Reg_padre);
                        if (existeMadre && existePadre && u != null)
                        {
                            if (Fachada.Instance.AnimalInsert(nuevoAnimal, u.Nickname))
                            {
                                this.lblStatus.Text = "El animal se guardó";
                                this.LimpiarFormulario();
                            }
                            else
                            {
                                this.lblStatus.Text = "El animal no se pudo guardar";
                            }
                        }
                    }
                    else this.lblStatus.Text = "El registro ya existe";
                }
                else this.lblStatus.Text = "Ingrese un registro";
            }
            catch (Exception ex)
            {
                this.lblStatus.Text = "Ocurrió un error al guardar el animal";
            }
        }
    }
}