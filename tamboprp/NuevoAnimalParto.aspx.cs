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
        //private VOParto _voParto = new VOParto();
        //private List<VOAnimal> _listCrias = new List<VOAnimal>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
            //   (Session["EsLector"] != null && !(bool)Session["EsLector"]))
            //{
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.LimpiarFormularioCria();
                    this.LimpiarFormularioParto();
                }
            //}
            //else Response.Redirect("~/Default.aspx", true);
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

        protected void btn_NuevaCria(object sender, EventArgs e)
        {
            try
            {
                var ok = true;
                var voParto = this.CargarParto();
                var voCria = this.CargarCria();

                if (voParto!=null && this.checkVivo.Checked && voCria != null)
                {
                    if (Fachada.Instance.AnimalInsert(voCria))
                    {
                        if (!Fachada.Instance.ExisteParto(voParto))
                        {
                            ok = Fachada.Instance.PartoInsert(voParto);
                            if (ok)
                            {
                                //this.lblStatus.Text = "Se guardó el parto y la cría";
                            }
                        }
                    }
                }
                else
                {
                    if (voParto != null && !Fachada.Instance.ExisteParto(voParto))
                    {
                        ok = Fachada.Instance.PartoInsert(voParto);
                        if (ok)
                        {
                            //this.lblStatus.Text = "Se guardó el parto y la cría";
                        }
                    }
                }
                
                this.CargarListaCriasIngresadas(voParto);
                this.LimpiarFormularioCria();
            }
            catch (Exception ex)
            {

            }
        }

        private VOAnimal CargarCria()
        {
            var voCria = new VOAnimal();
            if (this.checkVivo.Checked)
            {
                // alta de una cría, se incluye en la lsta de crisa
                voCria.Registro = this.fRegCria.Value;
                //voCria.Fecha_nacim = DateTime.Parse(this.mydate.Value);
                string strDate = Request.Form["mydate"];
                var fecha = DateTime.Parse(strDate, new CultureInfo("en-US"));
                voCria.Fecha_nacim = fecha;
                voCria.Gen = int.Parse(this.fGen.Value); // ojo
                voCria.Identificacion = this.fIdentif.Value;
                voCria.Reg_madre = this.fRegistro.Text;
                voCria.Reg_padre = this.fRegPadre.Value;
                voCria.Reg_trazab = this.fTraz.Value;
                voCria.Sexo = this.checkSexo.Checked ? 'H' : 'M';
                voCria.IdCategoria = voCria.Sexo == 'H' ? 1 : 7;
                voCria.Nombre = this.fNombre.Value;
                voCria.Origen = this.fOrigen.Value;
            }
            return voCria;
        }

        private VOParto CargarParto()
        {
            if (this.fRegistro.Text != "") return null;
            // registro con la primer cría el parto
            var voParto = new VOParto();
            voParto.Registro = this.fRegistro.Text;
            voParto.Observaciones = this.checkVivo.Checked ? "" : "NACIO MUERTO";
            voParto.Comentarios = this.fComentario.Value;
            //voParto.Fecha = DateTime.Parse(this.mydate.Value);
            string strDate = Request.Form["mydate"];
            var fecha = DateTime.Parse(strDate, new CultureInfo("en-US"));
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
            this.mydateServ.Value = "";
            this.fRegPadre.Value = "";
            this.fComentario.Value = "";

            this.fOrigen.Value = "";
            this.fGen.Value = "";
        }

        protected void EventosRegistro(object sender, EventArgs e)
        {
            if (this.fRegistro.Text != "")
            {
                var ultServicio = Fachada.Instance.GetUltimoServicio(this.fRegistro.Text);
                var ultDiagnostico = Fachada.Instance.GetUltimoDiagnostico(this.fRegistro.Text);
                if (ultServicio != null && ultDiagnostico != null && ultDiagnostico.Diagnostico == 'P')
                {
                    this.fRegPadre.Value = ultServicio.Reg_padre;
                    this.mydateServ.Value = ultServicio.Fecha.ToShortDateString();
                }
                
            }
        }
    }
}