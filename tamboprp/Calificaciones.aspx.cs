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
    public partial class Calificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarCalificaciones();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Cabaña", "Cabana.aspx"));
            list.Add(new VoListItemDuplaString("Calificaciones", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        public void CargarCalificaciones()
        {
            List<Calificacion> lst = Fachada.Instance.GetCalificacionesAll();
            this.gvCalificaciones.DataSource = lst;
            this.gvCalificaciones.DataBind();
            this.titCantCalif.Visible = true;
            this.lblCantCalif.Text = lst.Count.ToString();
            this.lblTotal.Text = lst.Count.ToString();
            int cant = lst.Count;
            if (cant > 0)
            {
                //this.lblEX.Text = Fachada.Instance.GetCantCalificacionesEx().ToString();
                //this.lblMB.Text = Fachada.Instance.GetCantCalificacionesMb().ToString();
                //this.lblBM.Text = Fachada.Instance.GetCantCalificacionesBm().ToString();
                //this.lblB.Text = Fachada.Instance.GetCantCalificacionesB().ToString();
                //this.lblProm.Text = Fachada.Instance.GetCalificacionProm().ToString();
                //this.lblMax.Text = Fachada.Instance.GetCalificacionesMax().ToString();

                int sum = 0; int maxPts = 0; string maxLetras = "";
                int cantEx = 0; int cantMb = 0; int cantBm = 0; 
                int cantB = 0; int cantR = 0;

                for (int i = 0; i < cant; i++)
                {
                    if (lst[i].Puntos > maxPts)
                    {
                        maxPts = lst[i].Puntos;
                        maxLetras = lst[i].Letras;
                    }
                    switch (lst[i].Letras)
                    {
                        case "EX":
                            cantEx++;
                            break;
                        case "MB":
                            cantMb++;
                            break;
                        case "BM":
                            cantBm++;
                            break;
                        case "B":
                            cantB++;
                            break;
                        case "R":
                            cantR++;
                            break;
                    }
                    sum += lst[i].Puntos;
                }

                this.lblEX.Text = cantEx.ToString();
                this.lblMB.Text = cantMb.ToString();
                this.lblBM.Text = cantBm.ToString();
                this.lblB.Text = cantB.ToString();
                this.lblR.Text = cantR.ToString();
                this.lblMax.Text = (maxLetras + " " + maxPts).ToString();
                this.lblProm.Text = (sum / cant).ToString();
            }
            
        }

        protected void GvCalificaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarCalificaciones();
        }

        private void LimpiarTabla()
        {
            this.gvCalificaciones.DataSource = null;
            this.gvCalificaciones.DataBind();
            this.lblCantCalif.Text = "";
            this.lblEX.Text = "";
            this.lblMB.Text = "";
            this.lblBM.Text = "";
            this.lblB.Text = "";
            this.lblR.Text = "";
            this.lblTotal.Text = "";
            this.lblMax.Text = "";
            this.lblProm.Text = "";
        }
    }
}