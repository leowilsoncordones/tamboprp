using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class ReporteMapper : AbstractMapper
    {
        private Reporte _reporte;

        private static string ReporteDest_SelectByIdReporte = "ReporteDest_SelectByIdReporte";
        private static string Reporte_UpdateProgramacion = "Reporte_UpdateProgramacion";
        private static string ReporteDest_DeleteByIdReporte = "ReporteDest_DeleteByIdReporte";
        private static string ReporteDest_InsertByIdReporte = "ReporteDest_InsertByIdReporte";
        

        public ReporteMapper(Reporte reporte)
        {
            _reporte = reporte;
        }

        public ReporteMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Reporte GetReporteById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Reporte)load(dr);
        }


        public List<Reporte> GetAll()
        {
            List<Reporte> ls = new List<Reporte>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Reporte> loadAll(SqlDataReader rs)
        {
            List<Reporte> result = new List<Reporte>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        
        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_SelecById";
                cmd.Parameters.Add(new SqlParameter("@ID", _reporte.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID", _reporte.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Insert";
                cmd.Parameters.Add(new SqlParameter("@TITULO", _reporte.Titulo));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", _reporte.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FRECUENCIA", _reporte.Frecuencia));
                cmd.Parameters.Add(new SqlParameter("@DIA", _reporte.Dia));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Update";
                cmd.Parameters.Add(new SqlParameter("@ID", _reporte.Id));
                cmd.Parameters.Add(new SqlParameter("@TITULO", _reporte.Titulo));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", _reporte.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FRECUENCIA", _reporte.Frecuencia));
                cmd.Parameters.Add(new SqlParameter("@DIA", _reporte.Dia));
            }
            return cmd;
        }

        protected Reporte load(SqlDataReader record)
        {
            var repo = new Reporte();
            repo.Id = (DBNull.Value == record["ID"]) ? 0 : int.Parse(record["ID"].ToString());
            repo.Titulo = (DBNull.Value == record["TITULO"]) ? string.Empty : (string)record["TITULO"];
            repo.Descripcion = (DBNull.Value == record["DESCRIPCION"]) ? string.Empty : (string)record["DESCRIPCION"];
            repo.Frecuencia = (DBNull.Value == record["FRECUENCIA"]) ? 0 : int.Parse(record["FRECUENCIA"].ToString());
            repo.Dia = (DBNull.Value == record["DIA"]) ? 0 : int.Parse(record["DIA"].ToString());
            return repo;
        }

        public List<string> GetDestinatariosReporte(int idRep)
        {
            var result = new List<string>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", idRep));
            cmd.CommandText = ReporteDest_SelectByIdReporte;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadDestinatarios(dr));
            dr.Close();
            return result;
        }

        protected string loadDestinatarios(SqlDataReader record)
        {
            return (DBNull.Value == record["NICKNAME"]) ? string.Empty : (string)record["NICKNAME"];
            
        }

        public int UpdateProgramacionReporte(int idRepo, int dia, int frecuencia)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Reporte_UpdateProgramacion;
            cmd.Parameters.Add(new SqlParameter("@ID", idRepo));
            cmd.Parameters.Add(new SqlParameter("@DIA", dia));
            cmd.Parameters.Add(new SqlParameter("@FRECUENCIA", frecuencia));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int LimpiarDestinatariosReporteById(int idEmpRem)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = ReporteDest_DeleteByIdReporte;
            cmd.Parameters.Add(new SqlParameter("@ID", idEmpRem));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int UpdateDestinatariosReporteById(int idEmpRem, string userEmail)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = ReporteDest_InsertByIdReporte;
            cmd.Parameters.Add(new SqlParameter("@ID", idEmpRem));
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", userEmail));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }
        
    }
}
