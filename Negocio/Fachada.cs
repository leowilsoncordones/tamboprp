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
        private static CategoriaMapper _catMapper = new CategoriaMapper();
        private static CategConcursoMapper _catConcMapper = new CategConcursoMapper();
        private static EmpleadoMapper _empMapper = new EmpleadoMapper();
        private static TipoEventoMapper _tevMapper = new TipoEventoMapper();
        private static LugarConcursoMapper _lugConcMapper = new LugarConcursoMapper();

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
            List<Animal> animals = _animalMapper.GetBusqAnimal(registro, 0);
            return animals;
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
                        CategoriaConcurso laCat = lstCategConcurso.FirstOrDefault(c => c.Id_categ == conc.Categoria.Id_categ);
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
                            Empleado insCompleto = lstInseminadores.FirstOrDefault(e => e.Id_empleado == serv.Inseminador.Id_empleado);
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
            voA.CantLactancia1 = _animalMapper.GetEnOrdeneLanctancia1();
            voA.CantLactancia2 = _animalMapper.GetEnOrdeneLanctancia2();
            //voA.CantLactanciaMayor2 = _animalMapper.GetEnOrdeneLanctanciaMayor2();
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
                voServ.Inseminador = emp != null ? emp.Nombre + " " + emp.Apellido : "sin registro";
                listaVoServ.Add(voServ);
            }
            return listaVoServ;
        }


        private List<VOServicio> CargarVOServicios(List<Servicio> listServVaqEnt, List<Animal> listAnimVaqEnt, Dictionary<string, int> dictCantServ)
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
                voServ.Inseminador = emp != null ? emp.Nombre + " " + emp.Apellido : "sin registro";
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

        public List<Controles_totalesMapper.VOControlTotal> ControlTotalGetAll()
        {
            var controlTotal = new Controles_totalesMapper();
            return controlTotal.GetAll();
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
            var catMap = new CategoriaMapper();
            return catMap.GetAll();
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
            voAnim.Identificacion = anim.Identificacion;
            voAnim.Fotos = _animalMapper.GetFotosByRegistro(anim.Registro);
            voAnim.Nombre = anim.Nombre;
            voAnim.Origen = anim.Origen;
            voAnim.Reg_madre = anim.Reg_madre;
            voAnim.Reg_padre = anim.Reg_padre;
            voAnim.Reg_trazab = anim.Origen;
            voAnim.Registro = anim.Registro;
            voAnim.Sexo = anim.Sexo;
            voAnim.Vivo = anim.Vivo;
            return voAnim;
        }


        public double[] ControlTotalLecheGetAll()
        {
            var controlTotal = new Controles_totalesMapper();
            var lista =  controlTotal.GetAll();
            var listaleche = new Dictionary<string, double>();

            foreach (var a in lista)
            {
                listaleche.Add(a.Fecha.ToString(),a.Leche);
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
                    var abortoMap = new AbortoMapper((Aborto)evento);
                    return abortoMap.Insert() > 1;
                case 2: // CELO SIN SERVICIO
                    var celoMap = new Celo_Sin_ServicioMapper((Celo_Sin_Servicio)evento);
                    return celoMap.Insert() > 1;
                case 3: // SERVICIO
                    var servMap = new ServicioMapper((Servicio)evento);
                    return servMap.Insert() > 1;
                case 4: // SECADO
                    var secMap = new SecadoMapper((Secado)evento);
                    return secMap.Insert() > 1;
                case 7: // DIAGNOSTICO DE PRENEZ
                    var diagMap = new Diag_PrenezMapper((Diag_Prenez)evento);
                    return diagMap.Insert() > 1;
                case 8: // CONTROL DE PRODUCCION
                    var contMap = new Control_ProduccMapper((Control_Producc)evento);
                    return contMap.Insert() > 1;
                case 9: // CALIFICACION
                    var califMap = new CalificacionMapper((Calificacion)evento);
                    return califMap.Insert() > 1;
                case 10: // CONCURSO
                    var concursMap = new ConcursoMapper((Concurso)evento);
                    return concursMap.Insert() > 1;
                case 11: // BAJA POR VENTA
                    var bajaMap = new VentaMapper((Venta)evento);
                    return bajaMap.Insert() > 1;
                case 12: // BAJA POR MUERTE
                    var muerteMap = new MuerteMapper((Muerte)evento);
                    return muerteMap.Insert() > 1;
                default:
                    return false;
            }

        }
    }
}
