using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.SqlServer.Server;

namespace Datos
{
    public class UsuarioMapper: AbstractMapper
    {
        private Usuario _user;

        private static string UsuariosRoles_SelectAll = "UsuariosRoles_SelectAll";
        private static string Usuario_SelectAtLogin = "Usuario_SelectAtLogin";
        private static string Usuario_Logoff = "Usuario_Logoff";
        private static string Usuario_UpdatePassword = "Usuario_UpdatePassword";
        private static string Usuario_DeleteUsuario = "Usuario_DeleteUsuario";
        private static string Usuario_FotoPerfilUsuario = "Usuario_FotoPerfilUsuario";
        private static string Usuario_Deshabilitar = "Usuario_Deshabilitar";
        private static string Usuario_Habilitar = "Usuario_Habilitar";
        

        public UsuarioMapper(Usuario user)
        {
            _user = user;
        }

        public UsuarioMapper()
        {
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public Usuario GetUsuarioById()
        {
            SqlDataReader dr = Find(OperationType.SELECT_ID);
            dr.Read();
            return (Usuario)load(dr);
        }

        public List<Usuario> GetAll()
        {
            var ls = new List<Usuario>();
            ls = loadAll(Find(OperationType.SELECT_DEF));
            return ls;
        }


        protected List<Usuario> loadAll(SqlDataReader rs)
        {
            var result = new List<Usuario>();
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
                cmd.CommandText = "Usuario_SelectById";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _user.Nickname));
            }

            else if (opType == OperationType.SELECT_DEF)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_SelectAll";
            }
            else if (opType == OperationType.DELETE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_Delete";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _user.Nickname));
            }
            else if (opType == OperationType.INSERT)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_Insert";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _user.Nickname));
                cmd.Parameters.Add(new SqlParameter("@PASSW", _user.Password));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _user.Nombre));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO", _user.Apellido));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", _user.Email));
                cmd.Parameters.Add(new SqlParameter("@FOTO", _user.Foto));
                cmd.Parameters.Add(new SqlParameter("@ROL", _user.Rol.NombreRol));
                cmd.Parameters.Add(new SqlParameter("@HABILITADO", _user.Habilitado));
            }
            else if (opType == OperationType.UPDATE)
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_Update";
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", _user.Nickname));
                //cmd.Parameters.Add(new SqlParameter("@PASSW", _user.Password));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", _user.Nombre));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO", _user.Apellido));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", _user.Email));
                cmd.Parameters.Add(new SqlParameter("@FOTO", _user.Foto));
                cmd.Parameters.Add(new SqlParameter("@ROL", _user.Rol.NombreRol));
                cmd.Parameters.Add(new SqlParameter("@HABILITADO", _user.Habilitado));
            }
            return cmd;
        }

        protected Usuario load(SqlDataReader record)
        {
            var user = new Usuario();
            user.Nickname = (DBNull.Value == record["NICKNAME"]) ? string.Empty : (string)record["NICKNAME"];
            //user.Password = (DBNull.Value == record["PASSW"]) ? string.Empty : (string)record["PASSW"];
            user.Nombre = (DBNull.Value == record["NOMBRE"]) ? string.Empty : (string)record["NOMBRE"];
            user.Apellido = (DBNull.Value == record["APELLIDO"]) ? string.Empty : (string)record["APELLIDO"];
            user.Email = (DBNull.Value == record["EMAIL"]) ? string.Empty : (string)record["EMAIL"];
            user.Foto = (DBNull.Value == record["FOTO"]) ? string.Empty : (string)record["FOTO"];
            var rol = new RolUsuario();
            rol.NombreRol = (DBNull.Value == record["ROL"]) ? string.Empty : (string)record["ROL"];
            user.Rol = rol;
            int hab = (short)((DBNull.Value == record["HABILITADO"]) ? 0 : (Int16)record["HABILITADO"]);
            if (hab == 1) user.Habilitado = true;
            else user.Habilitado = false;
            return user;
        }


        protected RolUsuario loadRol(SqlDataReader record)
        {
            var rol = new RolUsuario();
            rol.Nivel = (short)((DBNull.Value == record["NIVEL"]) ? 0 : (Int16)record["NIVEL"]);
            rol.NombreRol = (DBNull.Value == record["ROL"]) ? string.Empty : (string)record["ROL"];
            rol.Descripcion = (DBNull.Value == record["DESCRIPCION"]) ? string.Empty : (string)record["DESCRIPCION"];
            return rol;
        }

        public List<RolUsuario> GetRolesUsuarioAll()
        {
            var result = new List<RolUsuario>();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = UsuariosRoles_SelectAll;

            SqlDataReader dr = FindByCmd(cmd);
            while (dr.Read())
                result.Add(loadRol(dr));
            dr.Close();
            return result;
        }


        public Usuario GetUsuarioAtLogin(string user, string password)
        {
            Usuario usu = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = Usuario_SelectAtLogin;
                cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));
                cmd.Parameters.Add(new SqlParameter("@PASS", password));

                SqlDataReader dr = FindByCmd(cmd);
                dr.Read();
                usu = (Usuario)load(dr);
            }
            catch { }
            return usu;
        }

        public int LogoffUsuario(string user)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_Logoff;
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int UpdateContrasenaUsuario(string admin, string user, string newPass)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_UpdatePassword;
            cmd.Parameters.Add(new SqlParameter("@ADMIN", admin));
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));
            cmd.Parameters.Add(new SqlParameter("@NUEVAPASS", newPass));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int DeleteUsuario(string admin, string user)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_DeleteUsuario;
            cmd.Parameters.Add(new SqlParameter("@ADMIN", admin));
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int UpdateFotoPerfilUsuario()
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_FotoPerfilUsuario;
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", _user.Nickname));
            cmd.Parameters.Add(new SqlParameter("@FOTO", _user.Foto));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int UsuarioDeshabilitar(string admin, string user)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_Deshabilitar;
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));
            cmd.Parameters.Add(new SqlParameter("@ADMIN", admin));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }

        public int UsuarioHabilitar(string admin, string user)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Usuario_Habilitar;
            cmd.Parameters.Add(new SqlParameter("@NICKNAME", user));
            cmd.Parameters.Add(new SqlParameter("@ADMIN", admin));

            var value = ReturnScalarValue(cmd);
            return Convert.ToInt32(value);
        }
    }
}
