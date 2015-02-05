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
    public class PartoMapper : AbstractMapper
    {
        private Parto _parto;
        private string _registroAnimal;
        private string _nickname;

        private static string Parto_SelecByRegistro = "Parto_SelecByRegistro";
        private static string Parto_SelecNacimByRegistro = "Parto_SelecNacimByRegistro";
        private static string Parto_SelecByAnio = "Parto_SelecByAnio";
        private static string Parto_SelecCountByMonth = "Parto_SelecCountByMonth";
        private static string Parto_SelecBy2fechas = "Parto_SelecBy2fechas";
        private static string Parto_SelectExisteParto = "Parto_SelectExisteParto";
        private static string Parto_SelecUltimoByRegistro = "Parto_SelecUltimoByRegistro";
        
        
        public PartoMapper(Parto parto)
        {
            _parto = parto;
        }

        public PartoMapper(Parto parto, string nickname)
        {
            _parto = parto;
            _nickname = nickname;
        }

        public PartoMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public PartoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Parto GetPartoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Parto)load(dr);
        }


        public List<Parto> GetAll()
        {
            var ls = new List<Parto>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Parto> loadAll(SqlDataReader rs)
        {
            var result = new List<Parto>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetPartosByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Parto_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public Parto GetUltimoPartoByRegistro()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            cmd.CommandText = Parto_SelecUltimoByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            if (dr.Read()) return (Parto)load(dr);
            else return null;
        }
        
        public List<Evento> GetNacimientoByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REG_HIJO", regAnimal));
            cmd.CommandText = Parto_SelecNacimByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Parto> GetPartosByAnio(int anio)
        {
            var result = new List<Parto>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Parto_SelecByAnio;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public List<Parto> GetPartosBy2fechas(string fecha1, string fecha2)
        {
            var result = new List<Parto>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = Parto_SelecBy2fechas;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public int GetPartosByMesAnio(DateTime fecha)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DATE", fecha));
            cmd.CommandText = Parto_SelecCountByMonth;
            
            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public bool ExisteParto()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _parto.Registro));
            cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", _parto.Fecha));
            cmd.CommandText = Parto_SelectExisteParto;
            
            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value) > 0;
        }
        
        
        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _parto.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _parto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", _parto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _parto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _parto.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@SEXO_PARTO", _parto.Sexo_parto));
                cmd.Parameters.Add(new SqlParameter("@REG_HIJO", _parto.Reg_hijo));
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickname));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Parto_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _parto.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _parto.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA_PARTO", _parto.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _parto.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", _parto.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@SEXO_PARTO", _parto.Sexo_parto));
                cmd.Parameters.Add(new SqlParameter("@REG_HIJO", _parto.Reg_hijo));
                //cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickname));
            }
            return cmd;
        }

        protected Parto load(SqlDataReader record)
        {
            var parto = new Parto();
            parto.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            parto.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA_PARTO"]) ? string.Empty : record["FECHA_PARTO"].ToString();
            //if (strDate != string.Empty) parto.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) parto.Fecha = DateTime.Parse(strDate);
            //strDate = (DBNull.Value == record["FECHA_SERV"]) ? string.Empty : record["FECHA_SERV"].ToString();
            //if (strDate != string.Empty) parto.Fecha_serv = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            parto.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            parto.Observaciones = (DBNull.Value == record["OBSERVACIONES"]) ? string.Empty : (string)record["OBSERVACIONES"];
            parto.Sexo_parto = (DBNull.Value == record["SEXO_PARTO"]) ? ' ' : Convert.ToChar(record["SEXO_PARTO"]);
            parto.Reg_hijo = (DBNull.Value == record["REG_HIJO"]) ? string.Empty : (string)record["REG_HIJO"];

            return parto;
        }

    }
}
