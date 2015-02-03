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
using Microsoft.Win32;
using Negocio;

namespace Datos
{
    public class AnimalMapper: AbstractMapper
    {
        private Animal _animal;

        private static string Animal_BusqByID = "Animal_BusqByID";
        //private static string Animal_BusqByCategoria = "Animal_BusqByCategoria";
        private static string Animal_SelectByCategoria = "Animal_SelectByCategoria";
        private static string Animal_SelectFotosByRegistro = "Animal_SelectFotosByRegistro";
        private static string Animales_SelectVitalicias = "Animales_SelectVitalicias";
        private static string Animal_SelectCount_MellizosByAnio = "Animal_SelectCount_MellizosByAnio";
        private static string Animal_SelectCount_TrillizosByAnio = "Animal_SelectCount_TrillizosByAnio";
        private static string Animal_SelectCount_NacimientosByAnio = "Animal_SelectCount_NacimientosByAnio";
        private static string Animal_SelectCountNacimientosPorToroByAnio = "Animal_SelectCountNacimientosPorToroByAnio";
        private static string Animal_SelectCountNacimientosHPorToroByAnio = "Animal_SelectCountNacimientosHPorToroByAnio";
        private static string Animal_SelectCountNacimientosHPorToroBy2fechas = "Animal_SelectCountNacimientosHPorToroBy2fechas";
        private static string Animal_SelectCountNacimientosPorToroBy2fechas = "Animal_SelectCountNacimientosPorToroBy2fechas";
        private static string Animal_SelectCount_NacimientosBy2fechas = "Animal_SelectCount_NacimientosBy2fechas";
        private static string Animal_SelectCount_MellizosBy2fechas = "Animal_SelectCount_MellizosBy2fechas";
        private static string Animal_SelectCount_TrillizosBy2fechas = "Animal_SelectCount_TrillizosBy2fechas";
        private static string Animal_UpdateDatosModificables = "Animal_UpdateDatosModificables";
        private static string Animal_SubirFotoByRegistro = "Animal_SubirFotoByRegistro";
        private static string Animal_InsertFotoByRegistro = "Animal_InsertFotoByRegistro";
        private static string Animal_SelectCriasByParto = "Animal_SelectCriasByParto";
        
               
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

        public List<Animal> GetCriasIngresadasParto(string regMadre, DateTime fecha)
        {
            var result = new List<Animal>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REG_MADRE", regMadre));
            cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", fecha));
            cmd.CommandText = Animal_SelectCriasByParto;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<VOFoto> GetFotosByRegistro(string reg)
        {
            var result = new List<VOFoto>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", reg));
            cmd.CommandText = Animal_SelectFotosByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadFoto(dr));
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
                cmd.CommandText = "Animal_SelectByRegistro";
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
                cmd.Parameters.Add(new SqlParameter("@GEN", _animal.Gen));
                cmd.Parameters.Add(new SqlParameter("@IDENTIFICACION", _animal.Identificacion));
                cmd.Parameters.Add(new SqlParameter("@SEXO", _animal.Sexo));
                cmd.Parameters.Add(new SqlParameter("@FECHA_NACIM", _animal.Fecha_nacim));
                cmd.Parameters.Add(new SqlParameter("@ORIGEN", _animal.Origen));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _animal.Nombre));
                // al insertar el nuevo animal, tambien inserto la categoria 
                // en la tabla animales_categ, como una transaccion conjunta
                cmd.Parameters.Add(new SqlParameter("@CATEGORIA", _animal.IdCategoria)); 
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Animal_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _animal.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@REG_MADRE", _animal.Reg_madre));
                cmd.Parameters.Add(new SqlParameter("@REG_TRAZAB", _animal.Reg_trazab));
                cmd.Parameters.Add(new SqlParameter("@GEN", _animal.Gen));
                cmd.Parameters.Add(new SqlParameter("@IDENTIFICACION", _animal.Identificacion));
                cmd.Parameters.Add(new SqlParameter("@SEXO", _animal.Sexo));
                cmd.Parameters.Add(new SqlParameter("@FECHA_NACIM", _animal.Fecha_nacim));
                cmd.Parameters.Add(new SqlParameter("@ORIGEN", _animal.Origen));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _animal.Nombre));

                //cmd.Parameters.Add(new SqlParameter("@CATEGORIA", _animal.IdCategoria)); 
            }
            return cmd;
        }

        protected Animal load(SqlDataReader record)
        {
            var anim = new Animal();
            anim.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            anim.Identificacion = (DBNull.Value == record["IDENTIFICACION"]) ? string.Empty : (string)record["IDENTIFICACION"];
            //string strGen = (DBNull.Value == record["GEN"]) ? string.Empty : record["GEN"].ToString();
            anim.Gen = (DBNull.Value == record["GEN"]) ? -1 : int.Parse(record["GEN"].ToString());
            //anim.IdCategoria = (short)((DBNull.Value == record["CATEGORIA"]) ? 0 : (Int16)record["CATEGORIA"]);
            anim.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            anim.Reg_trazab = (DBNull.Value == record["REG_TRAZAB"]) ? string.Empty : (string)record["REG_TRAZAB"];
            anim.Sexo = (DBNull.Value == record["SEXO"]) ? 'X' : Convert.ToChar(record["SEXO"]);

            string strDate = (DBNull.Value == record["FECHA_NACIM"]) ? string.Empty : record["FECHA_NACIM"].ToString();
            //if (strDate != string.Empty) anim.Fecha_nacim = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) anim.Fecha_nacim = DateTime.Parse(strDate);
            
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


        public int GetPromDiasLactancias()
        {
            return GetScalarInt("Animal_SelectCountEnOrdenePromDiasLactancias");
        }

        public int GetCantMellizosByAnio(int anio)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Animal_SelectCount_MellizosByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantMellizosBy2fechas(string fecha1, string fecha2)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Animal_SelectCount_MellizosBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantTrillizosByAnio(int anio)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Animal_SelectCount_TrillizosByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantTrillizosBy2fechas(string fecha1, string fecha2)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Animal_SelectCount_TrillizosBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }


        public int GetCantNacimientosByAnio(int anio)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Animal_SelectCount_NacimientosByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantNacimientosBy2fechas(string fecha1, string fecha2)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Animal_SelectCount_NacimientosBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantNacimientosHPorToroByAnio(string reg, int anio)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REG_PADRE", reg));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Animal_SelectCountNacimientosHPorToroByAnio;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int GetCantNacimientosHPorToroBy2fechas(string reg, string fecha1, string fecha2)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REG_PADRE", reg));
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Animal_SelectCountNacimientosHPorToroBy2fechas;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        protected VOFoto loadFoto(SqlDataReader record)
        {
            var voFoto = new VOFoto();
            voFoto.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            voFoto.Ruta = (DBNull.Value == record["FOTO"]) ? string.Empty : (string)record["FOTO"];
            voFoto.Thumb = (DBNull.Value == record["THUMB"]) ? string.Empty : (string)record["THUMB"];
            voFoto.PieDeFoto = (DBNull.Value == record["PIE_DE_FOTO"]) ? string.Empty : (string)record["PIE_DE_FOTO"];
            voFoto.Comentario = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            return voFoto;
        }

        public class VOFoto
        {
            public VOFoto()
            {
                
            }

            public VOFoto(string reg, string pie, string ruta, string thumb, string comentario)
            {
                Registro = reg;
                PieDeFoto = pie;
                Ruta = ruta;
                Thumb = thumb;
                Comentario = comentario;
            }

            public string Comentario { get; set; }

            public string Ruta { get; set; }

            public string Thumb { get; set; }

            public string PieDeFoto { get; set; }

            public string Registro { get; set; }
        }

        protected VOAnimalVitalicio loadVitalicias(SqlDataReader record)
        {
            var anim = new Animal();
            anim.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            anim.Identificacion = (DBNull.Value == record["IDENTIFICACION"]) ? string.Empty : (string)record["IDENTIFICACION"];
            anim.Gen = (DBNull.Value == record["GEN"]) ? -1 : int.Parse(record["GEN"].ToString());
            anim.IdCategoria = (short)((DBNull.Value == record["CATEGORIA"]) ? 0 : (Int16)record["CATEGORIA"]);
            //anim.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            anim.Reg_trazab = (DBNull.Value == record["REG_TRAZAB"]) ? string.Empty : (string)record["REG_TRAZAB"];
            //anim.Sexo = (DBNull.Value == record["SEXO"]) ? 'X' : Convert.ToChar(record["SEXO"]);
            string strDate = (DBNull.Value == record["FECHA_NACIM"]) ? string.Empty : record["FECHA_NACIM"].ToString();
            //if (strDate != string.Empty) anim.Fecha_nacim = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) anim.Fecha_nacim = DateTime.Parse(strDate);
            //anim.Origen = (DBNull.Value == record["ORIGEN"]) ? string.Empty : (string)record["ORIGEN"];
            anim.Reg_padre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];
            anim.Reg_madre = (DBNull.Value == record["REG_MADRE"]) ? string.Empty : (string)record["REG_MADRE"];

            var voVital = new VOAnimalVitalicio(anim);
            voVital.ProdVitalicia = (DBNull.Value == record["PROD_VITALICIA"]) ? 0 : double.Parse(record["PROD_VITALICIA"].ToString());
            voVital.NumLact = (DBNull.Value == record["LACT_NUM"]) ? 0 : int.Parse(record["LACT_NUM"].ToString());
            
            return voVital;
        }

        
        public List<VOAnimalVitalicio> GetVitalicias()
        {
            var result = new List<VOAnimalVitalicio>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Animales_SelectVitalicias;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadVitalicias(dr));
            dr.Close();
            return result;
        }

        public class VOAnimalVitalicio
        {
            public VOAnimalVitalicio()
            {
                Vivo = true;
            }

            public VOAnimalVitalicio(Animal anim)
            {
                IdCategoria = anim.IdCategoria;
                Identificacion = anim.Identificacion;
                Origen = anim.Origen;
                Reg_madre = anim.Reg_madre;
                Reg_padre = anim.Reg_padre;
                Fecha_nacim = anim.Fecha_nacim;
                Sexo = anim.Sexo;
                Reg_trazab = anim.Reg_trazab;
                Gen = anim.Gen;
                NumGen = Gen == -1 ? "-" : Gen.ToString();
                Registro = anim.Registro;
                Nombre = anim.Nombre;
                Calific = anim.Calific;
            }

            public string Reg_madre { get; set; }

            public string Reg_padre { get; set; }

            public string Calific { get; set; }

            public bool Vivo { get; set; }

            public string Nombre { get; set; }

            public string Registro { get; set; }

            public int Gen { get; set; }

            public string NumGen { get; set; }

            public string Reg_trazab { get; set; }

            public Char Sexo { get; set; }

            public DateTime Fecha_nacim { get; set; }

            public string Origen { get; set; }

            public string Identificacion { get; set; }

            public int IdCategoria { get; set; }

            public string Categoria { get; set; }

            public double ProdVitalicia { get; set; }

            public int NumLact { get; set; }

            public override string ToString()
            {
                return Registro;
            }

        }

        public class VOToro
        {
            public VOToro()
            {

            }

            public string Registro { get; set; }

            public string Nombre { get; set; }

            public string Origen { get; set; }

            public int CantNacim { get; set; }

            public int CantH { get; set; }

            public int CantM { get; set; }

            public double PorcHembras { get; set; }

        }

        public List<VOToro> GetNacimientosPorToroByAnio(int anio)
        {
            var result = new List<VOToro>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Animal_SelectCountNacimientosPorToroByAnio;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadNacimPorToroByAnio(dr));
            dr.Close();
            return result;
        }

        public List<VOToro> GetNacimientosPorToroBy2fechas(string fecha1, string fecha2)
        {
            var result = new List<VOToro>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Animal_SelectCountNacimientosPorToroBy2fechas;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadNacimPorToroByAnio(dr));
            dr.Close();
            return result;
        }

        protected VOToro loadNacimPorToroByAnio(SqlDataReader record)
        {
            var toro = new VOToro();
            toro.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            //toro.IdCategoria = (short)((DBNull.Value == record["CATEGORIA"]) ? 0 : (Int16)record["CATEGORIA"]);
            //toro.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            toro.Origen = (DBNull.Value == record["ORIGEN"]) ? string.Empty : (string)record["ORIGEN"];
            toro.CantNacim = (DBNull.Value == record["CANT_NACIM"]) ? 0 : int.Parse(record["CANT_NACIM"].ToString());
            //toro.CantH = (short)((DBNull.Value == record["CANT_H"]) ? 0 : (Int16)record["CANT_H"]);
            //toro.CantH = (short)((DBNull.Value == record["CANT_M"]) ? 0 : (Int16)record["CANT_M"]);
            //toro.CantNacim = toro.CantH + toro.CantM;
            //if (toro.CantNacim > 0)
                //toro.PorcHembras = Math.Round((double)toro.CantH / toro.CantNacim * 100, 1);
            return toro;
        }
        public int GetAnimalOrdeneServSinPrenez()
        {
            return GetScalarInt("Animal_OrdeneServicioSinPrenez");
        }

        public int GetAnimalOrdenePrenezConf()
        {
            return GetScalarInt("Animal_OrdenePrenezConfirmada");
        }

        public bool AnimalExiste(string registro)
        {
            return GetScalarIntReg("Animal_Existe", registro) == 1;
        }

        public int UpdateDatosModificables()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _animal.Registro));
            cmd.Parameters.Add(new SqlParameter("@IDENTIFICACION", _animal.Identificacion));
            cmd.Parameters.Add(new SqlParameter("@NOMBRE", _animal.Nombre));
            cmd.Parameters.Add(new SqlParameter("@REG_TRAZAB", _animal.Reg_trazab));
            cmd.Parameters.Add(new SqlParameter("@ORIGEN", _animal.Origen));
            cmd.CommandText = Animal_UpdateDatosModificables;

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int SubirFotoAnimal(VOFoto foto)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", foto.Registro));
            cmd.Parameters.Add(new SqlParameter("@FOTO", foto.Ruta));
            cmd.Parameters.Add(new SqlParameter("@THUMB", foto.Thumb));
            cmd.Parameters.Add(new SqlParameter("@PIE_DE_FOTO", foto.PieDeFoto));
            cmd.Parameters.Add(new SqlParameter("@COMENTARIO", foto.Comentario));
            cmd.CommandText = "Foto_Insert";

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int CambioCategoriaAnimales()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Animales_Categoria_Actualizar";

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

    }
}

