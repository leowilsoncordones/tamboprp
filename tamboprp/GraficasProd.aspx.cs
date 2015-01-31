using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace tamboprp
{
    public partial class GraficasProd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    CargarDdl();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = false;
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Producción", "Produccion.aspx"));
            list.Add(new VoListItemDuplaString("Gráficas de Producción", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarDdl()
        {
            this.ddlFechasGraf.Items.Add(new ListItem("Este año", "0"));
            this.ddlFechasGraf.Items.Add(new ListItem("Último año", "1"));
            this.ddlFechasGraf.Items.Add(new ListItem("Seleccionar fechas", "2"));
        }


        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAnioCorriente()
        {
            var hoy = DateTime.Now.ToString("yyyy-MM-dd");
            int year = DateTime.Now.Year;
            var primerDia = new DateTime(year, 1, 1);
            string fecha1 = primerDia.ToString("yyyy-MM-dd");
            return Fachada.Instance.GetControlesTotalesEntreDosFechas(fecha1, hoy);
        }

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> ControlTotalGetUltimoAnio()
        {
            var hoy = DateTime.Now.ToString("yyyy-MM-dd");
            var newDate = DateTime.Now.AddYears(-1);
            string fecha1 = newDate.ToString("yyyy-MM-dd");
            return Fachada.Instance.GetControlesTotalesEntreDosFechas(fecha1, hoy);
        }

        [WebMethod]
        public static List<Controles_totalesMapper.VOControlTotal> GetControlesTotalesEntreDosFechas(string fecha1, string fecha2)
        {
            return Fachada.Instance.GetControlesTotalesEntreDosFechas(fecha1, fecha2);
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            switch (this.ddlFechasGraf.SelectedValue)
            {
                case "0":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "1":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(1)", true);
                    this.pnlFechasGraf.Visible = false;
                    break;
                case "2":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
                    this.pnlFechasGraf.Visible = true;
                    break;
            }
        }

        //protected void ddlFechasGraf_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (this.ddlFechasGraf.SelectedValue)
        //    {
        //        case "0":
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
        //            this.pnlFechasGraf.Visible = false;
        //            break;
        //        case "1":
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
        //            this.pnlFechasGraf.Visible = false;
        //            break;
        //        case "2":
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "GetValoreLeche", "GetValoreLeche(0)", true);
        //            this.pnlFechasGraf.Visible = true;
        //            break;
        //    }
        //}
    }
}