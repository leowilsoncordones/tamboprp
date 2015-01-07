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
    public class CasoSoporteMapper: AbstractMapper
    {
        private CasoSoporte _caso;

        public CasoSoporteMapper(CasoSoporte caso)
        {
            _caso = caso;
        }

        public CasoSoporteMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public CasoSoporte GetCasoSoporteById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (CasoSoporte)load(dr);
        }


        public List<CasoSoporte> GetAll()
        {
            var ls = new List<CasoSoporte>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        protected List<CasoSoporte> loadAll(SqlDataReader rs)
        {
            var result = new List<CasoSoporte>();
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
                cmd.CommandText = "CasoSoporte_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID", _caso.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CasoSoporte_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CasoSoporte_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@ID", _caso.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CasoSoporte_Insert";
                //cmd.Parameters.Add(new SqlParameter("@ID", _caso.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTABLECIMIENTO", _caso.Establecimiento));
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _caso.Nickname));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE_APELLIDO", _caso.NombreApellido));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _caso.Telefono));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", _caso.Email));
                cmd.Parameters.Add(new SqlParameter("@TITULO", _caso.Titulo));
                cmd.Parameters.Add(new SqlParameter("@TIPO", _caso.Tipo));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", _caso.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@ADJUNTO", _caso.Adjunto));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CasoSoporte_Update";
                cmd.Parameters.Add(new SqlParameter("@ID", _caso.Id));
                cmd.Parameters.Add(new SqlParameter("@ESTABLECIMIENTO", _caso.Establecimiento));
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _caso.Nickname));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE_APELLIDO", _caso.NombreApellido));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO", _caso.Telefono));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", _caso.Email));
                cmd.Parameters.Add(new SqlParameter("@TITULO", _caso.Titulo));
                cmd.Parameters.Add(new SqlParameter("@TIPO", _caso.Tipo));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPCION", _caso.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@ADJUNTO", _caso.Adjunto));

            }
            return cmd;
        }

        protected CasoSoporte load(SqlDataReader record)
        {
            var caso = new CasoSoporte();
            caso.Id = (short)((DBNull.Value == record["ID"]) ? 0 : Int16.Parse(record["ID"].ToString()));
            caso.Establecimiento = (DBNull.Value == record["ESTABLECIMIENTO"]) ? string.Empty : (string)record["ESTABLECIMIENTO"];
            caso.Nickname = (DBNull.Value == record["NICKNAME"]) ? string.Empty : (string)record["NICKNAME"];
            caso.NombreApellido = (DBNull.Value == record["NOMBRE_APELLIDO"]) ? string.Empty : (string)record["NOMBRE_APELLIDO"];
            caso.Telefono = (DBNull.Value == record["TELEFONO"]) ? string.Empty : (string)record["TELEFONO"];
            caso.Email = (DBNull.Value == record["EMAIL"]) ? string.Empty : (string)record["EMAIL"];
            caso.Titulo = (DBNull.Value == record["TITULO"]) ? string.Empty : (string)record["TITULO"];
            caso.Tipo = (DBNull.Value == record["TIPO"]) ? string.Empty : (string)record["TIPO"];
            caso.Descripcion = (DBNull.Value == record["DESCRIPCION"]) ? string.Empty : (string)record["DESCRIPCION"];
            caso.Adjunto = (DBNull.Value == record["ADJUNTO"]) ? string.Empty : (string)record["ADJUNTO"];
            return caso;
        }

    }
}
