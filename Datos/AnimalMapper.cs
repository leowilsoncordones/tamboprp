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
    public class AnimalMapper: AbstractMapper
    {
        private Animal _animal;

        private static string Animal_BusqByID = "Animal_BusqByID";
        //private static string Animal_BusqByCategoria = "Animal_BusqByCategoria";
        private static string Animal_SelectByCategoria = "Animal_SelectByCategoria";        

        public AnimalMapper(Animal animal)
        {
            _animal = animal;
        }

        public AnimalMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Animal GetAnimalById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Animal)load(dr);
        }

        public List<Animal> GetAll()
        {
            var ls = new List<Animal>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Animal> GetBusqAnimal(string buscar, int criterio)
        {
            var result = new List<Animal>();
            if (criterio < 0 || criterio > 1) return result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            switch (criterio)
            {
                case 0:
                {
                    cmd.Parameters.Add(new SqlParameter("@REGISTRO", buscar));
                    cmd.CommandText = Animal_BusqByID;
                }
                    break;
                case 1:
                    {
                        //cmd.Parameters.Add(new SqlParameter("@CATEGORIA", buscar));
                        //cmd.CommandText = Animal_BusqByCategoria;
                    }
                    break;
            }

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Animal> GetAnimalesByCategoria(int idCategoria)
        {
            var result = new List<Animal>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CATEGORIA", idCategoria));
            cmd.CommandText = Animal_SelectByCategoria;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        protected List<Animal> loadAll(SqlDataReader rs)
        {
            var result = new List<Animal>();
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
                cmd.CommandText = "Animal_SelecById";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Animal_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Animal_Delete";
                /* eliminar referencias primero */
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Animal_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _animal.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@REG_MADRE", _animal.Reg_madre)); 
                cmd.Parameters.Add(new SqlParameter("@REG_TRAZAB", _animal.Reg_trazab)); 
                cmd.Parameters.Add(new SqlParameter("@VIVO", _animal.Vivo));

            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Animal_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
                cmd.Parameters.Add(new SqlParameter("@REG_TRAZAB", _animal.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@VIVO", _animal.Vivo));
            }
            return cmd;
        }

        protected Animal load(SqlDataReader record)
        {
            var anim = new Animal();
            anim.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            anim.Identificacion = (DBNull.Value == record["IDENTIFICACION"]) ? string.Empty : (string)record["IDENTIFICACION"];
            string strGen = (DBNull.Value == record["GEN"]) ? string.Empty : record["GEN"].ToString();
            anim.Gen = (DBNull.Value == record["GEN"]) ? -1 : int.Parse(record["GEN"].ToString());
            anim.IdCategoria = (short)((DBNull.Value == record["CATEGORIA"]) ? 0 : (Int16)record["CATEGORIA"]);
            anim.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            anim.Reg_trazab = (DBNull.Value == record["REG_TRAZAB"]) ? string.Empty : (string)record["REG_TRAZAB"];
            anim.Sexo = (DBNull.Value == record["SEXO"]) ? 'X' : Convert.ToChar(record["SEXO"]);

            string strDate = (DBNull.Value == record["FECHA_NACIM"]) ? string.Empty : record["FECHA_NACIM"].ToString();
            if (strDate != string.Empty) anim.Fecha_nacim = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            
            anim.Origen = (DBNull.Value == record["ORIGEN"]) ? string.Empty : (string)record["ORIGEN"];
            anim.Reg_padre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];
            anim.Reg_madre = (DBNull.Value == record["REG_MADRE"]) ? string.Empty : (string)record["REG_MADRE"];
            return anim;
        }

        public int GetCantOrdene()
        {
            return GetScalarInt("Animal_SelectCountOrdene");
        }

        public int GetCantEntoradas()
        {
            return GetScalarInt("Animal_SelectCountEntorada");
        }

        public int GetCantSecas()
        {
            return GetScalarInt("Animal_SelectCountSeca");
        }

        public int GetEnOrdeneLanctancia1()
        {
            return GetScalarInt("Animal_SelectCountEnOrdeneLactancia1");
        }

        public int GetEnOrdeneLanctanciaMayor2()
        {
            return GetScalarInt("Animal_SelectCountEnOrdeneLactanciaMayor2");
        }

        public int GetEnOrdeneLanctancia2()
        {
            return GetScalarInt("Animal_SelectCountEnOrdeneLactancia2");
        }

        public int GetEnOrdenePromProdLecheLts()
        {
            return GetScalarInt("Animal_SelectCountEnOrdenePromProdLecheLts");
        }

        public int GetEnOrdeneServicioSinPrenez()
        {
            return GetScalarInt("Animal_SelectCountEnOrdeneServicioSinPrenez");
        }

        public int GetEnOrdenePrenezConfirmada()
        {
            return GetScalarInt("Animal_SelectCountEnOrdenePrenezConfirmada");
        }

        public int GetPromDiasLactancias()
        {
            return GetScalarInt("Animal_SelectCountEnOrdenePromDiasLactancias");
        }


    }
}
