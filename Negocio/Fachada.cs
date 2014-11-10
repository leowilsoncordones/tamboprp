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
        private static Control_ProduccMapper _controlProdMapper = new Control_ProduccMapper();

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
            List<Evento> listTemp = new List<Evento>();

            /* hay q hacer en evento nacimiento? */

            /* Cargo los concursos del animal */
            var concMap = new ConcursoMapper(registro);
            listTemp = concMap.GetConcursosByRegistro(registro);
            if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            
            /*if (a.esHembra())
            {*/
                /* Cargo las calificaciones del animal, los M puede ser calificado? */
                var calMap = new CalificacionMapper();
                listTemp = calMap.GetCalificacionesByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los controles de produccion del animal */
                var conMap = new Control_ProduccMapper(registro);
                listTemp = conMap.GetControlesProduccByRegistro();
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los celos sin servicio del animal */
                var celoMap = new Celo_Sin_ServicioMapper(registro);
                listTemp = celoMap.GetCelosByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los partos del animal */
                var partoMap = new PartoMapper(registro);
                listTemp = partoMap.GetPartosByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los abortos del animal */
                var abortoMap = new AbortoMapper(registro);
                listTemp = abortoMap.GetAbortosByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los secados del animal */
                var secMap = new SecadoMapper(registro);
                listTemp = secMap.GetSecadosByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los servicios del animal */
                var servMap = new ServicioMapper(registro);
                listTemp = servMap.GetServiciosByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

                /* Cargo los diagnosticos de prenez del animal */
                var diagMap = new Diag_PrenezMapper(registro);
                listTemp = diagMap.GetDiag_PrenezByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            /*}*/
            
            /* Ordeno la coleccion de eventos del animal de forma descendente para mostrarlos en la ficha */
            a.Eventos.Sort();
            a.Eventos.Reverse();
            return a;
        }

        public int GetCantAbortosEsteAnio()
        {
            var abmap = new AbortoMapper();
            return abmap.GetCantAbortosEsteAnio();
        }

        public int GetCantAnimalesUltControl()
        {
            return _controlProdMapper.GetCantAnimalesUltControl();
        }

        public float GetSumLecheUltControl()
        {
            return _controlProdMapper.GetSumLecheUltControl();
        }

        public float GetPromLecheUltControl()
        {
            return _controlProdMapper.GetPromLecheUltControl();
        }

        public float GetSumGrasaUltControl()
        {
            return _controlProdMapper.GetSumGrasaUltControl();
        }

        public float GetPromGrasaUltControl()
        {
            return _controlProdMapper.GetPromGrasaUltControl();
        }

    }
}
