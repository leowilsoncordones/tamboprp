using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOReporte
    {
        
        public VOReporte()
        {
            
        }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public int Dia { get; set; }

        public string Envio { get; set; }

        public int Frecuencia  { get; set; }

        public VoListItem CantVacasEnOrdene { get; set; }

        public VoListItem SinDiagEcográficos { get; set; }

        public VoListItem SinDiag70Dias { get; set; }

        public VoListItem EnLactSinServ80Dias { get; set; }

        public VoListItem PrenezConfirmada { get; set; }

        public VoListItem CantNacidosAnio { get; set; }

        public VoListItem CantAbortosAnio { get; set; }

       

    }
}
