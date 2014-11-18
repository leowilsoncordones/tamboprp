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
    public class Celo_Sin_ServicioMapper : AbstractMapper
    {
        private Celo_Sin_Servicio _celo;
        private string _registroAnimal;

        private static string Celo_Sin_Servicio_SelecByRegistro = "Celo_Sin_Servicio_SelecByRegistro";

        public Celo_Sin_ServicioMapper(Celo_Sin_Servicio CeloSS)
        {
            _celo = CeloSS;
        }

        public Celo_Sin_ServicioMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public Celo_Sin_ServicioMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Celo_Sin_Servicio GetCeloById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Celo_Sin_Servicio)load(dr);
        }


        public List<Celo_Sin_Servicio> GetAll()
        {
            var ls = new List<Celo_Sin_Servicio>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Celo_Sin_Servicio> loadAll(SqlDataReader rs)
        {
            var result = new List<Celo_Sin_Servicio>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetCelosByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Celo_Sin_Servicio_SelecByRegistro;

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
                cmd.CommandText = "Celo_Sin_Servicio_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Celo_Sin_Servicio_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Celo_Sin_Servicio_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Celo_Sin_Servicio_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _celo.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _celo.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _celo.Comentarios));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Celo_Sin_Servicio_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _celo.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _celo.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _celo.Comentarios));
            }
            return cmd;
        }

        protected Celo_Sin_Servicio load(SqlDataReader record)
        {
            var celo = new Celo_Sin_Servicio();
            celo.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) celo.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            celo.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];

            return celo;
        }

    }
}
