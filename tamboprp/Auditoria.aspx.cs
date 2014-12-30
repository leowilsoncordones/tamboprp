using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Negocio;

namespace tamboprp
{
    public partial class Auditoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetPageBreadcrumbs();
            //this.CargarAuditoria();
            this.CargarLogsAuditoria();
        }

        protected void SetPageBreadcrumbs()
        {
            var list = new List<VoListItemDuplaString>();
            list.Add(new VoListItemDuplaString("Sistema", "Sistema.aspx"));
            list.Add(new VoListItemDuplaString("Auditoría", ""));
            var strB = PageControl.SetBreadcrumbsPath(list);
            if (Master != null)
            {
                var divBreadcrumbs = Master.FindControl("breadcrumbs") as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (divBreadcrumbs != null) divBreadcrumbs.InnerHtml = strB.ToString();
            }
        }

        private void CargarLogsAuditoria()
        {
            var list = Fachada.Instance.LogGetAll();
            this.ListarAuditoriaPorDia(list);
        }

        //private void AgregoAManoAuditoria()
        //{
        //    var list = new List<Log>();
        //    var item = new Log();
        //    item.Fecha = new DateTime(2014, 11, 18, 9, 15, 0);
        //    item.Operacion = "login";
        //    item.Registro = "";
        //    item.Tabla = "";
        //    item.User = "Luis Sela";
        //    list.Add(item);
        //    var item0 = new Log();
        //    item0.Fecha = new DateTime(2014, 11, 18, 9, 18, 0);
        //    item0.Operacion = "insert";
        //    item0.Registro = "3554";
        //    item0.Tabla = "partos";
        //    item0.User = "Luis Sela";
        //    list.Add(item0);
        //    var item1 = new Log();
        //    item1.Fecha = new DateTime(2014, 11, 18, 13, 16, 0);
        //    item1.Operacion = "insert";
        //    item1.Registro = "2214";
        //    item1.Tabla = "celo_sin_servicio";
        //    item1.User = "Luis Sela";
        //    list.Add(item1);
        //    var item2 = new Log();
        //    item2.Fecha = new DateTime(2014, 11, 18, 13, 23, 0);
        //    item2.Operacion = "logoff";
        //    item2.Registro = "";
        //    item2.Tabla = "";
        //    item2.User = "Luis Sela";
        //    list.Add(item2);
        //    var item3 = new Log();
        //    item3.Fecha = new DateTime(2014, 12, 12, 17, 15, 0);
        //    item3.Operacion = "logoff";
        //    item3.Registro = "";
        //    item3.Tabla = "";
        //    item3.User = "Martín Gurgitano";
        //    list.Add(item3);
        //    var item4 = new Log();
        //    item4.Fecha = new DateTime(2014, 12, 12, 16, 35, 0);
        //    item4.Operacion = "login";
        //    item4.Registro = "";
        //    item4.Tabla = "";
        //    item4.User = "Martín Gurgitano";
        //    list.Add(item4);
        //    var item5 = new Log();
        //    item5.Fecha = new DateTime(2014, 12, 12, 16, 56, 0);
        //    item5.Operacion = "insert";
        //    item5.Registro = "1996";
        //    item5.Tabla = "celo_sin_servicio";
        //    item5.User = "Martín Gurgitano";
        //    list.Add(item5);
        //    var item6 = new Log();
        //    item6.Fecha = new DateTime(2014, 12, 13, 15, 02, 0);
        //    item6.Operacion = "login";
        //    item6.Registro = "";
        //    item6.Tabla = "";
        //    item6.User = "Luis Sela";
        //    list.Add(item6);

        //    list.Sort();
        //    ListarAuditoriaPorDia(list);
        //}

        private void ListarAuditoriaPorDia(List<Log> list)
        {
            list.Sort();
            var sb = new StringBuilder();
            int i = 0;
            //for (i = 0; i < list.Count; i++)
            while (i < list.Count)
            {
                DateTime laFecha = list[i].Fecha;
                string strFecha = laFecha.ToString("ddd d MMM", CultureInfo.CreateSpecificCulture("es-ES"));
                //string strFecha = laFecha.ToShortDateString();
                if (laFecha.ToShortDateString() == DateTime.Today.ToShortDateString()) strFecha = "Hoy";
                else
                {
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (laFecha.ToShortDateString() == yesterday.ToShortDateString()) strFecha = "Ayer";
                }
                sb.Append("<span class='timeline-label'><b>" + strFecha + "</b></span>");
                sb.Append("<div class='timeline-items'>");
                for (int j = i; j < list.Count; j++)
                {
                    sb.Append("<div class='timeline-item clearfix' >");
                        sb.Append("<div class='timeline-info'>");
                            DateTime laFecha2 = list[j].Fecha;
                            sb.Append("<span class='timeline-date'>" + laFecha2.Hour.ToString() + ":" + laFecha2.Minute.ToString() + "</span>");
                            sb.Append("<i class='timeline-indicator btn btn-info no-hover'></i>");
                        sb.Append("</div>");
                        sb.Append("<div class='widget-box transparent'>");
                            sb.Append("<div class='widget-body'>");
                                sb.Append("<div class='widget-main no-padding'>");
                                    // elijo texto y su correspondiente estilo según operación y tabla
                                    string operation = GetOperationVoAuditoria(list[j]);
                                    sb.Append(operation);
                                sb.Append("</div>");
                            sb.Append("</div>");
                        sb.Append("</div>");
                    sb.Append("</div>");
                    i = j;
                    if (j + 1 < list.Count)
                        if (list[j + 1].Fecha.ToShortDateString() != laFecha.ToShortDateString())
                        {
                            break;
                        }
                }
                sb.Append("</div><!-- /.timeline-items -->");
                sb.Append("<br/>");
                i++;
            }
            this.contenedor_dia.InnerHtml += sb.ToString();
        }

        private string GetOperationVoAuditoria(Log logAudit)
        {
            string operation = "";
            string botones = "";
            operation += "<span class='black bolder'>" + logAudit.User + "</span> ";
            switch (logAudit.Operacion)
            {
                case "login":
                    operation += " <span class='red bolder'>ingresó</span> al sistema";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-sign-in red bigger-125'></i></div>";
                    break;
                case "logoff":
                    operation += " <span class='red bolder'>salió</span> al sistema";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-sign-out red bigger-125'></i></div>";
                    break;
                case "insert":
                    operation += " dió de alta un";
                    break;
                case "update":
                    operation += " modificó un";
                    break;
                case "delete":
                    operation += " eliminó un";
                    break;
                default:
                    break;
            }
            switch (logAudit.Tabla)
            {
                case "abortos":
                    operation += " <span class='warning bolder'>aborto</span> de la vaca ";
                    break;
                case "partos":
                    operation += " <span class='pink2 bolder'>parto</span> de la vaca ";
                    break;
                case "celos_sin_servicio":
                    operation += " <span class='pink2 bolder'>celo</span> de la vaca ";
                    break;
                case "diag_prenez":
                    operation += " <span class='pink2 bolder'>diagnóstico de preñez</span> de la vaca ";
                    break;
                case "bajas":
                    operation += "a <span class='black bolder'>baja</span> del animal ";
                    break;
                case "concursos":
                    operation += " <span class='purple bolder'>concurso</span> del animal ";
                    break;
                case "calificaciones":
                    operation += "a <span class='purple bolder'>calificación</span> del animal ";
                    break;
                case "controles_producc":
                    operation += " <span class='orange2 bolder'>control de producción</span> de la vaca ";
                    break;
                case "empleados":
                    operation += " empleado de nombre ";
                    break;
                case "enfermedades":
                    operation += "a <span class='black bolder'>enfermedad</span> ";
                    break;
                case "fotos":
                    operation += "a foto del animal ";
                    break;
                case "remitos_planta":
                    operation += " <span class='blue bolder'>remito a planta</span> correspondiente al día ";
                    break;
                case "secados":
                    operation += " <span class='pink2 bolder'>secado</span> de la vaca ";
                    break;
                case "servicios":
                    operation += " <span class='pink2 bolder'>servicio</span> de la vaca ";
                    break;
                case "usuarios":
                    operation += " <span class='black bolder'>usuario</span> con nickname ";
                    break;
                case "usuarios_roles":
                    operation += " <span class='red bolder'>rol de usuario</span> de ";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-user red bigger-125'></i></div>";
                    break;
                default:
                    break;
            }
            operation += "<span class='green bolder'>" + logAudit.Registro + "</span>";
            operation += botones;
            return operation;
        }

    }
}