using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class VOCaso
    {
        public int Id { get; set; }

        public string Establecimiento { get; set; }

        public string Nickname { get; set; }

        public string NombreApellido { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Titulo { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public override string ToString()
        {
            return "Caso #" + Id + "\n" +
                   Establecimiento + "\n" +
                   NombreApellido + " (" + Nickname + "), " + Telefono + ", " + Email + "\n" +
                   "Titulo: " + Titulo + "\n" +
                   "Tipo: " + Tipo + "\n" +
                   "Descripción: " + Descripcion;
        }

    }
}
