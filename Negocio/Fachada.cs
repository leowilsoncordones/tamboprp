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
            List<Animal> animals = amap.GetBusqAnimal(registro, 0);
            return animals;
        }

        public Animal GetEventosAnimal(string registro)
        {
            var a = new Animal {Registro = registro};
            a.Eventos = new List<Evento>();

            /* hay q hacer en evento nacimiento? */
            //listPartos = partoMap.GetNacimientoByRegistro(registro);
            //if (listPartos.Count > 0) a.Eventos.AddRange(listPartos);
            
            /*if (a.esHembra())
            {*/
                /* Cargo las calificaciones del animal, los M puede ser calificado? */
                var calMap = new CalificacionMapper();
                List<Evento> listEv = calMap.GetCalificacionesByRegistro(registro);
                if (listEv.Count > 0) a.Eventos.AddRange(listEv);

                /* Cargo los controles de produccion del animal */
                var conMap = new Control_ProduccMapper(registro);
                List<Evento> lisConProd = conMap.GetControlesProduccByRegistro();
                if (lisConProd.Count > 0) a.Eventos.AddRange(lisConProd);

                /* Cargo los celos sin servicio del animal */
                var celoMap = new Celo_Sin_ServicioMapper(registro);
                List<Evento> listCelos = celoMap.GetCelosByRegistro(registro);
                if (listCelos.Count > 0) a.Eventos.AddRange(listCelos);

                /* Cargo los partos del animal */
                var partoMap = new PartoMapper(registro);
                List<Evento> listPartos = partoMap.GetPartosByRegistro(registro);
                if (listPartos.Count > 0) a.Eventos.AddRange(listPartos);

                /* Cargo los abortos del animal */
                var abortoMap = new AbortoMapper(registro);
                List<Evento> listAbortos = abortoMap.GetAbortosByRegistro(registro);
                if (listAbortos.Count > 0) a.Eventos.AddRange(listAbortos);

            /*}*/
            
            /* Ordeno la coleccion de eventos del animal de forma descendente para mostrarlos en la ficha */
            a.Eventos.Sort();
            a.Eventos.Reverse();
            return a;
        }

    }
}
