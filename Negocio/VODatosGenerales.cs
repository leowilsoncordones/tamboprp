using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VODatosGenerales
    {
        
        public VODatosGenerales()
        {
            
        }

        public int CantAbortosEsteAnio { get; set; }

        public int CantAnimUltControl { get; set; }

        public double SumLecheUltControl { get; set; }

        public double PromLecheUltControl { get; set; }

        public double SumGrasaUltControl { get; set; }

        public double PromGrasaUltControl { get; set; }
        
        public int CantOrdene { get; set; }

        public int CantEntoradas { get; set; }

        public int CantSecas { get; set; }

        public string FechaUltControl { get; set; }
        

    }
}
