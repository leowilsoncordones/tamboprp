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
    public class EmpresaMapper: AbstractMapper
    {
        private Empresa _empresa;

        private static string Empresa_SelectActual = "Empresa_SelectActual";

        public EmpresaMapper(Empresa empresa)
        {
            _empresa = empresa;
        }

        public EmpresaMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Empresa GetEmpresaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Empresa)load(dr);
        }

        public Empresa GetEmpresaSelectActual()
        {
            var result = new List<Empresa>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Empresa_SelectActual;

            SqlDataReader dr = FindByCmd(cmd);
            dr.Read();
            return (Empresa)load(dr);

        }
        
        public List<Empresa> GetAll()
        {
            var ls = new List<Empresa>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        protected List<Empresa> loadAll(SqlDataReader rs)
        {
            var result = new List<Empresa>();
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
                cmd.CommandText = "Empresa_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID", _empresa.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@ID", _empresa.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Insert";
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empresa.Nombre));
                cmd.Parameters.Add(new SqlParameter("@RAZON_SOCIAL", _empresa.RazonSocial));
                cmd.Parameters.Add(new SqlParameter("@RUT", _empresa.Rut));
                cmd.Parameters.Add(new SqlParameter("@LETRA_SISTEMA", _empresa.Rut));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION", _empresa.Direccion));
                cmd.Parameters.Add(new SqlParameter("@CIUDAD", _empresa.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _empresa.Telefono));
                cmd.Parameters.Add(new SqlParameter("@CELULAR", _empresa.Celular));
                cmd.Parameters.Add(new SqlParameter("@WEB", _empresa.Web));
                cmd.Parameters.Add(new SqlParameter("@CP", _empresa.Cpostal));
                cmd.Parameters.Add(new SqlParameter("@LOGO", _empresa.Logo));
                cmd.Parameters.Add(new SqlParameter("@LOGO_CH", _empresa.LogoCh));
                cmd.Parameters.Add(new SqlParameter("@ACTUAL", _empresa.Actual));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Update";
                cmd.Parameters.Add(new SqlParameter("@ID", _empresa.Id));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empresa.Nombre));
                cmd.Parameters.Add(new SqlParameter("@RAZON_SOCIAL", _empresa.RazonSocial));
                cmd.Parameters.Add(new SqlParameter("@RUT", _empresa.Rut));
                cmd.Parameters.Add(new SqlParameter("@LETRA_SISTEMA", _empresa.Rut));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION", _empresa.Direccion));
                cmd.Parameters.Add(new SqlParameter("@CIUDAD", _empresa.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _empresa.Telefono));
                cmd.Parameters.Add(new SqlParameter("@CELULAR", _empresa.Celular));
                cmd.Parameters.Add(new SqlParameter("@WEB", _empresa.Web));
                cmd.Parameters.Add(new SqlParameter("@CP", _empresa.Cpostal));
                cmd.Parameters.Add(new SqlParameter("@LOGO", _empresa.Logo));
                cmd.Parameters.Add(new SqlParameter("@LOGO_CH", _empresa.LogoCh));
                cmd.Parameters.Add(new SqlParameter("@ACTUAL", _empresa.Actual));
            }
            return cmd;
        }

        protected Empresa load(SqlDataReader record)
        {
            var empresa = new Empresa();
            empresa.Id = (short)((DBNull.Value == record["ID"]) ? 0 : Int16.Parse(record["ID"].ToString()));
            empresa.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            empresa.RazonSocial = (DBNull.Value == record["RAZON_SOCIAL"]) ? string.Empty : (string)record["RAZON_SOCIAL"];
            empresa.Rut = (DBNull.Value == record["RUT"]) ? string.Empty : (string)record["RUT"];
            empresa.LetraSistema = (DBNull.Value == record["LETRA_SISTEMA"]) ? string.Empty : (string)record["LETRA_SISTEMA"];
            empresa.LogoCh = (DBNull.Value == record["LOGO_CH"]) ? string.Empty : (string)record["LOGO_CH"];
            empresa.Direccion = (DBNull.Value == record["DIRECCION"]) ? string.Empty : (string)record["DIRECCION"];
            empresa.Ciudad = (DBNull.Value == record["CIUDAD"]) ? string.Empty : (string)record["CIUDAD"];
            empresa.Telefono = (DBNull.Value == record["TELEFONO"]) ? string.Empty : (string)record["TELEFONO"];
            empresa.Celular = (DBNull.Value == record["CELULAR"]) ? string.Empty : (string)record["CELULAR"];
            empresa.Web = (DBNull.Value == record["WEB"]) ? string.Empty : (string)record["WEB"];
            empresa.Cpostal = (DBNull.Value == record["CP"]) ? string.Empty : (string)record["CP"];
            empresa.Logo = (DBNull.Value == record["LOGO"]) ? string.Empty : (string)record["LOGO"];
            empresa.LogoCh = (DBNull.Value == record["LOGO_CH"]) ? string.Empty : (string)record["LOGO_CH"];
            empresa.Actual = (DBNull.Value == record["ACTUAL"]) ? ' ' : Convert.ToChar(record["ACTUAL"]);
            return empresa;
        }

    }
}
