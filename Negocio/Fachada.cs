using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Datos;
using Entidades;


namespace Negocio
{
    public class Fachada
    {
        private static Fachada _instance;
        private static Control_ProduccMapper _controlProdMapper = new Control_ProduccMapper();
        private static AnimalMapper _animalMapper = new AnimalMapper();
        private static LactanciaMapper _lactMapper = new LactanciaMapper();
        private static ServicioMapper _servMapper = new ServicioMapper();

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

        public bool EstaMuertoAnimal(string registro)
        {
            var muerteMap = new MuerteMapper(registro);
            return muerteMap.EstaMuertoAnimal(registro);
        }

        public bool FueVendidoAnimal(string registro)
        {
            var ventaMap = new VentaMapper(registro);
            return ventaMap.FueVendidoAnimal(registro);
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
            var listTemp = new List<Evento>();

            /* Cargo los concursos del animal */
            var concMap = new ConcursoMapper(registro);
            listTemp = concMap.GetConcursosByRegistro(registro);
            if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            /* Cargo si el animal esta dado de baja por venta */
            var ventaMap = new VentaMapper(registro);
            listTemp = ventaMap.GetVentaByRegistro(registro);
            if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            /* Cargo si el animal esta dado de baja por muerte */
            var muerteMap = new MuerteMapper(registro);
            listTemp = muerteMap.GetMuerteByRegistro(registro);
            if (listTemp.Count > 0)
            {
                a.Vivo = false;
                a.Eventos.AddRange(listTemp);
            }
            
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

        public List<Animal> GetAnimalesByCategoria(int idCategoria)
        {
            var amap = new AnimalMapper();
            return amap.GetAnimalesByCategoria(idCategoria);
        }
        public int GetCantOrdene()
        {
            return _animalMapper.GetCantOrdene();
        }

        public int GetCantEntoradas()
        {
            return _animalMapper.GetCantEntoradas();
        }

        public int GetCantSecas()
        {
            return _animalMapper.GetCantSecas();
        }

        public string GetFechaUltimoControl()
        {
            return _controlProdMapper.GetFechaUltimoControl();
        }
        


        public List<VOLactancia> GetLactanciasActuales()
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciaActualCategoriaVacaOrdene();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var voLact = new VOLactancia(tmp.Registro, tmp.Numero, tmp.Dias, 
                                             tmp.Leche305, tmp.Grasa305, tmp.Leche365, 
                                             tmp.Grasa365, tmp.ProdLeche, tmp.ProdGrasa);
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public List<VOLactancia> GetLactanciasHistoricas()
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciasHistoricas();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var voLact = new VOLactancia(tmp.Registro, tmp.Numero, tmp.Dias, 
                                             tmp.Leche305, tmp.Grasa305, tmp.Leche365, tmp.Grasa365, 
                                             tmp.ProdLeche, tmp.ProdGrasa);
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public List<VOLactancia> GetMejorProduccion305Dias()
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciaMejorProduccion305();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var voLact = new VOLactancia(tmp.Registro, tmp.Numero, tmp.Dias,
                                             tmp.Leche305, tmp.Grasa305, tmp.Leche365, tmp.Grasa365,
                                             tmp.ProdLeche, tmp.ProdGrasa);
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public List<VOLactancia> GetMejorProduccion365Dias()
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciaMejorProduccion365();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var voLact = new VOLactancia(tmp.Registro, tmp.Numero, tmp.Dias,
                                             tmp.Leche305, tmp.Grasa305, tmp.Leche365, tmp.Grasa365,
                                             tmp.ProdLeche, tmp.ProdGrasa);
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public List<VOControlProdUltimo> GetControlesProduccUltimo()
        {
            
            var lstResult = new List<VOControlProdUltimo>();
            List<Control_Producc> lstLact = _controlProdMapper.GetControlesProduccUltimo();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var lactAux = new LactanciaMapper(tmp.Registro);
                int numLact = lactAux.GetMaxLactanciaByRegistro();
                //fechaServicio
                //numServicio
                var voLact = new VOControlProdUltimo(tmp.Registro, numLact, tmp.Dias_para_control,
                                                     tmp.Leche, tmp.Grasa, tmp.Fecha);
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public VOAnalitico GetAnaliticoVacasEnOrdene()
        {
            var voA = new VOAnalitico();
            voA.CantVacasEnOrdene = _animalMapper.GetCantOrdene();
            //voA.PromProdLecheLts = _animalMapper.GetEnOrdenePromProdLecheLts();
            //voA.CantLactancia1 = _animalMapper.GetEnOrdeneLanctancia1();
            //voA.CantLactancia2 = _animalMapper.GetEnOrdeneLanctancia2();
            //voA.CantOtrasLactancias = _animalMapper.GetEnOrdeneLanctanciaMayor2();
            //voA.ConServicioSinPreñez = _animalMapper.GetEnOrdeneServicioSinPrenez();
            //voA.PrenezConfirmada = _animalMapper.GetEnOrdenePrenezConfirmada();
            //voA.PromDiasLactancias = _animalMapper.GetPromDiasLactancias();
            return voA;
        }

        public List<Enfermedad> GetEnfermedades()
        {
            var enfMap = new EnfermedadMapper();
            return enfMap.GetAll();
        }




        public List<DateTime> GetMeses()
        {
            var lista = new List<DateTime>();
            DateTime fecha = DateTime.Now;
            lista.Add(fecha);
            for ( int i = 0; i <= 10; i++)
            {
                fecha = fecha.AddMonths(1); 
                lista.Add(fecha);
            }
            return lista;
        }

        public List<VOServicio01> GetProximosPartos(DateTime idFecha)
        {
            var mes = idFecha.ToString("MM");
            var anio = idFecha.ToString("yyyy");
            var serMap = new ServicioMapper();
            var listServ = serMap.GetProximosPartos(mes, anio);
            var listVOServ01 = new List<VOServicio01>();
            var dictCantServ = serMap.GetRegCantServAntesPrenez();
            foreach (var serv in listServ)
            {
                DateTime fechaParto = serv.Fecha.AddDays(285);
                int cantServ = dictCantServ.ContainsKey(serv.Registro)? dictCantServ[serv.Registro] : -1 ; 
                var voServ = new VOServicio01
                {
                    Registro = serv.Registro, 
                    FechaServicio = serv.Fecha, 
                    RegistroPadre = serv.Reg_padre,
                    CantServicios = cantServ,
                    FechaParto = fechaParto
                };

                listVOServ01.Add(voServ);
            }


            return listVOServ01;
        }


        public List<VOServicio> GetServicios70SinDiagPrenezVaqEnt()
        {           
            var listServVaqEnt = _servMapper.GetServicios70SinDiagPrenezVaqEnt();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(3);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }

        public List<VOServicio> GetServicios70SinDiagPrenezVacOrdene()
        {
            var listServVaqEnt = _servMapper.GetServicios70SinDiagPrenezVacOrdene();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(4);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }

        public List<VOServicio> GetServicios70SinDiagPrenezVacSecas()
        {
            var listServVaqEnt = _servMapper.GetServicios70SinDiagPrenezVacSecas();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(5);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }


        public List<VOServicio> GetServicios35SinDiagPrenezVaqEnt()
        {
            var listServVaqEnt = _servMapper.GetServicios35SinDiagPrenezVaqEnt();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(3);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }

        public List<VOServicio> GetServicios35SinDiagPrenezVacOrdene()
        {
            var listServVaqEnt = _servMapper.GetServicios35SinDiagPrenezVacOrdene();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(4);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }

        public List<VOServicio> GetServicios35SinDiagPrenezVacSecas()
        {
            var listServVaqEnt = _servMapper.GetServicios35SinDiagPrenezVacSecas();
            var listAnimVaqEnt = _animalMapper.GetAnimalesByCategoria(5);
            var dictCantServ = _servMapper.GetRegCantServDespuesPrenez();
            var listaVOServ = CargarVOServicios(listServVaqEnt, listAnimVaqEnt, dictCantServ);
            return listaVOServ;
        }


        private List<VOServicio> CargarVOServicios(List<Servicio> listServVaqEnt, List<Animal> listAnimVaqEnt, Dictionary<string, int> dictCantServ)
        {
            var listaVOServ = new List<VOServicio>();

            foreach (var vaca in listServVaqEnt)
            {
                var anim = listAnimVaqEnt.FirstOrDefault(p => p.Registro == vaca.Registro);
                int cantServ = dictCantServ.ContainsKey(vaca.Registro) ? dictCantServ[vaca.Registro] : -1;
                var voServ = new VOServicio();
                voServ.Registro = vaca.Registro;
                voServ.RegistroPadre = vaca.Reg_padre;
                voServ.FechaServicio = vaca.Fecha;
                voServ.Edad = CalcularEdad(anim.Fecha_nacim);
                voServ.CantServicios = cantServ;
                voServ.DiasServicio = CalcularDiasServicio(vaca.Fecha);
                listaVOServ.Add(voServ);
            }
            return listaVOServ;
        }


        public string CalcularEdad(DateTime fechaNacimiento)
        {
            TimeSpan intervalo = DateTime.Now - fechaNacimiento;
            var intervaloAnos = (int)((double)intervalo.Days / 365.2425);
            var intervaloMeses = (int)((double)(intervalo.Days%365.2425) / 30.436875);
            return intervaloAnos.ToString() + " años, "+ intervaloMeses.ToString()+ " meses";
        }

        public string CalcularDiasServicio(DateTime fechaServicio)
        {
            TimeSpan intervalo = DateTime.Now - fechaServicio;
            return intervalo.Days.ToString();
        }
    }
}
