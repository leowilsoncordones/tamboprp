namespace Entidades
{
    public class RolUsuario
    {
        public RolUsuario()
        {
        }

        public RolUsuario(int nivel, string rol, string desc)
        {
            Nivel = nivel;
            Rol = rol;
            Descripcion = desc;
        }

        public int Nivel { get; set; }
        public string Rol { get; set; }

        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Rol;
        }

    }
}

