using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Negocio;

namespace Datos
{
    public class LogMapper : AbstractMapper
    {
        private Log _log;

        private static string Log_SelecByRegistro = "Log_SelecByRegistro";
        private static string Log_SelecByUser = "Log_SelecByUser";
        private static string Log_SelecByDate = "Log_SelecByDate";
        private static string Log_SelecXDays = "Log_SelecXDays";
        
        public LogMapper(Log log)
        {
            _log = log;
        }

        public LogMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Log GetLogById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Log)load(dr);
        }


        public List<Log> GetAll()
        {
            List<Log> ls = new List<Log>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Log> loadAll(SqlDataReader rs)
        {
            List<Log> result = new List<Log>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Log> GetLogByRegistro()
        {
            List<Log> result = new List<Log>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _log.Registro));
            cmd.CommandText = Log_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        public List<Log> GetLastXDays(int days)
        {
            List<Log> result = new List<Log>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DAYS", days));
            cmd.CommandText = Log_SelecXDays;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Log> GetLogByUser()
        {
            List<Log> result = new List<Log>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@USUARIO", _log.User));
            cmd.CommandText = Log_SelecByUser;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Log> GetLogByDate()
        {
            List<Log> result = new List<Log>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA", _log.Fecha));
            cmd.CommandText = Log_SelecByDate;

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
                cmd.CommandText = "Log_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@ID", _log.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Log_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                //cmd = new SqlCommand();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "Log_Delete";
                //cmd.Parameters.Add(new SqlParameter("@ID", _log.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Log_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _log.Registro));
                cmd.Parameters.Add(new SqlParameter("@USUARIO", _log.User));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _log.Fecha));
                cmd.Parameters.Add(new SqlParameter("@TABLA", _log.Tabla));
                cmd.Parameters.Add(new SqlParameter("@OPERACION", _log.Operacion));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Log_Update";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _log.Registro));
                cmd.Parameters.Add(new SqlParameter("@USUARIO", _log.User));
                //cmd.Parameters.Add(new SqlParameter("@FECHA", _log.Fecha));
                //cmd.Parameters.Add(new SqlParameter("@TABLA", _log.Tabla));
                //cmd.Parameters.Add(new SqlParameter("@OPERACION", _log.Operacion));
            }
            return cmd;
        }

        protected Log load(SqlDataReader record)
        {
            var log = new Log();
            //log.Id = (short)((DBNull.Value == record["ID"]) ? 0 : (Int16)record["ID"]);
            log.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            log.User = (DBNull.Value == record["USUARIO"]) ? string.Empty : (string)record["USUARIO"];
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) log.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            log.Tabla = (DBNull.Value == record["TABLA"]) ? string.Empty : (string)record["TABLA"];
            log.Operacion = (DBNull.Value == record["OPERACION"]) ? string.Empty : (string)record["OPERACION"];
            return log;
        }

    }
}
