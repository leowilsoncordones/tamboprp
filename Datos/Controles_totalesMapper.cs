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
    public class Controles_totalesMapper : AbstractMapper
    {

        public Controles_totalesMapper()
        {
        }


        public List<VOControlTotal> GetAll()
        {
            var ls = new List<VOControlTotal>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        protected List<VOControlTotal> loadAll(SqlDataReader rs)
        {
            var result = new List<VOControlTotal>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {   /*
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_producc_SelectByRegistro";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
                */
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Controles_Totales_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
             /*   cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_producc_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
               */ 
            }
            else if (opType == OperationType.INSERT)
            {
                /*
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_Producc_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _Control_Producc.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _Control_Producc.Fecha));
                //cmd.Parameters.Add(new SqlParameter("@DIAS_DEL_MES", _Control_Producc.Dias_para_control));
                cmd.Parameters.Add(new SqlParameter("@DIAS_LACTANCIA", _Control_Producc.Dias_para_control));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _Control_Producc.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@GRASA", _Control_Producc.Grasa));
                cmd.Parameters.Add(new SqlParameter("@LECHE", _Control_Producc.Leche));
            */
            }
            else if (opType == OperationType.UPDATE)
            {
                /*
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_producc_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _Control_Producc.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _Control_Producc.Fecha));
                //cmd.Parameters.Add(new SqlParameter("@DIAS_DEL_MES", _Control_Producc.Dias_para_control));
                cmd.Parameters.Add(new SqlParameter("@DIAS_LACTANCIA", _Control_Producc.Dias_para_control));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _Control_Producc.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@GRASA", _Control_Producc.Grasa));
                cmd.Parameters.Add(new SqlParameter("@LECHE", _Control_Producc.Leche));
                 */
            }
            return cmd;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }


        protected VOControlTotal load(SqlDataReader record)
        {
            var cp = new VOControlTotal();
            var date = new DateTime();
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) date = DateTime.Parse(strDate);
            cp.Fecha = date.ToString("yyyy/MM/dd");
            cp.Leche = (DBNull.Value == record["LECHE"]) ? 0 : double.Parse(record["LECHE"].ToString());
            cp.Grasa = (DBNull.Value == record["GRASA"]) ? 0 : double.Parse(record["GRASA"].ToString());
            return cp;
        }



        public class VOControlTotal
        {
            public VOControlTotal()
            {
            }

            public VOControlTotal(string fecha, double leche, double grasa)
            {
                Fecha = fecha;
                Leche = leche;
                Grasa = grasa;
            }

            public string Fecha { get; set; }
            public double Leche { get; set; }
            public double Grasa { get; set; }
        }

    }
}
