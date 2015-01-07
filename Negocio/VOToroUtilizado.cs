using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class VOToroUtilizado : IComparable
    {
        public VOToroUtilizado()
        {

        }

        public string Registro { get; set; }
        
        public string Nombre { get; set; }

        public string Origen { get; set; }

        public int CantServicios { get; set; }

        public int CantDiagP { get; set; }

        public int CantNacim { get; set; }

        public int CantH { get; set; }

        public int CantM { get; set; }

        public double PorcHembras { get; set; }

        public double PorcEfectividad { get; set; }

        public override string ToString()
        {
            return Registro;
        }

        public int CompareTo(Object obj)
        {
            var voT = obj as VOToroUtilizado;
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

