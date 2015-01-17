using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class VOInseminadorRank : IComparable
    {
        public VOInseminadorRank()
        {

        }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Inciales { get; set; }

        public string NombreCompleto { get; set; }

        public int IdEmpleado { get; set; }

        public int CantServicios { get; set; }

        public int CantPrenadas { get; set; }

        public double PorcEfectividad { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Apellido + "(" + Inciales + ")";
        }

        public int CompareTo(Object obj)
        {
            var voT = obj as VOInseminadorRank;
            if (voT != null)
            {
                if (voT.PorcEfectividad.Equals(this.PorcEfectividad))
                {
                    if (voT.CantServicios > this.CantServicios) return 1;
                    else return -1;
                }
                else if (voT.PorcEfectividad > this.PorcEfectividad) return 1;
                else return -1;
            }
            return 0;
        }

    }
}

