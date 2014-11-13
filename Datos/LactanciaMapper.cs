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
    public class LactanciaMapper: AbstractMapper
    {
        private Lactancia _lactancia;
        private string _regAnimal;

        private static string Lactancia_SelectByRegistro = "Lactancia_SelectByRegistro";
        private static string Lactancias_SelectMaxVacaEnOrdene = "Lactancias_SelectMaxVacaEnOrdene";
        private static string Lactancias_SelectHistorica = "Lactancias_SelectHistorica";
        private static string Lactancias_SelectMejorProduccion305 = "Lactancias_SelectMejorProduccion305";
        private static string Lactancias_SelectMejorProduccion365 = "Lactancias_SelectMejorProduccion365";
        
        public LactanciaMapper(Lactancia lact)
        {
            _lactancia = lact;
        }

        public LactanciaMapper(Lactancia lact, string regAnimal)
        {
            _lactancia = lact;
            _regAnimal = regAnimal;
        }

        public LactanciaMapper(string regAnimal)
        {
            _regAnimal = regAnimal;
        }

        public LactanciaMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Lactancia GetControl_ProduccById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Lactancia)load(dr);
        }

        public List<Lactancia> GetAll()
        {
            List<Lactancia> ls = new List<Lactancia>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Lactancia> GetLactanciasByRegistro()
        {
            List<Lactancia> result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            cmd.CommandText = Lactancia_SelectByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        public List<Lactancia> GetLactanciaActualCategoriaVacaOrdene()
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancias_SelectMaxVacaEnOrdene;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Lactancia> GetLactanciasHistoricas()
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancias_SelectHistorica;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Lactancia> GetLactanciaMejorProduccion305()
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancias_SelectMejorProduccion305;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Lactancia> GetLactanciaMejorProduccion365()
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancias_SelectMejorProduccion365;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        protected List<Lactancia> loadAll(SqlDataReader rs)
        {
            List<Lactancia> result = new List<Lactancia>();
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
                cmd.CommandText = "Lactancia_SelectByRegistro";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Lactancia_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Lactancia_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Lactancia_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
                cmd.Parameters.Add(new SqlParameter("@LACTANCIAS", _lactancia.Numero));
                cmd.Parameters.Add(new SqlParameter("@DIAS", _lactancia.Dias));
                cmd.Parameters.Add(new SqlParameter("@LECHE305", _lactancia.Leche305));
                cmd.Parameters.Add(new SqlParameter("@GRASA305", _lactancia.Grasa305));
                cmd.Parameters.Add(new SqlParameter("@LECHE365", _lactancia.Leche365));
                cmd.Parameters.Add(new SqlParameter("@GRASA365", _lactancia.Grasa365));
                cmd.Parameters.Add(new SqlParameter("@PRODLECHE", _lactancia.ProdLeche));
                cmd.Parameters.Add(new SqlParameter("@PRODGRASA", _lactancia.ProdGrasa));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Lactancia_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));
                cmd.Parameters.Add(new SqlParameter("@LACTANCIAS", _lactancia.Numero));
                cmd.Parameters.Add(new SqlParameter("@DIAS", _lactancia.Dias));
                cmd.Parameters.Add(new SqlParameter("@LECHE305", _lactancia.Leche305));
                cmd.Parameters.Add(new SqlParameter("@GRASA305", _lactancia.Grasa305));
                cmd.Parameters.Add(new SqlParameter("@LECHE365", _lactancia.Leche365));
                cmd.Parameters.Add(new SqlParameter("@GRASA365", _lactancia.Grasa365));
                cmd.Parameters.Add(new SqlParameter("@PRODLECHE", _lactancia.ProdLeche));
                cmd.Parameters.Add(new SqlParameter("@PRODGRASA", _lactancia.ProdGrasa));
            }
            return cmd;
        }

        protected Lactancia load(SqlDataReader record)
        {
            var lact = new Lactancia();
            lact.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            lact.Numero = (DBNull.Value == record["LACTANCIAS"]) ? 0 : int.Parse(record["LACTANCIAS"].ToString());
            lact.Dias = (DBNull.Value == record["DIAS"]) ? 0 : int.Parse(record["DIAS"].ToString());
            lact.Leche305 = (DBNull.Value == record["LECHE305"]) ? 0 : double.Parse(record["LECHE305"].ToString());
            lact.Grasa305 = (DBNull.Value == record["GRASA305"]) ? 0 : double.Parse(record["GRASA305"].ToString());
            lact.Leche365 = (DBNull.Value == record["LECHE365"]) ? 0 : double.Parse(record["LECHE365"].ToString());
            lact.Grasa365 = (DBNull.Value == record["GRASA365"]) ? 0 : double.Parse(record["GRASA365"].ToString());
            lact.ProdLeche = (DBNull.Value == record["PRODLECHE"]) ? 0 : double.Parse(record["PRODLECHE"].ToString());
            lact.ProdGrasa = (DBNull.Value == record["PRODGRASA"]) ? 0 : double.Parse(record["PRODGRASA"].ToString());
            return lact;
        }

        public string GetFechaUltimoControl()
        {
            return GetScalarDate("Control_producc_SelectFechaUltimoControl");
        }
    }
}
