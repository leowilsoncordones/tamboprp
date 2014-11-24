using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class ServicioMapper : AbstractMapper
    {
        private Servicio _servicio;
        private string _registroAnimal;

        private static string Servicio_SelecByRegistro = "Servicio_SelecByRegistro";
        private static string Servicio_SelecProximosPartos = "Servicio_SelecProximosPartos";

        public ServicioMapper(Servicio servicio)
        {
            _servicio = servicio;
        }

        public ServicioMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public ServicioMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Servicio GetServicioById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Servicio)load(dr);
        }


        public List<Servicio> GetAll()
        {
            var ls = new List<Servicio>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Servicio> loadAll(SqlDataReader rs)
        {
            var result = new List<Servicio>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetServiciosByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Servicio_SelecByRegistro;

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
                cmd.CommandText = "Servicio_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _servicio.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _servicio.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _servicio.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@SERV_MONTA_NATURAL", _servicio.Serv_monta_natural));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _servicio.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@INSEMINADOR", _servicio.Inseminador.Id_empleado));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _servicio.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _servicio.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _servicio.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@SERV_MONTA_NATURAL", _servicio.Serv_monta_natural));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _servicio.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@INSEMINADOR", _servicio.Inseminador.Id_empleado));
            }
            return cmd;
        }

        protected Servicio load(SqlDataReader record)
        {
            var serv = new Servicio();
            serv.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            serv.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) serv.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            serv.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            serv.Serv_monta_natural = (DBNull.Value == record["SERV_MONTA_NATURAL"]) ? ' ' : Convert.ToChar(record["SERV_MONTA_NATURAL"]);
            serv.Reg_padre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];

            int idInseminador = (short)((DBNull.Value == record["INSEMINADOR"]) ? 0 : (Int16)record["INSEMINADOR"]);
            if (idInseminador > 0)
            {
                var insTemp = new Empleado();
                insTemp.Id_empleado = (Int16)idInseminador;
                serv.Inseminador = insTemp;
            }
            return serv;
        }


        public List<Servicio> GetProximosPartos(string mes, string anio)
        {
            List<Servicio> result = new List<Servicio>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Servicio_SelecProximosPartos;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

    }
}
