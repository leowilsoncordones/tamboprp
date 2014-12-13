using System.Collections;
using System.Runtime.InteropServices;

namespace Negocio
{
    public class VoListItemDuplaString
    {
        public VoListItemDuplaString() { }

        public VoListItemDuplaString(string valor1, string valor2)
        {
            Valor1 = valor1;
            Valor2 = valor2;
        }

        public string Valor1 { get; set; }

        public string Valor2 { get; set; }

    }
}