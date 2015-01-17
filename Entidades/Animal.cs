using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Animal
    {
        public Animal()
        {
            Vivo = true;
        }

        public Animal(string reg)
        {
            Registro = reg;
        }

        public Animal(bool vivo, IEnumerable<Evento> evento, int idCategoria, List<Evento> eventos, List<string> fotos, string identificacion, string origen, string regMadre, string regPadre, DateTime fechaNacim, char sexo, string regTrazab, int gen, string registro, string nombre, string calific)
        {
            Vivo = vivo;
            Evento = evento;
            IdCategoria = idCategoria;
            Eventos = eventos;
            Fotos = fotos;
            Identificacion = identificacion;
            Origen = origen;
            Reg_madre = regMadre;
            Reg_padre = regPadre;
            Fecha_nacim = fechaNacim;
            Sexo = sexo;
            Reg_trazab = regTrazab;
            Gen = gen;
            Registro = registro;
            Nombre = nombre;
            Calific = calific;
        }

        public string Calific { get; set; }

        public bool Vivo { get; set; }

        public string Nombre { get; set; }

        public string Registro { get; set; }

        public int Gen { get; set; }

        public string Reg_trazab { get; set; }

        public Char Sexo { get; set; }

        public DateTime Fecha_nacim { get; set; }

        public string Reg_padre { get; set; }

        public string Reg_madre { get; set; }

        public string Origen { get; set; }

        public string Identificacion { get; set; }

        public List<string> Fotos { get; set; }

        public List<Evento> Eventos { get; set; }

        public int IdCategoria { get; set; }

        public bool esHembra()
        {
            return Sexo != 'M';
        }
        public bool esMacho()
        {
            return Sexo == 'M';
        }

        public virtual IEnumerable<Evento> Evento { get; set; }

        public override string ToString()
        {
            return Registro;
        }

    }
}

