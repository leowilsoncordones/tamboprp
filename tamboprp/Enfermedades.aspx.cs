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
    public partial class Enfermedades : System.Web.UI.Page
    {
        private List<Enfermedad> _listTemp = new List<Enfermedad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarEnfermedades();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sanidad", "Sanidad.aspx"));
            list.Add(new VoListItemDuplaString("Tabla de Enfermedades", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvEnfermedades.DataSource = null;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
            this.lblStatus.Text = "";
            this.newEnfermedad.Text = "";
        }

        private void CargarEnfermedades()
        {
            _listTemp = Fachada.Instance.GetEnfermedades();
            this.gvEnfermedades.DataSource = _listTemp;
            this.gvEnfermedades.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = _listTemp.Count.ToString();
        }

        protected void GvEnfermedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            CargarEnfermedades();
        }

        protected void btn_GuardarEnfermedad(object sender, EventArgs e)
        {
            try
            {
                if (this.GuardarEnfermedad())
                {
                    this.lblStatus.Text = "La enfermedad se ha guardado con éxito";
                    this.CargarEnfermedades();
                }
                else
                {
                    this.lblStatus.Text = "La enfermedad no se ha podido guardar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool GuardarEnfermedad()
        {
            if (this.newEnfermedad.Text != "")
            {
                var enf = new Enfermedad(this.newEnfermedad.Text);
                return Fachada.Instance.EnfermedadInsert(enf);
            }
            else
            {
                this.lblStatus.Text = "Ingrese un nombre";
            }
            return false;
        }

    }
}