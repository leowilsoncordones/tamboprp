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
    public partial class Notificaciones : System.Web.UI.Page
    {
        private List<VOReporte> _lstReportes;
        //private List<Usuario> _lstUsersConMail;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]))
            {
                this.CargarReportes();
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.CargarReporteEjemplo();
                    this.CargarDdlReportes();
                    this.CargarDdlDias();
                    this.CargarDdlFrecuencia();
                    this.CargarCheckBoxUsuarios();
                }
            }
            else Response.Redirect("~/Default.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Notificaciones", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        protected void CargarReportes()
        {
            _lstReportes = Fachada.Instance.GetReportesNotificaciones();
            this.gvReportes.DataSource = _lstReportes;
            this.gvReportes.DataBind();
        }

        protected void CargarDdlReportes()
        {
            var lstRepo = Fachada.Instance.GetReportesNotificaciones();
            var lst = new List<VoListItem>();
            for (int i = 0; i < lstRepo.Count; i++)
            {
                var item = new VoListItem { Id = lstRepo[i].Id, Nombre = lstRepo[i].Titulo };
                lst.Add(item);
            }

            //this.ddlReportes.DataSource = lst;
            //this.ddlReportes.DataValueField = "Id";
            //this.ddlReportes.DataTextField = "Nombre";
            //this.ddlReportes.DataBind();
            
            this.ddlReportesProg.DataSource = lst;
            this.ddlReportesProg.DataValueField = "Id";
            this.ddlReportesProg.DataTextField = "Nombre";
            this.ddlReportesProg.DataBind();
            
        }

        protected void CargarCheckBoxUsuarios()
        {
            var checkboxlst = new CheckBoxList();
            for (int k = 0; k < _lstReportes.Count; k++)
            {
                switch (k)
                {
                    case 0:
                        checkboxlst = this.cboxUsuarios1;
                        break;
                    case 1:
                        checkboxlst = this.cboxUsuarios2;
                        break;
                }
                this.CargarCheckBoxReporte(_lstReportes[k].Id, checkboxlst);
            }
        }

        protected void CargarCheckBoxReporte(int idRepo, CheckBoxList chkBox)
        {
            chkBox.Items.Clear();
            var lstUsersConMail = Fachada.Instance.GetUsuariosConMailAll();
            //_lstUsersConMail = Fachada.Instance.GetUsuariosConMailAll();
            List<Usuario> lstIncluidos = Fachada.Instance.ListaDestinatariosReporte(idRepo);
            for (int i = 0; i < lstUsersConMail.Count; i++)
                {
                    var user = lstUsersConMail[i];
                    chkBox.Items.Insert(i, user.Nickname);
                    Usuario userTemp = lstIncluidos.FirstOrDefault(u => u.Nickname == user.Nickname);
                    if (userTemp != null)
                    {
                        chkBox.Items[i].Selected = true;
                    }
                }

                chkBox.DataValueField = "Nickname";
                chkBox.DataTextField = "Nickname";
                chkBox.DataBind();
        }

        protected void btn_SeleccionarDestinatarios(object sender, EventArgs e)
        {
            this.CambiarDestinatariosReporte();
        }

        protected void CambiarDestinatariosReporte()
        {
            var lstUsersConMail = Fachada.Instance.GetUsuariosConMailAll();
            // Seleccionados en el resumen operativo
            var lstReporte1 = new List<Usuario>();
            var selectedCbox1 = new List<ListItem>();
            foreach (ListItem item in this.cboxUsuarios1.Items)
            {
                if (item.Selected)
                {
                    selectedCbox1.Add(item);
                    Usuario userTemp = lstUsersConMail.FirstOrDefault(u => u.Nickname == item.Value);
                    if (userTemp != null)
                    {
                        lstReporte1.Add(userTemp);
                    }
                }
            }
            var cantRepo = _lstReportes.Count;
            if (cantRepo > 0)
            {
                Fachada.Instance.CambiarDestinatariosReporte(_lstReportes[0].Id, lstReporte1);
            }
            
            // Seleccionados en el informe cierre de mes
            var lstReporte2 = new List<Usuario>();
            var selectedCbox2 = new List<ListItem>();
            foreach (ListItem item in this.cboxUsuarios2.Items)
            {
                if (item.Selected)
                {
                    selectedCbox2.Add(item);
                    Usuario userTemp = lstUsersConMail.FirstOrDefault(u => u.Nickname == item.Value);
                    if (userTemp != null)
                    {
                        lstReporte2.Add(userTemp);
                    }
                }
            }
            if (cantRepo > 1)
            {
                Fachada.Instance.CambiarDestinatariosReporte(_lstReportes[1].Id, lstReporte2);
            }
            this.CargarCheckBoxUsuarios();
        }

        protected void btn_EnviarPrueba(object sender, EventArgs e)
        {
            var u = (VOUsuario)Session["Usuario"];
            if (u != null)
            {
                var dest = u.Email;
                Fachada.Instance.EnviarReporteSemanalPrueba(dest);
                this.lblStatus.Text = "Enviado, revise su bandeja de entrada";
            }
            //Fachada.Instance.EnviarReporteSemanal();
        }

        public void CargarReporteEjemplo()
        {
            var strHtml = Fachada.Instance.ReporteSemanalPrueba();
            this.bodyModal.InnerHtml = strHtml;
        }

        protected void btn_ProgReporte(object sender, EventArgs e)
        {
            if (this.ProgramarReporte())
            {
                this.lblStatus.Text = "Se reprogramó el reporte";
                this.CargarReportes();
            }
            else
            {
                this.lblStatus.Text = "No se pudo reprogramar el reporte";
            }
        }

        public bool ProgramarReporte()
        {
            var repo = int.Parse(this.ddlReportesProg.SelectedItem.Value);
            var dia = int.Parse(this.ddlDias.SelectedValue);
            var frec = int.Parse(this.ddlFrecuencia.SelectedValue);
            return Fachada.Instance.ProgramarReporte(repo, dia, frec);
        }

        public void CargarDdlDias()
        {
            var lst = new List<VoListItem>();
            var item0 = new VoListItem { Id = 0, Nombre = "DOMINGO" }; lst.Add(item0);
            var item1 = new VoListItem { Id = 1, Nombre = "LUNES" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "MARTES" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "MIERCOLES" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "JUEVES" }; lst.Add(item4);
            var item5 = new VoListItem { Id = 5, Nombre = "VIERNES" }; lst.Add(item5);
            var item6 = new VoListItem { Id = 6, Nombre = "SABADO" }; lst.Add(item6);
            
            this.ddlDias.DataSource = lst;
            this.ddlDias.DataTextField = "Nombre";
            this.ddlDias.DataValueField = "Id";
            this.ddlDias.DataBind();
            this.ddlDias.SelectedIndex = 5;
        }

        public void CargarDdlFrecuencia()
        {
            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 0, Nombre = "SEMANAL" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 4, Nombre = "MENSUAL" }; lst.Add(item2);

            this.ddlFrecuencia.DataSource = lst;
            this.ddlFrecuencia.DataTextField = "Nombre";
            this.ddlFrecuencia.DataValueField = "Id";
            this.ddlFrecuencia.DataBind();
            this.ddlFrecuencia.SelectedIndex = 0;
        }

    }
}