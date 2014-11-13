using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOEvento
    {
        public VOEvento() { }

        public VOEvento(string fecha, string nombreEvento, string comentario)
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
