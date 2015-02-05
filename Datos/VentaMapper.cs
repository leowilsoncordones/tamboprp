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
    public class VentaMapper : AbstractMapper
    {
        private Venta _venta;
        private string _registroAnimal;
        private string _nickName;

        private static string Venta_SelecByRegistro = "Venta_SelecByRegistro";
        private string Venta_SelectCountEsteAnio = "Venta_SelectCountEsteAnio";
        private string FueVendido_AnimalByRegistro = "FueVendido_AnimalByRegistro";        

        public VentaMapper(Venta venta)
        {
            _venta = venta;
        }

        public VentaMapper(Venta venta, string nickName)
        {
            _venta = venta;
            _nickName = nickName;
        }

        public VentaMapper(string  registroAnimal)
        {
            _registroAnimal = registroAnimal;
        }

        public VentaMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Venta GetVentaById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Venta)load(dr);
        }


        public List<Venta> GetAll()
        {
            List<Venta> ls = new List<Venta>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Venta> loadAll(SqlDataReader rs)
        {
            List<Venta> result = new List<Venta>();
            while (rs.Read())
                result.Add(load(rs));
            rs.Close();
            return result;
        }

        public List<Evento> GetVentaByRegistro(string regAnimal)
        {
            List<Evento> result = new List<Evento>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = Venta_SelecByRegistro;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(load(dr));
            dr.Close();
            return result;
        }

        public bool FueVendidoAnimal(string regAnimal)
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@REGISTRO", regAnimal));
            cmd.CommandText = FueVendido_AnimalByRegistro;

            result = (int)ReturnScalarValue(cmd);
            return result > 0;
        }

        public int GetCantVentasEsteAnio()
        {
            int result;
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Venta_SelectCountEsteAnio;

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
                cmd.CommandText = "Venta_SelecById";
                //cmd.Parameters.Add(new SqlParameter("@REGISTRO", _calificacion.));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Venta_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Venta_Delete";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Baja_Insert";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _venta.Registro));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _venta.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _venta.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _venta.Comentarios));
                cmd.Parameters.Add(new SqlParameter("@BAJAS", ""));
                cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _venta.Enfermedad));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Venta_Update";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _venta.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _venta.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _venta.Comentarios));
            }
            return cmd;
        }

        protected Venta load(SqlDataReader record)
        {
            var venta = new Venta();
            venta.Id_evento = (short)((DBNull.Value == record["EVENTO"]) ? 0 : (Int16)record["EVENTO"]);
            string strDate = (DBNull.Value == record["FECHA"]) ? string.Empty : record["FECHA"].ToString();
            //if (strDate != string.Empty) venta.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            if (strDate != string.Empty) venta.Fecha = DateTime.Parse(strDate);
            venta.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];

            return venta;
        }

        public bool InsertVentaLactancia(Lactancia lact)
        {
            bool salida = false;
            SqlConnection con = null;
            SqlCommand cmdVenta = null;
            SqlCommand cmdVentaLac = null;
            SqlTransaction trn = null;
            int affected = 0;

            try
            {
                con = new SqlConnection(GetConnectionString());
                con.Open();
                cmdVenta = GetStatement(OperationType.INSERT);
                cmdVenta.Connection = con;
                trn = con.BeginTransaction();
                cmdVenta.Transaction = trn;
                int affectedSec = cmdVenta.ExecuteNonQuery();

                if (affectedSec > 0)
                {
                    cmdVentaLac = new SqlCommand();
                    cmdVentaLac.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdVentaLac.CommandText = "Lactancia_Insert";
                    cmdVentaLac.Parameters.Add(new SqlParameter("@NICKNAME", _nickName));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@REGISTRO", lact.Registro));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@LACTANCIAS", lact.Numero));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@DIAS", lact.Dias));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@LECHE305", lact.Leche305));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@GRASA305", lact.Grasa305));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@LECHE365", lact.Leche365));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@GRASA365", lact.Grasa365));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@PRODLECHE", lact.ProdLeche));
                    cmdVentaLac.Parameters.Add(new SqlParameter("@PRODGRASA", lact.ProdGrasa));
                    cmdVentaLac.Transaction = trn;
                    cmdVentaLac.Connection = con;
                    affected = cmdVentaLac.ExecuteNonQuery();

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
                    if (cmdVenta != null) cmdVenta.Dispose();
                    if (cmdVentaLac != null) cmdVentaLac.Dispose();
                    if (trn != null) trn.Dispose();
                }
            }

            return salida;
        }
    }
}
