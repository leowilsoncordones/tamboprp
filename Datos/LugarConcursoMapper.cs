using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class LugarConcursoMapper : AbstractMapper
    {
        private LugarConcurso _lugarConcurso;

        

        public LugarConcursoMapper(LugarConcurso lugarConcurso)
        {
            _lugarConcurso = lugarConcurso;
        }


        public LugarConcursoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public LugarConcurso GetLugarConcursoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (LugarConcurso)load(dr);
        }


        public List<LugarConcurso> GetAll()
        {
            var ls = new List<LugarConcurso>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<LugarConcurso> loadAll(SqlDataReader rs)
        {
            var result = new List<LugarConcurso>();
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
                cmd.CommandText = "Lugar_Concurso_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID", _lugarConcurso.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Lugar_Concurso_SelectAll";
            }
/*
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "LugarConcurso_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "LugarConcurso_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _concurso.Id_evento));
                //cmd.Parameters.Add(new SqlParameter("@NOM_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@LUGAR", _concurso.));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _concurso.Fecha));
                //cmd.Parameters.Add(new SqlParameter("@CATEG_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@PREMIO", _concurso.));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Concurso_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _concurso.Id_evento));
                //cmd.Parameters.Add(new SqlParameter("@NOM_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@LUGAR", _concurso.));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _concurso.Fecha));
                //cmd.Parameters.Add(new SqlParameter("@CATEG_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@PREMIO", _concurso.));
            }
*/
            return cmd;
        }

        protected LugarConcurso load(SqlDataReader record)
        {
            var lugConc = new LugarConcurso();
            lugConc.Id = (int)((DBNull.Value == record["ID"]) ? 0 : (int)record["ID"]);
            lugConc.NombreExpo = (DBNull.Value == record["NOM_CONCURSO"]) ? string.Empty : (string)record["NOM_CONCURSO"];
            lugConc.Lugar = (DBNull.Value == record["LUGAR"]) ? string.Empty : (string)record["LUGAR"];
            return lugConc;
        }

    }
}
