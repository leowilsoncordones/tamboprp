using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOAnalitico
    {
        
        public VOAnalitico()
        {
            
        }

        public VOAnalitico( int cantVacasEnOrdene, double promProdLecheLts, int cantLactancia1, int cantLactancia2,
                            int cantLactanciaMayor2, int conServicioSinPreñez, int prenezConfirmada, double promDiasLactancias, double promServicioSinPrenez, double promPrenezConfirmada)
        {
            CantVacasEnOrdene = cantVacasEnOrdene;
            PromProdLecheLts = promProdLecheLts;
            CantLactancia1 = cantLactancia1;
            CantLactancia2 = cantLactancia2;
            CantLactanciaMayor2 = cantLactanciaMayor2;
            ConServicioSinPreñez = conServicioSinPreñez;
            PrenezConfirmada = prenezConfirmada;
            PromDiasLactancias = promDiasLactancias;
            PromServicioSinPrenez = promServicioSinPrenez;
            PromPrenezConfirmada = promPrenezConfirmada;
        }

        public int CantVacasEnOrdene { get; set; }

        public double PromDiasLactancias { get; set; }

        public int PrenezConfirmada { get; set; }

        public int ConServicioSinPreñez { get; set; }

        public int CantLactancia1 { get; set; }

        public double PorcLactancia1 { get; set; }
        
        public int CantLactancia2 { get; set; }

        public double PorcLactancia2 { get; set; }
        
        public int CantLactanciaMayor2 { get; set; }

        public double PorcLactanciaMayor2 { get; set; }
        
        public double PromProdLecheLts { get; set; }

        public double PromServicioSinPrenez { get; set; }

        public double PromPrenezConfirmada { get; set; }

    }
}
