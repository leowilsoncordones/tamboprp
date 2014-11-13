using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EventoString
    {
        public EventoString() { }

        public EventoString(string fecha, string nombreEvento, string comentario)
        {
            Fecha = fecha;
            NombreEvento = nombreEvento;
            Comentario = comentario;
        }

        public string Fecha { get; set; }

        public string NombreEvento { get; set; }

        public string Comentario { get; set; }

    }
}
