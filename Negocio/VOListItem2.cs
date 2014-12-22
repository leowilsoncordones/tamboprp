using System.Collections;
using System.Runtime.InteropServices;

namespace Negocio
{
    public class VoListItem2
    {
        public VoListItem2() { }

        public VoListItem2(int id, int num, string nombre)
        {
            Id = id;
            Num = num;
            Nombre = nombre;
        }

        public int Id { get; set; }

        public int Num { get; set; }

        public string Nombre { get; set; }

    }
}