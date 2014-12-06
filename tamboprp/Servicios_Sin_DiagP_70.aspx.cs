using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace tamboprp
{
    public partial class Servicios_Sin_DiagP_70 : System.Web.UI.Page
    {

        private List<VOServicio> _listEnt = Fachada.Instance.GetServicios70SinDiagPrenezVaqEnt();
        private List<VOServicio> _listOrd = Fachada.Instance.GetServicios70SinDiagPrenezVacOrdene();
        private List<VOServicio> _listSec = Fachada.Instance.GetServicios70SinDiagPrenezVacSecas();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarGrilla();
            }
        }




        public void cargarGrilla()
        {
            var list = new List<VOServicio>();
            list.AddRange(_listEnt);
            list.AddRange(_listOrd);
            list.AddRange(_listSec);
            this.lblCantAnimales.Text = list.Count.ToString();
            this.titCantAnimales.Visible = true;
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
                tc.Text = "VAQUILLONAS ENTORADAS  "+"( "+_listEnt.Count+" )";
                tc.ColumnSpan = 7;
                tc.Font.Bold = true;
                tc.BackColor = Color.LightBlue;
                tc.HorizontalAlign = HorizontalAlign.Center;
                gvRow.Cells.Add(tc);
                gvServicios.Controls[0].Controls.AddAt(1, gvRow);
            }

            if (e.Row.RowIndex == _listEnt.Count+2)
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

        protected void Print(object sender, EventArgs e)
        {
            gvServicios.UseAccessibleHeader = true;
            gvServicios.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvServicios.FooterRow.TableSection = TableRowSection.TableFooter;
            gvServicios.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in gvServicios.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvServicios.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvServicios.DataBind();

            cargarGrilla();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
    }
}