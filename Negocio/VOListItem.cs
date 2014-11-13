using System.Collections;
using System.Runtime.InteropServices;

namespace Negocio
{
    public class VoListItem
    {
        public VoListItem() { }

        public VoListItem(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

    }
}