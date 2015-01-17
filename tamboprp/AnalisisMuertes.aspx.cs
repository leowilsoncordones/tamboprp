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

        //private void CargarResumen(List<BajaMapper.VOEnfermedad> lst)
        //{
        //    var sb = new StringBuilder();
        //    int cant = lst.Count;
        //    for (int i = 0; i < cant; i++)
        //    {
        //        sb.Append("<li class='bigger-110'><i class='ace-icon fa fa-caret-right blue'></i>");
        //        sb.Append("<span> Enf_1 <small>Cant - Porcentaje</small> &nbsp;&nbsp;</span>");
        //        sb.Append("<strong>" + "" + "</strong></li>");
        //    }
        //    this.listaEnfResumen.InnerHtml += sb.ToString();
        //}

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

        //private void ListaAcordeonMuertes(List<VOBaja> lst)
        //{
        //    var sb = new StringBuilder();
        //    //for (int i = 0; i < lst.Count; i++)
        //    for (int i = 0; i < 10; i++)
        //    {
        //        //if (lst[i].Titulo)
        //        //{
        //            sb.Append("<div class='panel panel-default'>");
        //                sb.Append("<div class='panel-heading'>");
        //                    sb.Append("<h4 class='panel-title'>");
        //                    sb.Append("<a class='accordion-toggle collapsed' data-toggle='collapse' data-parent='#accordion' href='#collapse_" + i + "'>");
        //                    sb.Append(lst[i].Enfermedad.ToString() + " - Cantidad: " + lst[i].Cantidad);
        //                    sb.Append("</a>");
        //                    sb.Append("</h4>");
        //                sb.Append("</div>");
        //                sb.Append("<div id='collapse_" + i + " class='panel-collapse collapse'>");
        //                    sb.Append("<div class='panel-body'>");
        //                    // lista de registros
        //                    sb.Append(lst[i].Registro + " " + lst[i].Fecha);
        //                    sb.Append("</div>");
        //                sb.Append("</div>");
        //            sb.Append("</div>");
        //            this.accordion.InnerHtml += sb.ToString();
        //        //}
        //    }
        //}
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
