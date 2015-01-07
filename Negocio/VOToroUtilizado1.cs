using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class VOToroUtilizado1 : IComparable
    {
        public VOToroUtilizado1()
        {

        }

        public VOToroUtilizado1(VOToroUtilizado vToro)
        {
            Registro = vToro.Registro;
            Nombre = vToro.Nombre;
            Origen = vToro.Origen;
            CantServicios = vToro.CantServicios;
            CantDiagP = vToro.CantDiagP;
            CantNacim = vToro.CantNacim;
            CantH = vToro.CantH;
            CantM = vToro.CantM;
            PorcHembras = vToro.PorcHembras;
            PorcEfectividad = vToro.PorcEfectividad;
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
            var voT = obj as VOToroUtilizado1;
            if (voT != null)
            {
                if (voT.CantServicios.Equals(this.CantServicios))
                {
                    if (voT.PorcEfectividad > this.PorcEfectividad) return 1;
                    else return -1;
                }
                else if (voT.CantServicios > this.CantServicios) return 1;
                else return -1;
            }
            return 0;
        }

    }
}

