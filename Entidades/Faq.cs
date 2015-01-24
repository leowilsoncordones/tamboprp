using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Faq
    {
        public Faq() { }

        public Faq(int id, string pregunta, string respuesta, string icono)
        {
            Id = id;
            Pregunta = pregunta;
            Respuesta = respuesta;
            Icono = icono;
        }

        public int Id { get; set; }

        public string Pregunta { get; set; }

        public string Respuesta { get; set; }

        public string Icono { get; set; }

        public string Operacion { get; set; }

    }
}
