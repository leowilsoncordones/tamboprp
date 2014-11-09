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
            var calMap = new CalificacionMapper();           
            List<Evento> listEv = calMap.GetCalificacionesRegistro(registro);
            if (listEv.Count > 0) a.Eventos.AddRange(listEv);
            var conMap = new Control_ProduccMapper(registro);
            var lisConProd = conMap.GetControlesProduccRegistro();
            if (lisConProd.Count > 0) a.Eventos.AddRange(lisConProd);
            return a;
        }

    }
}
