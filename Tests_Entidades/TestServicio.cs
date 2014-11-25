using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio;
using NUnit.Framework;

namespace Tests_Entidades
{
    [TestFixture]
    public class TestServicio
    {
        [Test]
        public void GetMeses_should_return_months_list()
        {
            var list = Fachada.Instance.GetMeses();
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void GetProximos_partos_should_return()
        {
            var fecha = DateTime.Now;
            var list = Fachada.Instance.GetProximosPartos(fecha);
            Assert.IsNotEmpty(list);

        }

        [Test]
        public void CalculcarEdad_shoul_ok()
        {
            var fecha = new DateTime(1982, 6, 10, 9, 0, 0, 0);
            var edad = Fachada.Instance.CalcularEdad(fecha);
            Assert.AreEqual(edad,"32 años, 5 meses");
        }

        [Test]
        public void CotrolTotal_should_get_all()
        {
            var lista = Fachada.Instance.ControlTotalGetAll();
            Assert.IsNotEmpty(lista);
        }

    }
}
