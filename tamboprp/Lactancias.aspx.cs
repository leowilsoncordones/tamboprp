using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Lactancias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LimpiarTabla();
                this.CargarDdlTipoListado();
            }
        }


        private void LimpiarTabla()
        {
            this.gvLactancias.DataSource = null;
            this.gvLactancias.DataBind();
            this.lblTitulo.Visible = false;
            this.lblTitulo.Text = "";
        }
        
        private void CargarDdlTipoListado()
        {
            //var catMap = new CategoriaMapper();
            //List<Tipo> lst = catMap.GetAll();

            var lst = new List<VoListItem>();
            var item1 = new VoListItem { Id = 1, Nombre = "ACTUALES" }; lst.Add(item1);
            var item2 = new VoListItem { Id = 2, Nombre = "HISTORICAS" }; lst.Add(item2);
            var item3 = new VoListItem { Id = 3, Nombre = "MEJOR PRODUCCION 305 DIAS" }; lst.Add(item3);
            var item4 = new VoListItem { Id = 4, Nombre = "MEJOR PRODUCCION 365 DIAS" }; lst.Add(item4);

            this.ddlTipoListado.DataSource = lst;
            this.ddlTipoListado.DataTextField = "Nombre";
            this.ddlTipoListado.DataValueField = "Id";
            this.ddlTipoListado.DataBind();
        }

        private void GetLactanciasActuales()
        {
            var listTemp = Fachada.Instance.GetLactanciasActuales();
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = false;
            this.gvLactancias.Columns[8].Visible = false;
        }

        private void GetLactanciasHistoricas()
        {
            var listTemp = Fachada.Instance.GetLactanciasHistoricas();
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
        }

        private void GetMejorProduccion305()
        {
            var listTemp = Fachada.Instance.GetMejorProduccion305Dias();
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = true;
            this.gvLactancias.Columns[4].Visible = true;
            this.gvLactancias.Columns[5].Visible = false;
            this.gvLactancias.Columns[6].Visible = false;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
        }

        private void GetMejorProduccion365()
        {
            var listTemp = Fachada.Instance.GetMejorProduccion365Dias();
            this.gvLactancias.DataSource = listTemp;
            this.gvLactancias.DataBind();
            this.gvLactancias.Columns[1].Visible = true;
            this.gvLactancias.Columns[2].Visible = true;
            this.gvLactancias.Columns[3].Visible = false;
            this.gvLactancias.Columns[4].Visible = false;
            this.gvLactancias.Columns[5].Visible = true;
            this.gvLactancias.Columns[6].Visible = true;
            this.gvLactancias.Columns[7].Visible = true;
            this.gvLactancias.Columns[8].Visible = true;
        }


        protected void btnListar_Click(object sender, EventArgs e)
        {
            this.LimpiarTabla();
            switch (this.ddlTipoListado.SelectedValue)
            {
                case "1": 
                    this.GetLactanciasActuales();
                    break;
                case "2":
                    this.GetLactanciasHistoricas();
                    break;
                case "3":
                    this.GetMejorProduccion305();
                    break;
                case "4":
                    this.GetMejorProduccion365();
                    break;
                default:
                    this.GetLactanciasActuales();
                    break;
            }

            this.lblTitulo.Visible = true;
            this.lblTitulo.Text = this.ddlTipoListado.SelectedItem.Text;
        }
    }
}