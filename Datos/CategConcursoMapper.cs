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
    public class CategConcursoMapper : AbstractMapper
    {
        private CategoriaConcurso _categ;

        public CategConcursoMapper()
        {
        }

        public CategConcursoMapper(CategoriaConcurso categ)
        {
            _categ = categ;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public CategoriaConcurso GetCategoriaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (CategoriaConcurso)load(dr);
        }


        public List<CategoriaConcurso> GetAll()
        {
            var ls = new List<CategoriaConcurso>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<CategoriaConcurso> loadAll(SqlDataReader rs)
        {
            var result = new List<CategoriaConcurso>();
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
                cmd.CommandText = "CategoriaConcurso_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG_CONC", _categ.Id_categ));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CategoriaConcurso_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CategoriaConcurso_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG_CONC", _categ.Id_categ));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CategoriaConcurso_Insert";
                //cmd.Parameters.Add(new SqlParameter("@ID_CATEG", _categ.Id_categ));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _categ.Nombre));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CategoriaConcurso_Update";
                cmd.Parameters.Add(new SqlParameter("@ID_CATEG_CONC", _categ.Id_categ));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _categ.Nombre));
            }
            return cmd;
        }

        protected CategoriaConcurso load(SqlDataReader record)
        {
            var categ = new CategoriaConcurso();
            categ.Id_categ = (short)((DBNull.Value == record["ID_CATEG_CONC"]) ? 0 : (Int16)record["ID_CATEG_CONC"]);
            categ.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];

            return categ;
        }

    }
}
