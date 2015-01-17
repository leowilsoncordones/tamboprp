using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOConcurso
    {
        public VOConcurso() { }

        public VOConcurso(string reg, string fecha, string lugar, string nombreExpo, string premio, string comentario)
        {
            Registro = reg;
            Fecha = fecha;
            Lugar = lugar;
            NombreExpo = nombreExpo;
            ElPremio = premio;
            Comentarios = comentario;
        }

        public string Registro { get; set; }

        public string Fecha { get; set; }

        public string Lugar { get; set; }

        public string NombreExpo { get; set; }

        public string CategConcurso { get; set; }

        public string ElPremio { get; set; }

        public string Comentarios { get; set; }


    }
}
