using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VOIndicadores
    {
        
        public VOIndicadores()
        {
        }

        public VoListItemInd VacasEnOrdene { get; set; }

        public VoListItemInd PromLeche { get; set; }

        public VoListItemInd LecheUltControl { get; set; }

        public VoListItemInd PromDiasLactancias { get; set; }

        public VoListItemInd PartosMes { get; set; }

        public VoListItemInd AbortosAnual { get; set; }

        public VoListItemInd NacidosAnual { get; set; }

        public VoListItemInd Prenadas { get; set; }

        public VoListItemInd ToroMasUsado { get; set; }

    }
}
