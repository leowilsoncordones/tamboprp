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
    public class TipoEventoMapper : AbstractMapper
    {
        private TipoEvento _tipoev;

        public TipoEventoMapper()
        {
        }

        public TipoEventoMapper(TipoEvento tev)
        {
            _tipoev = tev;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public TipoEvento GetTipoEventoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (TipoEvento)load(dr);
        }


        public List<TipoEvento> GetAll()
        {
            var ls = new List<TipoEvento>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        public List<TipoEvento> GetEventosEnUso()
        {
            var result = new List<TipoEvento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "TipoEvento_SelectEnUso";

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        protected List<TipoEvento> loadAll(SqlDataReader rs)
        {
            var result = new List<TipoEvento>();
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
                cmd.CommandText = "TipoEvento_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID_EVENTO", _tipoev.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "TipoEvento_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "TipoEvento_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID_EVENTO", _tipoev.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "TipoEvento_Insert";
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _tipoev.Nombre));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "TipoEvento_Update";
                cmd.Parameters.Add(new SqlParameter("@ID_EVENTO", _tipoev.Id));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _tipoev.Nombre));
            }
            return cmd;
        }

        protected TipoEvento load(SqlDataReader record)
        {
            var tev = new TipoEvento();
            tev.Id = (short)((DBNull.Value == record["ID_EVENTO"]) ? 0 : (Int16)record["ID_EVENTO"]);
            tev.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];

            return tev;
        }

    }
}
