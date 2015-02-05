using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web.ModelBinding;
using System.Web.Profile;
using Datos;
using Entidades;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;


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
                    // paso fecha a formato español
                    //string strDate = conc.Fecha.ToString();
                    //if (strDate != string.Empty) conc.Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));

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
                    // paso fecha a formato español
                    //string strDate = listTemp[i].Fecha.ToString();
                    //if (strDate != string.Empty) listTemp[i].Fecha = DateTime.Parse(strDate, new CultureInfo("fr-FR"));

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
                var lactancias = GetLactanciaByRegistro(registro);
                var secMap = new SecadoMapper(registro);
                listTemp = secMap.GetSecadosByRegistro(registro);
                if (listTemp.Count > 0)
                {
                    for (int i = 0; i < listTemp.Count; i++)
                    {
                        var sec = (Secado)listTemp[i];
                        switch (sec.IdMotivoSecado)
                        {
                            case 1:
                                sec.MotivoSecado = "LACTANCIA COMPLETA";
                                break;
                            case 2:
                                sec.MotivoSecado = "RAZONES SANITARIAS";
                                Enfermedad enf = lstEnfermedades.FirstOrDefault(e => e.Id == sec.Enfermedad);
                                if (enf != null) sec.EnfermedadNombre = enf.Nombre_enfermedad;
                                break;
                            case 3:
                                sec.MotivoSecado = "BAJA PRODUCCION";
                                break;
                            case 4:
                                sec.MotivoSecado = "PREÑEZ AVANZADA";
                                break;
                        }
                        VOLactancia lact = lactancias.FirstOrDefault(l => l.Numero == i+1);
                        if (lact != null) sec.LiquidacionLact = lact.Dias + "d " + lact.ProdLeche + "kg " + lact.ProdGrasa+ "kg";
                    }
                    a.Eventos.AddRange(listTemp);
                }
                //if (listTemp.Count > 0) a.Eventos.AddRange(listTemp);

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
            // FORMA ANTERIOR DE TRAER LAS LACTANCIAS ACTUALES
            //var lstResult = new List<VOLactancia>();
            //List<Lactancia> lstLact = _lactMapper.GetLactanciaActualCategoriaVacaOrdene();
            //for (int i = 0; i < lstLact.Count; i++)
            //{
            //    var tmp = lstLact[i];
            //    var voLact = new VOLactancia(tmp.Registro, tmp.Numero, tmp.Dias,
            //        tmp.Leche305, tmp.Grasa305, tmp.Leche365,
            //        tmp.Grasa365, tmp.ProdLeche, tmp.ProdGrasa);
            //    lstResult.Add(voLact);
            //}
            //return lstResult;

            //FORMA CON CALCULO DE LAS ACTUALES
            var lstResult = new List<VOLactancia>();
            var listRegs = _animalMapper.AnimalCategGetRegistrosAllOrdene();
            if (listRegs != null)
                foreach (var reg in listRegs)
                {
                    var lact = ConsolidarLactancia(reg);
                    var voLact = new VOLactancia(lact);
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
                //Categoria catTemp = this.GetCategoriaByRegistro(tmp.Registro);
                Categoria categ = lstCateg.FirstOrDefault(c => c.Id_categ == tmp.IdCategoria);
                if (categ != null)
                    tmp.Categoria = categ.ToString();

            }
            return lstResult;
        }

        public List<VOControlProd> GetControlesProduccUltimo()
        {
            try
            {
                var lstResult = new List<VOControlProd>();
                List<Control_Producc> lstLact = _controlProdMapper.GetControlesProduccUltimo();
                for (int i = 0; i < lstLact.Count; i++)
                {
                    var tmp = lstLact[i];
                    var numLact = 0;
                    var diasLact = 0;
                    var lactMap = new LactanciaMapper(tmp.Registro);
                    if (lactMap.LactancialExiste())
                    {
                        // AHORA SE CALCULA LA LACTANCIA ACTUAL, NO ESTA MAS EN LA TABLA DE LACTANCIAS
                        numLact = lactMap.GetMaxLactanciaByRegistro();
                        diasLact = lactMap.GetDiasMaxLactanciaByRegistro();
                    }

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
                    voLact.ProdLeche = this.GetProdLecheLactanciaAnimalActual(tmp.Registro);
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
            catch (Exception ex)
            {
                return new List<VOControlProd>();
            }
            
        }

        public double GetProdLecheLactanciaAnimalActual(string registro)
        {
            try
            {
                _controlProdMapper = new Control_ProduccMapper(registro);
                return _controlProdMapper.GetProdLecheControlesProduccDeLactanciaActual();
            }
            catch (Exception ex)
            {
                return 0;
            }
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
            //voA.PrenezConfirmada = _animalMapper.GetAnimalOrdenePrenezConf();
            voA.PrenezConfirmada = GetAnimalOrdenePrenezConf();
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


        public int GetAnimalOrdenePrenezConf()
        {
            return _animalMapper.GetAnimalOrdenePrenezConf();
        }

        public VODatosGenerales GetDatosGenerales()
        {
            var voA = new VODatosGenerales();
            voA.CantAbortosEsteAnio = this.GetCantAbortosEsteAnio();
            voA.CantAnimUltControl = this.GetCantAnimalesUltControl();
            voA.SumLecheUltControl = this.GetSumLecheUltControl();
            voA.PromLecheUltControl = Math.Round(this.GetPromLecheUltControl(), 2);
            voA.SumGrasaUltControl = this.GetSumGrasaUltControl();
            voA.PromGrasaUltControl = Math.Round(this.GetPromGrasaUltControl(), 2);
            voA.CantOrdene = this.GetCantOrdene();
            voA.CantEntoradas = this.GetCantEntoradas();
            voA.CantSecas = this.GetCantSecas();
            voA.FechaUltControl = this.GetFechaUltimoControl().ToString();
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

        public Categoria GetCategoriaByRegistro(string registro)
        {
            _catMapper = new CategoriaMapper();
            return _catMapper.GetCategoriaByRegistro(registro);
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

        public List<Log> LogGetLastXDays()
        {
            var logMap = new LogMapper();
            return logMap.GetLastXDays(30);
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

        public List<Remito> GetRemitoByEmpresa2fechas(int idEmpresa, string fecha1, string fecha2)
        {
            var remMap = new RemitoMapper();
            return remMap.GetRemitoByEmpresa2fechas(idEmpresa, fecha1, fecha2);
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

        public List<VOEmpresa> GetEmpresasRemisoras()
        {
            var lstResult = new List<VOEmpresa>();
            var lstRem = GetEmpresasRemisorasAll();
            foreach (var empresaRemisora in lstRem)
            {
                var voE = CopiarVOEmpresaRemisora(empresaRemisora);
                lstResult.Add(voE);
            }
            return lstResult;
        }

        private VOEmpresa CopiarVOEmpresaRemisora(EmpresaRemisora empresaRemisora)
        {
            var voE = new VOEmpresa();
            voE.Id = empresaRemisora.Id;
            voE.Actual = empresaRemisora.Actual == 'S';
            voE.EsActual = empresaRemisora.Actual == 'S' ? "SI" : "NO";
            voE.Telefono = empresaRemisora.Telefono;
            voE.Nombre = empresaRemisora.Nombre;
            voE.RazonSocial = empresaRemisora.RazonSocial;
            voE.Rut = empresaRemisora.Rut;
            voE.Direccion = empresaRemisora.Direccion;
            return voE;
        }

        public bool GuardarEmpresaRemisora(VOEmpresa voEmp, string nickName)
        {
            try
            {
                var empRem = this.CopiarEmpresaRemisoraVoEmpresa(voEmp);
                var empMap = new EmpresaRemisoraMapper(empRem, nickName);
                return empMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private EmpresaRemisora CopiarEmpresaRemisoraVoEmpresa(VOEmpresa voEmpresaRemisora)
        {
            var empRem = new EmpresaRemisora();
            empRem.Id = voEmpresaRemisora.Id;
            empRem.Actual = voEmpresaRemisora.Actual ? 'S' : 'N';
            empRem.Telefono = voEmpresaRemisora.Telefono;
            empRem.Nombre = voEmpresaRemisora.Nombre;
            empRem.RazonSocial = voEmpresaRemisora.RazonSocial;
            empRem.Rut = voEmpresaRemisora.Rut;
            empRem.Direccion = voEmpresaRemisora.Direccion;
            return empRem;
        }

        public bool UpdateEmpresaRemisoraActual(int id)
        {
            try
            {
                var empMap = new EmpresaRemisoraMapper();
                return empMap.UpdateEmpresaRemisoraActual(id) == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEmpleado(VOEmpleado voEmp)
        {
            try
            {
                var empleado = CopiarEmpleadoVoEmpleado(voEmp);
                _empMapper = new EmpleadoMapper(empleado);
                return _empMapper.Update() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Empleado CopiarEmpleadoVoEmpleado(VOEmpleado voEmpleado)
        {
            var empleado = new Empleado();
            empleado.Activo = voEmpleado.Activo;
            empleado.Apellido = voEmpleado.Apellido;
            empleado.Id_empleado = (short)voEmpleado.Id;
            empleado.Nombre = voEmpleado.Nombre;
            empleado.Iniciales = voEmpleado.Iniciales;
            return empleado;
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

        public List<VOConcurso> GetConcursosby2fechas(string fecha1, string fecha2)
        {
            var lstCategConc = this.GetCategoriasConcurso();
            var lstResult = new List<VOConcurso>();
            var concMap = new ConcursoMapper();
            var lst = concMap.Getby2fechas(fecha1, fecha2);
            foreach (Concurso conc in lst)
            {
                int id = conc.NombreLugarConcurso.Id;
                conc.NombreLugarConcurso = this.GetConcursoById(id);
                CategoriaConcurso laCat = lstCategConc.FirstOrDefault(c => c.Id_categ == conc.Categoria.Id_categ);
                if (laCat != null) conc.Categoria.Nombre = laCat.Nombre;
                var voConc = new VOConcurso();
                voConc.Fecha = conc.Fecha.ToShortDateString();
                voConc.CategConcurso = conc.Categoria.Nombre;
                voConc.Comentarios = conc.Comentarios;
                voConc.ElPremio = conc.ElPremio;
                voConc.Lugar = conc.NombreLugarConcurso.Lugar;
                voConc.Registro = conc.Registro;
                voConc.NombreExpo = conc.NombreLugarConcurso.NombreExpo;
                voConc.NombreYLugarDeConcurso = conc.NombreLugarConcurso.ToString();
                lstResult.Add(voConc);
            }
            return lstResult;
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

        public List<Usuario> GetUsuariosConMailAll()
        {
            var lstResult = new List<Usuario>();
            var lst = this.GetUsuariosAll();
            foreach (var u in lst)
            {
                if (u != null && u.Email != "") lstResult.Add(u);
            }
            return lstResult;
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

            //string strDate = anim.Fecha_nacim.ToShortDateString();
            //if (strDate != string.Empty) voAnim.Fecha_nacim = DateTime.Parse(strDate, new CultureInfo("fr-FR"));
            
            voAnim.Gen = anim.Gen;
            //voAnim.IdCategoria = anim.IdCategoria;
            //voAnim.Categoria = this.GetCategoriaById(anim.IdCategoria).ToString();
            Categoria catAnimal = GetCategoriaByRegistro(anim.Registro);
            voAnim.Categoria = catAnimal.ToString();
            voAnim.IdCategoria = catAnimal.Id_categ;
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

        public List<AnimalMapper.VOFoto> GetFotosAnimal(string registro)
        {
            return _animalMapper.GetFotosByRegistro(registro);
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
            try
            {
                var celoMapper = new Celo_Sin_ServicioMapper(celo);
                return celoMapper.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TipoEvento> GetTipoEventosAnimal()
        {
            return _tevMapper.GetAll();
        }

        public List<TipoEvento> GetTipoEventosEnUso()
        {
            return _tevMapper.GetEventosEnUso();
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

        public bool InsertarEvento(Evento evento, string nickName)
        {
            try
            {
                switch (evento.Id_evento)
                {
                    case 0: // ABORTO
                        var abortoMap = new AbortoMapper((Aborto) evento, nickName);
                        return abortoMap.Insert() > 1;
                    case 2: // CELO SIN SERVICIO
                        var celoMap = new Celo_Sin_ServicioMapper((Celo_Sin_Servicio) evento, nickName);
                        return celoMap.Insert() > 1;
                    case 3: // SERVICIO
                        var servMap = new ServicioMapper((Servicio) evento, nickName);
                        return servMap.Insert() > 1;
                    case 4: // SECADO
                        var secMap = new SecadoMapper((Secado) evento, nickName);
                        return secMap.Insert() > 1;
                    case 7: // DIAGNOSTICO DE PRENEZ
                        var diagMap = new Diag_PrenezMapper((Diag_Prenez) evento, nickName);
                        return diagMap.Insert() > 1;
                    case 8: // CONTROL DE PRODUCCION
                        var contMap = new Control_ProduccMapper((Control_Producc) evento, evento.Registro, nickName);
                        return contMap.Insert() > 1;
                    case 9: // CALIFICACION
                        var califMap = new CalificacionMapper((Calificacion) evento, nickName);
                        return califMap.Insert() > 1;
                    case 10: // CONCURSO
                        var concursMap = new ConcursoMapper((Concurso) evento, nickName);
                        return concursMap.Insert() > 1;
                    case 11: // BAJA POR VENTA
                        var bajaMap = new VentaMapper((Venta) evento, nickName);
                        return bajaMap.Insert() > 1;
                    case 12: // BAJA POR MUERTE
                        var muerteMap = new MuerteMapper((Muerte) evento, nickName);
                        return muerteMap.Insert() > 1;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool InsertarRemito(Remito remito, string nickName)
        {
            try
            {
                var remitoMap = new RemitoMapper(remito, nickName);
                return remitoMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public List<Diag_PrenezMapper.VODiagnostico> GetInseminacionesExitosas2fechas(string fecha1, string fecha2)
        {
            var listemp = _empMapper.GetAll();
            var list = _diagMapper.GetInseminacionesExitosas2fechas(fecha1, fecha2);
            foreach (var diagVo in list)
            {
                var emp = listemp.FirstOrDefault(e => e.Id_empleado == diagVo.IdInseminador);
                diagVo.Inseminador = emp != null ? emp.ToString() : "-";
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
            try
            {
                var userMap = new UsuarioMapper(usuario);
                return userMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUsuario(Usuario usuario)
        {
            try
            {
                var userMap = new UsuarioMapper(usuario);
                return userMap.Update() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            
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

        public List<BajaMapper.VOEnfermedad> GetCantidadMuertesPorEnfermedadPor2fechas(string fecha1, string fecha2)
        {
            var bajaMap = new BajaMapper();
            var lstResult = bajaMap.GetCantidadMuertesPorEnfermedadPor2fechas(fecha1, fecha2);
            var sum = 0;
            for (int i = 0; i < lstResult.Count; i++) sum += lstResult[i].Cantidad;
            if (sum > 0)
            {
                for (int j = 0; j < lstResult.Count; j++)
                {
                    double avg = ((double)lstResult[j].Cantidad / sum) * 100;
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


        public List<VOParto> GetPartosPor2fechas(string fecha1, string fecha2)
        {
            var lstResult = new List<VOParto>();
            var partoMap = new PartoMapper();
            var lst = partoMap.GetPartosBy2fechas(fecha1, fecha2);
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

        public int GetCantMellizosPor2fechas(string fecha1, string fecha2)
        {
            return _animalMapper.GetCantMellizosBy2fechas(fecha1, fecha2);
        }

        public int GetCantTrillizosPorAnio(int anio)
        {
            return _animalMapper.GetCantTrillizosByAnio(anio);
        }

        public int GetCantTrillizosPor2fechas(string fecha1, string fecha2)
        {
            return _animalMapper.GetCantTrillizosBy2fechas(fecha1, fecha2);
        }

        public int GetCantNacimientosPorAnio(int anio)
        {
            return _animalMapper.GetCantNacimientosByAnio(anio);
        }

        public int GetCantNacimientosPor2fechas(string fecha1, string fecha2)
        {
            return _animalMapper.GetCantNacimientosBy2fechas(fecha1, fecha2);
        }

        public List<RemitoMapper.VORemitoGrafica> GetRemitosGraficas()
        {
            var remMap = new RemitoMapper();
            return remMap.GetRemitosGrafica();
        }

        public bool InsertarCasoSoporte(CasoSoporte caso)
        {
            try
            {
                var casoMap = new CasoSoporteMapper(caso);
                return casoMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private CasoSoporte CopiarVOCasoSoporte(VOCaso voCaso)
        {
            var casoSop = new CasoSoporte();
            casoSop.Descripcion = voCaso.Descripcion;
            casoSop.Email = voCaso.Email;
            casoSop.Establecimiento = voCaso.Establecimiento;
            casoSop.Id = voCaso.Id;
            casoSop.Nickname = voCaso.Nickname;
            casoSop.NombreApellido = voCaso.NombreApellido;
            casoSop.Telefono = voCaso.Telefono;
            casoSop.Tipo = voCaso.Tipo;
            casoSop.Titulo = voCaso.Titulo;
            return casoSop;
        }

        public bool EnviarCasoSoporte(VOCaso voCaso)
        {
            // envia por mail el caso de soporte a la casilla definida para eso
            // e inserta en la base de datos para dejar el registro
            try
            {
                var caso = CopiarVOCasoSoporte(voCaso);
                var email = new Mail();
                email.EnviarMail(caso.ToString(), caso.Titulo);
                return InsertarCasoSoporte(caso);
            }
            catch (Exception ex)
            {
                return false;
            }
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


        public List<VOToroUtilizado> GetTorosUtilizadosPorAnio2fechas(string fecha1, string fecha2)
        {
            var lstToros = new List<VOToroUtilizado>();
            var lst = _servMapper.GetServDiagPorToroUtilizado2fechas(fecha1, fecha2);
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
                                Math.Round((double)lstToros[j].CantDiagP / lstToros[j].CantServicios * 100, 1);
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
                    voToro.PorcEfectividad = Math.Round((double)voToro.CantDiagP / voToro.CantServicios * 100, 1);
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

        public List<AnimalMapper.VOToro> GetTorosNacimPorGenero2fechas(string fecha1, string fecha2)
        {
            var lst = _animalMapper.GetNacimientosPorToroBy2fechas(fecha1, fecha2);
            foreach (AnimalMapper.VOToro vT in lst)
            {
                vT.CantH = _animalMapper.GetCantNacimientosHPorToroBy2fechas(vT.Registro, fecha1,fecha2);
                vT.CantM = vT.CantNacim - vT.CantH;
                vT.PorcHembras = Math.Round((double)vT.CantH / vT.CantNacim * 100, 1);
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
            var cant35 = _servMapper.GetServicio_DiasSinDiagPrenezCount35();
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
            var cant70 = _servMapper.GetServicio_DiasSinDiagPrenezCount70();
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
            var cant80 = _servMapper.GetServicio_80DiasLactanciaSinServicioCount();
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

        public List<VoListItemInd> GetExtrasGraficaCategorias()
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

        public int GetCantVentasPorMes(int mes)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasPorMes(mes);
        }

        public int GetCantVentasPor2fechas(string fecha1, string fecha2)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasPor2fechas(fecha1, fecha2);
        }

        public int GetCantVentasAFrigPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasAFrigPorAnio(anio);
        }

        public int GetCantVentasAFrigPorMes(int mes)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasAFrigPorMes(mes);
        }

        public int GetCantVentasAFrigPor2fechas(string fecha1, string fecha2)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasAFrigPor2fechas(fecha1, fecha2);
        }

        public int GetCantVentasRecienNacidosPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasRecienNacidosPorAnio(anio);
        }

        public int GetCantVentasRecienNacidosPorMes(int mes)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasRecienNacidosPorMes(mes);
        }

        public int GetCantVentasRecienNacidosPor2fechas(string fecha1, string fecha2)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasRecienNacidosPor2fechas(fecha1, fecha2);
        }

        public int GetCantVentasViejasPorAnio(int anio)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasViejasPorAnio(anio);
        }

        public int GetCantVentasViejasPorMes(int mes)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasViejasPorMes(mes);
        }

        public int GetCantVentasViejasPor2fechas(string fecha1, string fecha2)
        {
            var _bajMap = new BajaMapper();
            return _bajMap.GetCantVentasViejasPor2fechas(fecha1, fecha2);
        }

        public bool UpdateDatosCorporativos(VOEmpresa voEmp)
        {
            try
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
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool EnfermedadInsert(Enfermedad enfermedad, string nickName)
        {
            try
            {
                var enfMapper = new EnfermedadMapper();
                var lastId = enfMapper.GetLastIdEnfermedad();
                enfermedad.Id = lastId + 1;
                enfMapper = new EnfermedadMapper(enfermedad, nickName);
                return enfMapper.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CategConcursoInsert(CategoriaConcurso categ)
        {
            try
            {
                var catConcMap = new CategConcursoMapper(categ);
                return catConcMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertarEmpleado(Empleado empleado, string nickName)
        {
            try
            {
                var empMap = new EmpleadoMapper(empleado, nickName);
                return empMap.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
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
                    //_lactMapper = new LactanciaMapper(voA.Registro);
                    //voA.Lactancias = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());
                    voA.Lactancias = this.GetLactanciasInclusoLaActual(voA);
                }
                voA.Concursos = this.GetConcursosAnimal(voA.Registro);
                // ARBOL POR PARTE DE MADRE
                if (voA.Reg_madre != "H-DESCONOC")
                {
                    voA.Madre = CopiarVOAnimal(GetAnimalByRegistro(voA.Reg_madre));
                    voA.Madre.Vivo = !EstaMuertoAnimal(voA.Reg_madre);
                    voA.Madre.Vendido = FueVendidoAnimal(voA.Reg_madre);
                    voA.Madre.Lactancias =  this.GetLactanciasInclusoLaActual(voA.Madre);

                    if (voA.Madre != null && voA.Madre.Registro != "H-DESCONOC")
                    {
                        string strAbuelaM = voA.Madre.Reg_madre;
                        string strAbueloM = voA.Madre.Reg_padre;
                        if (strAbuelaM != "H-DESCONOC")
                        {
                            voA.Madre.Madre = CopiarVOAnimal(GetAnimalByRegistro(strAbuelaM));
                            voA.Madre.Madre.Vivo = !EstaMuertoAnimal(strAbuelaM);
                            voA.Madre.Madre.Vendido = FueVendidoAnimal(strAbuelaM);
                            voA.Madre.Madre.Lactancias = this.GetLactanciasInclusoLaActual(voA.Madre.Madre);
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
                            voA.Padre.Madre.Lactancias = this.GetLactanciasInclusoLaActual(voA.Padre.Madre);
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


        public List<VOLactancia> GetLactanciasInclusoLaActual(VOAnimal voA)
        {
            try
            {
                // traigo las lactancias historicas
                _lactMapper = new LactanciaMapper(voA.Registro);
                var lactList = this.CopiarVOLactanciaList(_lactMapper.GetLactanciasByRegistro());
                // Si esta en ordeñe me traigo la lactancia actual calculada
                // (no esta en la tabla de lactancias consolidadas al momento del secado)

                //if (voA.IdCategoria == 4 || voA.IdCategoria == 9)
                // DATABASE VOLVER A TRAER LAS LACTANCIAS "TEMPORALES" 
                // DE VACAS QUE SE DIERON DE BAJA Y NO SE LIQUIDO LA LACT ULTIMA, 
                // DEBEN DE QUEDAR LIQUIDADAS COMO HISTORICAS
                if (voA.IdCategoria == 4)  
                {
                    var lactActual = this.ConsolidarLactancia(voA.Registro);
                    if (lactActual != null)
                    {
                        var voLactActual = new VOLactancia();
                        voLactActual.Numero = lactActual.Numero;
                        voLactActual.Dias = lactActual.Dias;
                        voLactActual.ProdLeche = lactActual.ProdLeche;
                        voLactActual.ProdGrasa = lactActual.ProdGrasa;
                        voLactActual.Leche305 = lactActual.Leche305;
                        voLactActual.Grasa305 = lactActual.Grasa305;
                        voLactActual.Leche365 = lactActual.Leche365;
                        voLactActual.Grasa365 = lactActual.Grasa365;
                        voLactActual.PorcentajeGrasa = Math.Round(lactActual.ProdGrasa/lactActual.ProdLeche*100, 2);
                        lactList.Add(voLactActual);
                    }
                }
                return lactList;
            }
            catch (Exception ex)
            {
                return new List<VOLactancia>();
            }
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


        public List<VOInseminadorRank> GetRankingInseminadores2fechas(string fecha1, string fecha2)
        {
            var lstResult = new List<VOInseminadorRank>();
            var listEmp = _empMapper.GetAll();
            var esta = false;

            var lst = _diagMapper.GetTrabajoInseminadores2fechas(fecha1, fecha2);

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

        public bool CheckAnimalConDiagPrenezActual(string registro)
        {
            var lista = _diagMapper.GetAnimalesConDiagPrenezActual();
            return lista.Any(u => u.Registro == registro);
        }



        public List<VOBaja> GetMuertesPor2fechas(string fecha1, string fecha2)
        {
            var bajaMap = new BajaMapper();
            var lst = bajaMap.GetMuertesPor2fechas(fecha1, fecha2);
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

        public List<VOConcurso> GetConcursosByRegistro(string reg)
        {
            var lstCategConc = this.GetCategoriasConcurso();
            var concMap = new ConcursoMapper();
            var listaConc = new List<VOConcurso>();
            var listTemp = concMap.GetConcursosByRegistro(reg);
            foreach (Evento e in listTemp)
            {
                var conc = new Concurso();
                conc = (Concurso) e;
                int id = conc.NombreLugarConcurso.Id;
                conc.NombreLugarConcurso = this.GetConcursoById(id);
                CategoriaConcurso laCat = lstCategConc.FirstOrDefault(c => c.Id_categ == conc.Categoria.Id_categ);
                if (laCat != null) conc.Categoria.Nombre = laCat.Nombre;
                var voConc = new VOConcurso();
                voConc.Fecha = conc.Fecha.ToShortDateString();
                voConc.CategConcurso = conc.Categoria.Nombre;
                voConc.Comentarios = conc.Comentarios;
                voConc.ElPremio = conc.ElPremio;
                voConc.Lugar = conc.NombreLugarConcurso.Lugar;
                voConc.Registro = conc.Registro;
                voConc.NombreExpo = conc.NombreLugarConcurso.NombreExpo;
                voConc.NombreYLugarDeConcurso = conc.NombreLugarConcurso.ToString();
                listaConc.Add(voConc);
            }
            return listaConc;
        }

        public List<string> ExportarLogCompleto()
        {
            var lstResult = new List<string>();
            var logMap = new LogMapper();
            var lstLog = logMap.GetAll();
            for (int i = 0; i < lstLog.Count; i++)
            {
                var linea = lstLog[i].ToString();
                lstResult.Add(linea);
            }
            return lstResult;
        }

        public void EnviarMail(string msj, string asunto)
        {
            var email = new Mail();
            email.EnviarMail(msj, asunto);
        }

        public List<VOFaq> GetAllFaqs()
        {
            var lstResult = new List<VOFaq>();
            var faqMap = new FaqMapper();
            var lstFaq = faqMap.GetAll();
            foreach (var faq in lstFaq)
            {
                var voF = CopiarVOFaq(faq);
                lstResult.Add(voF);
            }
            return lstResult;
        }

        private VOFaq CopiarVOFaq(Faq faq)
        {
            var voFaq = new VOFaq();
            voFaq.Id = faq.Id;
            voFaq.Pregunta = faq.Pregunta;
            voFaq.Respuesta = faq.Respuesta;
            voFaq.Icono = faq.Icono;
            return voFaq;
        }

        public bool ResetearPassword(string admin, string user, string newPassword)
        {
            try
            {
                if (admin != "" && user != "" && newPassword != "")
                {
                    return (_userMapper.UpdateContrasenaUsuario(admin, user, newPassword) > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarUsuario(string admin, string user)
        {
            try
            {
                if (admin != user && admin != "" && user != "")
                {
                    return (_userMapper.DeleteUsuario(admin, user) > 0);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Reporte> GetAllReportes()
        {
            var repoMap = new ReporteMapper();
            return repoMap.GetAll();
        }

        public List<VOReporte> GetReportesNotificaciones()
        {
            var lstResult = new List<VOReporte>();
            var repoMap = new ReporteMapper();
            var lst = repoMap.GetAll();
            foreach (var repo in lst)
            {
                var voRepo = CopiarVoReporte(repo);
                lstResult.Add(voRepo);
            }
            return lstResult;
        }

        private VOReporte CopiarVoReporte(Reporte repo)
        {
            var voRepo = new VOReporte();
            voRepo.Id = repo.Id;
            voRepo.Titulo = repo.Titulo;
            voRepo.Descripcion = repo.Descripcion;
            voRepo.Frecuencia = repo.Frecuencia;
            voRepo.Dia = repo.Dia;
            var voItem = this.GetDiaDelMesYFrecuenciaEnvioReporte(repo.Dia, repo.Frecuencia);
            var str = " ";
            var strS = "";
            if (voItem.Valor2 == "Todos")
            {
                str = " los ";
                if (voItem.Valor1 == "sábado" || voItem.Valor1 == "domingo") strS = "s";
            }
            voRepo.Envio = voItem.Valor2 + str + voItem.Valor1 + strS + " del mes";
            return voRepo;
        }

        public VOReporte ReporteSemanal()
        {
            VOReporte voRep = null;
            var hoy = DateTime.Today;

            var lstReportes = GetAllReportes();
            Reporte repo = lstReportes.FirstOrDefault(r => r.Id == 2);
            if (repo != null)
            {
                var voRepSem = CopiarVoReporte(repo);
                voRepSem.CantVacasEnOrdene = new VoListItem();
                voRepSem.CantVacasEnOrdene.Nombre = "Cantidad de vacas en ordeñe";
                voRepSem.CantVacasEnOrdene.Id = GetCantOrdene();
                voRepSem.PrenezConfirmada = new VoListItem();
                voRepSem.PrenezConfirmada.Nombre = "Hembras en ordeñe con preñez confirmada";
                voRepSem.PrenezConfirmada.Id = GetInseminacionesExitosas(hoy).Count();
                voRepSem.SinDiag70Dias = new VoListItem();
                voRepSem.SinDiag70Dias.Nombre = "Hembras en ordeñe con 70 días y sin diagnóstico";
                voRepSem.SinDiag70Dias.Id = GetServicios70SinDiagPrenezVacOrdene().Count;
                voRepSem.SinDiagEcográficos = new VoListItem();
                voRepSem.SinDiagEcográficos.Nombre = "Hembras en ordeñe sin diagnósticos ecográficos";
                voRepSem.SinDiagEcográficos.Id = GetServicios35SinDiagPrenezVacOrdene().Count;
                voRepSem.EnLactSinServ80Dias = new VoListItem();
                voRepSem.EnLactSinServ80Dias.Nombre = "Hembras con 80 o más días en lactancia y sin servicio";
                voRepSem.EnLactSinServ80Dias.Id = GetLactanciasSinServicio80().Count;
                voRepSem.CantAbortosAnio = new VoListItem();
                voRepSem.CantAbortosAnio.Nombre = "Cantidad de abortos en este año";
                voRepSem.CantAbortosAnio.Id = GetAbortosByAnio(hoy);
                voRepSem.CantNacidosAnio = new VoListItem();
                voRepSem.CantNacidosAnio.Nombre = "Cantidad de nacidos este año";
                voRepSem.CantNacidosAnio.Id = GetCantNacimientosPorAnio(hoy.Year);
                voRep = voRepSem;
            }
            
            return voRep;
        }

        private string CrearMailHtmlReporte(VOReporte voRepo)
        {
            var strB = new StringBuilder();
            if (voRepo != null)
            {
                strB = this.ReporteBodyHtml(voRepo);
            }
            return strB.ToString();
        }

        private VoListItemDuplaString GetDiaDelMesYFrecuenciaEnvioReporte(int dia, int frec)
        {
            var voItem = new VoListItemDuplaString();
            
            switch (dia)
            {
                case 0:
                    voItem.Valor1 = "domingo";
                    break;
                case 1:
                    voItem.Valor1 = "lunes";
                    break;
                case 2:
                    voItem.Valor1 = "martes";
                    break;
                case 3:
                    voItem.Valor1 = "miércoles";
                    break;
                case 4:
                    voItem.Valor1 = "jueves";
                    break;
                case 5:
                    voItem.Valor1 = "viernes";
                    break;
                case 6:
                    voItem.Valor1 = "sábado";
                    break;
            }

            switch (frec)
            {
                case 0:
                    voItem.Valor2 = "Todos";
                    break;
                case 1:
                    voItem.Valor2 = "Primer";
                    break;
                case 2:
                    voItem.Valor2 = "Segundo";
                    break;
                case 3:
                    voItem.Valor2 = "Tercero";
                    break;
                case 4:
                    voItem.Valor2 = "Último";
                    break;
            }
            return voItem;
        }

        public void EnviarReporteSemanal()
        {
            var voRepSem = ReporteSemanal();
            var mailBodyHtml = CrearMailHtmlReporte(voRepSem);
            var email = new Mail();
            var lstDestinatarios = ListaDestinatariosReporte(voRepSem.Id);
            foreach (var dest in lstDestinatarios)
            {
                email.EnviarMailHtml(dest.Email, voRepSem.Titulo, mailBodyHtml);
            }
        }

        public void EnviarReporteCierreMes()
        {
            //var voRepCierreMes = ReporteCierreMes();
            //var mailBodyHtml = CrearMailHtmlReporte(voRepCierreMes);
            //var email = new Mail();
            //var lstDestinatarios = ListaDestinatariosReporte(voRepCierreMes.Id);
            //foreach (var dest in lstDestinatarios)
            //{
            //    email.EnviarMailHtml(dest.Email, voRepCierreMes.Titulo, mailBodyHtml);
            //}
        }

        public void EnviarReporteSemanalPrueba(string dest)
        {
            var voRepSem = ReporteSemanal();
            var mailBodyHtml = CrearMailHtmlReporte(voRepSem);
            var email = new Mail();
            email.EnviarMailHtml(dest, voRepSem.Titulo, mailBodyHtml);
        }

        public string ReporteSemanalPrueba()
        {
            var voRepSem = ReporteSemanal();
            var mailBodyHtml = CrearMailHtmlReporte(voRepSem);
            return this.ReporteBodyHtml(voRepSem).ToString();
        }

        public StringBuilder ReporteBodyHtml(VOReporte voRepo)
        {
            var sb = new StringBuilder();
            sb.Append("<!-- COMIENZA CUERPO DEL MAIL -->");
            sb.Append("<div marginwidth='0' marginheight='0' style='background-color:#ffffff'>");
            sb.Append("<div style='margin-top:10px;padding:0px;background-color:#ffffff'>");
            sb.Append("<img src='http://www.tamboprp.uy/img_public/logo_tamboprp.png' />");
            sb.Append("</div>");
            sb.Append("<div style='padding:5px;width:460px;background-color:#ffffff'>");
            sb.Append("<h1 style='font-size:24px;font-weight:light;line-height:100%;letter-spacing:normal'>");
            sb.Append(voRepo.Titulo);
            sb.Append("</h1>");
            sb.Append("<span style='font-size:14px;color:#888888;font-weight:normal;line-height:100%;letter-spacing:normal'>");
            sb.Append(voRepo.Descripcion);
            sb.Append("</span>");
            sb.Append("</div>");
            sb.Append("<hr style='width:460px;margin-left:0'/>");
            sb.Append("<div style='padding:5px;width:460px;background-color:#ffffff'>");
            sb.Append("<h3 style='color:#333333;display:block;font-size:18px;font-weight:normal;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;text-align:left'>");
            sb.Append("Notificaciones:");
            sb.Append("</h3>");
            sb.Append("<br/>");
            sb.Append("<table style='margin-top:30px;font-size:16px;margin-top:0;margin-right:0;margin-bottom:0px;margin-left:0;text-align:left'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.CantVacasEnOrdene.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.CantVacasEnOrdene.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.PrenezConfirmada.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.PrenezConfirmada.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.SinDiagEcográficos.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.SinDiagEcográficos.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.SinDiag70Dias.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.SinDiag70Dias.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.EnLactSinServ80Dias.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.EnLactSinServ80Dias.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.CantAbortosAnio.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.CantAbortosAnio.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width='40px'><p style='color:#9abc32;font-weight:bold'>" + voRepo.CantNacidosAnio.Id + "</p></td>");
            sb.Append("<td width='420px'><p style='color:#333333'>" + voRepo.CantNacidosAnio.Nombre + "</p></td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("<hr style='width:460px;margin-left:0'/>");
            sb.Append("<div style='padding:5px'>");
            sb.Append("<table style='margin-top:30px;font-size:13px;margin-top:0;margin-right:0;margin-bottom:0px;margin-left:0;text-align:left'>");
            sb.Append("<tbody>");
            sb.Append("<tr><td width='460px'>");
            sb.Append("Clic aquí para <a href='http://www.tamboprp.uy' style='color:#478fca;text-decoration:none;' target='_blank'>ingresar al sistema</a> y ver más información.");
            sb.Append("</td></tr>");
            sb.Append("<tr><td width='460px'>");
            sb.Append("El administrador de su establecimiento puede configurar el envío de estos ");
            sb.Append("<a href='http://www.tamboprp.uy/Notificaciones' style='color:#478fca;text-decoration:none' target='_blank'>");
            sb.Append("mensajes automáticos.");
            sb.Append("</a>");
            sb.Append("</td></tr>");
            sb.Append("<tr><td width='460px'>");
            sb.Append("Si Ud. recibió este mensaje por error, por favor notifique a ");
            sb.Append("<a href='mailto:soporte@tamboprp.uy' style='color:#478fca;text-decoration:none' target='_blank'>");
            sb.Append("soporte@tamboprp.uy");
            sb.Append("</a> y será excluído a la brevedad. Gracias y disculpas.");
            sb.Append("</td></tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("<hr style='width:460px;margin-left:0'/>");
            sb.Append("<div style='padding:5px;margin:0px 0px 20px 0px;background-color:#ffffff'>");
            sb.Append("<p style='font-size:14px'>");
            //sb.Append("<span class='ace-icon fa fa-check-square-o' aria-hidden='true'></span>");
            var anioActual = DateTime.Now.Year;
            sb.Append("<strong>tambo<span style='color:#478fca;text-decoration:none'>prp</span></strong> | &copy; " + anioActual.ToString() + " todos los derechos reservados");
            sb.Append("</p>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<!-- FIN CUERPO DEL MAIL -->");
               
            return sb;
        }

        public List<Usuario> ListaDestinatariosReporte(int id)
        {
            var lstTodos = _userMapper.GetAll();
            var lstDest = new List<Usuario>();
            var repoMap = new ReporteMapper();
            var lst = repoMap.GetDestinatariosReporte(id);
            foreach (var userNick in lst)
            {
                Usuario user = lstTodos.FirstOrDefault(u => u.Nickname == userNick);
                if (user != null && user.Email != "")
                {
                    lstDest.Add(user);
                }
            }
            return lstDest;
        }

        public bool ProgramarReporte(int idRepo, int dia, int frecuencia)
        {
            try
            {
                var repMap = new ReporteMapper();
                return repMap.UpdateProgramacionReporte(idRepo, dia, frecuencia) == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public void CambiarDestinatariosReporte(int idRepo, List<Usuario> destinatarios)
        {
            try
            {
                var repMap = new ReporteMapper();
                repMap.LimpiarDestinatariosReporteById(idRepo);
                foreach (var user in destinatarios)
                {
                    repMap.UpdateDestinatariosReporteById(idRepo, user.Nickname);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public bool UpdateDatosAnimal(Animal animTemp, string nickname)
        {
            try
            {
                _animalMapper = new AnimalMapper(animTemp, nickname);
                return _animalMapper.UpdateDatosModificables() == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SubirFotoAnimal(AnimalMapper.VOFoto foto)
        {
            try
            {
                _animalMapper = new AnimalMapper();
                return _animalMapper.SubirFotoAnimal(foto) == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SubirFotoPerfilUsuario(Usuario u)
        {
            try
            {
                _userMapper = new UsuarioMapper(u);
                return _userMapper.UpdateFotoPerfilUsuario() == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeshabilitarUsuario(string admin, string user)
        {
            try
            {
                _userMapper = new UsuarioMapper();
                return _userMapper.UsuarioDeshabilitar(admin, user) == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HabilitarUsuario(string admin, string user)
        {
            try
            {
                _userMapper = new UsuarioMapper();
                return _userMapper.UsuarioHabilitar(admin, user) == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public VOControlProdMU LeerArchivoControl(string archivo, string nickName)
        {
            try
            {
                var listaFallida = new List<Control_Producc>();
                var lista = new List<Control_Producc>();
                DateTime fechaControl = new DateTime();

                // recorro y leo datos de el archivo
                using (CsvFileReader reader = new CsvFileReader(archivo))
                {
                    CsvRow row = new CsvRow();
                    while (reader.ReadRow(row))
                    {
                        double leche = 0;
                        bool guardar = row[0] == "11";
                        if (guardar)
                        {
                            var control = new Control_Producc
                            {
                                Comentarios = "test_tamboprp",
                                Id_evento = 8,
                                Grasa = 3.60,
                                Dias_para_control = 99
                            };
                            for (int s = 0; s < row.Count; s++)
                            {
                                if (s == 2)
                                {
                                    string reg = row[s].Replace("'", "");
                                    control.Registro = reg;
                                }
                                if (s == 5)
                                {
                                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    string num = row[s];
                                    if (currentCulture.Name == "es-UY")
                                        num = row[s].Replace('.', ',');
                                    leche += double.Parse(num);
                                }
                                if (s == 6)
                                {
                                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                                    string num = row[s];
                                    if (currentCulture.Name == "es-UY")
                                        num = row[s].Replace('.', ',');
                                    leche += double.Parse(num);
                                    string lecheLetra = leche.ToString();
                                    control.Leche = leche;
                                }
                                if (s == 17)
                                {
                                    string fecha = row[s].Replace("'", "");
                                    fecha = FormatoFecha(fecha);
                                    fechaControl = Convert.ToDateTime(fecha);
                                    control.Fecha = Convert.ToDateTime(fecha);
                                }
                            }
                            lista.Add(control);
                        }
                    }
                }
                // chequeo que los registros esten, sino se guardan en otra lista fallida y no se guardan en controles
                foreach (var control in lista)
                {
                    if (_animalMapper.AnimalExiste(control.Registro))
                    {
                        var contMap = new Control_ProduccMapper(control, control.Registro, nickName);
                        try
                        {
                            var affected = contMap.Insert();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        listaFallida.Add(control);
                    }
                }

                // armo objeto para devolver a la pagina
                var voControl = new VOControlProdMU
                {
                    CantTotales = (short) lista.Count,
                    CantExitosas = (short) (lista.Count - listaFallida.Count),
                    ControlesFallidos = listaFallida
                };

                // armo control total para insertar en controles_totales
                var lecheTotal = ConsolidarLeche(lista);
                var grasaTotal = ConsolidarGrasa(lista);
                var conTotal = new Control_Total
                {
                    Fecha = fechaControl,
                    Vacas = voControl.CantTotales,
                    Leche = lecheTotal,
                    Grasa = grasaTotal,
                };
                var controlTotMap = new Controles_totalesMapper(conTotal, nickName);
                controlTotMap.Insert();


                return voControl;
            }
            catch (Exception ex)
            {
                // devuelve uno vacío si hay falla
                return new VOControlProdMU();
            }
        }


        public double ConsolidarLeche(List<Control_Producc> lista)
        {
            double lecheTotal = 0;
            foreach (var control in lista)
            {
                lecheTotal += control.Leche;
            }
            return lecheTotal;
        }

        public double ConsolidarGrasa(List<Control_Producc> lista)
        {
            double grasaTotal = 0;
            foreach (var control in lista)
            {
                grasaTotal += control.Grasa;
            }
            return grasaTotal;
        }


        public string DiasMes(string fecha)
        {
            var res = fecha.Split(Convert.ToChar("-"));
            var mes = res[1];
            var ano = int.Parse(res[0]);
            if (mes == "01" || mes == "03" || mes == "05" || mes == "07" || mes == "08" || mes == "10" || mes == "12")
            {
                return "31";
            }
            else if (mes == "04" || mes == "06" || mes == "09" || mes == "11")
            {
                return "30";
            }
            else
            {
                if (mes == "02" && ano % 4 == 0)
                {
                    return "29";
                }
                return "28";
            }
        }

        public string FormatoFecha(string fecha)
        {
            var res = fecha.Split(Convert.ToChar("/"));
            var salida = "";
            salida = res[2] + "-" + res[1] + "-" + res[0];
            return salida;
        }

        public string ConvertirLeche(double leche)
        {
            var res = leche.ToString();
            res = res.Replace(",", ".");
            return res;
        }

        public bool AnimalExiste(string registro)
        {
            return _animalMapper.AnimalExiste(registro);
        }

        public bool AnimalEstaEnOrdene(string registro)
        {
            return _animalMapper.AnimalEstaEnOrdene(registro);
        }


        public bool BajaExiste(string registro)
        {
            var mumap = new MuerteMapper();
            return mumap.BajaExiste(registro);
        }
        public void CorrerTareaProgramadas()
        {
            var hoy = new DayOfWeek();
            hoy = DateTime.Today.DayOfWeek;

            var diaParaRepSem = false;
            var diaParaRepCierreMes = false;

            // veo los reportes programados, si corresponde el envio ahora
            var _repMap = new ReporteMapper();
            var lstRepo = _repMap.GetAll();

            foreach (var reporte in lstRepo)
            {
                // es el dia para correr el semanal?
                if (reporte.Frecuencia == 0 && reporte.Dia == (int) hoy)
                {
                    diaParaRepSem = true;
                }
                // es el dia para correr el cierre de mes?
                //if (reporte.Frecuencia == 4 && reporte.Dia == (int) hoy)
                //{
                //    diaParaRepCierreMes = true;
                //}
            }
            
            // pregunto si las tareas programadas ya corrieron hoy y sino las lanzo
            // cambio de categoria de terneros y terneras
            if (!this.CorrioTareaProgCambioCategoria())
            {
                _animalMapper = new AnimalMapper();
                var cant = _animalMapper.CambioCategoriaAnimales();
            }
            // reporte semanal
            if (diaParaRepSem && !this.CorrioTareaProgReporteSemanal())
            {
                this.EnviarReporteSemanal();
                var _logMap = new LogMapper();
                var cant = _logMap.TareaProgReporteSemanal();
            }
            // reporte cierre de mes
            if (diaParaRepCierreMes && !this.CorrioTareaProgReporteCierreMes())
            {
                this.EnviarReporteCierreMes();
                var _logMap = new LogMapper();
                var cant = _logMap.TareaProgReporteCierreMes();
            }
        }

        private bool CorrioTareaProgCambioCategoria()
        {
            var _logMap = new LogMapper();
            return _logMap.CorrioTareaProgCambioCategoria();
        }

        private bool CorrioTareaProgReporteSemanal()
        {
            var _logMap = new LogMapper();
            return _logMap.CorrioTareaProgReporteSemanal();
        }

        private bool CorrioTareaProgReporteCierreMes()
        {
            var _logMap = new LogMapper();
            return _logMap.CorrioTareaProgReporteCierreMes();
        }

        public bool AnimalInsert(VOAnimal voA, string nickname)
        {
            try
            {
                if (nickname == "") return false;
                if (AnimalExiste(voA.Registro)) return false;
                if (!AnimalExiste(voA.Reg_madre) || !AnimalExiste(voA.Reg_padre)) return false;

                var a = new Animal
                {
                    Registro = voA.Registro,
                    Reg_madre = voA.Reg_madre,
                    Reg_padre = voA.Reg_padre,
                    Reg_trazab = voA.Reg_trazab,
                    Gen = voA.Gen,
                    Fecha_nacim = voA.Fecha_nacim,
                    IdCategoria = voA.IdCategoria,
                    Identificacion = voA.Identificacion,
                    Sexo = voA.Sexo,
                    Origen = voA.Origen,
                    Nombre = voA.Nombre
                };

                _animalMapper = new AnimalMapper(a, nickname);
                return _animalMapper.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PartoInsert(VOParto voP, string nickname)
        {
            try
            {
                var p = new Parto
                {
                    Id_evento = 1,
                    Registro = voP.Registro,
                    Fecha = voP.Fecha,
                    Comentarios = voP.Comentarios,
                    Observaciones = voP.Observaciones,
                    Sexo_parto = voP.Sexo_parto,
                    Reg_hijo = voP.Reg_hijo
                };

                var _partoMapper = new PartoMapper(p, nickname);
                return _partoMapper.Insert() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExisteParto(VOParto voP)
        {
            var p = new Parto
            {
                Id_evento = 1,
                Registro = voP.Registro,
                Fecha = voP.Fecha,
                Comentarios = voP.Comentarios,
                Observaciones = voP.Observaciones,
                Sexo_parto = voP.Sexo_parto,
                Reg_hijo = voP.Reg_hijo
            };

            var _partoMapper = new PartoMapper(p);
            return _partoMapper.ExisteParto();
        }

        public List<Animal> GetCriasIngresadasParto(VOParto voP)
        {
            if (this.ExisteParto(voP))
            {
                _animalMapper = new AnimalMapper();
                return _animalMapper.GetCriasIngresadasParto(voP.Registro, voP.Fecha);
            }
            return null;
        }


        public int[] CalcularEdadYMD(DateTime dateOfBirth)
        {
            try
            {
                int[] result = {0, 0, 0};
                DateTime currentDate = DateTime.Now;
                // paso ambas fechas a un sistema comun
                DateTime nacimiento = dateOfBirth.ToUniversalTime();
                DateTime hoy = currentDate.ToUniversalTime();

                string nacimientoStr = nacimiento.ToShortDateString();
                string hoyStr = hoy.ToShortDateString();

                if (hoyStr.Equals(nacimientoStr)) return result;
                TimeSpan difference = currentDate.Subtract(dateOfBirth);
                // This is to convert the timespan to datetime object
                DateTime age = DateTime.MinValue + difference;
                // Min value is 01/01/0001
                // Actual age is say 24 yrs, 9 months and 3 days represented as timespan
                // Min Valye + actual age = 25 yrs , 10 months and 4 days.
                // subtract our addition or 1 on all components to get the actual date.
                int ageInYears = age.Year - 1;
                int ageInMonths = age.Month - 1;
                int ageInDays = age.Day - 1;

                int[] resultado = {ageInYears, ageInMonths, ageInDays};
                return resultado;
            }
            catch (Exception ex)
            {
                int[] resultado =  {0, 0, 0};
                return resultado;
            }
        }


        public Servicio GetUltimoServicioParaParto(string registro)
        {
            try
            {
                var _partoMapper = new PartoMapper(registro);
                var ultParto = _partoMapper.GetUltimoPartoByRegistro();

                var fecha = new DateTime(1900, 1, 1);
                if (ultParto != null) fecha = ultParto.Fecha;

                _servMapper = new ServicioMapper(registro);
                var servUlt = _servMapper.GetUltimoServicioDespFechaByRegistro(fecha);

                if (servUlt != null && servUlt.Fecha >= DateTime.Today.AddDays(-300)) return servUlt;
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Diag_Prenez GetUltimoDiagnosticoParaParto(string registro)
        {
            try
            {
                var _partoMapper = new PartoMapper(registro);
                var ultParto = _partoMapper.GetUltimoPartoByRegistro();

                var fecha = new DateTime(1900, 1, 1);
                if (ultParto != null) fecha = ultParto.Fecha;

                _diagMapper = new Diag_PrenezMapper(registro);
                var diagUlt = _diagMapper.GetUltimoDiagnosticoDespFechaByRegistro(fecha);

                if (diagUlt != null && diagUlt.Fecha >= DateTime.Today.AddDays(-300)) return diagUlt;
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Lactancia ConsolidarLactancia(string registro)
        {
            var result = new Lactancia();
            var hoy = DateTime.Now;
           // var hoy = new DateTime(2014, 09, 10);
            var pMapper = new PartoMapper(registro);
            var parto = pMapper.GetUltimoPartoByRegistro();
            if (parto != null)
            {
                DateTime fechaParto = parto.Fecha;
                int dias = 0;
                var lactMapper = new LactanciaMapper(registro);
                var cantLactancias = lactMapper.GetMaxLactanciaByRegistro() + 1;

                var conMap = new Control_ProduccMapper(registro);
                var listaControles = conMap.GetControlesProduccPosterioresUltimoParto();

                bool continuar305 = true;
                bool continuar365 = true;
                double lechetotal = 0;
                double leche305 = 0;
                double leche365 = 0;
                double grasatotal = 0;
                double grasa305 = 0;
                double grasa365 = 0;
                double subtotatLeche305 = 0;
                double subtotatLeche365 = 0;
                double subtotatGrasa305 = 0;
                double subtotatGrasa365 = 0;

                if (listaControles != null)
                    foreach (var control in listaControles)
                    {
                        dias += control.Dias_para_control;
                        lechetotal += control.Leche * control.Dias_para_control;
                        grasatotal += control.Grasa / 100 * control.Leche * control.Dias_para_control;

                        if (dias < 305)
                        {
                            subtotatLeche305 += control.Leche * control.Dias_para_control;
                            subtotatGrasa305 += control.Grasa / 100 * control.Leche * control.Dias_para_control;
                        }

                        if (dias >= 305)
                        {
                            if (continuar305)
                            {
                                var diasDiferencia = dias - 305;
                                leche305 = subtotatLeche305 +  control.Leche * (control.Dias_para_control - diasDiferencia);
                                grasa305 = subtotatGrasa305 + control.Grasa / 100 * control.Leche * (control.Dias_para_control - diasDiferencia);
                                continuar305 = false;
                            }                                                                        
                        }

                        if (dias < 365)
                        {
                            subtotatLeche365 += control.Leche * control.Dias_para_control;
                            subtotatGrasa365 += control.Grasa / 100 * control.Leche * control.Dias_para_control;
                        }
                        if (dias >= 365)
                        {
                            if (continuar365)
                            {
                                var diasDiferencia = dias - 365;
                                leche365 = subtotatLeche365 + control.Leche * (control.Dias_para_control - diasDiferencia);
                                grasa365 = subtotatGrasa365 + control.Grasa / 100 * control.Leche * (control.Dias_para_control - diasDiferencia);
                                continuar365 = false;
                            }                        
                        }
                    }

                var lactancia = new Lactancia
                {
                    Registro = registro,
                    Numero = cantLactancias,
                    Dias = dias,
                    Leche305 = Math.Round(leche305,2),
                    Grasa305 = Math.Round(grasa305,2),
                    Leche365 = Math.Round(leche365,2),
                    Grasa365 = Math.Round(grasa365,2),
                    ProdLeche = Math.Round(lechetotal,2),
                    ProdGrasa = Math.Round(grasatotal,2)
                };
                result = lactancia;
            }
            return result;
        }

    }
}
