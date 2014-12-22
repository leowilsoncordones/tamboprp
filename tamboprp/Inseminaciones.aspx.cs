using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class Inseminaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            this.CargarGrilla();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Inseminaciones con diágnostico de preñez confirmado", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarGrilla()
        {
            // DATEPICKER, por defecto diagnosticos del ultimo mes
            var date = new DateTime(2014, 09, 01);
            //var date = new DateTime();
            //date = DateTime.Today;
            var list = Fachada.Instance.GetInseminacionesExitosas(date);
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvInseminaciones.DataSource = list;
            this.gvInseminaciones.DataBind();

            this.ConsolidarResumen(list);
        }

        public void ConsolidarResumen(List<Diag_PrenezMapper.VODiagnostico> lst)
        {
            var lstInseminadores = new List<VoListItem2>();
            bool esta = false;
            // Recorro la lista para consolidar la inseminaciones por Inseminador
            for (int i = 0; i < lst.Count; i++)
            {
                esta = false;
                for (int j = 0; j < lstInseminadores.Count; j++)
                {
                    if (lst[i].IdInseminador.Equals(lstInseminadores[j].Id))
                    {
                        lstInseminadores[j].Num++;
                        esta = true;
                        break;
                    }
                }
                if (!esta)
                {
                    var it = new VoListItem2(lst[i].IdInseminador, 1, lst[i].Inseminador);
                    lstInseminadores.Add(it);
                }
            }
            this.ImprimirResumen(lstInseminadores);
            this.lblTotal.Text = lst.Count.ToString();
        }

        public void ImprimirResumen(List<VoListItem2> lstInseminadores)
        {
            // agrego al cuadro de resumen la lista de inseminadores consolidada
            var sb = new StringBuilder();
            this.listaInseminadores.InnerHtml = "";
            for (int j = 0; j < lstInseminadores.Count; j++)
            {
                sb.Append("<li class='bigger-110'><i class='ace-icon fa fa-caret-right blue'></i>");
                sb.Append("<span><strong>" + lstInseminadores[j].Num + "</strong> &nbsp;&nbsp;</span>");
                sb.Append(lstInseminadores[j].Nombre + "</li>");
            }
            this.listaInseminadores.InnerHtml += sb.ToString();
        }


        protected void GvInseminaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarGrilla();
        }

    }
}