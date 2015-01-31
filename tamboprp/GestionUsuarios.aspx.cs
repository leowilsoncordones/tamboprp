using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace tamboprp
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        //private List<VOUsuario> _listVo;
        private List<Usuario> _lst = Fachada.Instance.GetUsuariosAll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]))
            {
                if (!Page.IsPostBack)
                {
                    this.SetPageBreadcrumbs();
                    this.LimpiarTabla();
                    this.CargarUsuarios();
                    this.CargarDdlRoldeUsuario();
                    //this.CargarDdlUsuarios();
                }
            }
            else Response.Redirect("~/Login.aspx", true);
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Gestión de usuarios", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void LimpiarTabla()
        {
            this.gvUsuario.DataSource = null;
            this.gvUsuario.DataBind();
            this.lblStatus.Text = "";
        }

        private void RecargarUsuarios()
        {
            _lst = Fachada.Instance.GetUsuariosAll();
            this.CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            //_lst = Fachada.Instance.GetUsuariosAll();

            var listVo = this.CargarVoUsuarios();
            this.gvUsuario.DataSource = listVo;
            this.gvUsuario.DataBind();

            this.ddlUsuariosModificar.DataSource = listVo;
            this.ddlUsuariosModificar.DataTextField = "Nickname";
            this.ddlUsuariosModificar.DataValueField = "Nickname";
            this.ddlUsuariosModificar.DataBind();

            this.dllUsuariosChangePwd.DataSource = listVo;
            this.dllUsuariosChangePwd.DataTextField = "Nickname";
            this.dllUsuariosChangePwd.DataValueField = "Nickname";
            this.dllUsuariosChangePwd.DataBind();

            this.ddlUsuarioEliminar.DataSource = listVo;
            this.ddlUsuarioEliminar.DataTextField = "Nickname";
            this.ddlUsuarioEliminar.DataValueField = "Nickname";
            this.ddlUsuarioEliminar.DataBind();

            this.ddlUsuarioSelecc.DataSource = listVo;
            this.ddlUsuarioSelecc.DataTextField = "Nickname";
            this.ddlUsuarioSelecc.DataValueField = "Nickname";
            this.ddlUsuarioSelecc.DataBind();
        }

        private List<VOUsuario> CargarVoUsuarios()
        {
            var listVo = new List<VOUsuario>();
            for (int i = 0; i < _lst.Count; i++)
            {
                var uItem = new VOUsuario()
                {
                    Nombre = _lst[i].Nombre + " " + _lst[i].Apellido,
                    Nickname = _lst[i].Nickname,
                    Email = _lst[i].Email,
                    Rol = _lst[i].Rol,
                    HabilitadoText = _lst[i].Habilitado ? "Si" : "No"
                };
                listVo.Add(uItem);
            }
            return listVo;
        }

        protected void GvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarUsuarios();
        }

        protected void btn_ResetearPassword(object sender, EventArgs e)
        {
            try
            {
                if (this.ResetearPassword())
                {
                    this.lblStatus.Text = "La contraseña se actualizó";
                }
                else
                {
                    this.lblStatus.Text = "La contraseña no se pudo actualizar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool ResetearPassword()
        {
            Usuario voU = null;
            var uAdmin = (VOUsuario)Session["Usuario"];
            var sel = this.dllUsuariosChangePwd.SelectedValue;
            if (_lst != null) voU = _lst.FirstOrDefault(u => u.Nickname.Equals(sel));
            if (uAdmin != null && voU != null && this.password.Value != "")
            {
                return Fachada.Instance.ResetearPassword(uAdmin.Nickname, voU.Nickname, this.password.Value);
            }
            else
            {
                this.lblStatus.Text = "La contraseña no puede ser vacía";
            }
            return false;
        }


        protected void btn_ModificarUsuario(object sender, EventArgs e)
        {
            try
            {
                if (this.ModificarUsuario())
                {
                    this.lblStatus.Text = "El usuario se modificó";
                }
                else
                {
                    this.lblStatus.Text = "El usuario no se pudo modificar";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool ModificarUsuario()
        {
            bool ret = false;
            Usuario voU = null;
            var sel = this.ddlUsuarioSelecc.SelectedValue;
            if (_lst != null) voU = _lst.FirstOrDefault(u => u.Nickname.Equals(sel));
            var admin = (VOUsuario)Session["Usuario"];

            var rol = new RolUsuario();
            //var rolSel = this.ddlRolUsuario.SelectedIndex;
            var rolSel = this.ddlRolUsuario.SelectedItem.ToString();
            var listRol = Fachada.Instance.GetRolesDeUsuario();
            //if (listRol != null) rol = listRol.FirstOrDefault(u => u.Nivel == rolSel);
            if (listRol != null) rol = listRol.FirstOrDefault(u => u.NombreRol == rolSel);
            
            if (voU != null && admin != null && rol != null)
            {
                voU.Nombre = this.fNombre.Value;
                voU.Apellido = this.fApellido.Value;
                voU.Email = this.fEmail.Value;
                voU.Rol = rol;
                ret = Fachada.Instance.UpdateUsuario(voU);
                
                this.CargarUsuarios();
            }
            return ret;
        }

        protected void btn_EliminarUsuario(object sender, EventArgs e)
        {
            try
            {
                if (this.EliminarUsuario())
                {
                    this.lblStatus.Text = "Se eliminó el usuario";
                }
                else
                {
                    this.lblStatus.Text = "No se pudo eliminar el usuario";
                }
            }
            catch (Exception ex)
            {

            }
        }


        private bool EliminarUsuario()
        {
            bool ret = false;
            var uAdmin = (VOUsuario)Session["Usuario"];
            var admin = uAdmin.Nickname;
            var user = this.ddlUsuarioEliminar.SelectedItem.ToString();
            if (uAdmin != null)
            {
                ret = Fachada.Instance.EliminarUsuario(admin, user);
            }
            this.RecargarUsuarios();
            return ret;
        }

        private void CargarDdlRoldeUsuario()
        {
            var lst = Fachada.Instance.GetRolesDeUsuario();
            this.ddlRolUsuario.DataSource = lst;
            this.ddlRolUsuario.DataTextField = "NombreRol";
            this.ddlRolUsuario.DataValueField = "Nivel";
            this.ddlRolUsuario.DataBind();
            // por defecto seleccionado el rol DIGITADOR
            this.ddlRolUsuario.SelectedIndex = 1;
        }

        protected void btn_HabilitarUsuario(object sender, EventArgs e)
        {
            if (this.HabilitarUsuario())
            {
                this.lblStatus.Text = "Cambio hecho";
            }
            else
            {
                this.lblStatus.Text = "No se pudo hacer el cambio";
            }
        }

        private bool HabilitarUsuario()
        {
            bool ret = false;
            Usuario voU = null;
            var sel = this.ddlUsuariosModificar.SelectedValue;
            if (_lst != null) voU = _lst.FirstOrDefault(u => u.Nickname.Equals(sel));
            var admin = (VOUsuario)Session["Usuario"];
            if (voU != null && admin != null)
            {
                if (!this.checkHabilitabo.Checked)
                {
                    ret = Fachada.Instance.DeshabilitarUsuario(admin.Nickname, voU.Nickname);
                }
                else if (this.checkHabilitabo.Checked)
                {
                    ret = Fachada.Instance.HabilitarUsuario(admin.Nickname, voU.Nickname);
                }
                this.RecargarUsuarios();
            }
            return ret;
        }


    }
}