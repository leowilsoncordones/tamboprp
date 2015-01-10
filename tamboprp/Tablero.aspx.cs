using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class Tablero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
              // (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]))
            //{
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.SetIndicadoresTablero();
                    this.SetAlertasYNotificacionesTablero();
                    this.SetExtrasGraficaCategorías();
                }
            //}
            //else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Tablero", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAll()
        {
            var hoy = DateTime.Now.ToString("yyyy-MM-dd");
            var newDate = DateTime.Now.AddYears(-1);
            string fecha1 = newDate.ToString("yyyy-MM-dd");
            return Fachada.Instance.GetControlesTotalesEntreDosFechas(fecha1, hoy);
        }

        public void SetIndicadoresTablero()
        {
            var ind = Fachada.Instance.GetIndicadoresTablero();
            
            // #section:pages/dashboard.infobox

            var voVacaOrdene = ind.VacasEnOrdene;
            var sb = new StringBuilder();
            //sb.Append("<div class='infobox-icon'><i class='ace-icon fa fa-gears'></i></div>");
            sb.Append("<div class='infobox-chart'>");
                sb.Append("<span class='sparkline' data-values='" + voVacaOrdene.DataValue + "'>");
                    sb.Append("<canvas width='44' height='27' style='display: inline-block; width: 44px; height: 27px; vertical-align: top;'></canvas>");
                sb.Append("</span>");
            sb.Append("</div>");
                sb.Append("<div class='infobox-data'>");
                    sb.Append("<span class='infobox-data-number'>" + voVacaOrdene.Valor +"</span>");
                    sb.Append("<div class='infobox-content'>"+ voVacaOrdene.Texto +"</div>");
                sb.Append("</div>");
            //sb.Append("<div class='stat stat-success'>" + voVacaOrdene.Porcentaje + "</div>");
            if (this.fVacasOrdene != null) this.fVacasOrdene.InnerHtml = sb.ToString();

            var voPromLeche = ind.PromLeche;
            var sb0 = new StringBuilder();
            sb0.Append("<div class='infobox-icon'><i class='ace-icon fa fa-tint'></i></div>");
            sb0.Append("<div class='infobox-data'>");
                sb0.Append("<span class='infobox-data-number'>" + voPromLeche.Valor + "<small>kg</small></span>");
                sb0.Append("<div class='infobox-content'>" + voPromLeche.Texto + "</div>");
            sb0.Append("</div>");
            if (this.fPromLeche != null) this.fPromLeche.InnerHtml = sb0.ToString();

            var voLecheUltControl = ind.LecheUltControl;
            var sb1 = new StringBuilder();
            //sb1.Append("<div class='infobox-chart'>");
                //sb1.Append("<span class='sparkline' data-values='" + voLecheUltControl.DataValue + "'>");
			        //sb1.Append("<canvas width='44' height='27' style='display: inline-block; width: 44px; height: 27px; vertical-align: top;'></canvas>");
			    //sb1.Append("</span>");
            //sb1.Append("</div>");
            sb1.Append("<div class='infobox-icon'><i class='ace-icon fa fa-gears'></i></div>");
            sb1.Append("<div class='infobox-data'>");
                sb1.Append("<span class='infobox-data-number'>" + voLecheUltControl.Valor + "<small>kg</small></span>");
                sb1.Append("<div class='infobox-content'>" + voLecheUltControl.Texto + "</div>");
            sb1.Append("</div>");
            //sb1.Append("<div class='stat stat-" + voLecheUltControl.Status + "'>" + voLecheUltControl.Porcentaje + "</div>");
            if (this.fLecheUltControl != null) this.fLecheUltControl.InnerHtml = sb1.ToString();

            var voPromDiasLact = ind.PromDiasLactancias;
            var sb2 = new StringBuilder();
            sb2.Append("<div class='infobox-icon'><i class='ace-icon fa fa-calendar'></i></div>");
            sb2.Append("<div class='infobox-data'>");
                sb2.Append("<span class='infobox-data-number'>" + voPromDiasLact.Valor + "<small> días</small></span>");
                sb2.Append("<div class='infobox-content'>" + voPromDiasLact.Texto + "</div>");
            sb2.Append("</div>");
            if (this.fPromDiasLact != null) this.fPromDiasLact.InnerHtml = sb2.ToString();

            var voPartos = ind.PartosMes;
            var sb3 = new StringBuilder();
            sb3.Append("<div class='infobox-icon'><i class='ace-icon fa fa-tags'></i></div>");
            sb3.Append("<div class='infobox-data'>");
                sb3.Append("<span class='infobox-data-number'>" + voPartos.Valor + "</span>");
                sb3.Append("<div class='infobox-content'>" + voPartos.Texto + "</div>");
            sb3.Append("</div>");
            sb3.Append("<div class='stat stat-" + voPartos.Status + "'>" + voPartos.Porcentaje + "</div>");
            if (this.fPartos != null) this.fPartos.InnerHtml = sb3.ToString();

            var voAbortos = ind.AbortosAnual;
            var sb4 = new StringBuilder();
            sb4.Append("<div class='infobox-icon'><i class='ace-icon fa fa-thumbs-o-down'></i></div>");
            sb4.Append("<div class='infobox-data'>");
                sb4.Append("<span class='infobox-data-number'>" + voAbortos.Valor + "</span>");
                sb4.Append("<div class='infobox-content'>" + voAbortos.Texto + "</div>");
            sb4.Append("</div>");
            sb4.Append("<div class='stat stat-" + voAbortos.Status + "'>" + voAbortos.Porcentaje + "</div>");
            if (this.fAbortos != null) this.fAbortos.InnerHtml = sb4.ToString();

            // #section:pages/dashboard.infobox.dark

            var voNacidos = ind.NacidosAnual;
            var sb5 = new StringBuilder();
            sb5.Append("<div class='infobox-icon'><i class='ace-icon fa fa-github-alt'></i></div>");
            sb5.Append("<div class='infobox-data'>");
                sb5.Append("<div class='infobox-content'><a href='../ListPartos.aspx' class='white' title='Listado de partos y nacimientos' >" + voNacidos.Texto + "</a></div>");
                sb5.Append("<div class='infobox-content'>" + voNacidos.Valor + "</div>");
            sb5.Append("</div>");
            if (this.fNacidos != null) this.fNacidos.InnerHtml = sb5.ToString();

            var voPrenadas = ind.Prenadas;
            var sb6 = new StringBuilder();
            sb6.Append("<div class='infobox-icon'><i class='ace-icon fa fa-hand-o-right'></i></div>");
            sb6.Append("<div class='infobox-data'>");
                sb6.Append("<div class='infobox-content'><a href='../Inseminaciones.aspx' class='white' title='Preñez confirmada' >" + voPrenadas.Texto + "</a></div>");
                sb6.Append("<div class='infobox-content'>" + voPrenadas.Valor + "</div>");
            sb6.Append("</div>");
            if (this.fPrenadas != null) this.fPrenadas.InnerHtml = sb6.ToString();

            var voToroMasUsado = ind.ToroMasUsado;
            var sb7 = new StringBuilder();
            sb7.Append("<div class='infobox-progress'>");
                sb7.Append("<div class='easy-pie-chart percentage' data-percent='" + voToroMasUsado.Porcentaje + "' data-size='22' style='height: 39px; width: 39px; line-height: 38px;'>");
				    sb7.Append("<span class='percent'>" + voToroMasUsado.Porcentaje + "</span>");
			        sb7.Append("<canvas height='39' width='39'></canvas></div>");
		        sb7.Append("</div>");
            sb7.Append("<div class='infobox-data'>");
                sb7.Append("<div class='infobox-content'><a href='../AnalisisToros.aspx' class='white' title='Toro más usado' >" + voToroMasUsado.Texto + "</a></div>");
                sb7.Append("<div class='infobox-content'>" + voToroMasUsado.Valor + "</div>");
            sb7.Append("</div>");
            if (this.fToroMasUsado != null) this.fToroMasUsado.InnerHtml = sb7.ToString();

        }

        public void SetAlertasYNotificacionesTablero()
        {
            // #section:alertas y notificaciones
            var lstAlertas = Fachada.Instance.GetAlertasYNotificacionesTablero();
            var sb = new StringBuilder();
            sb.Append("<h4 class='smaller lighter blue'><i class='ace-icon fa icon-animated-bell fa-bell-o'></i> Alertas y notificaciones</h4>");
            sb.Append("<div class='space-6'></div>");

            for (int i = 0; i < lstAlertas.Count; i++)
            {
                var item = lstAlertas[i];
                sb.Append("<div class='alert alert-" + item.Status + "'>");
                    sb.Append("<button class='close' data-dismiss='alert'><i class='ace-icon fa fa-times'></i></button>");
                    sb.Append("<i class='ace-icon fa " + item.Icono + "'></i><strong> " + item.Valor + "</strong> " + item.Texto);
                    sb.Append(" <a href='" + item.Link + "' class='grey' title='" + item.LinkAlt + "' ><small> ver</small></a>");
                sb.Append("</div>");
            }
            if (this.fAlertas != null) this.fAlertas.InnerHtml = sb.ToString();
        }

        public void SetExtrasGraficaCategorías()
        {
            // #section:custom/extra.grid 
            var lstExtras = Fachada.Instance.GetExtrasGraficaCategorías(); ;
            var sb = new StringBuilder();

            for (int i = 0; i < lstExtras.Count; i++)
            {
                var item = lstExtras[i];
                sb.Append("<div class='grid3'>");
                sb.Append("<span class='grey'><i class='ace-icon fa " + item.Icono + " fa-2x " + item.Color + "'></i>&nbsp; ");
                sb.Append("<a href='" + item.Link + "' class='grey' title='" + item.LinkAlt + "' >" + item.Texto + "</a></span>");
                sb.Append("<h4 class='bigger pull-right'>" + item.Valor + "</h4>");
                sb.Append("</div>");
            }
            if (this.fExtraGrid != null) this.fExtraGrid.InnerHtml = sb.ToString();
        }
    }
}