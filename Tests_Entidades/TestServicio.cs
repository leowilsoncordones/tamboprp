using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
