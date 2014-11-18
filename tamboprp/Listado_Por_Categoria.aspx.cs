using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;
using Entidades;

namespace tamboprp
{
    public partial class ListadoPorCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LimpiarTabla();
                this.CargarDdlCategorias();
            }
        }

        
        private void LimpiarTabla()
        {
            this.gvAnimales.DataSource = null;
            this.gvAnimales.DataBind();
            this.lblCateg.Text = "";
        }

        private void CargarDdlCategorias()
        {
            var catMap = new CategoriaMapper();
            List<Categoria> lst = catMap.GetAll();
            this.ddlCategorias.DataSource = lst;
            this.ddlCategorias.DataTextField = "Nombre";
            this.ddlCategorias.DataValueField = "Id_categ";
            this.ddlCategorias.DataBind();
            // valor por defecto SELECCIONAR?
        }

        private void CargarAnimalesPorCategoria()
        {
            int idCategoria = int.Parse(this.ddlCategorias.SelectedValue);
            var listTemp = Fachada.Instance.GetAnimalesByCategoria(idCategoria);
            this.gvAnimales.DataSource = listTemp;
            this.gvAnimales.DataBind();
            // formatear la fecha
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            this.CargarAnimalesPorCategoria();
            //this.lblCateg.Text = this.ddlCategorias.SelectedItem.Text;
        }

    }
}