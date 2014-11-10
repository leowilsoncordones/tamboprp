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
    public class ConcursoMapper : AbstractMapper
    {
        private Concurso _concurso;
        private string _registroAnimal;

        private static string Concurso_SelecByRegistro = "Concurso_SelecByRegistro";

        public ConcursoMapper(Concurso concurso)
        {
            _concurso = concurso;
        }

        public ConcursoMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public ConcursoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Concurso GetConcursoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Concurso)load(dr);
        }


        public List<Concurso> GetAll()
        {
            List<Concurso> ls = new List<Concurso>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Concurso> loadAll(SqlDataReader rs)
        {
            List<Concurso> result = new List<Concurso>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetConcursosByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Concurso_SelecByRegistro;

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
                cmd.CommandText = "Concurso_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Concurso_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Concurso_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Concurso_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _concurso.Id_evento));
                //cmd.Parameters.Add(new SqlParameter("@NOM_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@LUGAR", _concurso.));
                cmd.Parameters.Add(new SqlParameter("@ANIO", _concurso.Anio));
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
                cmd.Parameters.Add(new SqlParameter("@ANIO", _concurso.Anio));
                //cmd.Parameters.Add(new SqlParameter("@CATEG_CONCURSO", _concurso.));
                //cmd.Parameters.Add(new SqlParameter("@PREMIO", _concurso.));
            }
            return cmd;
        }

        protected Concurso load(SqlDataReader record)
        {
            var conc = new Concurso();
            conc.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            conc.Anio = (short)((DBNull.Value == record["ANIO"]) ? 0 : (Int16)record["ANIO"]);

            return conc;
        }

    }
}
