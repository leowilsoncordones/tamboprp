using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class EmpleadoMapper: AbstractMapper
    {
        private Empleado _empleado;
        private string _nickName;
        private readonly string Empleado_SelectAllActivos = "Empleado_SelectAllActivos";

        public EmpleadoMapper(Empleado empleado)
        {
            _empleado = empleado;
        }

        public EmpleadoMapper(Empleado empleado, string nickName)
        {
            _empleado = empleado;
            _nickName = nickName;
        }

        public EmpleadoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Empleado GetEmpleadoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Empleado)load(dr);
        }

        public List<Empleado> GetAll()
        {
            var ls = new List<Empleado>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Empleado> GetAllActivos()
        {
            var result = new List<Empleado>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Empleado_SelectAllActivos;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        protected List<Empleado> loadAll(SqlDataReader rs)
        {
            var result = new List<Empleado>();
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
                cmd.CommandText = "Empleado_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", _empleado.Id_empleado));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empleado_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empleado_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", _empleado.Id_empleado));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empleado_Insert";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empleado.Nombre));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO", _empleado.Apellido));
                cmd.Parameters.Add(new SqlParameter("@INICIALES", _empleado.Iniciales));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", _empleado.Activo));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Empleado_Update";
                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", _empleado.Id_empleado));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _empleado.Nombre));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO", _empleado.Apellido));
                //cmd.Parameters.Add(new SqlParameter("@INICIALES", _empleado.Iniciales));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", _empleado.Activo));
            }
            return cmd;
        }

        protected Empleado load(SqlDataReader record)
        {
            var emp = new Empleado();
            var idEmp = (short)((DBNull.Value == record["ID_EMPLEADO"]) ? 0 : (Int16) record["ID_EMPLEADO"]);
            emp.Id_empleado = idEmp;
            emp.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            emp.Apellido = (DBNull.Value == record["APELLIDO"]) ? string.Empty : (string)record["APELLIDO"];
            emp.Iniciales = (DBNull.Value == record["INICIALES"]) ? string.Empty : (string)record["INICIALES"];
            emp.Activo = (bool)record["ACTIVO"];
            return emp;
        }

    }
}
