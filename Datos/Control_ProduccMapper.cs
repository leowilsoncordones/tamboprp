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
    public class Control_ProduccMapper: AbstractMapper
    {
        private Control_Producc _Control_Producc;
        private string _regAnimal;

        //private static string Control_Producc_BusqByID = "Control_Producc_BusqByID";
        private static string Control_producc_SelectByRegistro = "Control_producc_SelectByRegistro";
        private static string Control_producc_SelectUltimoVacaEnOrdene = "Control_producc_SelectUltimoVacaEnOrdene";

        public Control_ProduccMapper(Control_Producc controlProducc)
        {
            _Control_Producc = controlProducc;
        }

        public Control_ProduccMapper(Control_Producc controlProducc,string regAnimal)
        {
            _Control_Producc = controlProducc;
            _regAnimal = regAnimal;
        }

        public Control_ProduccMapper(string regAnimal)
        {
            _regAnimal = regAnimal;
        }

        public Control_ProduccMapper()
        {
        }



        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Control_Producc GetControl_ProduccById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Control_Producc)load(dr);
        }

        public List<Control_Producc> GetAll()
        {
            var ls = new List<Control_Producc>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Evento> GetControlesProduccByRegistro()
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            cmd.CommandText = Control_producc_SelectByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Control_Producc> GetControlesProduccUltimo()
        {
            var result = new List<Control_Producc>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Control_producc_SelectUltimoVacaEnOrdene;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        public int GetCantAnimalesUltControl()
        {
            return GetScalarInt("Control_producc_SelectCantAnimalesUltimo");
        }

        public float GetSumLecheUltControl()
        {
            return GetScalarFloat("Control_producc_SelectSumLecheUltimo");
        }

        public float GetPromLecheUltControl()
        {
            return GetScalarFloat("Control_producc_SelectPromLecheUltimo");
        }

        public float GetSumGrasaUltControl()
        {
            return GetScalarFloat("Control_producc_SelectSumGrasaUltimo");
        }

        public float GetPromGrasaUltControl()
        {
            return GetScalarFloat("Control_producc_SelectPromGrasaUltimo");
        }

        protected List<Control_Producc> loadAll(SqlDataReader rs)
        {
            var result = new List<Control_Producc>();
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
                cmd.CommandText = "Control_producc_SelectByRegistro";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_producc_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Control_producc_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
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

            }
            else if (opType == OperationType.UPDATE)
            {
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
            }
            return cmd;
        }

        protected Control_Producc load(SqlDataReader record)
        {
            var cp = new Control_Producc();
            cp.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            cp.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            cp.Grasa = (DBNull.Value == record["GRASA"]) ? 0 : double.Parse(record["GRASA"].ToString());
            cp.Leche = (DBNull.Value == record["LECHE"]) ? 0 : double.Parse(record["LECHE"].ToString());
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            if (strDate != string.Empty) cp.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            //cp.Dias_para_control = (DBNull.Value == record["DIAS_DEL_MES"]) ? 0 : int.Parse(record["DIAS_DEL_MES"].ToString());
            cp.Dias_para_control = (DBNull.Value == record["DIAS_LACTANCIA"]) ? 0 : int.Parse(record["DIAS_LACTANCIA"].ToString());
            cp.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            return cp;
        }

        public string GetFechaUltimoControl()
        {
            return GetScalarDate("Control_producc_SelectFechaUltimoControl");
        }
    }
}
