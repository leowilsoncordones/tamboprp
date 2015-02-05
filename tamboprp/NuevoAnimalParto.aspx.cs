using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class NuevoAnimalParto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    //this.fComentario.Value = "";
                    //this.mydateServ.Value = "";
                    this.LimpiarTabla();
                    this.LimpiarFormularioCria();
                    this.LimpiarFormularioParto();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Animales", "Animales.aspx"));
            list.Add(new VoListItemDuplaString("Ingreso de parto y sus crías", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void btn_NuevaCria(object sender, EventArgs e)
        {
            try
            {
                var u = (VOUsuario)Session["Usuario"];
                //var registro = this.fRegistro.Text;
                //var regCria = this.fRegCria.Value;
                
                var voParto = this.CargarParto();
                if (voParto != null)
                {
                    var voCria = this.CargarCria();
                    var existeMadre = Fachada.Instance.AnimalExiste(voCria.Reg_madre);
                    var existePadre = Fachada.Instance.AnimalExiste(voCria.Reg_padre);
                    var existeCria = Fachada.Instance.AnimalExiste(voCria.Registro);
                    
                    if (existeCria) this.lblStatus.Text += " Ya existe una cría con registro" + voCria.Registro;
                    else if (!existeMadre) this.lblStatus.Text = " No existe una vaca con registro " + voCria.Reg_madre;
                    else if (!existePadre && voCria.Reg_padre != "M-DESCONOC") this.lblStatus.Text = " No existe un toro con registro " + voCria.Reg_padre;

                    if (u != null && !existeCria && existeMadre && existePadre)
                    {
                        if (this.checkVivo.Checked)
                        {
                            if (Fachada.Instance.AnimalInsert(voCria, u.Nickname))
                            {
                                if (!Fachada.Instance.ExisteParto(voParto))
                                {
                                    if (Fachada.Instance.PartoInsert(voParto, u.Nickname))
                                    {
                                        this.lblStatus.Text = "Se guardó el parto y la cría";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!Fachada.Instance.ExisteParto(voParto))
                            {
                                if (Fachada.Instance.PartoInsert(voParto, u.Nickname))
                                {
                                    this.lblStatus.Text = "Se guardó el parto";
                                }
                            }
                        }
                        this.CargarListaCriasIngresadas(voParto);
                        this.LimpiarFormularioCria();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private VOAnimal CargarCria()
        {
            var registro = this.fRegistro.Text;
            var voCria = new VOAnimal();
            if (this.checkVivo.Checked)
            {
                // alta de una cría, se incluye en la lsta de crisa
                voCria.Registro = this.fRegCria.Value;
                //voCria.Fecha_nacim = DateTime.Parse(this.mydate.Value);
                string strDate = Request.Form["mydate"];
                var fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
                voCria.Fecha_nacim = fecha;
                int gen;
                bool ok = int.TryParse(this.fGen.Value, out gen);
                if (ok) voCria.Gen = gen;
                //voCria.Gen = int.Parse(this.fGen.Value); // ojo
                voCria.Identificacion = this.fIdentif.Value;
                voCria.Reg_madre = registro;
                voCria.Reg_padre = this.fRegPadre.Value == "" ? "M-DESCONOC" : this.fRegPadre.Value;
                voCria.Reg_trazab = this.fTraz.Value;
                voCria.Sexo = this.checkSexo.Checked ? 'H' : 'M';
                voCria.IdCategoria = voCria.Sexo == 'H' ? 1 : 7;
                voCria.Nombre = this.fNombre.Value;
                voCria.Origen = this.fOrigen.Value;
            }
            return voCria;
        }

        // si cargo mas de una cría ver que no se cambie el datepicker de fecha (se resetea a hoy)

        private VOParto CargarParto()
        {
            var registro = this.fRegistro.Text;
            if (registro == "") return null;
            // registro con la primer cría el parto
            var voParto = new VOParto();
            voParto.Registro = this.fRegistro.Text;
            voParto.Observaciones = this.checkVivo.Checked ? "" : "NACIO MUERTO";
            voParto.Comentarios = this.fComentario.Value;
            //voParto.Fecha = DateTime.Parse(this.mydate.Value);
            string strDate = Request.Form["mydate"];
            var fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (fecha > DateTime.Today)
            {
                this.lblStatus.Text = "No puede ingresar un parto de una fecha futura";
                return null;
            }
            voParto.Fecha = fecha;
            voParto.Reg_hijo = this.checkVivo.Checked ? this.fRegCria.Value : "DESCONOCIDO";
            voParto.Sexo_parto = this.checkSexo.Checked ? 'H' : 'M';
            return voParto;
        }

        public void CargarListaCriasIngresadas(VOParto voP)
        {
            var lstCriasIngresadas = Fachada.Instance.GetCriasIngresadasParto(voP);
            if (lstCriasIngresadas.Count > 0)
            {
                this.panelCriasIngresadas.Visible = true;
                this.gvAnimales.DataSource = lstCriasIngresadas;
                this.gvAnimales.DataBind();
            }
        }

        private void LimpiarTabla()
        {
            this.panelCriasIngresadas.Visible = false;
            this.gvAnimales.DataSource = null;
            this.gvAnimales.DataBind();
        }

        protected void btn_LimpiarFormulario(object sender, EventArgs e)
        {
            this.LimpiarFormularioCria();
            this.LimpiarTabla();
            this.LimpiarFormularioParto();
            this.lblStatus.Text = "";
        }

        private void LimpiarFormularioCria()
        {
            this.fTraz.Value = "";
            this.fRegCria.Value = "";
            this.fNombre.Value = "";
            this.fIdentif.Value = "";
            //this.checkVivo.Checked = true;
            //this.checkSexo.Checked = true;
        }

        private void LimpiarFormularioParto()
        {
            this.fRegistro.Text = "";
            this.fRegPadre.Value = "";
            this.fComentario.Value = "";
            this.mydateServ.Value = "";
            this.fOrigen.Value = "PROPIETARIO";
            this.fGen.Value = "";
            this.lblStatus.Text = "";
            //var corp = (VOEmpresa)Session["Corporativo"];
            //if (corp != null) this.lblLetraSistema.Text = corp.LetraSistema;
        }

        protected void EventosRegistro(object sender, EventArgs e)
        {
            this.lblStatus.Text = "";
            this.LimpiarFormularioCria();
            
            var registro = this.fRegistro.Text;
            if (registro != "" && Fachada.Instance.AnimalExiste(registro))
            {
                var madreAnimal = Fachada.Instance.GetAnimalByRegistro(registro);
                if (madreAnimal != null)
                {
                    var corp = (VOEmpresa)Session["Corporativo"];
                    if (corp != null) this.lblLetraSistema.Text = corp.LetraSistema;
                    // Calculo algunos valores sugeridos para la cria
                    var genCria = madreAnimal.Gen + 1;
                    var strGen = genCria.ToString();
                    this.fGen.Value = strGen;
                    if (genCria < 10) strGen = "0" + strGen;
                    if (corp != null && strGen != "")
                    {
                        this.fIdentif.Value = strGen + corp.LetraSistema + this.fRegCria.Value;
                    }
                }

                var ultServicio = Fachada.Instance.GetUltimoServicioParaParto(registro);
                var ultDiagnostico = Fachada.Instance.GetUltimoDiagnosticoParaParto(registro);
                if (ultDiagnostico != null && ultDiagnostico.Diagnostico == 'P')
                {
                    if (ultServicio != null)
                    {
                        this.fRegPadre.Value = ultServicio.Reg_padre;
                        this.mydateServ.Value = ultServicio.Fecha.ToShortDateString();
                    }
                    else
                    {
                        this.lblStatus.Text = "No hay un servicio correspondiente para " + registro;
                    }
                }
                else
                {
                    this.lblStatus.Text = "No hay un diagnóstico de preñez confirmado correspondiente para " + registro;
                }
            }
            else
            {
                if (registro == "") this.lblStatus.Text = "Ingrese un registro no vacío";
                else this.lblStatus.Text = "No existe una vaca con el registro " + registro;
            }
            
        }
    }
}