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
    public class CmtMapper : AbstractMapper
    {
        private Cmt _cmt;
        private string _registroAnimal;

        private static string Cmt_SelecByRegistro = "Cmt_SelecByRegistro";

        public CmtMapper(Cmt cmt)
        {
            _cmt = cmt;
        }

        public CmtMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public CmtMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Cmt GetCmtById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Cmt)load(dr);
        }


        public List<Cmt> GetAll()
        {
            var ls = new List<Cmt>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Cmt> loadAll(SqlDataReader rs)
        {
            var result = new List<Cmt>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Cmt> GetCmtByRegistro(string regAnimal)
        {
            var result = new List<Cmt>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Cmt_SelecByRegistro;

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
                cmd.CommandText = "Cmt_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Cmt_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Cmt_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Cmt_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _cmt.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _cmt.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _cmt.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _cmt.Comentarios));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Cmt_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _cmt.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _cmt.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _cmt.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _cmt.Comentarios));
            }
            return cmd;
        }

        protected Cmt load(SqlDataReader record)
        {
            var cmt = new Cmt();
            cmt.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            cmt.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) cmt.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            cmt.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            return cmt;
        }

    }
}
