using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class ServicioMapper : AbstractMapper
    {
        private Servicio _servicio;
        private string _registroAnimal;

        private static string Servicio_SelecByRegistro = "Servicio_SelecByRegistro";
        private static string Servicio_SelecProximosPartos = "Servicio_SelecProximosPartos";
        private static string Servicio_RegistroCantServAntesPrenez = "Servicio_RegistroCantServAntesPrenez";
        private static string Servicio_DiasSinDiagPrenez = "Servicio_DiasSinDiagPrenez";
        private static string Servicio_RegistroCantServDespuesPrenez = "Servicio_RegistroCantServDespuesPrenez";
        private static string Servicio_80DiasLactanciaSinServicio = "Servicio_80DiasLactanciaSinServicio";
        private static string Servicio_SelectByRegistroDespUltParto = "Servicio_SelectByRegistroDespUltParto";
        private static string ServicioyDiag_PorTorosUtilizByAnio = "ServicioyDiag_PorTorosUtilizByAnio";
        private static string ServicioyDiag_PorTorosUtilizBy2fechas = "ServicioyDiag_PorTorosUtilizBy2fechas";
        private static string Servicio_DiasSinDiagPrenezCount70 = "Servicio_DiasSinDiagPrenezCount70";
        private static string Servicio_DiasSinDiagPrenezCount35 = "Servicio_DiasSinDiagPrenezCount35";
        private static string Servicio_80DiasLactanciaSinServicioCount = "Servicio_80DiasLactanciaSinServicioCount";
        private static string Servicio_SelectUltimoDespFechaByRegistro = "Servicio_SelectUltimoDespFechaByRegistro";
        
        public ServicioMapper(Servicio servicio)
        {
            _servicio = servicio;
        }

        public ServicioMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public ServicioMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Servicio GetServicioById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Servicio)load(dr);
        }

        public Servicio GetUltimoServicioDespFechaByRegistro(DateTime fecha)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            cmd.Parameters.Add(new SqlParameter("@FECHA", fecha));
            cmd.CommandText = Servicio_SelectUltimoDespFechaByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            if (dr.Read()) return (Servicio) load(dr);
            else return null;
        }


        public List<Servicio> GetAll()
        {
            var ls = new List<Servicio>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }

        public List<Servicio> GetServicios70SinDiagPrenezVaqEnt()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez,3,70);
        }

        public List<Servicio> GetServicios70SinDiagPrenezVacOrdene()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez,4,70);
        }

        public List<Servicio> GetServicios70SinDiagPrenezVacSecas()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez,5,70);
        }

        public List<Servicio> GetServicios35SinDiagPrenezVaqEnt()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez, 3, 35);
        }

        public List<Servicio> GetServicios35SinDiagPrenezVacOrdene()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez, 4, 35);
        }

        public List<Servicio> GetServicios35SinDiagPrenezVacSecas()
        {
            return GetServiciosSinDiagPrenez(Servicio_DiasSinDiagPrenez, 5, 35);
        }


        public List<Servicio> GetServicio80DiasLactanciaSinServicio()
        {
            return GetServiciosSinDiagPrenez(Servicio_80DiasLactanciaSinServicio, null, null);
        }

        private List<Servicio> GetServiciosSinDiagPrenez(string storedProcedure, short? categoria, int? dias)
        {
            var result = new List<Servicio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if(categoria!=null)cmd.Parameters.Add(new SqlParameter("@CATEGORIA", categoria));
            if(dias!=null)cmd.Parameters.Add(new SqlParameter("@DIAS", dias));
            cmd.CommandText = storedProcedure;
            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
            {
                var serv = new Servicio();
                serv.Registro = (DBNull.Value == dr["REGISTRO"]) ? string.Empty : (string)dr["REGISTRO"];
                string strDate = (DBNull.Value == dr["FECHA"]) ? string.Empty : dr["FECHA"].ToString();
                //if (strDate != string.Empty) serv.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
                if (strDate != string.Empty) serv.Fecha = DateTime.Parse(strDate);
                serv.Reg_padre = (DBNull.Value == dr["REG_PADRE"]) ? string.Empty : (string)dr["REG_PADRE"];
                short idIns = (short)((DBNull.Value == dr["INSEMINADOR"]) ? 0 : (Int16)dr["INSEMINADOR"]);
                var emp = new Empleado {Id_empleado = idIns};
                serv.Inseminador = emp;
                result.Add(serv);
            }
            
            dr.Close();
            return result;
        }


        protected List<Servicio> loadAll(SqlDataReader rs)
        {
            var result = new List<Servicio>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetServiciosByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Servicio_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        
        public List<Evento> GetServiciosByRegistroDespUltParto(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Servicio_SelectByRegistroDespUltParto;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _servicio.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _servicio.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _servicio.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _servicio.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@SERV_MONTA_NATURAL", _servicio.Serv_monta_natural));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _servicio.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@INSEMINADOR", _servicio.Inseminador.Id_empleado));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Servicio_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _servicio.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _servicio.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _servicio.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _servicio.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@SERV_MONTA_NATURAL", _servicio.Serv_monta_natural));
                cmd.Parameters.Add(new SqlParameter("@REG_PADRE", _servicio.Reg_padre));
                cmd.Parameters.Add(new SqlParameter("@INSEMINADOR", _servicio.Inseminador.Id_empleado));
            }
            return cmd;
        }

        protected Servicio load(SqlDataReader record)
        {
            var serv = new Servicio();
            serv.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            serv.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            //if (strDate != string.Empty) serv.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) serv.Fecha = DateTime.Parse(strDate);
            serv.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            serv.Serv_monta_natural = (DBNull.Value == record["SERV_MONTA_NATURAL"]) ? ' ' : Convert.ToChar(record["SERV_MONTA_NATURAL"]);
            serv.Reg_padre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];

            int idInseminador = (short)((DBNull.Value == record["INSEMINADOR"]) ? 0 : (Int16)record["INSEMINADOR"]);
            if (idInseminador > 0)
            {
                var insTemp = new Empleado();
                insTemp.Id_empleado = (Int16)idInseminador;
                serv.Inseminador = insTemp;
            }
            return serv;
        }


        public List<Servicio> GetProximosPartos(string mes, string anio)
        {
            var result = new List<Servicio>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = Servicio_SelecProximosPartos;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }


        private Dictionary<string, int> GetRegCantServ(string storedProcedure)
        {
            var dictionary = new Dictionary<string, int>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storedProcedure;
            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
            {
                dictionary.Add(
                    (DBNull.Value == dr["REGISTRO"]) ? string.Empty : (string) dr["REGISTRO"],
                    (int) ((DBNull.Value == dr["CANTFECHA"]) ? 0 : (Int32) dr["CANTFECHA"])
                    );
            }
            dr.Close();
            return dictionary;
        }


        public Dictionary<string, int> GetRegCantServAntesPrenez()
        {
            return GetRegCantServ(Servicio_RegistroCantServAntesPrenez);
        }

        public Dictionary<string, int> GetRegCantServDespuesPrenez()
        {
            return GetRegCantServ(Servicio_RegistroCantServDespuesPrenez);
        }


        public List<VOServicioDiag> GetServDiagPorToroUtilizado(int anio)
        {
            var result = new List<VOServicioDiag>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.CommandText = ServicioyDiag_PorTorosUtilizByAnio;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadServDiagPorToroUtilizado(dr));
            dr.Close();
            return result;
        }

        public List<VOServicioDiag> GetServDiagPorToroUtilizado2fechas(string fecha1, string fecha2)
        {
            var result = new List<VOServicioDiag>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FECHA1", fecha1));
            cmd.Parameters.Add(new SqlParameter("@FECHA2", fecha2));
            cmd.CommandText = ServicioyDiag_PorTorosUtilizBy2fechas;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadServDiagPorToroUtilizado(dr));
            dr.Close();
            return result;
        }

        protected VOServicioDiag loadServDiagPorToroUtilizado(SqlDataReader record)
        {
            var voServDiagToro = new VOServicioDiag();
            voServDiagToro.Registro = (DBNull.Value == record["REGISTRO"]) ? string.Empty : (string)record["REGISTRO"];
            string strDate = (DBNull.Value == record["FECHA_SERV"]) ? string.Empty : record["FECHA_SERV"].ToString();
            voServDiagToro.RegPadre = (DBNull.Value == record["REG_PADRE"]) ? string.Empty : (string)record["REG_PADRE"];
            //if (strDate != string.Empty) voServDiagToro.FechaServ = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) voServDiagToro.FechaServ = DateTime.Parse(strDate);
            int idInseminador = (short)((DBNull.Value == record["INSEMINADOR"]) ? 0 : (Int16)record["INSEMINADOR"]);
            if (idInseminador > 0)
            {
                var insTemp = new Empleado();
                insTemp.Id_empleado = (Int16)idInseminador;
                voServDiagToro.Inseminador = insTemp;
            }
            string strDate2 = (DBNull.Value == record["FECHA_DIAG"]) ? string.Empty : record["FECHA_DIAG"].ToString();
            //if (strDate2 != string.Empty) voServDiagToro.FechaDiag = DateTime.Parse(strDate2, new CultureInfo("fr-FR"));
            if (strDate2 != string.Empty) voServDiagToro.FechaDiag = DateTime.Parse(strDate2);
            voServDiagToro.Diagnostico = (DBNull.Value == record["DIAGNOSTIC"]) ? ' ' : Convert.ToChar(record["DIAGNOSTIC"]);
            return voServDiagToro;
        }


        public class VOServicioDiag
        {
            public VOServicioDiag()
            {

            }

            public string Registro { get; set; }

            public DateTime FechaServ { get; set; }

            public string RegPadre { get; set; }

            public Empleado Inseminador { get; set; }

            public DateTime FechaDiag { get; set; }

            public char Diagnostico { get; set; }

        }


        public int GetServicio_DiasSinDiagPrenezCount70()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Servicio_DiasSinDiagPrenezCount70;

            result = (int)ReturnScalarValue(cmd);
            return result;
        }

        public int GetServicio_DiasSinDiagPrenezCount35()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Servicio_DiasSinDiagPrenezCount35;

            result = (int)ReturnScalarValue(cmd);
            return result;
        }

        public int GetServicio_80DiasLactanciaSinServicioCount()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Servicio_80DiasLactanciaSinServicioCount;

            result = (int)ReturnScalarValue(cmd);
            return result;
        }

    }
}
