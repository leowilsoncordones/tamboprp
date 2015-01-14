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
    public partial class CategConcurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarCategConcurso();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Cabaña", "Cabana.aspx"));
            list.Add(new VoListItemDuplaString("Categorías de concurso", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvCategConcurso.DataSource = null;
            this.gvCategConcurso.DataBind();
            this.titCantEnf.Visible = false;
            this.lblCantEnf.Text = "";
            this.lblStatus.Text = "";
            this.newCateg.Text = "";
        }

        private void CargarCategConcurso()
        {
            var lst = Fachada.Instance.GetCategoriasConcursoAll();
            this.gvCategConcurso.DataSource = lst;
            this.gvCategConcurso.DataBind();
            this.titCantEnf.Visible = true;
            this.lblCantEnf.Text = lst.Count.ToString();
        }

        protected void GvCategConcurso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarCategConcurso();
        }


        protected void btn_GuardarCategConcurso(object sender, EventArgs e)
        {
            try
            {
                if (this.GuardarCategConcurso())
                {
                    this.lblStatus.Text = "La categoría se ha guardado con éxito";
                    this.CargarCategConcurso();
                }
                else
                {
                    this.lblStatus.Text = "La categoría no se ha podido guardar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool GuardarCategConcurso()
        {
            if (this.newCateg.Text != "")
            {
                var categC = new CategoriaConcurso(this.newCateg.Text);
                return Fachada.Instance.CategConcursoInsert(categC);
            }
            else
            {
                this.lblStatus.Text = "Ingrese un nombre";
            }
            return false;
        }

    }
}