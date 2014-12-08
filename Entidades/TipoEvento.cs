namespace Entidades
{
    public class TipoEvento
    {
        public TipoEvento()
        {
        }

        public TipoEvento(int id, string nombre)
        {
            Nombre = nombre;
            Id = id;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}

