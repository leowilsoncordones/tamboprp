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
        private static AbortoMapper _abortoMapper = new AbortoMapper();
        private static Control_ProduccMapper _controlProdMapper = new Control_ProduccMapper();
        private static AnimalMapper _animalMapper = new AnimalMapper();
        private static LactanciaMapper _lactMapper = new LactanciaMapper();
        private static ServicioMapper _servMapper = new ServicioMapper();
        private static CategoriaMapper _catMapper = new CategoriaMapper();
        private static CategConcursoMapper _catConcMapper = new CategConcursoMapper();
        private static EmpleadoMapper _empMapper = new EmpleadoMapper();
        private static TipoEventoMapper _tevMapper = new TipoEventoMapper();
        private static LugarConcursoMapper _lugConcMapper = new LugarConcursoMapper();
        private static Diag_PrenezMapper _diagMapper = new Diag_PrenezMapper();
        private static UsuarioMapper _userMapper = new UsuarioMapper();

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

        public List<VOAnimal> GetSearchAnimal(string registro)
        {
            List<VOAnimal> lst = new List<VOAnimal>();
            var a = new Animal();
            a.Registro = registro;
            List<Animal> animals = _animalMapper.GetBusqAnimal(registro, 0);
            foreach (Animal anim in animals)
            {
                var voA = CopiarVOAnimal(anim);
                lst.Add(voA);
            }
            return lst;
        }

        public Animal GetAnimalByRegistro(string registro)
        {
            var a = new Animal();
            a.Registro = registro;
            _animalMapper = new AnimalMapper(a);
            return _animalMapper.GetAnimalById();
        }

        public List<Evento> GetEventosAnimal(Animal a)
        {
            //var a = new Animal {Registro = registro};
            var registro = a.Registro;
            a.Eventos = new List<Evento>();
            var listTemp = new List<Evento>();

            /* Cargo los concursos del animal */
            var concMap = new ConcursoMapper(registro);
            listTemp = concMap.GetConcursosByRegistro(registro);
            //if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);
            if (listTemp.Count > 0)
            {
                var lstCategConcurso = _catConcMapper.GetAll();
                var lstLugConc = _lugConcMapper.GetAll();
                foreach (Concurso conc in listTemp)
                {
                    if (conc.ElPremio != null && conc.Categoria != null && conc.Categoria.Id_categ != 0)
                    {
                        CategoriaConcurso laCat =
                            lstCategConcurso.FirstOrDefault(c => c.Id_categ == conc.Categoria.Id_categ);
                        if (laCat != null) conc.Categoria.Nombre = laCat.Nombre;

                        var lugConc = lstLugConc.FirstOrDefault(lc => lc.Id == conc.NombreLugarConcurso.Id);
                        if (lugConc != null)
                        {
                            conc.NombreLugarConcurso.NombreExpo = lugConc.NombreExpo;
                            conc.NombreLugarConcurso.Lugar = lugConc.Lugar;
                        }
                    }
                }
                a.Eventos.AddRange(listTemp);
            }

            /* traigo lista de enfermedades */
            var lstEnfermedades = Fachada.Instance.GetEnfermedades();

            /* Cargo si el animal esta dado de baja por venta */
            //var ventaMap = new VentaMapper(registro);
            //listTemp = ventaMap.GetVentaByRegistro(registro);
            //if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            /* Cargo si el animal esta dado de baja por muerte */
            //var muerteMap = new MuerteMapper(registro);
            //listTemp = muerteMap.GetMuerteByRegistro(registro);

            /* Cargo baja del animal muerte */
            var bajaMap = new BajaMapper(registro);
            listTemp = bajaMap.GetBajaByRegistro(registro);

            if (listTemp.Count > 0)
            {
                for (int i = 0; i < listTemp.Count; i++)
                {
                    var baja = (Baja) listTemp[i];
                    Enfermedad enf = lstEnfermedades.FirstOrDefault(e => e.Id == baja.Enfermedad.Id);
                    baja.Enfermedad = enf;
                    a.Eventos.Add(listTemp[i]);
                }
                a.Vivo = false;
                //a.Eventos.AddRange(listTemp);
            }

            if (a.esHembra())
            {
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
                if (listTemp.Count > 0)
                {
                    var lstInseminadores = _empMapper.GetAll();
                    foreach (Servicio serv in listTemp)
                    {
                        if (serv.Inseminador != null && serv.Inseminador.Id_empleado != 0)
                        {
                            Empleado insCompleto =
                                lstInseminadores.FirstOrDefault(e => e.Id_empleado == serv.Inseminador.Id_empleado);
                            serv.Inseminador = insCompleto;
                        }
                    }
                    a.Eventos.AddRange(listTemp);
                }

                /* Cargo los diagnosticos de prenez del animal */
                var diagMap = new Diag_PrenezMapper(registro);
                listTemp = diagMap.GetDiag_PrenezByRegistro(registro);
                if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

            }

            /* Ordeno la coleccion de eventos del animal de forma descendente para mostrarlos en la ficha */
            a.Eventos.Sort();
            a.Eventos.Reverse();
            return a.Eventos;
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

        public List<VOLactancia> GetMejorProduccion305Dias(int top)
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciaMejorProduccion305(top);
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

        public List<VOLactancia> GetMejorProduccion365Dias(int top)
        {
            var lstResult = new List<VOLactancia>();
            List<Lactancia> lstLact = _lactMapper.GetLactanciaMejorProduccion365(top);
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

        public List<VOLactancia> GetLactanciaByRegistro(string registro)
        {
            var lstResult = new List<VOLactancia>();
            _lactMapper = new LactanciaMapper(registro);
            List<Lactancia> lstLact = _lactMapper.GetLactanciasByRegistro();
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

        public List<AnimalMapper.VOAnimalVitalicio> GetVitalicias()
        {
            var lstCateg = _catMapper.GetAll();
            var lstResult = _animalMapper.GetVitalicias();
            for (int i = 0; i < lstResult.Count; i++)
            {
                var tmp = lstResult[i];
                Categoria categ = lstCateg.FirstOrDefault(c => c.Id_categ == tmp.IdCategoria);
                if (categ != null)
                    tmp.Categoria = categ.ToString();

            }
            return lstResult;
        }

        public List<VOControlProd> GetControlesProduccUltimo()
        {

            var lstResult = new List<VOControlProd>();
            List<Control_Producc> lstLact = _controlProdMapper.GetControlesProduccUltimo();
            for (int i = 0; i < lstLact.Count; i++)
            {
                var tmp = lstLact[i];
                var lactMap = new LactanciaMapper(tmp.Registro);
                var numLact = lactMap.GetMaxLactanciaByRegistro();
                var diasLact = lactMap.GetDiasMaxLactanciaByRegistro();
                /* var lactUlt = lactMap.GetUltimaLactanciaByRegistro();
                if (lactUlt != null)
                {
                    numLact = lactUlt.Numero;
                    diasLact = lactUlt.Dias;
                } */

                // Armo el value object
                var voLact = new VOControlProd(tmp.Registro, numLact, diasLact,
                    tmp.Leche, tmp.Grasa, tmp.Fecha.ToShortDateString());
                voLact.FechaServicio = "-";
                voLact.FechaProbParto = "-";
                voLact.Diag = '-';
                // traigo los servicios luego del ultimo parto
                var listServDespUltParto = _servMapper.GetServiciosByRegistroDespUltParto(tmp.Registro);
                var cantServ = listServDespUltParto.Count();
                if (cantServ > 0)
                {
                    listServDespUltParto.Sort();
                    var fechaUltServicio = listServDespUltParto[cantServ - 1].Fecha;
                    voLact.FechaServicio = fechaUltServicio.ToShortDateString();
                    // traigo los diagnosticos hechos luego del ultimo servicio
                    var listDiagDespUltServicio = _diagMapper.GetDiag_PrenezByRegistroUltDespFecha(tmp.Registro,
                        fechaUltServicio);
                    var cantDiag = listDiagDespUltServicio.Count();
                    if (cantDiag > 0)
                    {
                        listDiagDespUltServicio.Sort();
                        var ultDiag = (Diag_Prenez) listDiagDespUltServicio[cantDiag - 1];
                        voLact.Diag = ultDiag.Diagnostico;
                        // si el disg en preñada sumo 285 días para obtener la fecha probable de parto
                        if (ultDiag.Diagnostico == 'P')
                            voLact.FechaProbParto = (fechaUltServicio.AddDays(285).ToShortDateString());
                    }

                }

                voLact.NumServicio = cantServ;
                lstResult.Add(voLact);
            }
            return lstResult;
        }

        public VOAnalitico GetAnaliticoVacasEnOrdene()
        {
            var voA = new VOAnalitico();
            var totalEnOrdene = _animalMapper.GetCantOrdene();
            voA.CantVacasEnOrdene = totalEnOrdene;
            voA.PromProdLecheLts = Math.Round(_lactMapper.GetPromProdLecheActual(), 2);
            var lact1 = _animalMapper.GetEnOrdeneLanctancia1();
            voA.CantLactancia1 = lact1;
            var lact2 = _animalMapper.GetEnOrdeneLanctancia2();
            voA.CantLactancia2 = lact2;
            //voA.CantLactanciaMayor2 = _animalMapper.GetEnOrdeneLanctanciaMayor2();
            voA.CantLactanciaMayor2 = totalEnOrdene - (lact1 + lact2);
            voA.ConServicioSinPreñez = _animalMapper.GetAnimalOrdeneServSinPrenez();
            voA.PrenezConfirmada = _animalMapper.GetAnimalOrdenePrenezConf();
            if (voA.CantVacasEnOrdene > 0)
            {
                voA.PorcLactancia1 = Math.Round(voA.CantLactancia1/(double) voA.CantVacasEnOrdene*100, 1);
                voA.PorcLactancia2 = Math.Round(voA.CantLactancia2/(double) voA.CantVacasEnOrdene*100, 1);
                voA.PorcLactanciaMayor2 = Math.Round(voA.CantLactanciaMayor2/(double) voA.CantVacasEnOrdene*100, 1);
                voA.PromServicioSinPrenez = Math.Round(voA.ConServicioSinPreñez/(double) voA.CantVacasEnOrdene*100, 1);
                voA.PromPrenezConfirmada = Math.Round(voA.PrenezConfirmada/(double) voA.CantVacasEnOrdene*100, 1);
            }

            voA.PromDiasLactancias = _lactMapper.GetLactanciaPromedioDiasActual();
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
            for (int i = 0; i <= 10; i++)
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
                int cantServ = dictCantServ.ContainsKey(serv.Registro) ? dictCantServ[serv.Registro] : -1;
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

        public Categoria GetCategoriaById(int idCateg)
        {
            Categoria cat = new Categoria();
            cat.Id_categ = idCateg;
            _catMapper = new CategoriaMapper(cat);
            return _catMapper.GetCategoriaById();
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

        public List<VOServicio> GetLactanciasSinServicio80()
        {
            var listemp = _empMapper.GetAll();
            var listaVoServ = new List<VOServicio>();
            var list80 = _servMapper.GetServicio80DiasLactanciaSinServicio();
            foreach (var serv in list80)
            {
                var voServ = new VOServicio
                {
                    Registro = serv.Registro,
                    FechaServicio = serv.Fecha,
                    RegistroPadre = serv.Reg_padre,
                };
                var emp = listemp.FirstOrDefault(e => e.Id_empleado == serv.Inseminador.Id_empleado);
                voServ.Inseminador = emp != null ? emp.Nombre + " " + emp.Apellido : "-";
                listaVoServ.Add(voServ);
            }
            return listaVoServ;
        }


        private List<VOServicio> CargarVOServicios(List<Servicio> listServVaqEnt, List<Animal> listAnimVaqEnt,
            Dictionary<string, int> dictCantServ)
        {
            var listaVOServ = new List<VOServicio>();
            var listemp = _empMapper.GetAll();

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
                var emp = listemp.FirstOrDefault(e => e.Id_empleado == vaca.Inseminador.Id_empleado);
                voServ.Inseminador = emp != null ? emp.Nombre + " " + emp.Apellido : "-";
                listaVOServ.Add(voServ);
            }
            return listaVOServ;
        }


        public string CalcularEdad(DateTime fechaNacimiento)
        {
            TimeSpan intervalo = DateTime.Now - fechaNacimiento;
            var intervaloAnos = (int) ((double) intervalo.Days/365.2425);
            var intervaloMeses = (int) ((double) (intervalo.Days%365.2425)/30.436875);
            return intervaloAnos.ToString() + " años, " + intervaloMeses.ToString() + " meses";
        }

        public string CalcularDiasServicio(DateTime fechaServicio)
        {
            TimeSpan intervalo = DateTime.Now - fechaServicio;
            return intervalo.Days.ToString();
        }

        public List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAll()
        {
            var controlTotal = new Controles_totalesMapper();
            return controlTotal.GetAll();
        }

        public List<Controles_totalesMapper.VOControlTotal> GetControlesTotalesEntreDosFechas(string fecha1,
            string fecha2)
        {
            var controlTotal = new Controles_totalesMapper();
            return controlTotal.GetControlesTotalesEntreDosFechas(fecha1, fecha2);
        }

        public List<Controles_totalesMapper.VOControlTotal> ControlTotalUltAnio()
        {
            var controlTotal = new Controles_totalesMapper();
            return controlTotal.ControlestUltAnio();
        }

        public List<Controles_totalesMapper.VOControlTotal> ControlesByRegistroGetAll(string reg)
        {
            var controlesMap = new Controles_totalesMapper(reg);
            return controlesMap.GetControlesProduccByRegistro();
        }

        public List<Controles_totalesMapper.VOControlTotal> ControlesByRegistroGetUltAnio(string reg)
        {
            var controlesMap = new Controles_totalesMapper(reg);
            return controlesMap.ControlesByRegistroGetUltAnio();
        }


        public List<Log> LogGetAll()
        {
            var logMap = new LogMapper();
            return logMap.GetAll();
        }

        public List<Remito> RemitosGetAll()
        {
            var remMap = new RemitoMapper();
            return remMap.GetAll();
        }

        public List<Remito> GetRemitoByEmpresa(int idEmpresa)
        {
            var remMap = new RemitoMapper();
            return remMap.GetRemitoByEmpresa(idEmpresa);
        }

        public EmpresaRemisora GetEmpresaRemisoraById(int id)
        {
            var empRem = new EmpresaRemisora(id);
            var empRemMap = new EmpresaRemisoraMapper(empRem);
            return empRemMap.GetEmpresaRemisoraById();
        }

        public List<EmpresaRemisora> GetEmpresaRemisoraActual()
        {
            var empRemMap = new EmpresaRemisoraMapper();
            return empRemMap.GetEmpresaRemisoraSelectActual();
        }

        public List<EmpresaRemisora> GetEmpresaRemisoraAll()
        {
            var empRemMap = new EmpresaRemisoraMapper();
            return empRemMap.GetAll();
        }

        public List<Calificacion> GetCalificacionesAll()
        {
            var califMap = new CalificacionMapper();
            return califMap.GetAll();
        }

        public List<CategoriaConcurso> GetCategoriasConcursoAll()
        {
            var catConcMap = new CategConcursoMapper();
            return catConcMap.GetAll();
        }

        public List<Categoria> GetCategoriasAnimalAll()
        {
            var lstResult = new List<Categoria>();
            var catMap = new CategoriaMapper();
            var lst = catMap.GetAll();
            // Retorno solo las categorías REALES
            for (int i = 0; i < 10; i++)
            {
                lstResult.Add(lst[i]);
            }
            return lstResult;
        }

        public List<Concurso> GetConcursosAll()
        {
            var concMap = new ConcursoMapper();
            var lst = concMap.GetAll();
            foreach (Concurso conc in lst)
            {
                int id = conc.NombreLugarConcurso.Id;
                conc.NombreLugarConcurso = Fachada.Instance.GetConcursoById(id);
            }
            return lst;
        }

        public LugarConcurso GetConcursoById(int id)
        {
            var conc = new LugarConcurso();
            conc.Id = id;
            var lugConcMap = new LugarConcursoMapper(conc);
            return lugConcMap.GetLugarConcursoById();
        }

        //public int GetCalificacionesMax()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCalificacionMax();
        //}

        //public int GetCantCalificacionesTotal()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCantCalificacionesTotal();
        //}

        //public double GetCalificacionProm()
        //{
        //    var califMap = new CalificacionMapper();
        //    return (double)califMap.GetCalificacionProm();
        //}

        //public int GetCantCalificacionesEx()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCantCalificacionesEx();
        //}

        //public int GetCantCalificacionesMb()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCantCalificacionesMb();
        //}

        //public int GetCantCalificacionesBm()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCantCalificacionesBm();
        //}

        //public int GetCantCalificacionesB()
        //{
        //    var califMap = new CalificacionMapper();
        //    return califMap.GetCantCalificacionesB();
        //}

        public List<EmpresaRemisora> GetEmpresasRemisorasAll()
        {
            var empRemMap = new EmpresaRemisoraMapper();
            return empRemMap.GetAll();
        }

        public VOEmpresa GetDatosCorporativos()
        {
            var empMap = new EmpresaMapper();
            Empresa emp = empMap.GetEmpresaSelectActual();
            var voEmp = new VOEmpresa(emp);
            return voEmp;
        }


        public List<Usuario> GetUsuariosAll()
        {
            var uMap = new UsuarioMapper();
            return uMap.GetAll();
        }

        //public List<VOAnimal> GetArbolGenealogico(string reg)
        //{
        //    var list = new List<VOAnimal>();
        //    var voAnimal = new VOAnimal();

        //    var a = new Animal(reg);

        //    _animalMapper = new AnimalMapper(a);
        //    var anim = _animalMapper.GetAnimalById();
        //    voAnimal = this.CopiarVOAnimal(anim);

        //    for (int i = 1; i < 3; i++)
        //    {
        //        if (anim.Reg_madre != "M-DESCONOC")
        //        {

        //            var madre = new Animal(anim.Reg_madre);
        //            _animalMapper = new AnimalMapper(madre);
        //            voAnimal.Madre = CopiarVOAnimal()
        //        }
        //        if (anim.Reg_madre != "M-DESCONOC")
        //        {

        //        }

        //    }
        //    //voPadre = this.CopiarVOAnimal(padre);
        //    //voMadre = this.CopiarVOAnimal(madre);
        //    //voAbuelaPat = this.CopiarVOAnimal(nonapat);
        //    //voAbueloPat = this.CopiarVOAnimal(nonopat);
        //}

        //private VOAnimal GetPadresAnimal(VOAnimal vo)
        //{
        //    if (vo!=null)
        //    if (vo.Registro!="M-DESCONOC") vo.Madre=GetPadresAnimal()

        //}

        public VOAnimal CopiarVOAnimal(Animal anim)
        {
            var voAnim = new VOAnimal();
            voAnim.Calific = anim.Calific;
            voAnim.Fecha_nacim = anim.Fecha_nacim;
            voAnim.Gen = anim.Gen;
            voAnim.IdCategoria = anim.IdCategoria;
            voAnim.Categoria = this.GetCategoriaById(anim.IdCategoria).ToString();
            voAnim.Identificacion = anim.Identificacion;
            voAnim.Fotos = _animalMapper.GetFotosByRegistro(anim.Registro);
            voAnim.Nombre = anim.Nombre;
            voAnim.Origen = anim.Origen;
            voAnim.Reg_madre = anim.Reg_madre;
            voAnim.Reg_padre = anim.Reg_padre;
            voAnim.Reg_trazab = anim.Reg_trazab;
            voAnim.Registro = anim.Registro;
            voAnim.Sexo = anim.Sexo;
            voAnim.Vivo = anim.Vivo;
            return voAnim;
        }


        public double[] ControlTotalLecheGetAll()
        {
            var controlTotal = new Controles_totalesMapper();
            var lista = controlTotal.GetAll();
            var listaleche = new Dictionary<string, double>();

            foreach (var a in lista)
            {
                listaleche.Add(a.Fecha.ToString(), a.Leche);
            }

            var resultArray = listaleche.Values.ToArray();

            return resultArray;
        }


        public bool CeloSinServicioInsert(Celo_Sin_Servicio celo)
        {
            var celoMapper = new Celo_Sin_ServicioMapper(celo);
            return celoMapper.Insert() > 0;
        }

        public List<TipoEvento> GetTipoEventosAnimal()
        {
            return _tevMapper.GetAll();
        }

        public List<CategoriaConcurso> GetCategoriasConcurso()
        {
            return _catConcMapper.GetAll();
        }

        public List<Empleado> GetInseminadores()
        {
            return _empMapper.GetAll();
        }

        public List<LugarConcurso> GetLugaresConcurso()
        {
            return _lugConcMapper.GetAll();
        }

        public bool InsertarEvento(Evento evento)
        {
            switch (evento.Id_evento)
            {
                case 0: // ABORTO
                    var abortoMap = new AbortoMapper((Aborto) evento);
                    return abortoMap.Insert() > 1;
                case 2: // CELO SIN SERVICIO
                    var celoMap = new Celo_Sin_ServicioMapper((Celo_Sin_Servicio) evento);
                    return celoMap.Insert() > 1;
                case 3: // SERVICIO
                    var servMap = new ServicioMapper((Servicio) evento);
                    return servMap.Insert() > 1;
                case 4: // SECADO
                    var secMap = new SecadoMapper((Secado) evento);
                    return secMap.Insert() > 1;
                case 7: // DIAGNOSTICO DE PRENEZ
                    var diagMap = new Diag_PrenezMapper((Diag_Prenez) evento);
                    return diagMap.Insert() > 1;
                case 8: // CONTROL DE PRODUCCION
                    var contMap = new Control_ProduccMapper((Control_Producc) evento);
                    return contMap.Insert() > 1;
                case 9: // CALIFICACION
                    var califMap = new CalificacionMapper((Calificacion) evento);
                    return califMap.Insert() > 1;
                case 10: // CONCURSO
                    var concursMap = new ConcursoMapper((Concurso) evento);
                    return concursMap.Insert() > 1;
                case 11: // BAJA POR VENTA
                    var bajaMap = new VentaMapper((Venta) evento);
                    return bajaMap.Insert() > 1;
                case 12: // BAJA POR MUERTE
                    var muerteMap = new MuerteMapper((Muerte) evento);
                    return muerteMap.Insert() > 1;
                default:
                    return false;
            }

        }

        public bool InsertarRemito(Remito remito)
        {
            var remitoMap = new RemitoMapper(remito);
            return remitoMap.Insert() > 0;
        }

        public List<Diag_PrenezMapper.VODiagnostico> GetInseminacionesExitosas(DateTime fecha)
        {
            var listemp = _empMapper.GetAll();
            var list = _diagMapper.GetInseminacionesExitosas(fecha);
            foreach (var diagVo in list)
            {
                var emp = listemp.FirstOrDefault(e => e.Id_empleado == diagVo.IdInseminador);
                diagVo.Inseminador = emp != null ? emp.Nombre + " " + emp.Apellido : "-";
            }
            return list;
        }

        public List<RolUsuario> GetRolesDeUsuario()
        {
            var _userMap = new UsuarioMapper();
            return _userMap.GetRolesUsuarioAll();
        }


        public List<VoListItem> GetAbortosAnimalesConServicios()
        {
            var lista = _abortoMapper.GetAbortosAnimalesConServicio();
            var listaVO = new List<VoListItem>();
            foreach (var item in lista)
            {
                var reg = item.Registro;
                var valueobj = new VoListItem {Nombre = reg};
                listaVO.Add(valueobj);
            }
            return listaVO;
        }

        public string GetAbortoServicioPadre(string reg)
        {
            return _abortoMapper.GetServicioPadreAborto(reg);
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            var userMap = new UsuarioMapper(usuario);
            return userMap.Insert() > 0;
        }

        public bool UpdateUsuario(Usuario usuario)
        {
            var userMap = new UsuarioMapper(usuario);
            return userMap.Update() > 0;
        }

        public List<Cmt> GetCmtAll()
        {
            var cmtMap = new CmtMapper();
            return cmtMap.GetAll();
        }

        public List<VOBaja> GetMuertesPorAnio(int anio)
        {
            var bajaMap = new BajaMapper();
            var lst = bajaMap.GetMuertesPorAnio(anio);
            var lstEnfermedades = Fachada.Instance.GetEnfermedades();
            var lstResult = new List<VOBaja>();
            for (int i = 0; i < lst.Count; i++)
            {
                Enfermedad enf = lstEnfermedades.FirstOrDefault(e => e.Id == lst[i].Enfermedad.Id);
                var voBaja = new VOBaja(lst[i]);
                voBaja.Enfermedad = enf;
                lstResult.Add(voBaja);
            }
            return lstResult;
        }

        public List<BajaMapper.VOEnfermedad> GetCantidadMuertesPorEnfermedadPorAnio(int anio)
        {
            var bajaMap = new BajaMapper();
            var lstResult = bajaMap.GetCantidadMuertesPorEnfermedadPorAnio(anio);
            var sum = 0;
            for (int i = 0; i < lstResult.Count; i++) sum += lstResult[i].Cantidad;
            if (sum > 0)
            {
                for (int j = 0; j < lstResult.Count; j++)
                {
                    double avg = ((double) lstResult[j].Cantidad/sum)*100;
                    lstResult[j].Porcentaje = Math.Round(avg, 2);
                }
            }
            return lstResult;
        }

        public List<VOParto> GetPartosPorAnio(int anio)
        {
            var lstResult = new List<VOParto>();
            var partoMap = new PartoMapper();
            var lst = partoMap.GetPartosByAnio(anio);
            int cant = lst.Count;
            for (int i = 0; i < cant; i++)
            {
                var voP = new VOParto();
                voP.Comentarios = lst[i].Comentarios;
                voP.Observaciones = lst[i].Observaciones;
                voP.Fecha = lst[i].Fecha;
                voP.Registro = lst[i].Registro;
                voP.Sexo_parto = lst[i].Sexo_parto;
                if (lst[i].Reg_hijo == "DESCONOCIDO")
                {
                    voP.Reg_hijo = "-";
                }
                else voP.Reg_hijo = lst[i].Reg_hijo;
                lstResult.Add(voP);
            }
            return lstResult;
        }

        public int GetCantMellizosPorAnio(int anio)
        {
            return _animalMapper.GetCantMellizosByAnio(anio);
        }

        public int GetCantTrillizosPorAnio(int anio)
        {
            return _animalMapper.GetCantTrillizosByAnio(anio);
        }

        public int GetCantNacimientosPorAnio(int anio)
        {
            return _animalMapper.GetCantNacimientosByAnio(anio);
        }


        public List<RemitoMapper.VORemitoGrafica> GetRemitosGraficas()
        {
            var remMap = new RemitoMapper();
            return remMap.GetRemitosGrafica();
        }

        public bool InsertarCasoSoporte(CasoSoporte caso)
        {
            var casoMap = new CasoSoporteMapper(caso);
            return casoMap.Insert() > 0;
        }

        public bool EnviarCasoSoporte(CasoSoporte caso)
        {
            //envia por mail el caso de soporte a la casilla definida para eso

            // inserto en la base de datos para dejar el registro
            return InsertarCasoSoporte(caso);
        }

        public List<VOToroUtilizado> GetTorosUtilizadosPorAnio(int anio)
        {
            var lstToros = new List<VOToroUtilizado>();
            var lst = _servMapper.GetServDiagPorToroUtilizado(anio);
            bool esta = false;
            // Recorro la lista para consolidar los servicios y diag por toro
            for (int i = 0; i < lst.Count; i++)
            {
                esta = false;
                for (int j = 0; j < lstToros.Count; j++)
                {
                    if (lst[i].RegPadre.Equals(lstToros[j].Registro))
                    {
                        lstToros[j].CantServicios++;
                        if (lst[i].Diagnostico == 'P') lstToros[j].CantDiagP++;
                        if (lstToros[j].CantServicios > 0)
                        {
                            lstToros[j].PorcEfectividad =
                                Math.Round((double) lstToros[j].CantDiagP/lstToros[j].CantServicios*100, 1);
                        }
                        esta = true;
                        break;
                    }
                }
                if (!esta)
                {
                    Animal toro = GetAnimalByRegistro(lst[i].RegPadre);
                    var voToro = new VOToroUtilizado
                    {
                        Registro = lst[i].RegPadre,
                        Nombre = toro.Nombre,
                        Origen = toro.Origen,
                        CantServicios = 1,
                        CantDiagP = lst[i].Diagnostico == 'P' ? 1 : 0,
                        CantNacim = 0,
                        CantH = 0,
                        CantM = 0,
                        //PorcHembras = 0,
                    };
                    voToro.PorcEfectividad = Math.Round((double) voToro.CantDiagP/voToro.CantServicios*100, 1);
                    lstToros.Add(voToro);
                }

            }
            lstToros.Sort();
            return lstToros;
        }

        public List<AnimalMapper.VOToro> GetTorosNacimPorGenero(int anio)
        {
            var lst = _animalMapper.GetNacimientosPorToroByAnio(anio);
            foreach (AnimalMapper.VOToro vT in lst)
            {
                vT.CantH = _animalMapper.GetCantNacimientosHPorToroByAnio(vT.Registro, anio);
                vT.CantM = vT.CantNacim - vT.CantH;
                vT.PorcHembras = Math.Round((double) vT.CantH/vT.CantNacim*100, 1);
            }
            return lst;
        }

        public VOUsuario AskForLogin(string nick, string password)
        {
            var user = _userMapper.GetUsuarioAtLogin(nick, password);
            var lstRoles = this.GetRolesDeUsuario();
            if (user != null)
            {
                var nivel = 0;
                RolUsuario elRol = lstRoles.FirstOrDefault(c => c.NombreRol == user.Rol.NombreRol);
                //if (elRol != null) nivel = elRol.Nivel;

                var voUser = new VOUsuario
                {
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    Foto = user.Foto,
                    Habilitado = user.Habilitado,
                    Nickname = user.Nickname,
                    Rol = elRol,
                };
                return voUser;
            }
            return null;

        }

        public VOUsuario GetDatosUsuario(string nick)
        {
            var u = new Usuario();
            u.Nickname = nick;
            _userMapper = new UsuarioMapper(u);
            var user = _userMapper.GetUsuarioById();
            var lstRoles = this.GetRolesDeUsuario();
            if (user != null)
            {
                var nivel = 0;
                RolUsuario elRol = lstRoles.FirstOrDefault(c => c.NombreRol == user.Rol.NombreRol);
                var voUser = new VOUsuario
                {
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    Foto = user.Foto,
                    Habilitado = user.Habilitado,
                    Nickname = user.Nickname,
                    Rol = elRol,
                };
                return voUser;
            }
            return null;
        }

        public bool Logoff(string user)
        {
            return _userMapper.LogoffUsuario(user) == 0;
        }

        public List<RemitoMapper.VORemitoGrafica> GetRemitosEntreDosFechas(string fecha1, string fecha2)
        {
            var remMap = new RemitoMapper();
            return remMap.GetRemitosEntreDosFechas(fecha1, fecha2);
        }

        public VOIndicadores GetIndicadoresTablero()
        {
            var indTablero = new VOIndicadores();

            var _controlTotal = new Controles_totalesMapper();
            var lstUltXControles = _controlTotal.GetControlesLastXTotalesLast(9);

            // VACAS EN ORDEÑE
            indTablero.VacasEnOrdene = new VoListItemInd("vacas en ordeñe");
            var totalEnOrdene = _animalMapper.GetCantOrdene();
            indTablero.VacasEnOrdene.Valor = totalEnOrdene.ToString();
            // Valores de la gráfica pequeña
            var cAnterior = 0.0;
            var cActual = 0.0;
            var porc = 0.0;
            var strValuesGraficaChicaVacasOrdene = "";
            if (lstUltXControles.Count > 0)
            {
                for (int i = lstUltXControles.Count - 1; i >= 0; i--)
                {
                    strValuesGraficaChicaVacasOrdene += lstUltXControles[i].Vacas.ToString();
                    if (i != 0) strValuesGraficaChicaVacasOrdene += ",";
                }

                //if (lstUltXControles.Count > 1)
                //{
                //    cAnterior = lstUltXControles[lstUltXControles.Count - 1].Leche;
                //    cActual = lstUltXControles[lstUltXControles.Count - 2].Leche;
                //    porc = Math.Round(cActual * 100 / cAnterior, 1);
                //}
                //var strPre = "-";
                //indTablero.VacasEnOrdene.Status = "important";
                //if (porc > 0)
                //{
                //    strPre = "+";
                //    indTablero.VacasEnOrdene.Status = "success";
                //}
                //indTablero.VacasEnOrdene.Porcentaje = strPre + porc.ToString() + "%";
            }
            indTablero.VacasEnOrdene.DataValue = strValuesGraficaChicaVacasOrdene;

            // PROMEDIO LECHE EN ULTIMO CONTROL
            var promLecheUltCtl = Math.Round(GetPromLecheUltControl(), 1);
            indTablero.PromLeche = new VoListItemInd("promedio leche");
            indTablero.PromLeche.Valor = promLecheUltCtl.ToString();

            // LECHE ULTIMO CONTROL
            indTablero.LecheUltControl = new VoListItemInd("leche último control");
            if (lstUltXControles.Count > 0)
            {
                indTablero.LecheUltControl.Valor = Math.Truncate(lstUltXControles[0].Leche).ToString();
            }
            // Valores de la gráfica pequeña
            //var strValuesGraficaChica = "";
            //var ctlAnterior = 0.0;
            //var ctlActual = 0.0;
            //var porc = 0.0;
            //if (lstUltXControles.Count > 0)
            //{
            //    indTablero.LecheUltControl.Valor = Math.Truncate(lstUltXControles[0].Leche).ToString();
            //    for (int i = lstUltXControles.Count - 1; i >= 0; i--)
            //    {
            //        var valorTrunc = Math.Truncate(lstUltXControles[i].Leche);
            //        strValuesGraficaChica += valorTrunc.ToString();
            //        if (i != 0) strValuesGraficaChica += ",";
            //    }
            //    //if (lstUltXControles.Count > 1)
            //    //{
            //    //    ctlAnterior = lstUltXControles[lstUltXControles.Count - 1].Leche;
            //    //    ctlActual = lstUltXControles[lstUltXControles.Count - 2].Leche;
            //    //    porc = Math.Round(ctlActual * 100 / ctlAnterior, 1);
            //    //}
            //}
            //indTablero.LecheUltControl.Porcentaje = porc.ToString();
            //if (porc > 0) indTablero.LecheUltControl.Status = "success";
            //else indTablero.LecheUltControl.Status = "important";
            //indTablero.LecheUltControl.DataValue = strValuesGraficaChica;

            // PROMEDIO DE DIAS DE LACTANCIA
            var promDiasLactancias = _lactMapper.GetLactanciaPromedioDiasActual();
            indTablero.PromDiasLactancias = new VoListItemInd("lactancia promedio");
            indTablero.PromDiasLactancias.Valor = promDiasLactancias.ToString();

            // PARTOS PARA ESTE MES
            indTablero.PartosMes = new VoListItemInd("partos este mes");
            //var hoy = DateTime.Today;
            var hoy = new DateTime(2014, 09, 01); // TESTING --------------------------
            var mesAnt = hoy.AddMonths(-1);
            var partosMes = this.GetPartosByMesAnio(hoy);
            var partosMesAnt = this.GetPartosByMesAnio(mesAnt);
            indTablero.PartosMes.Valor = partosMes.ToString();
            if (partosMes > partosMesAnt) indTablero.PartosMes.Status = "success";
            if (partosMes < partosMesAnt) indTablero.PartosMes.Status = "important";
            indTablero.PartosMes.Porcentaje = partosMesAnt.ToString();

            // ABORTOS ESTE AÑO
            indTablero.AbortosAnual = new VoListItemInd("abortos este año");
            var anioAnt = hoy.AddYears(-1);
            var abortosAnio = this.GetAbortosByAnio(hoy);
            var abortosAnioAnt = this.GetAbortosByAnio(anioAnt);
            indTablero.AbortosAnual.Valor = abortosAnio.ToString();
            if (abortosAnio > abortosAnioAnt) indTablero.AbortosAnual.Status = "important";
            if (abortosAnio < abortosAnioAnt) indTablero.AbortosAnual.Status = "success";
            indTablero.AbortosAnual.Porcentaje = abortosAnioAnt.ToString();

            // NACIMIENTOS ESTE AÑO
            indTablero.NacidosAnual = new VoListItemInd("Nacidos");
            var nacimAnio = this.GetCantNacimientosPorAnio(2014); // TESTING --------------------------
            //var nacimAnio = this.GetCantNacimientosPorAnio(hoy.Year);
            indTablero.NacidosAnual.Valor = nacimAnio.ToString();

            // PREÑADAS
            var fecha = new DateTime(2014, 09, 01);
            var cantPrenadas = GetInseminacionesExitosas(fecha).Count;
            //var cantPrenadas = GetInseminacionesExitosas(hoy).Count;
            indTablero.Prenadas = new VoListItemInd("Preñadas");
            indTablero.Prenadas.Valor = cantPrenadas.ToString();

            // TORO MAS USADO
            var nomToro = "Sin insem.";
            var efect = 0.0;
            var lst = GetTorosUtilizadosPorAnio(2014); // TESTING --------------------------
            //var lst = GetTorosUtilizadosPorAnio(hoy.Year);
            if (lst.Count > 0)
            {
                var lstConsolidada = GetTopUtilizados(lst);
                nomToro = (lstConsolidada[0].Registro).ToLower();
                nomToro = char.ToUpper(nomToro[0]) + nomToro.Substring(1);
                efect = lstConsolidada[0].PorcEfectividad;
            }
            indTablero.ToroMasUsado = new VoListItemInd("% efectividad");
            indTablero.ToroMasUsado.Texto = nomToro;
            indTablero.ToroMasUsado.Valor = "efectividad";
            indTablero.ToroMasUsado.Porcentaje = efect.ToString() + "%";

            return indTablero;
        }

        public List<VOToroUtilizado1> GetTopUtilizados(List<VOToroUtilizado> lst)
        {
            var lstOrderByCantServ = new List<VOToroUtilizado1>();
            for (int i = 0; i < lst.Count; i++)
            {
                var item = new VOToroUtilizado1(lst[i]);
                lstOrderByCantServ.Add(item);
            }
            lstOrderByCantServ.Sort();
            return lstOrderByCantServ;
        }


        public int GetPartosByMesAnio(DateTime fecha)
        {
            var _partoMap = new PartoMapper();
            return _partoMap.GetPartosByMesAnio(fecha);
        }

        public int GetAbortosByAnio(DateTime fecha)
        {
            return _abortoMapper.GetAbortosByAnio(fecha);
        }

        public List<VoListItemInd> GetAlertasYNotificacionesTablero()
        {
            var lstAlertas = new List<VoListItemInd>();

            // ALERTA DIAG 35 DIAS DE SERVICIO Y SIN DIAGNOSTICO
            var alertDiag35 = new VoListItemInd("vacas con 35 días de servicio y sin diagnóstico ecográfico!");
            alertDiag35.Link = "../DiagEcograficos.aspx";
            alertDiag35.LinkAlt = "Diagnósticos ecográficos";
            alertDiag35.Icono = "fa-bell-o";
            //var cant35 = 0;   // TESTING -----------------------------
            var cant35 = this.GetServicios35SinDiagPrenezVacOrdene().Count +
                         this.GetServicios35SinDiagPrenezVacSecas().Count +
                         this.GetServicios35SinDiagPrenezVaqEnt().Count;
            alertDiag35.Valor = cant35.ToString();
            alertDiag35.Status = "success";
            if (cant35 == 0)
            {
                alertDiag35.Valor = "";
                alertDiag35.Texto = "No hay vacas con diagnóstico ecográfico pendiente!";

            }
            else if (cant35 <= 10) alertDiag35.Status = "info";
            else if (cant35 <= 20) alertDiag35.Status = "warning";
            else
            {
                alertDiag35.Status = "danger";
                alertDiag35.Icono = "fa-bomb";
            }
            lstAlertas.Add(alertDiag35);


            // ALERTA DIAG 70 DIAS DE SERVICIO Y SIN DIAGNOSTICO
            var alertDiag70 = new VoListItemInd("vacas con 70 días de servicio y sin diagnóstico!");
            alertDiag70.Link = "../ServiciosSinDiag.aspx";
            alertDiag70.LinkAlt = "Servicios sin diagnóstico de preñez";
            alertDiag70.Icono = "icon-animated-bell fa-bell-o";
            //var cant70 = 37;   // TESTING -----------------------------
            var cant70 = this.GetServicios70SinDiagPrenezVacOrdene().Count +
                         this.GetServicios70SinDiagPrenezVacSecas().Count +
                         this.GetServicios70SinDiagPrenezVaqEnt().Count;
            alertDiag70.Valor = cant70.ToString();
            alertDiag70.Status = "success";
            if (cant70 == 0)
            {
                alertDiag70.Valor = "";
                alertDiag70.Texto = "No hay vacas con 70 días de servicio sin diagnósticar!";

            }
            else if (cant70 <= 20) alertDiag70.Status = "info";
            else if (cant70 <= 40) alertDiag70.Status = "warning";
            else
            {
                alertDiag70.Status = "danger";
                alertDiag70.Icono = "fa-bomb";
            }
            lstAlertas.Add(alertDiag70);


            // ALERTA EN LACTANCIA 80 DIAS Y SIN SERVICIO
            var alertLact80 = new VoListItemInd("vacas con 80 días o más en lactancia y sin servicio!");
            alertLact80.Link = "../LactanciasSinServ80.aspx";
            alertLact80.LinkAlt = "Animales con más de 80 días en lactancia sin servicio";
            alertLact80.Icono = "fa-bell-o";
            var cant80 = this.GetLactanciasSinServicio80().Count;
            alertLact80.Valor = cant80.ToString();
            alertLact80.Status = "success";
            if (cant80 == 0)
            {
                alertLact80.Valor = "";
                alertLact80.Texto = "No hay vacas con 80 días o más en lactancia sin servicio!";

            }
            else if (cant80 <= 25) alertLact80.Status = "info";
            else if (cant80 <= 50) alertLact80.Status = "warning";
            else
            {
                alertLact80.Status = "danger";
                alertLact80.Icono = "fa-bomb";
            }
            lstAlertas.Add(alertLact80);

            return lstAlertas;
        }

        public List<VoListItemInd> GetExtrasGraficaCategorías()
        {
            var lstExtras = new List<VoListItemInd>();

            var extraSecas = new VoListItemInd("Vitalicias");
            var lstVitalicias = this.GetVitalicias();
            extraSecas.Valor = lstVitalicias.Count.ToString();
            extraSecas.Icono = "fa-star-o";
            extraSecas.Color = "orange";
            extraSecas.Link = "../ListVitalicias.aspx";
            extraSecas.LinkAlt = "Lista de vitalicias";
            lstExtras.Add(extraSecas);

            //int anio = DateTime.Today.Year;
            int anio = 2014; // TESTING -----------------------------
            var extraMuertes = new VoListItemInd("Muertes");
            extraMuertes.Valor = this.GetCantMuertesPorAnio(anio).ToString();
            extraMuertes.Icono = "fa-stethoscope";
            extraMuertes.Color = "red";
            extraMuertes.Link = "../AnalisisMuertes.aspx";
            extraMuertes.LinkAlt = "Análisis de Muertes";
            lstExtras.Add(extraMuertes);

            var extraVentas = new VoListItemInd("Ventas");
            extraVentas.Valor = this.GetCantVentasPorAnio(anio).ToString();
            extraVentas.Icono = "fa-money";
            extraVentas.Color = "green";
            extraVentas.Link = "../AnalisisVentas.aspx";
            extraVentas.LinkAlt = "Análisis de Ventas";
            lstExtras.Add(extraVentas);

            return lstExtras;
        }

        public int GetCantMuertesPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantMuertesPorAnio(anio);
        }

        public int GetCantVentasPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasPorAnio(anio);
        }

        public int GetCantVentasAFrigPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasAFrigPorAnio(anio);
        }

        public int GetCantVentasRecienNacidosPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasRecienNacidosPorAnio(anio);
        }

        public int GetCantVentasViejasPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasViejasPorAnio(anio);
        }

        public bool UpdateDatosCorporativos(VOEmpresa voEmp)
        {
            var empresa = new Empresa
            {
                Id = voEmp.Id,
                Nombre = voEmp.Nombre,
                RazonSocial = voEmp.RazonSocial,
                Rut = voEmp.Rut,
                LetraSistema = voEmp.LetraSistema,
                Direccion = voEmp.Direccion,
                Ciudad = voEmp.Ciudad,
                Telefono = voEmp.Telefono,
                Celular = voEmp.Celular,
                Web = voEmp.Web,
                Cpostal = voEmp.Cpostal,
                Logo = voEmp.Logo,
                LogoCh = voEmp.LogoCh,
                Actual = voEmp.Actual ? 'S' : 'N'
            };

            var empMapper = new EmpresaMapper(empresa);
            return empMapper.Update() > 0;
        }

        public bool EnfermedadInsert(Enfermedad enfermedad)
        {
            var enfMapper = new EnfermedadMapper();
            var lastId = enfMapper.GetLastIdEnfermedad();
            enfermedad.Id = lastId + 1;
            enfMapper = new EnfermedadMapper(enfermedad);
            return enfMapper.Insert() > 0;
        }

        public bool CategConcursoInsert(CategoriaConcurso categ)
        {
            var catConcMap = new CategConcursoMapper(categ);
            return catConcMap.Insert() > 0;
        }

        public bool InsertarEmpleado(Empleado empleado)
        {
            var empMap = new EmpleadoMapper(empleado);
            return empMap.Insert() > 0;
        }

        public VOAnimal GetArbolGenealogico(VOAnimal voA)
        {
            //var voA = new VOAnimal();
            if (voA != null)
            {
                //voA = CopiarVOAnimal(animal);
                voA.Vivo = !EstaMuertoAnimal(voA.Registro);
                voA.Vendido = FueVendidoAnimal(voA.Registro);
                if (voA.esHembra())
                {
                    _lactMapper = new LactanciaMapper(voA.Registro);
                    voA.Lactancias = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());
                }
                voA.Concursos = this.GetConcursosAnimal(voA.Registro);
                // ARBOL POR PARTE DE MADRE
                if (voA.Reg_madre != "H-DESCONOC")
                {
                    voA.Madre = CopiarVOAnimal(GetAnimalByRegistro(voA.Reg_madre));
                    voA.Madre.Vivo = !EstaMuertoAnimal(voA.Reg_madre);
                    voA.Madre.Vendido = FueVendidoAnimal(voA.Reg_madre);
                    _lactMapper = new LactanciaMapper(voA.Reg_madre);
                    voA.Madre.Lactancias = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());

                    if (voA.Madre != null && voA.Madre.Registro != "H-DESCONOC")
                    {
                        string strAbuelaM = voA.Madre.Reg_madre;
                        string strAbueloM = voA.Madre.Reg_padre;
                        if (strAbuelaM != "H-DESCONOC")
                        {
                            voA.Madre.Madre = CopiarVOAnimal(GetAnimalByRegistro(strAbuelaM));
                            voA.Madre.Madre.Vivo = !EstaMuertoAnimal(strAbuelaM);
                            voA.Madre.Madre.Vendido = FueVendidoAnimal(strAbuelaM);
                            _lactMapper = new LactanciaMapper(strAbuelaM);
                            voA.Madre.Madre.Lactancias = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());
                        }
                        if (strAbueloM != "M-DESCONOC")
                        {
                            voA.Madre.Padre = CopiarVOAnimal(GetAnimalByRegistro(strAbueloM));
                            voA.Madre.Padre.Vivo = !EstaMuertoAnimal(strAbueloM);
                            voA.Madre.Padre.Vendido = FueVendidoAnimal(strAbueloM);
                        }
                    }
                }
                // ARBOL POR PARTE DE PADRE
                if (voA.Reg_padre != "M-DESCONOC")
                {
                    voA.Padre = CopiarVOAnimal(GetAnimalByRegistro(voA.Reg_padre));
                    voA.Padre.Vivo = !EstaMuertoAnimal(voA.Reg_padre);
                    voA.Padre.Vendido = FueVendidoAnimal(voA.Reg_padre);
                    if (voA.Padre != null && voA.Padre.Registro != "M-DESCONOC")
                    {
                        string strAbuelaP = voA.Padre.Reg_madre;
                        string strAbueloP = voA.Padre.Reg_padre;
                        if (strAbuelaP != "H-DESCONOC")
                        {
                            voA.Padre.Madre = CopiarVOAnimal(GetAnimalByRegistro(strAbuelaP));
                            voA.Padre.Madre.Vivo = !EstaMuertoAnimal(strAbuelaP);
                            voA.Padre.Madre.Vendido = FueVendidoAnimal(strAbuelaP);
                            _lactMapper = new LactanciaMapper(strAbuelaP);
                            voA.Padre.Madre.Lactancias = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());
                        }
                        if (strAbueloP != "M-DESCONOC")
                        {
                            voA.Padre.Padre = CopiarVOAnimal(GetAnimalByRegistro(strAbueloP));
                            voA.Padre.Padre.Vivo = !EstaMuertoAnimal(strAbueloP);
                            voA.Padre.Padre.Vendido = FueVendidoAnimal(strAbueloP);
                        }
                    }
                }

            }
            return voA;
        }

        public List<VOLactancia> CopiarVOLactanciaList(List<Lactancia> lst)
        {
            var lstResult = new List<VOLactancia>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] != null)
                {
                    var voLact = new VOLactancia(lst[i]);
                    lstResult.Add(voLact);
                }
            }
            return lstResult;
        }

        public List<VOConcurso> GetConcursosAnimal(string registro)
        {
            var lst = new List<VOConcurso>();
            if (registro != "")
            {
                var lstCategConcurso = _catConcMapper.GetAll();
                var concMap = new ConcursoMapper(registro);
                var lstConc = concMap.GetConcursosByRegistro(registro);

                for (int i = 0; i < lstConc.Count; i++)
                {
                    var item = (Concurso)lstConc[i];
                    var conc = this.GetConcursoById(item.NombreLugarConcurso.Id);
                    var voConc = new VOConcurso
                    {
                        Registro = item.Registro,
                        Fecha = item.Fecha.ToShortDateString(),
                        Lugar = conc.Lugar,
                        NombreExpo = conc.NombreExpo,
                        //CategConcurso = laCat.Nombre,
                        ElPremio = item.ElPremio,
                        Comentarios = item.Comentarios
                    };
                    var laCat = lstCategConcurso.FirstOrDefault(c => c.Id_categ == item.Categoria.Id_categ);
                    if (laCat != null) voConc.CategConcurso = laCat.Nombre;

                    lst.Add(voConc);
                }
            }
            return lst;
        }

        public List<VOInseminadorRank> GetRankingInseminadores(int anio)
        {
            var lstResult = new List<VOInseminadorRank>();
            var listEmp = _empMapper.GetAll();
            var esta = false;

            var lst = _diagMapper.GetTrabajoInseminadores(anio);

            // Recorro la lista para consolidar los resultados por inseminador
            for (int i = 0; i < lst.Count; i++)
            {
                esta = false;
                for (int j = 0; j < lstResult.Count; j++)
                {
                    if (lst[i].IdInseminador.Equals(lstResult[j].IdEmpleado))
                    {
                        lstResult[j].CantServicios++;
                        if (lst[i].Diagnostico == 'P') lstResult[j].CantPrenadas++;
                        if (lstResult[j].CantServicios > 0)
                        {
                            lstResult[j].PorcEfectividad =
                                Math.Round((double)lstResult[j].CantPrenadas / lstResult[j].CantServicios * 100, 1);
                        }
                        esta = true;
                        break;
                    }
                }
                if (!esta)
                {
                    var emp = listEmp.FirstOrDefault(e => e.Id_empleado == lst[i].IdInseminador);
                    if (emp != null)
                    {
                        var voInsem = new VOInseminadorRank()
                        {
                            Apellido = emp.Apellido,
                            CantPrenadas = lst[i].CantPrenadas,
                            CantServicios = lst[i].CantServicios,
                            IdEmpleado = lst[i].IdInseminador,
                            Inciales = emp.Iniciales,
                            Nombre = emp.Nombre
                        };
                        voInsem.NombreCompleto = emp.Nombre + " " + emp.Apellido + " (" + emp.Iniciales + ")";
                        if (lst[i].CantServicios > 0)
                        {
                            voInsem.PorcEfectividad = Math.Round((double)lst[i].CantPrenadas / lst[i].CantServicios * 100, 1);
                        }
                        lstResult.Add(voInsem);
                    }
                }

            }
            lstResult.Sort();
            return lstResult;
        }


        public List<DateTime> GetFechasDiagnosticoPorAnio(int anio)
        {
            return _diagMapper.GetFechasDiagnosticoPorAnio(anio);
            
        }

        public List<VOEmpleado> GetAllEmpleados()
        {
            var lstResult = new List<VOEmpleado>();

            var emp = new EmpleadoMapper();
            List<Empleado> lst = emp.GetAll();
            foreach (Empleado empleado in lst)
            {
                var voEmp = new VOEmpleado(empleado);
                lstResult.Add(voEmp);
            }
            return lstResult;
        }

        public List<VOEmpleado> GetEmpleadosActivos()
        {
            var lstResult = new List<VOEmpleado>();

            var emp = new EmpleadoMapper();
            List<Empleado> lst = emp.GetAll();
            foreach (Empleado empleado in lst)
            {
                if (empleado.Activo)
                {
                    var voEmp = new VOEmpleado(empleado);
                    lstResult.Add(voEmp);
                }
            }
            return lstResult;
        }
    }
}
