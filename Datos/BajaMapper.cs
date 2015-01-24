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
        private static string Baja_MuerteSelectCountByAnio = "Baja_MuerteSelectCountByAnio";
        private static string Baja_VentaSelectCountByAnio = "Baja_VentaSelectCountByAnio";
        private static string Baja_VentaAFrigSelectCountByAnio = "Baja_VentaAFrigSelectCountByAnio";
        private static string Baja_VentaRecNacidoSelectCountByAnio = "Baja_VentaRecNacidoSelectCountByAnio";
        private static string Baja_VentaViejaSelectCountByAnio = "Baja_VentaViejaSelectCountByAnio";
        private static string Baja_VentaSelectCountByMes = "Baja_VentaSelectCountByMes";
        private static string Baja_VentaAFrigSelectCountByMes = "Baja_VentaAFrigSelectCountByMes";
        private static string Baja_VentaRecNacidoSelectCountByMes = "Baja_VentaRecNacidoSelectCountByMes";
        private static string Baja_VentaViejaSelectCountByMes = "Baja_VentaViejaSelectCountByMes";
        private static string Baja_VentaSelectCountBy2fechas = "Baja_VentaSelectCountBy2fechas";
        private static string Baja_VentaAFrigSelectCountBy2fechas = "Baja_VentaAFrigSelectCountBy2fechas";
        private static string Baja_VentaRecNacidoSelectCountBy2fechas = "Baja_VentaRecNacidoSelectCountBy2fechas";
        private static string Baja_VentaViejaSelectCountBy2fechas = "Baja_VentaViejaSelectCountBy2fechas";
        private static string Baja_MuerteByAnioSelectCount2fechas = "Baja_MuerteByAnioSelectCount2fechas";
        private static string Baja_MuerteBy2fechasSelectAll = "Baja_MuerteBy2fechasSelectAll";
        
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

        public int GetCantMuertesPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_MuerteSelectCountByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_VentaSelectCountByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasPorMes(int mes)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.CommandText = Baja_VentaSelectCountByMes;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_VentaSelectCountBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasAFrigPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_VentaAFrigSelectCountByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasAFrigPorMes(int mes)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.CommandText = Baja_VentaAFrigSelectCountByMes;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasAFrigPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_VentaAFrigSelectCountBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasRecienNacidosPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_VentaRecNacidoSelectCountByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasRecienNacidosPorMes(int mes)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.CommandText = Baja_VentaRecNacidoSelectCountByMes;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasRecienNacidosPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_VentaRecNacidoSelectCountBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasViejasPorAnio(int anio)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Baja_VentaViejaSelectCountByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }


        public int GetCantVentasViejasPorMes(int mes)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.CommandText = Baja_VentaViejaSelectCountByMes;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantVentasViejasPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_VentaViejaSelectCountBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
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

        public List<VOEnfermedad> GetCantidadMuertesPorEnfermedadPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<VOEnfermedad>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_MuerteByAnioSelectCount2fechas;

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

        public List<Baja> GetMuertesPor2fechas(string fecha1, string fecha2)
        {
            var result = new List<Baja>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Baja_MuerteBy2fechasSelectAll;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }
    }
}
