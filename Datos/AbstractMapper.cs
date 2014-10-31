using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public abstract class AbstractMapper
    {
        protected abstract SqlCommand GetStatement(OperationType opType);
        protected abstract string GetConnectionString();
        protected enum OperationType { INSERT, DELETE, UPDATE, SELECT_DEF, SELECT_ID };



        protected SqlDataReader Find(OperationType opType)
        {
            SqlDataReader drResults;
            string sConnectionString = GetConnectionString();
            SqlConnection conn = new SqlConnection(sConnectionString);

            SqlCommand cmd = GetStatement(opType);
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return drResults;
        }

        protected int DataModifySentece(OperationType opType)
        {
            int nRetunValue = -1;
            string sConnectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(sConnectionString))
            {
                SqlCommand cmd = GetStatement(opType);
                cmd.Connection = conn;
                conn.Open();
                nRetunValue = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            return nRetunValue;
        }

        public int Insert()
        {
            return DataModifySentece(OperationType.INSERT);
        }

        public int Update()
        {
            return DataModifySentece(OperationType.UPDATE);
        }

        public int Delete()
        {
            return DataModifySentece(OperationType.DELETE);
        }

    }
}
