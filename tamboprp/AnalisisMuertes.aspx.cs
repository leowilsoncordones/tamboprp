using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class AnalisisMuertes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.GetResumenMuertes();
                this.GetListaMuertes();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sanidad", "Sanidad.aspx"));
            list.Add(new VoListItemDuplaString("Información sobre muertes", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvMuertes.DataSource = null;
            this.gvMuertes.DataBind();
            this.gvMuertesResumen.DataSource = null;
            this.gvMuertesResumen.DataBind();
            this.lblCantAnimales.Text = "";
        }

        private void GetListaMuertes()
        {
            int anio = 2014;
            var listTemp = Fachada.Instance.GetMuertesPorAnio(anio);
            this.gvMuertes.DataSource = listTemp;
            this.gvMuertes.DataBind();
            this.titCantAnimales.Visible = true;
            this.lblCantAnimales.Text = listTemp.Count.ToString();
            this.titTotalMuertes.Visible = true;
            this.lblTotalMuertes.Text = listTemp.Count.ToString();
        }

        private void GetResumenMuertes()
        {
            int anio = 2014;
            var listTemp = Fachada.Instance.GetCantidadMuertesPorEnfermedadPorAnio(anio);
            this.gvMuertesResumen.DataSource = listTemp;
            this.gvMuertesResumen.DataBind();
            this.titEnfDif.Visible = true;
            this.lblEnfDif.Text = listTemp.Count.ToString();
        }

        protected void GvMuertes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetListaMuertes();
        }

        protected void GvMuertesResumen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.GetResumenMuertes();
        }

        protected void excelExport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void pdfExport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void print_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
