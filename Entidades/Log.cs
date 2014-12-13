using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Log : IComparable
    {
        public Log() { }

        public Log(int id, string registro, string usuario, DateTime date, string tabla, string op)
        {
            Id = Id;
            Registro = registro;
            User = usuario;
            Fecha = date;
            Tabla = tabla;
            Operacion = op;
        }

        public int Id { get; set; }

        public string Registro { get; set; }

        public string User { get; set; }

        public DateTime Fecha { get; set; }

        public string Tabla { get; set; }

        public string Operacion { get; set; }

        public int CompareTo(Object obj)
        {
            Log ev = obj as Log;
            if (ev != null)
            {
                return ev.Fecha.CompareTo(this.Fecha);
            }
            return 0;
        }

    }
}
