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
            NombreRol = rol;
            Descripcion = desc;
        }

        public int Nivel { get; set; }

        public string NombreRol { get; set; }

        public string Descripcion { get; set; }

        public override string ToString()
        {
            return NombreRol;
        }

    }
}

