using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.SqlServer.Server;

namespace Datos
{
    public class Diag_PrenezMapper : AbstractMapper
    {
        private Diag_Prenez _diag;
        private string _registroAnimal;

        private static string Diag_prenez_SelecByRegistro = "Diag_prenez_SelectByRegistro";
        private static string Diag_prenez_SelectByRegistroDespFecha = "Diag_prenez_SelectByRegistroDespFecha";
        private static string Diag_prenez_SelectInseminacionesExito = "Diag_prenez_SelectInseminacionesExito";
        private static string Diag_prenez_SelectRegistrosPrenadasAhora = "Diag_prenez_SelectRegistrosPrenadasAhora";


        public Diag_PrenezMapper(Diag_Prenez diag)
        {
            _diag = diag;
        }

        public Diag_PrenezMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public Diag_PrenezMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Diag_Prenez GetDiag_PrenezById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Diag_Prenez)load(dr);
        }


        public List<Diag_Prenez> GetAll()
        {
            var ls = new List<Diag_Prenez>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Diag_Prenez> loadAll(SqlDataReader rs)
        {
            var result = new List<Diag_Prenez>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetDiag_PrenezByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Diag_prenez_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Evento> GetDiag_PrenezByRegistroUltDespFecha(string regAnimal, DateTime fecha)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.Parameters.Add(new SqlParameter("@FECHA", fecha));
            cmd.CommandText = Diag_prenez_SelectByRegistroDespFecha;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        
        


        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Diag_prenez_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Diag_prenez_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Diag_prenez_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Diag_prenez_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _diag.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _diag.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _diag.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _diag.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@DIAGNOSTIC", _diag.Diagnostico));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Diag_prenez_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _diag.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _diag.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _diag.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@DIAGNOSTIC", _diag.Diagnostico));
            }
            return cmd;
        }

        protected Diag_Prenez load(SqlDataReader record)
        {
            var diagp = new Diag_Prenez();
            diagp.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) diagp.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            diagp.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            diagp.Diagnostico = (DBNull.Value == record["DIAGNOSTIC"]) ? ' ' : Convert.ToChar(record["DIAGNOSTIC"]);
            return diagp;
        }

        public List<VODiagnostico> GetInseminacionesExitosas(DateTime fecha)
        {
            var result = new List<VODiagnostico>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA", fecha));
            cmd.CommandText = Diag_prenez_SelectInseminacionesExito;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadInseminacionesExito(dr));
            dr.Close();
            return result;
        }

        protected VODiagnostico loadInseminacionesExito(SqlDataReader record)
        {
            var diagVo = new VODiagnostico();
            diagVo.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            string strDateS = (DBNull.Value == record["FECHA_SERV"]) ? string.Empty : record["FECHA_SERV"].ToString();
            if (strDateS != string.Empty) diagVo.FechaServicio = DateTime.Parse(strDateS, new CultureInfo("fr-FR"));
            diagVo.RegistroPadre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];
            string strDateD = (DBNull.Value == record["FECHA_DIAG"]) ? string.Empty : record["FECHA_DIAG"].ToString();
            if (strDateD != string.Empty) diagVo.FechaDiagnostico = DateTime.Parse(strDateD, new CultureInfo("fr-FR"));
            diagVo.Diagnostico = (DBNull.Value == record["DIAGNOSTIC"]) ? ' ' : Convert.ToChar(record["DIAGNOSTIC"]);
            diagVo.Comentario = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            diagVo.IdInseminador = (short)((DBNull.Value == record["INSEMINADOR"]) ? 0 : (Int16)record["INSEMINADOR"]);
            return diagVo;
        }



        public List<VORegDiag> GetAnimalesConDiagPrenezActual()
        {
            var result = new List<VORegDiag>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Diag_prenez_SelectRegistrosPrenadasAhora;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadVoRegDiag(dr));
            dr.Close();
            return result;
        }

        protected VORegDiag loadVoRegDiag(SqlDataReader record)
        {
            var diagVo = new VORegDiag();
            diagVo.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            return diagVo;
        }



        public class VORegDiag
    {
            public string Registro { get; set; }
    }

        public class VODiagnostico : IComparable
        {
            public string Registro { get; set; }

            public string Edad { get; set; }

            public DateTime FechaServicio { get; set; }

            public string RegistroPadre { get; set; }

            public int CantServicios { get; set; }

            public string DiasServicio { get; set; }

            public int IdInseminador { get; set; }

            public string Inseminador { get; set; }

            public string Comentario { get; set; }

            public DateTime FechaDiagnostico { get; set; }

            public char Diagnostico { get; set; }

            public int CompareTo(Object obj)
            {
                var vOdiag = obj as VODiagnostico;
                if (vOdiag != null)
                {
                    return vOdiag.FechaDiagnostico.CompareTo(this.FechaDiagnostico);
                }
                return 0;
            }
        }

    }
}
