using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOResultado
    {
        public VOResultado()
        {
        }

        public VOResultado(bool resultado, string mensaje)
        {
            Resultado = resultado;
            Mensaje = mensaje;
        }


        public bool Resultado { get; set; }

        public string Mensaje { get; set; }
    }
}
