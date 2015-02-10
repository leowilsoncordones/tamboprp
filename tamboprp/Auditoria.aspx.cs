using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            if ((Session["EstaLogueado"] != null && (bool)Session["EstaLogueado"]) &&
               (Session["EsAdmin"] != null && (bool)Session["EsAdmin"]))
            {
                this.SetPageBreadcrumbs();
                //this.CargarAuditoria();
                this.contenedor_dia.InnerHtml = "";
                this.lblStatus.Text = "";
                this.CargarLogsAuditoria();
            }
            else Response.Redirect("~/Default.aspx", true);
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
            var list = Fachada.Instance.LogGetLastXDays();
            this.ListarAuditoriaPorDia(list);
        }

        protected void btn_ExportarLog(object sender, EventArgs e)
        {
            try
            {
                var listaLog = Fachada.Instance.ExportarLogCompleto();
                var ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                foreach (string line in listaLog)
                {
                    tw.WriteLine(line);
                }
                tw.Flush();
                byte[] bytes = ms.ToArray();
                ms.Close();

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("Content-Disposition", "attachment; filename=tamboprpLog.txt");
                Response.BinaryWrite(bytes);
                Response.End(); 
                
            }
            catch (Exception ex)
            {
                this.lblStatus.Text = "No se pudo exportar el archivo";
            }
        }
        
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
                            sb.Append("<span class='timeline-date'>" + laFecha2.ToShortTimeString() + "</span>");
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
                    operation += " <span class='blue bolder'>ingresó</span> al sistema";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-sign-in blue bigger-125'></i></div>";
                    break;
                case "logoff":
                    operation += " <span class='blue bolder'>salió</span> del sistema";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-sign-out blue bigger-125'></i></div>";
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
                case "login_intento":
                    operation += " <span class='red bolder'>intentó ingresar</span> al sistema";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-times red bigger-125'></i></div>";
                    break;
                case "update_password":
                    operation += " cambió la contraseña de ";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-user blue bigger-125'></i></div>";
                    break;
                default:
                    break;
            }
            switch (logAudit.Tabla)
            {
                case "abortos":
                    operation += " ABORTO de la vaca ";
                    break;
                case "animales":
                    operation += " ANIMAL con registro ";
                    break;
                case "partos":
                    operation += " PARTO de la vaca ";
                    break;
                case "celos_sin_servicio":
                    operation += " CELO de la vaca ";
                    break;
                case "diag_prenez":
                    operation += " DIAGNÓSTICO DE PREÑEZ de la vaca ";
                    break;
                case "bajas":
                    operation += "a BAJA del animal ";
                    break;
                case "concursos":
                    operation += " CONCURSO del animal ";
                    break;
                case "calificaciones":
                    operation += "a CALIFICACIÓN del animal ";
                    break;
                case "controles_producc":
                    operation += " CONTROL DE PRODUCCIÓN de la vaca ";
                    break;
                case "empleados":
                    operation += " EMPLEADO de nombre ";
                    break;
                case "enfermedades":
                    operation += "a ENFERMEDAD ";
                    break;
                case "fotos":
                    operation += "a FOTO del animal ";
                    break;
                case "remitos_planta":
                    operation += " REMITO A PLANTA correspondiente al día ";
                    break;
                case "secados":
                    operation += " SECADO de la vaca ";
                    break;
                case "servicios":
                    operation += " SERVICIO de la vaca ";
                    break;
                case "usuarios":
                    if (logAudit.Operacion != "login_intento" && logAudit.Operacion != "login")
                    {
                        operation += " usuario con nickname ";
                    }
                    break;
                case "usuarios_roles":
                    operation += " ROL DE USUARIO de ";
                    botones += "<div class='action-buttons'><i class='ace-icon fa fa-user blue bigger-125'></i></div>";
                    break;
                default:
                    break;
            }
            if (logAudit.Registro != "Sistema")
            {
                operation += "<span class='green bolder'>" + logAudit.Registro + "</span>";
            }
            operation += botones;
            return operation;
        }

    }
}