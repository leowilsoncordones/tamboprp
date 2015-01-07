using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private static string Lactancia_SelectMaxVacaEnOrdene = "Lactancia_SelectMaxVacaEnOrdene";
        private static string Lactancia_SelectHistorica = "Lactancia_SelectHistorica";
        private static string Lactancia_SelectMejorProduccion305 = "Lactancia_SelectMejorProduccion305";
        private static string Lactancia_SelectMejorProduccion365Top = "Lactancia_SelectMejorProduccion365Top";
        private static string Lactancia_SelectMejorProduccion305Top = "Lactancia_SelectMejorProduccion305Top";
        private static string Lactancia_SelectMejorProduccion365 = "Lactancia_SelectMejorProduccion365";
        private static string Lactancia_SelectMaxByRegistro = "Lactancia_SelectMaxByRegistro";
        private static string Lactancia_SelectUltimaByRegistro = "Lactancia_SelectUltimaByRegistro";
        private static string Lactancia_SelectDiasMaxByRegistro = "Lactancia_SelectDiasMaxByRegistro";
      
        
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

        public Lactancia GetLactanciaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Lactancia)load(dr);
        }

        public List<Lactancia> GetAll()
        {
            var ls = new List<Lactancia>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Lactancia> GetLactanciasByRegistro()
        {
            var result = new List<Lactancia>();
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
            cmd.CommandText = Lactancia_SelectMaxVacaEnOrdene;

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
            cmd.CommandText = Lactancia_SelectHistorica;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        //public List<VOProdVitalicia> GetVitalicias()
        //{
        //    var result = new List<VOProdVitalicia>();
        //    SqlCommand cmd = null;
        //    cmd = new SqlCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = Lactancias_SelectVitalicias;

        //    SqlDataReader dr = FindByCmd(cmd);
        //    while (dr.Read())
        //        result.Add(loadVitalicia(dr));
        //    dr.Close();
        //    return result;
        //}

        public List<Lactancia> GetLactanciaMejorProduccion305(int tope)
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (tope != -1) // ver todos sin tope
            {
                cmd.Parameters.Add(new SqlParameter("@TOP", tope));
                cmd.CommandText = Lactancia_SelectMejorProduccion305Top;
            }
            else
            {
                cmd.CommandText = Lactancia_SelectMejorProduccion305;
            }

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Lactancia> GetLactanciaMejorProduccion365(int tope)
        {
            var result = new List<Lactancia>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (tope != -1) // ver todos sin tope
            {
                cmd.Parameters.Add(new SqlParameter("@TOP", tope));
                cmd.CommandText = Lactancia_SelectMejorProduccion365Top;
            }
            else
            {
                cmd.CommandText = Lactancia_SelectMejorProduccion365;
            }

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public int GetMaxLactanciaByRegistro()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancia_SelectMaxByRegistro;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetDiasMaxLactanciaByRegistro()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancia_SelectDiasMaxByRegistro;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public Lactancia GetUltimaLactanciaByRegistro()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Lactancia_SelectUltimaByRegistro;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _regAnimal));

            SqlDataReader dr = FindByCmd(cmd);
            dr.Read();
            return (Lactancia)load(dr);
        }

        protected List<Lactancia> loadAll(SqlDataReader rs)
        {
            var result = new List<Lactancia>();
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

        //protected VOProdVitalicia loadVitalicia(SqlDataReader record)
        //{
        //    var voVital = new VOProdVitalicia();
        //    voVital.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
        //    voVital.ProdLecheVitalicia = (DBNull.Value == record["PROD_VITALICIA"]) ? 0 : double.Parse(record["PROD_VITALICIA"].ToString());
        //    voVital.NumLact = (DBNull.Value == record["LACT_NUM"]) ? 0 : int.Parse(record["LACT_NUM"].ToString());
        //    return voVital;
        //}

        //public class VOProdVitalicia
        //{
        //    public VOProdVitalicia()
        //    {

        //    }

        //    public VOProdVitalicia(string reg, double prodVitalicia, int num)
        //    {
        //        Registro = reg;
        //        ProdLecheVitalicia = prodVitalicia;
        //        NumLact = num;
        //    }

        //    public string Registro { get; set; }

        //    public double ProdLecheVitalicia { get; set; }

        //    public int NumLact { get; set; }

        //}

        public double GetPromProdLecheActual()
        {
            return GetScalarFloat("Lactancia_PromedioProdLecheActual");
        }

        public int GetLactanciaPromedioDiasActual()
        {
            return GetScalarInt("Lactancia_PromedioDiasActual");
        }
    }
}
