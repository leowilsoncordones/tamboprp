using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        protected SqlDataReader FindByCmd(SqlCommand pcmd)
        {
            SqlDataReader drResults;
            string sConnectionString = GetConnectionString();
            SqlConnection conn = new SqlConnection(sConnectionString);

            SqlCommand cmd = pcmd;
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return drResults;
        }

        protected int DataModifySentence(OperationType opType)
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
            return DataModifySentence(OperationType.INSERT);
        }

        public int Update()
        {
            return DataModifySentence(OperationType.UPDATE);
        }

        public int Delete()
        {
            return DataModifySentence(OperationType.DELETE);
        }

        protected object ReturnScalarValue(SqlCommand pcmd)
        {
            object value;
            string sConnectionString = GetConnectionString();
            SqlConnection conn = new SqlConnection(sConnectionString);

            SqlCommand cmd = pcmd;
            cmd.Connection = conn;
            conn.Open();
            value = cmd.ExecuteScalar();

            return value;
        }

        public int GetScalarInt(string storedProcedure)
        {
            return (int)GetScalar(storedProcedure);
        }

        public float GetScalarFloat(string storedProcedure)
        {
            var scalar = GetScalar(storedProcedure).ToString();
            return float.Parse(scalar);
        }

        public string GetScalarDate(string storedProcedure)
        {
            var fecha =  (DateTime)GetScalar(storedProcedure);
            var result = fecha.ToShortDateString();
            return result;
        }

        private object GetScalar(string storedProcedure)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storedProcedure;
            return ReturnScalarValue(cmd);
        }

    }
}
