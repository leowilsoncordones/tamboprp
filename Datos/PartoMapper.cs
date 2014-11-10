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
    public class PartoMapper : AbstractMapper
    {
        private Parto _parto;
        private string _registroAnimal;

        private static string Parto_SelecByRegistro = "Partos_SelecByRegistro";

        public PartoMapper(Parto parto)
        {
            _parto = parto;
        }

        public PartoMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public PartoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Parto GetPartoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Parto)load(dr);
        }


        public List<Parto> GetAll()
        {
            List<Parto> ls = new List<Parto>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Parto> loadAll(SqlDataReader rs)
        {
            List<Parto> result = new List<Parto>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetPartosByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = "Parto_SelecByRegistro";

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Evento> GetNacimientoByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REG_HIJO", regAnimal));
            cmd.CommandText = "Parto_SelecNacimByRegistro";

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
                cmd.CommandText = "Parto_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _parto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", _parto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@FECHA_SERV", _parto.Fecha_serv));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _parto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _parto.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@SEXO_PARTO", _parto.Sexo_parto));
                cmd.Parameters.Add(new SqlParameter("@REG_HIJO", _parto.Reg_hijo));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _parto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", _parto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@FECHA_SERV", _parto.Fecha_serv));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _parto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _parto.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@SEXO_PARTO", _parto.Sexo_parto));
                cmd.Parameters.Add(new SqlParameter("@REG_HIJO", _parto.Reg_hijo));
            }
            return cmd;
        }

        protected Parto load(SqlDataReader record)
        {
            var parto = new Parto();
            parto.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA_PARTO"]) ? string.Empty : record["FECHA_PARTO"].ToString();
            if (strDate != string.Empty) parto.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            strDate = (DBNull.Value == record["FECHA_SERV"]) ? string.Empty : record["FECHA_SERV"].ToString();
            if (strDate != string.Empty) parto.Fecha_serv = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            parto.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            parto.Observaciones = (DBNull.Value == record["OBSERVACIONES"]) ? string.Empty : (string)record["OBSERVACIONES"];
            parto.Sexo_parto = (DBNull.Value == record["SEXO_PARTO"]) ? ' ' : Convert.ToChar(record["SEXO_PARTO"]);
            parto.Reg_hijo = (DBNull.Value == record["REG_HIJO"]) ? string.Empty : (string)record["REG_HIJO"];

            return parto;
        }

    }
}
