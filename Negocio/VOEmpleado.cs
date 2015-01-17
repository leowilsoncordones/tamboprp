using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class VOEmpleado
    {
        public VOEmpleado() { }

        public VOEmpleado(Empleado emp)
        {
            Id = emp.Id_empleado;
            Nombre = emp.Nombre;
            Apellido = emp.Apellido;
            NombreCompleto = Nombre + " " + Apellido;
            Iniciales = emp.Iniciales;
            NombreCompletoIniciales = Nombre + " " + Apellido + " (" + Iniciales +")";
            Activo = emp.Activo;
            EstaActivo = Activo ? "SI" : "NO";
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string NombreCompleto { get; set; }

        public string NombreCompletoIniciales { get; set; }

        public string Iniciales { get; set; }

        public string Rut { get; set; }

        public string EstaActivo { get; set; }

        public bool Activo { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }

    }
}
