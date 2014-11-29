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
    public class MuerteMapper : AbstractMapper
    {
        private Muerte _muerte;
        private string _registroAnimal;

        private static string Muerte_SelecByRegistro = "Muerte_SelecByRegistro";
        private string Muerte_SelectCountEsteAnio = "Muerte_SelectCountEsteAnio";
        private string EstaMuerto_AnimalByRegistro = "EstaMuerto_AnimalByRegistro";        

        public MuerteMapper(Muerte muerte)
        {
            _muerte = muerte;
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
                cmd.CommandText = "Muerte_Insert";
                cmd.Parameters.Add(new SqlParameter("@REGISTRO", _registroAnimal));
                cmd.Parameters.Add(new SqlParameter("@EVENTO", _muerte.Id_evento));
                cmd.Parameters.Add(new SqlParameter("@FECHA", _muerte.Fecha));
                cmd.Parameters.Add(new SqlParameter("@COMENTARIO", _muerte.Comentarios));
                //cmd.Parameters.Add(new SqlParameter("@ENFERMEDAD", _muerte.Enfermedad));
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
            if (strDate != string.Empty) muerte.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            muerte.Comentarios = (DBNull.Value == record["COMENTARIO"]) ? string.Empty : (string)record["COMENTARIO"];
            //muerte.Enfermedad = (short)((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16)record["ENFERMEDAD"]);

            // cambiar! no mappers anidados
            var e = new Enfermedad();
            e.Id = (short)((DBNull.Value == record["ENFERMEDAD"]) ? 0 : (Int16)record["ENFERMEDAD"]);
            var enfMap = new EnfermedadMapper(e);
            muerte.Enfermedad = enfMap.GetEnfermedadById();

            return muerte;
        }

    }
}
