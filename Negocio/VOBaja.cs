using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class VOBaja
    {
        public VOBaja()
        {

        }

        public VOBaja(string registro, DateTime fecha, string cod, string comentario, Enfermedad enf)
        {
            Registro = registro;
            Fecha = fecha;
            TipoBaja = TipoBaja;
            Codigo = cod;
            Comentarios = comentario;
            Enfermedad = enf;
        }

        public VOBaja(Baja baja)
        {
            Registro = baja.Registro;
            Fecha = baja.Fecha;
            TipoBaja = baja.Id_evento == 11 ? "Venta" : "Muerte";
            Codigo = baja.Codigo;
            Comentarios = baja.Comentarios;
            Enfermedad = baja.Enfermedad;
        }

        public string Registro { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; }

        public Enfermedad Enfermedad { get; set; }

        public string TipoBaja { get; set; }
        
        public string Codigo { get; set; }

        public string Comentarios { get; set; }

    }
}
