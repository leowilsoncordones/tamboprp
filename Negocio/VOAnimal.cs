using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class VOAnimal
    {
        public VOAnimal()
        {
            Vivo = true;
        }

        public VOAnimal(bool vivo, int idCategoria, string identificacion,
                        string origen, string regMadre, string regPadre, VOAnimal madre, VOAnimal padre, DateTime fechaNacim, 
                        char sexo, string regTrazab, int gen, string registro, string nombre, string calific)
        {
            Vivo = vivo;
            IdCategoria = idCategoria;
            Fotos = new List<AnimalMapper.VOFoto>();
            Identificacion = identificacion;
            Origen = origen;
            Reg_madre = regMadre;
            Madre = madre;
            Reg_padre = regPadre;
            Padre = padre;
            Fecha_nacim = fechaNacim;
            Sexo = sexo;
            Reg_trazab = regTrazab;
            Gen = gen;
            Registro = registro;
            Nombre = nombre;
            Calific = calific;
        }

        public string Reg_madre { get; set; }

        public string Reg_padre { get; set; }

        public VOAnimal Padre { get; set; }

        public VOAnimal Madre { get; set; }

        public string Calific { get; set; }

        public bool Vivo { get; set; }

        public string Nombre { get; set; }

        public string Registro { get; set; }

        public int Gen { get; set; }

        public string Reg_trazab { get; set; }

        public Char Sexo { get; set; }

        public DateTime Fecha_nacim { get; set; }

        public string Origen { get; set; }

        public string Identificacion { get; set; }

        public List<AnimalMapper.VOFoto> Fotos { get; set; }

        public int IdCategoria { get; set; }

        public bool esHembra()
        {
            return Sexo != 'M';
        }
        public bool esMacho()
        {
            return Sexo == 'M';
        }

        public override string ToString()
        {
            return Registro;
        }

    }
}

