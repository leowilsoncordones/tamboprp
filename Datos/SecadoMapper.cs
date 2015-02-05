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
    public class SecadoMapper : AbstractMapper
    {
        private Secado _secado;
        private string _registroAnimal;
        private string _nickName;

        private static string Secado_SelecByRegistro = "Secado_SelecByRegistro";

        public SecadoMapper(Secado secado)
        {
            _secado = secado;
        }

        public SecadoMapper(Secado secado, string nickName)
        {
            _secado = secado;
            _nickName = nickName;
        }

        public SecadoMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public SecadoMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Secado GetSecadoById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Secado)load(dr);
        }


        public List<Secado> GetAll()
        {
            List<Secado> ls = new List<Secado>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Secado> loadAll(SqlDataReader rs)
        {
            List<Secado> result = new List<Secado>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetSecadosByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Secado_SelecByRegistro;

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
                cmd.CommandText = "Secado_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Secado_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Secado_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Secado_Insert";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _secado.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _secado.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _secado.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _secado.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@MOTIVO_SECADO", _secado.IdMotivoSecado));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _secado.Enfermedad));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Secado_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _secado.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _secado.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _secado.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@MOTIVO_SECADO", _secado.IdMotivoSecado));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _secado.Enfermedad));
            }
            return cmd;
        }

        protected Secado load(SqlDataReader record)
        {
            var sec = new Secado();
            sec.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            //if (strDate != string.Empty) sec.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) sec.Fecha = DateTime.Parse(strDate);
            sec.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            //sec.IdMotivoSecado = (DBNull.Value == record["MOTIVO_SECADO"]) ? 0 : (Int16)record["MOTIVO_SECADO"];

            sec.IdMotivoSecado = (short)((DBNull.Value == record["MOTIVO_SECADO"]) ? 0 : (Int16)record["MOTIVO_SECADO"]);
            if (sec.IdMotivoSecado.Equals(2))
            {
                sec.Enfermedad = (short)((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16)record["ENFERMEDAD"]);             
            }

            return sec;
        }


        public bool InsertSecado(Lactancia lact)
        {
            bool salida = false;
            SqlConnection con = null;
            SqlCommand cmdSec = null;
            SqlCommand cmdSecLac = null;
            SqlTransaction trn = null;
            int affected = 0;

            try
            {
                con = new SqlConnection(GetConnectionString());
                con.Open();
                cmdSec = GetStatement(OperationType.INSERT);
                cmdSec.Connection = con;
                trn = con.BeginTransaction();
                cmdSec.Transaction = trn;
                int affectedSec = cmdSec.ExecuteNonQuery();

                if (affectedSec > 0)
                {
                    cmdSecLac = new SqlCommand();
                    cmdSecLac.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdSecLac.CommandText = "Lactancia_Insert";
                    cmdSecLac.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                    cmdSecLac.Parameters.Add(new SqlParameter("@REGISTRO", lact.Registro));
                    cmdSecLac.Parameters.Add(new SqlParameter("@LACTANCIAS", lact.Numero));
                    cmdSecLac.Parameters.Add(new SqlParameter("@DIAS", lact.Dias));
                    cmdSecLac.Parameters.Add(new SqlParameter("@LECHE305", lact.Leche305));
                    cmdSecLac.Parameters.Add(new SqlParameter("@GRASA305", lact.Grasa305));
                    cmdSecLac.Parameters.Add(new SqlParameter("@LECHE365", lact.Leche365));
                    cmdSecLac.Parameters.Add(new SqlParameter("@GRASA365", lact.Grasa365));
                    cmdSecLac.Parameters.Add(new SqlParameter("@PRODLECHE", lact.ProdLeche));
                    cmdSecLac.Parameters.Add(new SqlParameter("@PRODGRASA", lact.ProdGrasa));
                    cmdSecLac.Transaction = trn;
                    cmdSecLac.Connection = con;
                    affected = cmdSecLac.ExecuteNonQuery();
                    
                    if (affected > 0)
                    {
                        trn.Commit();
                        salida = true;
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch (SqlException e)
            {
                trn.Rollback();
            }
            catch (Exception e2)
            {
                trn.Rollback();
            }

            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                    if (cmdSec != null) cmdSec.Dispose();
                    if (cmdSecLac != null) cmdSecLac.Dispose();
                    if (trn != null) trn.Dispose();
                }
            }

            return salida;
        }



    }
}
