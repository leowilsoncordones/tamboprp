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
    public class EmpresaRemisoraMapper: AbstractMapper
    {
        private EmpresaRemisora _empRem;

        private static string EmpresaRemisora_SelectActual = "EmpresaRemisora_SelectActual";

        public EmpresaRemisoraMapper(EmpresaRemisora empRem)
        {
            _empRem = empRem;
        }

        public EmpresaRemisoraMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public EmpresaRemisora GetEmpresaRemisoraById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (EmpresaRemisora)load(dr);
        }

        public List<EmpresaRemisora> GetEmpresaRemisoraSelectActual()
        {
            var result = new List<EmpresaRemisora>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = EmpresaRemisora_SelectActual;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;

        }

        public List<EmpresaRemisora> GetAll()
        {
            var ls = new List<EmpresaRemisora>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        protected List<EmpresaRemisora> loadAll(SqlDataReader rs)
        {
            var result = new List<EmpresaRemisora>();
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
                cmd.CommandText = "EmpresaRemisora_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID", _empRem.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaRemisora_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaRemisora_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@ID", _empRem.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaRemisora_Insert";
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empRem.Nombre));
                cmd.Parameters.Add(new SqlParameter("@RAZON_SOCIAL", _empRem.RazonSocial));
                cmd.Parameters.Add(new SqlParameter("@RUT", _empRem.Rut));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION", _empRem.Direccion));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _empRem.Telefono));
                cmd.Parameters.Add(new SqlParameter("@ACTUAL", _empRem.Actual));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaRemisora_Update";
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empRem.Nombre));
                cmd.Parameters.Add(new SqlParameter("@RAZON_SOCIAL", _empRem.RazonSocial));
                cmd.Parameters.Add(new SqlParameter("@RUT", _empRem.Rut));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION", _empRem.Direccion));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _empRem.Telefono));
                cmd.Parameters.Add(new SqlParameter("@ACTUAL", _empRem.Actual));
            }
            return cmd;
        }

        protected EmpresaRemisora load(SqlDataReader record)
        {
            var empRem = new EmpresaRemisora();
            empRem.Id = (short)((DBNull.Value == record["ID"]) ? 0 : Int16.Parse(record["ID"].ToString()));
            empRem.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            empRem.RazonSocial = (DBNull.Value == record["RAZON_SOCIAL"]) ? string.Empty : (string)record["RAZON_SOCIAL"];
            empRem.Rut = (DBNull.Value == record["RUT"]) ? string.Empty : (string)record["RUT"];
            empRem.Direccion = (DBNull.Value == record["DIRECCION"]) ? string.Empty : (string)record["DIRECCION"];
            empRem.Telefono = (DBNull.Value == record["TELEFONO"]) ? string.Empty : (string)record["TELEFONO"];
            empRem.Actual = (DBNull.Value == record["ACTUAL"]) ? ' ' : Convert.ToChar(record["ACTUAL"]);
            return empRem;
        }

    }
}
