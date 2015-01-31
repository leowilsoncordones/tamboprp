using System;
using System.Collections.Generic;
using Entidades;
using iTextSharp.text;

namespace Negocio
{
    public class VOControlProdMU
    {
        public VOControlProdMU()
        {
            
        }

        public VOControlProdMU(List<Control_Producc> controlesTotales, List<Control_Producc> controlesFallidos, string mensajeExitosas, string mensajeFallidas, short cantTotales, short cantExitosas)
        {
            MensajeExitosas = mensajeExitosas;
            MensajeFallidas = mensajeFallidas;
            ControlesTotales = controlesTotales;
            ControlesFallidos = controlesFallidos;
            CantTotales = cantTotales;
            CantExitosas = cantExitosas;
        }


        public List<Control_Producc> ControlesTotales { get; set; }

        public List<Control_Producc> ControlesFallidos { get; set; }

        public string MensajeExitosas { get; set; }

        public string MensajeFallidas { get; set; }

        public short CantTotales { get; set; }
        public short CantExitosas { get; set; }

    }
}