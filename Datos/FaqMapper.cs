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
    public class FaqMapper : AbstractMapper
    {
        private Faq _faq;

        public FaqMapper()
        {
        }

        public FaqMapper(Faq faq)
        {
            _faq = faq;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }


        public List<Faq> GetAll()
        {
            List<Faq> ls = new List<Faq>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Faq> loadAll(SqlDataReader rs)
        {
            List<Faq> result = new List<Faq>();
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
                cmd.CommandText = "Faq_SelecById";
                cmd.Parameters.Add(new SqlParameter("@ID", _faq.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Faq_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Faq_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID", _faq.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Faq_Insert";
                cmd.Parameters.Add(new SqlParameter("@PREGUNTA", _faq.Pregunta));
                cmd.Parameters.Add(new SqlParameter("@RESPUESTA_HTML", _faq.Respuesta));
                cmd.Parameters.Add(new SqlParameter("@ICONO", _faq.Icono));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Faq_Update";
                cmd.Parameters.Add(new SqlParameter("@ID", _faq.Id));
                cmd.Parameters.Add(new SqlParameter("@PREGUNTA", _faq.Pregunta));
                cmd.Parameters.Add(new SqlParameter("@RESPUESTA_HTML", _faq.Respuesta));
                cmd.Parameters.Add(new SqlParameter("@ICONO", _faq.Icono));
            }
            return cmd;
        }

        protected Faq load(SqlDataReader record)
        {
            var faq = new Faq();
            faq.Id = (DBNull.Value == record["ID"]) ? -1 : int.Parse(record["ID"].ToString());
            faq.Pregunta = (DBNull.Value == record["PREGUNTA"]) ? string.Empty : (string)record["PREGUNTA"];
            faq.Respuesta = (DBNull.Value == record["RESPUESTA_HTML"]) ? string.Empty : (string)record["RESPUESTA_HTML"];
            faq.Icono = (DBNull.Value == record["ICONO"]) ? string.Empty : (string)record["ICONO"];
            return faq;
        }

    }
}
