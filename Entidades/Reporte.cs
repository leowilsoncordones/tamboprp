
namespace Entidades
{
    public class Reporte
    {
        public Reporte()
        {
        }

        public Reporte(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public int Dia { get; set; }

        public int Frecuencia { get; set; }

        public override string ToString()
        {
            return Titulo;
        }
    }
}
