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
    public class CalificacionMapper : AbstractMapper
    {
        private Calificacion _calificacion;
        private string _registroAnimal;

        private static string Calificacion_SelecByRegistro = "Calificacion_SelecByRegistro";

        public CalificacionMapper(Calificacion Calificacion)
        {
            _calificacion = Calificacion;
        }

        public CalificacionMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public CalificacionMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Calificacion GetCalificacionbyId()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Calificacion)load(dr);
        }


        public List<Calificacion> GetAll()
        {
            List<Calificacion> ls = new List<Calificacion>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Calificacion> loadAll(SqlDataReader rs)
        {
            List<Calificacion> result = new List<Calificacion>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetCalificacionesRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Calificacion_SelecByRegistro;

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
                cmd.CommandText = "Calificacion_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Calificacion_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Calificacion_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Calificacion_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _calificacion.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@LETRAS", _calificacion.Letras));
                cmd.Parameters.Add(new SqlParameter("@PUNTOS", _calificacion.Puntos));
                cmd.Parameters.Add(new SqlParameter("@FECHA_CALIF", _calificacion.Fecha));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Calificacion_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _calificacion.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@LETRAS", _calificacion.Letras));
                cmd.Parameters.Add(new SqlParameter("@PUNTOS", _calificacion.Puntos));
                cmd.Parameters.Add(new SqlParameter("@FECHA_CALIF", _calificacion.Fecha));
            }
            return cmd;
        }

        protected Calificacion load(SqlDataReader record)
        {
            var calif = new Calificacion();
            calif.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            calif.Puntos = (short)((DBNull.Value == record["PUNTOS"]) ? 0 : (Int16)record["PUNTOS"]);
            calif.Letras = (DBNull.Value == record["LETRAS"]) ? string.Empty : (string)record["LETRAS"];
            string strDate = (DBNull.Value == record["FECHA_CALIF"]) ? string.Empty : record["FECHA_CALIF"].ToString();
            if (strDate != string.Empty) calif.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            return calif;
        }

    }
}
