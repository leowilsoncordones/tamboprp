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
    public class Diag_PrenezMapper : AbstractMapper
    {
        private Diag_Prenez _diag;
        private string _registroAnimal;

        private static string Diag_prenez_SelecByRegistro = "Diag_prenez_SelectByRegistro";

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
            List<Diag_Prenez> ls = new List<Diag_Prenez>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Diag_Prenez> loadAll(SqlDataReader rs)
        {
            List<Diag_Prenez> result = new List<Diag_Prenez>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetDiag_PrenezByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
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

    }
}
