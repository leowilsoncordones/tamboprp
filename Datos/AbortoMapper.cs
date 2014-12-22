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
    public class AbortoMapper : AbstractMapper
    {
        private Aborto _aborto;
        private string _registroAnimal;

        private static string Aborto_SelecByRegistro = "Aborto_SelecByRegistro";
        private string Aborto_SelectCountEsteAnio = "Aborto_SelectCountEsteAnio";
        private string Aborto_SelectAnimalesConServicios = "Aborto_SelectAnimalesConServicios";
        private string Aborto_GetServicioPadre = "Aborto_GetServicioPadre";

        public AbortoMapper(Aborto aborto)
        {
            _aborto = aborto;
        }

        public AbortoMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public AbortoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Aborto GetAbortoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Aborto)load(dr);
        }


        public List<Aborto> GetAll()
        {
            List<Aborto> ls = new List<Aborto>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Aborto> loadAll(SqlDataReader rs)
        {
            List<Aborto> result = new List<Aborto>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetAbortosByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Aborto_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public int GetCantAbortosEsteAnio()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Aborto_SelectCountEsteAnio;

            result = (int)ReturnScalarValue(cmd);
            return result;
        }

        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Aborto_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Aborto_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Aborto_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Aborto_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _aborto.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _aborto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _aborto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _aborto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@REG_SERV", _aborto.Reg_padre));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Aborto_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _aborto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _aborto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _aborto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@REG_SERV", _aborto.Reg_padre));
            }
            return cmd;
        }

        protected Aborto load(SqlDataReader record)
        {
            var aborto = new Aborto();
            aborto.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) aborto.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            aborto.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            aborto.Reg_padre = (DBNull.Value == record["REG_SERV"]) ? string.Empty : (string)record["REG_SERV"];

            return aborto;
        }

        protected Aborto loadReg(SqlDataReader record)
        {
            var aborto = new Aborto();
            aborto.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"]; 
            return aborto;
        }

        public List<Aborto> GetAbortosAnimalesConServicio()
        {
            List<Aborto> result = new List<Aborto>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Aborto_SelectAnimalesConServicios;
            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadReg(dr));
            dr.Close();
            return result;
        }

        public string GetServicioPadreAborto(string reg)
        {
            return GetScalarString(Aborto_GetServicioPadre, reg);
        }

    }
}
