using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class EmpresasRemisoras : System.Web.UI.Page
    {
        private List<VOEmpresa> _lstRemisoras;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarModal();
                this.CargarEmpresasRemisoras();
            }
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Reportes", "Reportes.aspx"));
            list.Add(new VoListItemDuplaString("Empresas remisoras", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvRemisoras.DataSource = null;
            this.gvRemisoras.DataBind();
        }

        private void CargarEmpresasRemisoras()
        {
            _lstRemisoras = Fachada.Instance.GetEmpresasRemisoras();
            this.CargarGvEmpresasRemisoras();
            this.CargarDdlEmpresas();
        }

        private void CargarGvEmpresasRemisoras()
        {
            this.gvRemisoras.DataSource = _lstRemisoras;
            this.gvRemisoras.DataBind();
        }

        protected void GvRemisoras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
        }

        private void CargarDdlEmpresas()
        {
            this.ddlEmpresas.DataSource = _lstRemisoras;
            this.ddlEmpresas.DataTextField = "Nombre";
            this.ddlEmpresas.DataValueField = "Id";
            this.ddlEmpresas.DataBind();
        }

        protected void btn_GuardarRemisora(object sender, EventArgs e)
        {
            if (this.fNombre.Text != "")
            {
                var nuevaRemisora = new VOEmpresa();
                nuevaRemisora.Nombre = this.fNombre.Text;
                nuevaRemisora.RazonSocial = this.fRazonSocial.Text;
                nuevaRemisora.Rut = this.fRut.Text;
                nuevaRemisora.Telefono = this.fTelefono.Text;
                nuevaRemisora.Direccion = this.fDireccion.Text;
                if (Fachada.Instance.GuardarEmpresaRemisora(nuevaRemisora))
                {
                    this.lblStatus.Text = "La empresa se guardó con éxito";
                    this.CargarEmpresasRemisoras();
                }
                else
                {
                    this.lblStatus.Text = "La empresa no se pudo guardar";
                }
            }
            else this.lblStatus.Text = "Ingrese una nombre para la nueva empresa";
            this.LimpiarModal();
        }

        public void LimpiarModal()
        {
            this.fNombre.Text = "";
            this.fRazonSocial.Text = "";
            this.fRut.Text = "";
            this.fTelefono.Text = "";
            this.fDireccion.Text = "";
        }

        protected void btn_ModificarActiva(object sender, EventArgs e)
        {
            var idEmpRem = Int32.Parse(this.ddlEmpresas.SelectedValue);
            if (Fachada.Instance.UpdateEmpresaRemisoraActual(idEmpRem))
            {
                this.lblStatus.Text = "La empresa actual se cambió con éxito";
                this.CargarEmpresasRemisoras();
            }
            else
            {
                this.lblStatus.Text = "La empresa actual no se pudo cambiar";
            }
        }
    }
}