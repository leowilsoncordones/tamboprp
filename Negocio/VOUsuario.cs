using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class VOUsuario
    {
        public string Nickname { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public RolUsuario Rol { get; set; }

        public string Foto { get; set; }

        public bool Habilitado { get; set; }

        public string HabilitadoText { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
