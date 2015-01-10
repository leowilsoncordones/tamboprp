using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Negocio
{
    public class VoListItemInd
    {
        public VoListItemInd() { }

        public VoListItemInd(   string porcentaje, string value, string text, 
                                string status, string icon, string color,
                                string link, string linkAlt, string datavalue)
        {
            Porcentaje = porcentaje;
            Valor = value;
            Texto = text;
            Status = status;
            Icono = icon;
            Color = color;
            Link = link;
            LinkAlt = linkAlt;
            DataValue = datavalue;
        }

        public VoListItemInd(string text)
        {
            Texto = text;
        }

        public string Porcentaje { get; set; }

        public string Valor { get; set; }

        public string Texto { get; set; }

        public string Status { get; set; }

        public string Icono { get; set; }

        public string Color { get; set; }

        public string Link { get; set; }

        public string LinkAlt { get; set; }

        public string DataValue { get; set; }
    }
}