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
    public class EnfermedadMapper : AbstractMapper
    {
        private Enfermedad _enf;

        public EnfermedadMapper()
        {
        }

        public EnfermedadMapper(Enfermedad enf)
        {
            _enf = enf;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Enfermedad GetEnfermedadById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Enfermedad)load(dr);
        }


        public List<Enfermedad> GetAll()
        {
            var ls = new List<Enfermedad>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Enfermedad> loadAll(SqlDataReader rs)
        {
            var result = new List<Enfermedad>();
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
                cmd.CommandText = "Enfermedad_SelectById";
                cmd.Parameters.Add(new SqlParameter("@ID_ENFERMEDAD", _enf.Id));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Enfermedad_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Enfermedad_Delete";
                cmd.Parameters.Add(new SqlParameter("@ID_ENFERMEDAD", _enf.Id));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Enfermedad_Insert";
                cmd.Parameters.Add(new SqlParameter("@ID_ENFERMEDAD", _enf.Id));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _enf.Nombre_enfermedad));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Enfermedad_Update";
                cmd.Parameters.Add(new SqlParameter("@ID_ENFERMEDAD", _enf.Id));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _enf.Nombre_enfermedad));
            }
            return cmd;
        }

        protected Enfermedad load(SqlDataReader record)
        {
            var enf = new Enfermedad();
            enf.Id = (short)((DBNull.Value == record["ID_ENFERMEDAD"]) ? 0 : (Int16)record["ID_ENFERMEDAD"]);
            //cuidado cuando enfermedad es null salta
            enf.Nombre_enfermedad = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];

            return enf;
        }

        public int GetLastIdEnfermedad()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Enfermedad_LastId";

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        
        

    }
}
