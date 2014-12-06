using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Servicios_Sin_DiagP_35 : System.Web.UI.Page
    {

        private List<VOServicio> _listEnt = Fachada.Instance.GetServicios35SinDiagPrenezVaqEnt();
        private List<VOServicio> _listOrd = Fachada.Instance.GetServicios35SinDiagPrenezVacOrdene();
        private List<VOServicio> _listSec = Fachada.Instance.GetServicios35SinDiagPrenezVacSecas();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        public void cargarGrilla()
        {
            var list = new List<VOServicio>();
            list.AddRange(_listEnt);
            list.AddRange(_listOrd);
            list.AddRange(_listSec);
            this.lblCantAnimales.Text = list.Count.ToString();
            this.lblCantAnimales.Visible = true;
            this.gvServicios.DataSource = list;
            this.gvServicios.DataBind();
        }

        protected void gvServicios_created(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 1)
            {
                GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc = new TableCell();
                tc.Text = "VAQUILLONAS ENTORADAS  " + "( " + _listEnt.Count + " )";
                tc.ColumnSpan = 7;
                tc.Font.Bold = true;
                tc.BackColor = Color.LightBlue;
                tc.HorizontalAlign = HorizontalAlign.Center;
                gvRow.Cells.Add(tc);
                gvServicios.Controls[0].Controls.AddAt(1, gvRow);
            }

            if (e.Row.RowIndex == _listEnt.Count + 2)
            {
                GridViewRow gvRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc1 = new TableCell();
                tc1.Text = "VACAS EN ORDEÑE  " + "( " + _listOrd.Count + " )";
                tc1.ColumnSpan = 7;
                tc1.Font.Bold = true;
                tc1.BackColor = Color.LightBlue;
                tc1.HorizontalAlign = HorizontalAlign.Center;
                gvRow1.Cells.Add(tc1);
                gvServicios.Controls[0].Controls.AddAt(_listEnt.Count + 2, gvRow1);
            }

            if (e.Row.RowIndex == _listEnt.Count + _listOrd.Count + 3)
            {
                GridViewRow gvRow2 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell tc2 = new TableCell();
                tc2.Text = "VACAS SECAS  " + "( " + _listSec.Count + " )";
                tc2.ColumnSpan = 7;
                tc2.Font.Bold = true;
                tc2.BackColor = Color.LightBlue;
                tc2.HorizontalAlign = HorizontalAlign.Center;
                gvRow2.Cells.Add(tc2);
                gvServicios.Controls[0].Controls.AddAt(_listEnt.Count + _listOrd.Count + 3, gvRow2);
            }
        }
    }
}