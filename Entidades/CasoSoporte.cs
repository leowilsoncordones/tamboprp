namespace Entidades
{
    public class CasoSoporte
    {
        public CasoSoporte()
        {
            Adjunto = "";
        }

        public CasoSoporte(int id)
        {
            Id = id;
        }

        public CasoSoporte( int id, string establecim, string nickname, string nomape, 
                            string email, string telefono, string titulo, string tipo, 
                            string descripcion, string adjunto)
        {
            Id = id;
            Establecimiento = establecim;
            Nickname = nickname;
            NombreApellido = nomape;
            Telefono = telefono;
            Email = email;
            Titulo = titulo;
            Tipo = tipo;
            Descripcion = Descripcion;
            Adjunto = adjunto;
        }

        public CasoSoporte( string establecim, string nickname, string nomape,
                            string email, string telefono, string titulo, string tipo,
                            string descripcion, string adjunto)
        {
            Establecimiento = establecim;
            Nickname = nickname;
            NombreApellido = nomape;
            Telefono = telefono;
            Email = email;
            Titulo = titulo;
            Tipo = tipo;
            Descripcion = Descripcion;
            Adjunto = adjunto;
        }

        public int Id { get; set; }

        public string Establecimiento { get; set; }

        public string Nickname { get; set; }

        public string NombreApellido { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Titulo { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public string Adjunto { get; set; }

        public override string ToString()
        {
            return Id + " " + Tipo + " " + Titulo + " " + NombreApellido;
        }

    }
}
