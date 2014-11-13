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
    public class CategoriaMapper : AbstractMapper
    {
        private Categoria _categ;

        public CategoriaMapper()
        {
        }

        public CategoriaMapper(Categoria categ)
        {
            _categ = categ;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Categoria GetCategoriaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Categoria)load(dr);
        }


        public List<Categoria> GetAll()
        {
            List<Categoria> ls = new List<Categoria>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Categoria> loadAll(SqlDataReader rs)
        {
            List<Categoria> result = new List<Categoria>();
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
                cmd.CommandText = "Categoria_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG", _categ.Id_categ));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Categoria_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Categoria_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG", _categ.Id_categ));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Categoria_Insert";
                //cmd.Parameters.Add(new SqlParameter("@ID_CATEG", _categ.Id_categ));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _categ.Nombre));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACION", _categ.Observacion));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Categoria_Update";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG", _categ.Id_categ));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _categ.Nombre));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACION", _categ.Observacion));
            }
            return cmd;
        }

        protected Categoria load(SqlDataReader record)
        {
            var categ = new Categoria();
            categ.Id_categ = (short)((DBNull.Value == record["ID_CATEG"]) ? 0 : (Int16)record["ID_CATEG"]);
            categ.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            categ.Observacion = (DBNull.Value == record["OBSERVACION"]) ? string.Empty : (string)record["OBSERVACION"];

            return categ;
        }

    }
}
