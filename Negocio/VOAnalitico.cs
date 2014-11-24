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
                            int cantLactanciaMayor2, int conServicioSinPreñez, int prenezConfirmada, double promDiasLactancias)
        {
            CantVacasEnOrdene = cantVacasEnOrdene;
            PromProdLecheLts = promProdLecheLts;
            CantLactancia1 = cantLactancia1;
            CantLactancia2 = cantLactancia2;
            CantLactanciaMayor2 = cantLactanciaMayor2;
            ConServicioSinPreñez = conServicioSinPreñez;
            PrenezConfirmada = prenezConfirmada;
            PromDiasLactancias = promDiasLactancias;
        }

        public object CantVacasEnOrdene { get; set; }

        public object PromDiasLactancias { get; set; }

        public int PrenezConfirmada { get; set; }

        public int ConServicioSinPreñez { get; set; }

        public int CantLactanciaMayor2 { get; set; }

        public object CantLactancia2 { get; set; }

        public object CantLactancia1 { get; set; }

        public object PromProdLecheLts { get; set; }

    }
}
