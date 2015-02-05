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
    public class MuerteMapper : AbstractMapper
    {
        private Muerte _muerte;
        private string _registroAnimal;
        private string _nickName;

        private static string Muerte_SelecByRegistro = "Muerte_SelecByRegistro";
        private string Muerte_SelectCountEsteAnio = "Muerte_SelectCountEsteAnio";
        private string EstaMuerto_AnimalByRegistro = "EstaMuerto_AnimalByRegistro";        

        public MuerteMapper(Muerte muerte)
        {
            _muerte = muerte;
        }

        public MuerteMapper(Muerte muerte, string nickName)
        {
            _muerte = muerte;
            _nickName = nickName;
        }

        public MuerteMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public MuerteMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Muerte GetMuerteById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Muerte)load(dr);
        }


        public List<Muerte> GetAll()
        {
            var ls = new List<Muerte>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Muerte> loadAll(SqlDataReader rs)
        {
            var result = new List<Muerte>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetMuerteByRegistro(string regAnimal)
        {
            var result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Muerte_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public bool EstaMuertoAnimal(string regAnimal)
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = EstaMuerto_AnimalByRegistro;

            result = (int)ReturnScalarValue(cmd);
            return result > 0;
        }

        public int GetCantMuertesEsteAnio()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Muerte_SelectCountEsteAnio;

            result = (int)ReturnScalarValue(cmd);
            return result;
        }

        protected override SqlCommand GetStatement(OperationType opType)
        {
            SqlCommand cmd = null;
            if (opType == OperationType.SELECT_ID)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Muerte_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Muerte_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Muerte_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_Insert";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _muerte.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _muerte.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _muerte.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _muerte.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@BAJAS", ""));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _muerte.Enfermedad));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Muerte_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _muerte.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _muerte.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _muerte.Comentarios));
                //cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _muerte.Enfermedad));
            }
            return cmd;
        }

        protected Muerte load(SqlDataReader record)
        {
            var muerte = new Muerte();
            muerte.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            //if (strDate != string.Empty) muerte.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) muerte.Fecha = DateTime.Parse(strDate);
            muerte.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            muerte.Enfermedad = (short)((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16)record["ENFERMEDAD"]);
            //var enf = new Enfermedad();
            //enf.Id = (short)((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16)record["ENFERMEDAD"]);
            //muerte.Enfermedad = enf;
            return muerte;
        }

        public bool BajaExiste(string registro)
        {
            return GetScalarIntReg("Baja_Existe", registro) > 0;
        }

        public bool InsertMuerteLactancia(Lactancia lact)
        {
            bool salida = false;
            SqlConnection con = null;
            SqlCommand cmdMuerte = null;
            SqlCommand cmdMuerteLac = null;
            SqlTransaction trn = null;
            int affected = 0;

            try
            {
                con = new SqlConnection(GetConnectionString());
                con.Open();
                cmdMuerte = GetStatement(OperationType.INSERT);
                cmdMuerte.Connection = con;
                trn = con.BeginTransaction();
                cmdMuerte.Transaction = trn;
                int affectedSec = cmdMuerte.ExecuteNonQuery();

                if (affectedSec > 0)
                {
                    cmdMuerteLac = new SqlCommand();
                    cmdMuerteLac.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdMuerteLac.CommandText = "Lactancia_Insert";
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@REGISTRO", lact.Registro));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@LACTANCIAS", lact.Numero));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@DIAS", lact.Dias));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@LECHE305", lact.Leche305));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@GRASA305", lact.Grasa305));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@LECHE365", lact.Leche365));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@GRASA365", lact.Grasa365));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@PRODLECHE", lact.ProdLeche));
                    cmdMuerteLac.Parameters.Add(new SqlParameter("@PRODGRASA", lact.ProdGrasa));
                    cmdMuerteLac.Transaction = trn;
                    cmdMuerteLac.Connection = con;
                    affected = cmdMuerteLac.ExecuteNonQuery();

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
                    if (cmdMuerte != null) cmdMuerte.Dispose();
                    if (cmdMuerteLac != null) cmdMuerteLac.Dispose();
                    if (trn != null) trn.Dispose();
                }
            }

            return salida;
        }
    }
}
