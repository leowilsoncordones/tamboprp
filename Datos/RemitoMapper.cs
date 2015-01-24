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
    public class RemitoMapper: AbstractMapper
    {
        private Remito _remito;

        private static string Remito_SelectByFecha = "Remito_SelectByFecha";
        private static string Remito_SelectByEmpresa = "Remito_SelectByEmpresa";
        private static string Remito_SelectAll = "Remito_SelectAll";
        private static string Remito_SelectByEmpresa2fechas = "Remito_SelectByEmpresa2fechas";
        

        public RemitoMapper(Remito remito)
        {
            _remito = remito;
        }

        public RemitoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Remito GetRemitoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Remito)load(dr);
        }

        public List<Remito> GetAll()
        {
            var ls = new List<Remito>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Remito> GetRemitoByFecha()
        {
            var result = new List<Remito>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA", _remito.Fecha));
            cmd.CommandText = Remito_SelectByFecha;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Remito> GetRemitoByEmpresa(int idEmpresa)
        {
            var result = new List<Remito>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EMPRESA", idEmpresa));
            cmd.CommandText = Remito_SelectByEmpresa;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        public List<Remito> GetRemitoByEmpresa2fechas(int idEmpresa, string fecha1, string fecha2)
        {
            var result = new List<Remito>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EMPRESA", idEmpresa));
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Remito_SelectByEmpresa2fechas;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        protected List<Remito> loadAll(SqlDataReader rs)
        {
            var result = new List<Remito>();
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
                cmd.CommandText = "Remito_SelectByFactura";
                cmd.Parameters.Add(new SqlParameter("@FACTURA", _remito.Factura));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Remito_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Remito_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@FACTURA", _remito.Factura));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Remito_Insert";
                cmd.Parameters.Add(new SqlParameter("@FECHA", _remito.Fecha));
                cmd.Parameters.Add(new SqlParameter("@EMPRESA", _remito.Empresa.Id));
                cmd.Parameters.Add(new SqlParameter("@FACTURA", _remito.Factura));
                cmd.Parameters.Add(new SqlParameter("@MATRICULA", _remito.Matricula));
                cmd.Parameters.Add(new SqlParameter("@LITROS", _remito.Litros));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _remito.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@ENCARGADO", _remito.Encargado));
                cmd.Parameters.Add(new SqlParameter("@TEMP_1", _remito.Temp_1));
                cmd.Parameters.Add(new SqlParameter("@TEMP_2", _remito.Temp_2));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Remito_Update";
                cmd.Parameters.Add(new SqlParameter("@FECHA", _remito.Fecha));
                cmd.Parameters.Add(new SqlParameter("@EMPRESA", _remito.Empresa.Id));
                cmd.Parameters.Add(new SqlParameter("@FACTURA", _remito.Factura));
                cmd.Parameters.Add(new SqlParameter("@MATRICULA", _remito.Matricula));
                cmd.Parameters.Add(new SqlParameter("@LITROS", _remito.Litros));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _remito.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@ENCARGADO", _remito.Encargado));
                cmd.Parameters.Add(new SqlParameter("@TEMP_1", _remito.Temp_1));
                cmd.Parameters.Add(new SqlParameter("@TEMP_2", _remito.Temp_2));
            }
            return cmd;
        }

        protected Remito load(SqlDataReader record)
        {
            var rem = new Remito();
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) rem.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            var empRem = new EmpresaRemisora();
            empRem.Id = (short)((DBNull.Value == record["EMPRESA"]) ? 0 : Int16.Parse(record["EMPRESA"].ToString()));
            rem.Empresa = empRem;
            rem.Factura = (DBNull.Value == record["FACTURA"]) ? string.Empty : (string)record["FACTURA"];
            rem.Matricula = (DBNull.Value == record["MATRICULA"]) ? string.Empty : (string)record["MATRICULA"];
            rem.Litros = (DBNull.Value == record["LITROS"]) ? 0 : double.Parse(record["LITROS"].ToString());
            rem.Observaciones = (DBNull.Value == record["OBSERVACIONES"]) ? string.Empty : (string)record["OBSERVACIONES"];
            rem.Encargado = (DBNull.Value == record["ENCARGADO"]) ? string.Empty : (string)record["ENCARGADO"];
            rem.Temp_1 = (DBNull.Value == record["TEMP_1"]) ? 0 : double.Parse(record["TEMP_1"].ToString());
            rem.Temp_2 = (DBNull.Value == record["TEMP_2"]) ? 0 : double.Parse(record["TEMP_2"].ToString());
            return rem;
        }

        public class VORemitoGrafica
    {
        public string Fecha { get; set; }
        public double Leche { get; set; }
    }

        public List<VORemitoGrafica> GetRemitosGrafica()
        {
            var result = new List<VORemitoGrafica>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Remito_SelectAll;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadRemitoGRafica(dr));
            dr.Close();
            return result;

        }

        protected VORemitoGrafica loadRemitoGRafica(SqlDataReader record)
        {
            var cp = new VORemitoGrafica();
            var date = new DateTime();
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) date = DateTime.Parse(strDate);
            cp.Fecha = date.ToString("yyyy/MM/dd");
            cp.Leche = (DBNull.Value == record["LITROS"]) ? 0 : double.Parse(record["LITROS"].ToString());
            return cp;
        }

        public List<VORemitoGrafica> GetRemitosEntreDosFechas(string fecha1, string fecha2)
        {
            var result = new List<VORemitoGrafica>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = "Remitos_EntreDosFechas";

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadRemitoGRafica(dr));
            dr.Close();
            return result;
        }
    }
}
