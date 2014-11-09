using System;
using System.Collections.Generic;
using System.Text;
using Datos;
using Entidades;


namespace Negocio
{
    public class Fachada
    {
        private static Fachada _instance;

        private Fachada()
        {
        }

        public static Fachada Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Fachada();
                }
                return _instance;
            }
        }


        public List<Animal> GetSearchAnimal(string registro)
        {
            var a = new Animal();
            a.Registro = registro;
            var amap = new AnimalMapper(a);
            List<Animal> animals = amap.GetSearch(registro, 0);
            return animals;
        }

        public Animal GetEventosAnimal(string registro)
        {
            var a = new Animal {Registro = registro};
            a.Eventos = new List<Evento>();

            /* Cargo las calificaciones del animal */
            var calMap = new CalificacionMapper();
            List<Evento> listEv = calMap.GetCalificacionesRegistro(registro);
            if (listEv.Count > 0) a.Eventos.AddRange(listEv);

            /* Cargo los controles de produccion del animal */
            var conMap = new Control_ProduccMapper(registro);
            var lisConProd = conMap.GetControlesProduccRegistro();
            if (lisConProd.Count > 0) a.Eventos.AddRange(lisConProd);

            /* Cargo los celos sin servicio del animal */
            var celoMap = new Celo_Sin_ServicioMapper(registro);
            var listCelos = celoMap.GetCelosRegistro(registro);
            if (listCelos.Count > 0) a.Eventos.AddRange(listCelos);

            /* Ordeno los celos de forma descendente para mostrarlos */
            a.Eventos.Sort();
            a.Eventos.Reverse();
            return a;
        }

    }
}
