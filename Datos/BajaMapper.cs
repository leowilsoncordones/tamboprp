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
    public class BajaMapper : AbstractMapper
    {
        private Baja _baja;
        private string _registroAnimal;

        private static string Baja_SelecByRegistro = "Baja_SelecByRegistro";
        private static string Baja_MuerteByAnioSelectAll = "Baja_MuerteByAnioSelectAll";
        private static string Baja_MuerteByAnioSelectCount = "Baja_MuerteByAnioSelectCount";

        public BajaMapper(Baja baja)
        {
            _baja = baja;
        }

        public BajaMapper(string registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public BajaMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Baja GetBajaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Baja)load(dr);
        }


        public List<Baja> GetAll()
        {
            var ls = new List<Baja>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Baja> loadAll(SqlDataReader rs)
        {
            var result = new List<Baja>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetBajaByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Baja_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Baja> GetMuertesPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_MuerteByAnioSelectAll;

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
                cmd.CommandText = "Baja_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _baja.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _baja.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _baja.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _baja.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@BAJAS", _baja.Codigo));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _baja.Enfermedad));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _baja.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _baja.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _baja.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _baja.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@BAJAS", _baja.Codigo));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _baja.Enfermedad));
            }
            return cmd;
        }

        protected Baja load(SqlDataReader record)
        {
            var baja = new Baja();
            baja.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            baja.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) baja.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            baja.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            baja.Codigo = (DBNull.Value == record["BAJAS"]) ? string.Empty : (string)record["BAJAS"];
            var enf = new Enfermedad();
            enf.Id = (short) ((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16) record["ENFERMEDAD"]);
            baja.Enfermedad = enf;
        
            return baja;
        }

        public List<VOEnfermedad> GetCantidadMuertesPorEnfermedadPorAnio(int anio)
        {
            var result = new List<VOEnfermedad>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_MuerteByAnioSelectCount;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadCantEnfermedades(dr));
            dr.Close();
            return result;
        }

        protected VOEnfermedad loadCantEnfermedades(SqlDataReader record)
        {
            var enf = new VOEnfermedad();
            enf.Id = (short)((DBNull.Value == record["ID_ENFERMEDAD"]) ? 0 : (Int16)record["ID_ENFERMEDAD"]);
            enf.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : record["NOMBRE"].ToString();
            //enf.Cantidad = (short)((DBNull.Value == record["CANTIDAD"]) ? 0 : (Int16)record["CANTIDAD"]);
            enf.Cantidad = (DBNull.Value == record["CANTIDAD"]) ? 0 : int.Parse(record["CANTIDAD"].ToString());
            return enf;
        }

        public class VOEnfermedad
        {
            public VOEnfermedad()
            {

            }

            public VOEnfermedad(int idEnf, string nombre, int cantidad)
            {
                Id = idEnf;
                Nombre = nombre;
                Cantidad = cantidad;
            }

            public int Id { get; set; }

            public string Nombre { get; set; }

            public int Cantidad { get; set; }

            public double Porcentaje { get; set; }

        }

    }
}
