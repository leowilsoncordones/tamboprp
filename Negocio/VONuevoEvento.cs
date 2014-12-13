using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VONuevoEvento
    {
        public short IdEvento { get; set; }
        public string Registro { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
        public string AbRegistroServicio { get; set; }
        public string CalifLetras { get; set; }
        public short? CalifPuntos { get; set; }
        public double? ContLeche { get; set; }
        public double? ContGrasa { get; set; }
        public int? ContDiasLactancia { get; set; }
        public string Diagnostico { get; set; }
        public short? MotivoSecado { get; set; }
        public char? ServMontaNatural { get; set; }
        public string ServRegPadre { get; set; }
        public short? ServInseminador { get; set; }
        public short? ConcursoCategoria { get; set; }
        public string ConcursoPremio { get; set; }
        public string ConcursoLugar { get; set; }
        public string ConcursoNombre { get; set; }

    }
}
