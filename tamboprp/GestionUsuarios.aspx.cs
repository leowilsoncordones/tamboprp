using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Negocio;

namespace tamboprp
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private List<VOUsuario> _listUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetPageBreadcrumbs();
                this.LimpiarTabla();
                this.CargarUsuarios();
                this.CargarDdlRoldeUsuario();
                this.CargarDdlUsuarios();
            }
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

        private void CargarUsuarios()
        {
            var lst = Fachada.Instance.GetUsuariosAll();
            
            //this.gvUsuario.DataSource = lst;
            //this.gvUsuario.DataBind();

            //var sbHabilitado = new StringBuilder();
            //sbHabilitado.Append("<span class='label label-sm label-success'>Habilitado</span>");
            //var sbDeshabilitado = new StringBuilder();
            //sbDeshabilitado.Append("<span class='label label-sm label-inverse'>Deshabilitado</span>");

            //var sbEdit = new StringBuilder();
            //sbEdit.Append("<div class='hidden-sm hidden-xs action-buttons'>");
            //sbEdit.Append("<a class='blue' href='#modificarUsuario'><i class='ace-icon fa fa-search-plus bigger-130'></i></a>");
            //sbEdit.Append("<a class='green' href='#editarUsuario'><i class='ace-icon fa fa-pencil bigger-130'></i></a>");
            //sbEdit.Append("<a class='red' href='#eliminarUsuario'><i class='ace-icon fa fa-trash-o bigger-130'></i></a>");
            //sbEdit.Append("</div>");

            _listUser = new List<VOUsuario>();
            for (int i = 0; i < lst.Count; i++)
            {
                var uItem = new VOUsuario()
                {
                    Nombre = lst[i].Nombre + " " + lst[i].Apellido,
                    Nickname = lst[i].Nickname,
                    Email = lst[i].Email,
                    Rol = lst[i].Rol,
                    HabilitadoText = lst[i].Habilitado ? "Si" : "No"
                };
                _listUser.Add(uItem);
            }
            this.gvUsuario.DataSource = _listUser;
            this.gvUsuario.DataBind();
            
        }

        protected void GvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            this.CargarUsuarios();
        }

        //protected void GvUsuarios_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    var gv = (GridView)sender;
        //    //gv.SelectedIndex = e.NewPageIndex;
        //    //this.GetUsuarios();
        //    this.lblStatus.Text = "Seleccion";
        //}

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
            if (this.password.Value != "")
            {
                var uAdmin = (VOUsuario)Session["Usuario"];
                var admin = uAdmin.Nickname;
                var user = this.dllUsuariosChangePwd.SelectedItem.ToString();
                return Fachada.Instance.ResetearPassword(admin, user, this.password.Value);
            }
            else
            {
                this.lblStatus.Text = "La contraseña no puede ser vacía";
            }
            return false;
        }


        protected void btn_HabilitarUsuario(object sender, EventArgs e)
        {
            try
            {
                if (this.HabilitarUsuario())
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
            var uAdmin = (VOUsuario)Session["Usuario"];
            var admin = uAdmin.Nickname;
            var user = this.ddlUsuarioEliminar.SelectedItem.ToString();
            return Fachada.Instance.EliminarUsuario(admin, user);
        }

        private bool HabilitarUsuario()
        {
            //if (this.oldPwd.Text != null && this.newPwd.Text != "")
            //{
            //    //return Fachada.Instance.ResetearPassword(this.oldPwd.Text, this.newPwd.Text);
            //}
            //else
            //{
            //    this.lblStatus.Text = "La contraseña no puede ser vacía";
            //}
            return false;
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

        private void CargarDdlUsuarios()
        {
            this.ddlUsuariosModificar.DataSource = _listUser;
            this.ddlUsuariosModificar.DataTextField = "Nickname";
            this.ddlUsuariosModificar.DataValueField = "Nickname";
            this.ddlUsuariosModificar.DataBind();

            this.dllUsuariosChangePwd.DataSource = _listUser;
            this.dllUsuariosChangePwd.DataTextField = "Nickname";
            this.dllUsuariosChangePwd.DataValueField = "Nickname";
            this.dllUsuariosChangePwd.DataBind();

            this.ddlUsuarioEliminar.DataSource = _listUser;
            this.ddlUsuarioEliminar.DataTextField = "Nickname";
            this.ddlUsuarioEliminar.DataValueField = "Nickname";
            this.ddlUsuarioEliminar.DataBind();
        }


        protected void ddlRolUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var rol = this.ddlRolUsuario.SelectedIndex;
            //var rol1 = this.ddlRolUsuario.SelectedItem;
            //var rol2 = this.ddlRolUsuario.SelectedValue;
        }

        protected void ddlUsuariosModificar_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var sel = this.ddlUsuariosModificar.SelectedValue;
            VOUsuario voU = _listUser.FirstOrDefault(u => u.Nombre.Equals(sel));
            if (voU != null)
            {
                this.fNombre.Value = voU.Nombre;
                this.fApellido.Value = voU.Apellido;
                this.fEmail.Value = voU.Email;
                this.ddlRolUsuario.SelectedIndex = voU.Rol.Nivel;
            }

        }
    }
}